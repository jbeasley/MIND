using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Services;
using SCM.Data;

namespace Mind.Builders
{
    /// <summary>
    /// Abstract builder for attachments. The builder exposes a fluent API.
    /// </summary>
    public abstract class AttachmentBuilder<TAttachmentBuilder> : BaseBuilder, IAttachmentBuilder<TAttachmentBuilder>
        where TAttachmentBuilder: AttachmentBuilder<TAttachmentBuilder>
    {
        protected internal int _portBandwidthRequired;
        protected internal int _numPortsRequired;
        protected internal IEnumerable<Port> _ports;
        protected internal Attachment _attachment;

        private readonly Func<RoutingInstanceType, IRoutingInstanceDirector> _routingInstanceDirectorFactory;

        public AttachmentBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceDirector> routingInstanceDirectorFactory) : base(unitOfWork)
        {
            _attachment = new Attachment
            {
                Created = true,
                ShowCreatedAlert = true,
                Vifs = new List<Vif>()
            };

            _routingInstanceDirectorFactory = routingInstanceDirectorFactory;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> ForTenant(int tenantId)
        {
             _args.Add(nameof(ForTenant), tenantId);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithAttachmentBandwidth(int? attachmentBandwidthGbps)
        {
            _args.Add(nameof(WithAttachmentBandwidth), attachmentBandwidthGbps);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithAttachmentRole(string portPoolName, string attachmentRoleName)
        {
            _args.Add(nameof(portPoolName), portPoolName);
            _args.Add(nameof(attachmentRoleName), attachmentRoleName);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithContractBandwidth(int? contractBandwidthMbps)
        {
            if (contractBandwidthMbps != null)
            {
                _args.Add(nameof(contractBandwidthMbps), contractBandwidthMbps);
            }

            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithIpv4(List<SCM.Models.RequestModels.Ipv4AddressAndMask> ipv4Addresses)
        {
            if (ipv4Addresses != null) _args.Add(nameof(WithIpv4), ipv4Addresses);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithLocation(string locationName)
        {
            _args.Add(nameof(WithLocation), locationName);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithPlane(string planeName = "")
        {
            _args.Add(nameof(WithPlane), planeName);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithJumboMtu(bool? useJumboMtu)
        {
            _args.Add(nameof(WithJumboMtu), useJumboMtu != null ? useJumboMtu : false);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithExistingRoutingInstance(string existingRoutingInstanceName)
        {
            _args.Add(nameof(WithExistingRoutingInstance), existingRoutingInstanceName);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp)
        {
            _args.Add(nameof(WithTrustReceivedCosAndDscp), trustReceivedCosAndDscp != null ? trustReceivedCosAndDscp : false);
            return this;
        }

        /// <summary>
        /// Build the attachment
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Attachment> BuildAsync()
        {
            await Task.WhenAll(new List<Task>()
            {
                CreateAttachmentBandwidthAsync(),
                CreateAttachmentRoleAsync()
            });
            if (_args.ContainsKey(nameof(ForTenant))) await SetTenantAsync();
            SetNumberOfPortsRequired();
            SetPortBandwidthRequired();
            await AllocatePortsAsync();
            CreateInterfaces();
            await SetMtuAsync();

            if (_args.ContainsKey("contractBandwidthMbps"))
            {
                await CreateContractBandwidthPoolAsync();
                SetTrustReceivedCosAndDscp();
            }

            if (_args.ContainsKey(nameof(WithExistingRoutingInstance)) && _args[nameof(WithExistingRoutingInstance)] != null)
            {
                await AssociateExistingRoutingInstanceAsync();
            }
            else
            {
                await CreateRoutingInstanceAsync();
            }

            return _attachment;
        }

        /// <summary>
        /// Set the bandwidth of each port required. This method must be implemented by derived classes.
        /// </summary>
        protected abstract internal void SetPortBandwidthRequired();

        /// <summary>
        /// Set the number of ports required. This method must be implemented by derived classes.
        /// </summary>
        protected abstract internal void SetNumberOfPortsRequired();

        protected internal virtual async Task SetTenantAsync()
        {
            var tenantId = (int)_args[nameof(ForTenant)];
            var tenant = (from result in await _unitOfWork.TenantRepository.GetAsync(
                     q =>
                          q.TenantID == tenantId,
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _attachment.Tenant = tenant ?? throw new BuilderBadArgumentsException($"The tenant with ID '{tenantId}' was not found.");
        }

        /// <summary>
        /// Allocate some ports from inventory for the attachment
        /// </summary>
        /// <returns></returns>
        protected internal virtual async Task AllocatePortsAsync()
        {
            _ports = await GetPortsAsync();
            var assignedPortStatus = (from portStatus in await _unitOfWork.PortStatusRepository.GetAsync(q => q.PortStatusType == PortStatusTypeEnum.Assigned)
                                      select portStatus)
                                      .Single();
            foreach (var port in _ports)
            {
                port.PortStatusID = assignedPortStatus.PortStatusID;
                port.TenantID = _attachment.Tenant.TenantID;
            }
        }

        /// <summary>
        /// Create attachment bandwidth
        /// </summary>
        /// <returns></returns>
        protected internal virtual async Task CreateAttachmentBandwidthAsync()
        {
            var attachmentBandwidthGbps = (int)_args[nameof(WithAttachmentBandwidth)];
            var attachmentBandwidth = (from attachmentBandwidths in await _unitOfWork.AttachmentBandwidthRepository.GetAsync(
                                    q =>
                                       q.BandwidthGbps == attachmentBandwidthGbps, 
                                       AsTrackable: true)
                                       select attachmentBandwidths)
                                       .SingleOrDefault();

            _attachment.AttachmentBandwidth = attachmentBandwidth;
        }

        /// <summary>
        /// Create attachment role
        /// </summary>
        /// <returns></returns>
        protected internal virtual async Task CreateAttachmentRoleAsync()
        {
            var attachmentRoleName = _args["attachmentRoleName"].ToString();
            var portPoolName = _args["portPoolName"].ToString();
            var attachmentRole = (from attachmentRoles in await _unitOfWork.AttachmentRoleRepository.GetAsync(
                            q =>
                                q.PortPool.Name == portPoolName && q.Name == attachmentRoleName, 
                                includeProperties:"RoutingInstanceType," +
                                "PortPool.PortRole",
                                AsTrackable: true)
                                  select attachmentRoles)
                                  .SingleOrDefault();

            if (attachmentRole == null) throw new BuilderBadArgumentsException($"Could not find an attachment role from the supplied '{attachmentRoleName}' " +
                    $"and '{portPoolName}' arguments.");

            _attachment.AttachmentRoleID = attachmentRole.AttachmentRoleID;
            _attachment.AttachmentRole = attachmentRole;
            _attachment.IsLayer3 = attachmentRole.IsLayer3Role;
            _attachment.IsTagged = attachmentRole.IsTaggedRole;
            _attachment.RequiresSync = attachmentRole.RequireSyncToNetwork;
            _attachment.ShowRequiresSyncAlert = attachmentRole.RequireSyncToNetwork;
        }

        protected internal virtual void CreateInterfaces()
        {
            var iface = new Interface
            {
                DeviceID = _attachment.Device.DeviceID,
                Ports = _ports.ToList()
            };

            List<SCM.Models.RequestModels.Ipv4AddressAndMask> ipv4Addresses;
            ipv4Addresses = (List<SCM.Models.RequestModels.Ipv4AddressAndMask>)_args[nameof(WithIpv4)];
            var ipv4AddressAndMask = ipv4Addresses.FirstOrDefault();
            iface.IpAddress = ipv4AddressAndMask?.IpAddress;
            iface.SubnetMask = ipv4AddressAndMask?.SubnetMask;

            _attachment.Interfaces = new List<Interface> { iface };
        }

        protected internal virtual async Task CreateContractBandwidthPoolAsync()
        {
            var contractBandwidthMbps = (int)_args["contractBandwidthMbps"];
            var contractBandwidth = (from contractBandwidths in await _unitOfWork.ContractBandwidthRepository.GetAsync(q =>
                                     q.BandwidthMbps == contractBandwidthMbps)
                                     select contractBandwidths)
                                    .SingleOrDefault();

            if (contractBandwidth == null)  throw new BuilderBadArgumentsException($"The requested contract bandwidth of {_args["contractBandwidthMbps"]} " +
                $"Mbps is not valid.");

            var contractBandwidthPool = new ContractBandwidthPool
            {
                ContractBandwidthID = contractBandwidth.ContractBandwidthID,
                TenantID = _attachment.Tenant.TenantID,
                Name = Guid.NewGuid().ToString("N")
            };

            _attachment.ContractBandwidthPool = contractBandwidthPool;
        }

        protected internal virtual void SetTrustReceivedCosAndDscp()
        {
            if (_attachment.ContractBandwidthPool != null)
            {
                var trustReceivedCosAndDscp = _args[nameof(WithTrustReceivedCosAndDscp)] != null ? (bool)_args[nameof(WithTrustReceivedCosAndDscp)] : false;
                _attachment.ContractBandwidthPool.TrustReceivedCosDscp = trustReceivedCosAndDscp;
            }
        }

        protected internal virtual async Task CreateRoutingInstanceAsync()
        {
            if (_attachment.AttachmentRole.RoutingInstanceType != null) {

                var routingInstanceType = (from routingInstanceTypes in await _unitOfWork.RoutingInstanceTypeRepository.GetAsync(
                                        q =>
                                           q.RoutingInstanceTypeID == _attachment.AttachmentRole.RoutingInstanceType.RoutingInstanceTypeID)
                                           select routingInstanceTypes)
                                           .Single();

                var routingInstanceDirector = _routingInstanceDirectorFactory(routingInstanceType);
                var routingInstance = await routingInstanceDirector.BuildAsync(deviceId: _attachment.Device.DeviceID,
                                                                               tenantId: _attachment.Tenant.TenantID);

                _attachment.RoutingInstance = routingInstance;
            }
        }

        protected internal virtual async Task AssociateExistingRoutingInstanceAsync()
        {
            var routingInstanceName = _args[nameof(WithExistingRoutingInstance)].ToString();

            var existingRoutingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                    x => 
                                           x.Name == routingInstanceName
                                           && x.TenantID == _attachment.Tenant.TenantID
                                           && x.DeviceID == _attachment.Device.DeviceID,
                                           AsTrackable: true)
                                           select routingInstances)
                                           .SingleOrDefault();

            _attachment.RoutingInstance = existingRoutingInstance ?? throw new BuilderBadArgumentsException("Could not find existing routing " +
                $"instance '{routingInstanceName}' belonging to tenant '{_attachment.Tenant.Name}'.");
            _attachment.RoutingInstance = existingRoutingInstance;
        }

        protected internal virtual async Task SetMtuAsync()
        {
            var useLayer2InterfaceMtu = _attachment.Device.UseLayer2InterfaceMtu;
            var useJumboMtu = _args.ContainsKey(nameof(WithJumboMtu)) ? (bool)_args[nameof(WithJumboMtu)] : false;

            var mtu = (from mtus in await _unitOfWork.MtuRepository.GetAsync(
                x => 
                    x.ValueIncludesLayer2Overhead == useLayer2InterfaceMtu && x.IsJumbo == useJumboMtu,
                    AsTrackable: true)
                       select mtus)
                       .SingleOrDefault();

            _attachment.Mtu = mtu;
        }

        /// <summary>
        /// Get some available ports.
        /// </summary>
        /// <returns></returns>
        protected internal virtual async Task<IEnumerable<Port>> GetPortsAsync()
        {
            IList<Device> devices;

            // Find a collection of Devices matching the requested Location
            // with a status of 'Production'

            var locationName = _args[nameof(WithLocation)].ToString();
            if (string.IsNullOrEmpty(locationName))   throw new BuilderBadArgumentsException("A location is required but none was supplied.");

            var location = (from locations in await _unitOfWork.LocationRepository.GetAsync(q => q.SiteName == locationName)
                            select locations)
                            .SingleOrDefault();

            if (location == null) throw new BuilderBadArgumentsException($"The location {locationName} is not valid.");

            // Find all devices with Device Status of 'Production' which are in the requested Location and which suport the 
            // required Attachment Role

            var query = from d in await _unitOfWork.DeviceRepository.GetAsync(q => q.DeviceStatus.DeviceStatusType == DeviceStatusTypeEnum.Production
                && q.LocationID == location.LocationID
                && q.DeviceRole.DeviceRoleAttachmentRoles.Where(x => x.AttachmentRoleID == _attachment.AttachmentRole.AttachmentRoleID).Any(),
                includeProperties: "Ports.PortBandwidth,Ports.Device,Ports.PortStatus,Attachments", AsTrackable: true)
                        select d;

            // Filter devices collection to include only those devices which belong to the requested plane (if specified)

            var planeName = _args[nameof(WithPlane)] != null ? _args[nameof(WithPlane)].ToString() : "";
            if (!string.IsNullOrEmpty(planeName))
            {
                var plane = (from planes in await _unitOfWork.PlaneRepository.GetAsync(q => q.Name == planeName)
                             select planes)
                             .SingleOrDefault();

                if (plane == null)  throw new BuilderBadArgumentsException($"The plane {planeName} is not valid.");       
                query = query.Where(q => q.PlaneID == plane.PlaneID).ToList();
            }

            devices = query.ToList();
           
            // Filter devices collection to only those devices which have the required number of free ports
            // of the required bandwidth and which belong to the requested Port Pool

            devices = devices.Where(q => q.Ports.Where(p => p.PortStatus.PortStatusType == PortStatusTypeEnum.Free
                && p.PortPoolID == _attachment.AttachmentRole.PortPoolID
                && p.PortBandwidth.BandwidthGbps == _portBandwidthRequired).Count() >= _numPortsRequired).ToList();

            if (devices.Count == 0)
            {
                throw new BuilderUnableToCompleteException("Ports matching the requested "
                    + "parameters could not be found. "
                    + "Please change the input parameters and try again, "
                    + "or contact your system adminstrator to report this issue.");
            }
            else if (devices.Count > 1)
            {
                // Get device with the most free ports of the required Port Bandwidth
                _attachment.Device = devices.Aggregate(
                    (current, x) =>
                        (x.Ports.Where(p => p.PortStatus.PortStatusType == PortStatusTypeEnum.Free
                        && p.PortBandwidth.BandwidthGbps == _portBandwidthRequired)
                        .Count() >
                        current.Ports.Where(p => p.PortStatus.PortStatusType == PortStatusTypeEnum.Free
                        && p.PortBandwidth.BandwidthGbps == _portBandwidthRequired)
                        .Count() ? x : current));
            }
            else
            {
                // Only one device found
                _attachment.Device = devices.Single();
            }

            // Get the ports
            var ports = _attachment.Device.Ports.Where(
                    q => 
                        q.PortStatus.PortStatusType == PortStatusTypeEnum.Free
                        && q.PortBandwidth.BandwidthGbps == _portBandwidthRequired
                        && q.PortPoolID == _attachment.AttachmentRole.PortPoolID);

            return ports.Take(_numPortsRequired);
        }

        /// <summary>
        /// Validate the state of the attachment.
        /// </summary>
        protected virtual internal void Validate()
        {
            if (_attachment.Mtu == null) throw new BuilderIllegalStateException("An MTU is required for the attachment.");
            if (_attachment.AttachmentBandwidth == null) throw new BuilderIllegalStateException("An attachmnt bandwidth is required for the attachment.");
            if (_attachment.AttachmentRole == null) throw new BuilderIllegalStateException("An attachment role is required for the attachment.");
            if (_attachment.Device == null) throw new BuilderIllegalStateException("A device is required for the attachment.");
            if (!_attachment.Interfaces.Any()) throw new BuilderIllegalStateException("At least one interface is required for the attachment.");
            if (_attachment.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.TenantFacing && _attachment.Tenant == null)
            {
                throw new BuilderIllegalStateException("A tenant association is required for the attachment in accordance with the attachment role of " +
                    $"'{_attachment.AttachmentRole.Name}'.");
            }
            else if (_attachment.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.TenantInfrastructure && _attachment.Tenant == null)
            {
                throw new BuilderIllegalStateException("A tenant association is required for the attachment in accordance with the attachment role of " +
                    $"'{_attachment.AttachmentRole.Name}'.");
            }
            else if (_attachment.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.ProviderInfrastructure && _attachment.Tenant != null)
            {
                throw new BuilderIllegalStateException("A tenant association exists for the attachment but is NOT required in accordance with the " +
                    $"attachment role of '{_attachment.AttachmentRole.Name}'.");
            }

            if (_attachment.RoutingInstance == null && _attachment.AttachmentRole.RoutingInstanceTypeID.HasValue)
                throw new BuilderIllegalStateException("Illegal routing instance state. A routing instance for the attachment is required in accordance " +
                    $"with the requested attachment role of '{_attachment.AttachmentRole.Name}' but was not found.");

            if (_attachment.RoutingInstance != null && !_attachment.AttachmentRole.RoutingInstanceTypeID.HasValue)
                throw new BuilderIllegalStateException("Illegal routing instance state. A routing instance for the attachment has been assigned but is " +
                    $"not required for an attachment with attachment role of '{_attachment.AttachmentRole.Name}'.");

            if (_attachment.RoutingInstance != null && _attachment.AttachmentRole.RoutingInstanceType != null)
            {
                if (_attachment.RoutingInstance.RoutingInstanceType.RoutingInstanceTypeID != _attachment.AttachmentRole.RoutingInstanceTypeID)
                {
                    throw new BuilderIllegalStateException("Illegal routing instance state. The routing instance type for the attachment is different to that " +
                        $"required by the attachment role. The routing instance type required is '{_attachment.AttachmentRole.RoutingInstanceType.Type.ToString()}'. " +
                        $"The routing instance type assigned to the attachment is '{_attachment.RoutingInstance.RoutingInstanceType.Type.ToString()}'.");
                }
            }

            if (_attachment.IsLayer3)
            {
                if (_attachment.Interfaces.Where(x => !string.IsNullOrEmpty(x.IpAddress) &&
                !string.IsNullOrEmpty(x.SubnetMask)).Count() != _attachment.Interfaces.Count)
                {
                    throw new BuilderIllegalStateException("The attachment is enabled for layer 3 but insufficient IPv4 addresses have been requested.");
                }
            }
            else if (_attachment.Interfaces.Where(x => !string.IsNullOrEmpty(x.IpAddress) || !string.IsNullOrEmpty(x.SubnetMask)).Any())
            {
                throw new BuilderIllegalStateException("The attachment is NOT enabled for layer 3 but IPv4 addresses have been requested.");
            }

            if (_attachment.AttachmentRole.RequireContractBandwidth)
            {
                if (_attachment.ContractBandwidthPool == null)
                {
                    throw new BuilderIllegalStateException("A contract bandwidth for the attachment is required in accordance with the attachment role " +
                        $"of '{_attachment.AttachmentRole.Name}' but none is defined.");
                }

                if (_attachment.ContractBandwidthPool.ContractBandwidth.BandwidthMbps > _attachment.AttachmentBandwidth.BandwidthGbps * 1000)
                {
                    throw new BuilderIllegalStateException($"The requested contract bandwidth of " +
                        $"{_attachment.ContractBandwidthPool.ContractBandwidth.BandwidthMbps} Mbps is greater " +
                        $"than the bandwidth of the attachment which is {_attachment.AttachmentBandwidth.BandwidthGbps} Gbps.");
                }
            }
            else
            {
                if (_attachment.ContractBandwidthPool != null)
                {
                    throw new BuilderIllegalStateException("A contract bandwidth for the attachment is defined but is NOT required for the attachment role " +
                        $"of '{_attachment.AttachmentRole.Name}'.");
                }
            }

            if (_attachment.AttachmentRole.IsTaggedRole)
            {
                if (!_attachment.IsTagged)
                {
                    throw new BuilderIllegalStateException("The attachment must be enabled for tagging with the 'isTagged' property in accordance with the " +
                        $"attachment role of '{_attachment.AttachmentRole.Name}'.");
                }
                if (_attachment.IsLayer3) throw new BuilderIllegalStateException("Layer 3 cannot be enabled concurrently with a tagged attachment.");
            }
            else
            {
                if (_attachment.Vifs.Any())
                {
                    throw new BuilderIllegalStateException("Vifs were found for the attachment but the attachment is not enabled for tagging with the 'isTagged' properrty.");
                }
            }
        }
    }
}

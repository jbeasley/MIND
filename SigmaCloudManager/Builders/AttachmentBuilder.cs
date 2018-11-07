using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Services;
using SCM.Data;
using Microsoft.EntityFrameworkCore;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    /// <summary>
    /// Abstract builder for attachments. The builder exposes a fluent API.
    /// </summary>
    public abstract class AttachmentBuilder<TAttachmentBuilder> : BaseBuilder, IAttachmentBuilder<TAttachmentBuilder>
        where TAttachmentBuilder : AttachmentBuilder<TAttachmentBuilder>
    {
        protected internal int _portBandwidthRequired;
        protected internal int _numPortsRequired;
        protected internal IEnumerable<Port> _ports;
        protected internal Attachment _attachment;

        private readonly Func<RoutingInstanceType, IVrfRoutingInstanceDirector> _routingInstanceDirectorFactory;

        public AttachmentBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IVrfRoutingInstanceDirector> routingInstanceDirectorFactory) : base(unitOfWork)
        {
            _attachment = new Attachment
            {
                Created = true,
                ShowCreatedAlert = true,
                Vifs = new List<Vif>()
            };

            _routingInstanceDirectorFactory = routingInstanceDirectorFactory;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> ForTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(ForTenant), tenantId);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> ForDevice(int? deviceId)
        {
            if (deviceId.HasValue) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        public IAttachmentBuilder<TAttachmentBuilder> ForAttachment(int? attachmentId)
        {
            if (attachmentId.HasValue) _args.Add(nameof(ForAttachment), attachmentId);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithAttachmentBandwidth(int? attachmentBandwidthGbps)
        {
            if (attachmentBandwidthGbps.HasValue) _args.Add(nameof(WithAttachmentBandwidth), attachmentBandwidthGbps);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithPortPool(string portPoolName)
        {
            if (!string.IsNullOrEmpty(portPoolName)) _args.Add(nameof(WithPortPool), portPoolName);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithAttachmentRole(string attachmentRoleName)
        {
            if (!string.IsNullOrEmpty(attachmentRoleName)) _args.Add(nameof(WithAttachmentRole), attachmentRoleName);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithContractBandwidth(int? contractBandwidthMbps)
        {
            if (contractBandwidthMbps.HasValue) _args.Add(nameof(WithContractBandwidth), contractBandwidthMbps);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithIpv4(List<SCM.Models.RequestModels.Ipv4AddressAndMask> ipv4Addresses)
        {
            if (ipv4Addresses != null) _args.Add(nameof(WithIpv4), ipv4Addresses);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithLocation(string locationName)
        {
            if (!string.IsNullOrEmpty(locationName)) _args.Add(nameof(WithLocation), locationName);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithPlane(string planeName)
        {
            if (!string.IsNullOrEmpty(planeName)) _args.Add(nameof(WithPlane), planeName);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithJumboMtu(bool? useJumboMtu)
        {
            if (useJumboMtu.HasValue) _args.Add(nameof(WithJumboMtu), useJumboMtu);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> UseExistingRoutingInstance(string existingRoutingInstanceName)
        {
            if (!string.IsNullOrEmpty(existingRoutingInstanceName)) _args.Add(nameof(UseExistingRoutingInstance), existingRoutingInstanceName);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithNewRoutingInstance(bool? newRoutingInstance)
        {
            if (newRoutingInstance.HasValue) _args.Add(nameof(WithNewRoutingInstance), newRoutingInstance);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> UseDefaultRoutingInstance(bool? useDefaultRoutingInstance)
        {
            if (useDefaultRoutingInstance.HasValue) _args.Add(nameof(UseDefaultRoutingInstance), useDefaultRoutingInstance);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithRoutingInstance(RoutingInstanceRequest routingInstanceRequest)
        {
            if (routingInstanceRequest != null) _args.Add(nameof(WithRoutingInstance), routingInstanceRequest);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp)
        {
            if (trustReceivedCosAndDscp.HasValue) _args.Add(nameof(WithTrustReceivedCosAndDscp), trustReceivedCosAndDscp);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithDescription(string description)
        {
            if (!string.IsNullOrEmpty(description)) _args.Add(nameof(WithDescription), description);
            return this;
        }

        public virtual IAttachmentBuilder<TAttachmentBuilder> WithNotes(string notes)
        {
            if (!string.IsNullOrEmpty(notes)) _args.Add(nameof(WithNotes), notes);
            return this;
        }

        /// <summary>
        /// Build the attachment
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Attachment> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForAttachment)))
            {
                // Find and update an existing attachment
                await SetAttachmentAsync();

                if (_args.ContainsKey(nameof(UseExistingRoutingInstance)))
                {
                    await AssociateExistingRoutingInstanceAsync();
                }
                else if (_args.ContainsKey(nameof(WithNewRoutingInstance)) && (bool)_args[nameof(WithNewRoutingInstance)])
                {
                    await CreateRoutingInstanceAsync();
                }
            }
            else
            {
                // Create a new attachment
                if (_args.ContainsKey(nameof(WithAttachmentBandwidth))) await CreateAttachmentBandwidthAsync();
                if (_args.ContainsKey(nameof(WithAttachmentRole)) && _args.ContainsKey(nameof(WithPortPool))) await CreateAttachmentRoleAsync();
                if (_args.ContainsKey(nameof(ForTenant))) await SetTenantAsync();
                SetNumberOfPortsRequired();
                SetPortBandwidthRequired();

                // It is necessary to check the validity of the attachment role here, otherwise we may drop through to 
                // trying to allocate some ports and find that none are available if the attachment role is not compatible
                CheckAttachmentRoleIsValid();

                if (_args.ContainsKey(nameof(ForDevice)))
                {
                    // Use an explicitly provided device
                    await SetDeviceAsync();
                }
                else
                {
                    // Find a device from given constrainsts (e.g. Location, Plane)
                    await FindDeviceFromConstraintsAsync();
                }

                await AllocatePortsAsync();
                CreateInterfaces();

                if (_args.ContainsKey(nameof(UseDefaultRoutingInstance)))
                {
                    await AssociateDefaultRoutingInstanceAsync();
                }
                else if (_args.ContainsKey(nameof(UseExistingRoutingInstance)))
                {
                    await AssociateExistingRoutingInstanceAsync();
                }
                else
                {
                    await CreateRoutingInstanceAsync();
                }
            }

            SetIpv4();
            await SetMtuAsync();

            if (_args.ContainsKey(nameof(WithContractBandwidth))) await CreateContractBandwidthPoolAsync();
            if (_args.ContainsKey(nameof(WithTrustReceivedCosAndDscp))) SetTrustReceivedCosAndDscp();
            if (_args.ContainsKey(nameof(WithDescription))) SetDescription();
            if (_args.ContainsKey(nameof(WithNotes))) SetNotes();

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

        protected abstract internal void CheckAttachmentRoleIsValid();

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
            _ports = _attachment.Device.Ports.Where(
                                               q =>
                                               q.PortStatus.PortStatusType == SCM.Models.PortStatusTypeEnum.Free &&
                                               q.PortBandwidth.BandwidthGbps == _portBandwidthRequired &&
                                               q.PortPoolID == _attachment.AttachmentRole.PortPoolID)
                                               .Take(_numPortsRequired);

            // Check we have the required number of ports - the 'take' method will only return the number of ports found which may be 
            // less than the required number
            if (_ports.Count() != _numPortsRequired) throw new BuilderUnableToCompleteException("Could not find a sufficient number of free ports " +
                $"matching the requirements. {_numPortsRequired} ports of {_portBandwidthRequired} Gbps are required but {_ports.Count()} free ports were found.");

            var assignedPortStatus = (from portStatus in await _unitOfWork.PortStatusRepository.GetAsync(
                                      q => 
                                      q.PortStatusType == SCM.Models.PortStatusTypeEnum.Assigned)
                                      select portStatus)
                                      .Single();

            foreach (var port in _ports)
            {
                port.PortStatusID = assignedPortStatus.PortStatusID;
                //  Tenant may or may not be assigned the port - e.g. if the attachment is for a Provider Domain device such as a PE
                //  then the port should be assigned to a tenant. But if the attachment is for a Tenant domain device then a 
                //  tenant is not required.
                port.TenantID = _attachment.Tenant?.TenantID;
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
            var attachmentRoleName = _args[nameof(WithAttachmentRole)].ToString();
            var portPoolName = _args[nameof(WithPortPool)].ToString();
            var attachmentRole = (from attachmentRoles in await _unitOfWork.AttachmentRoleRepository.GetAsync(
                            q =>
                                  q.PortPool.Name == portPoolName && q.Name == attachmentRoleName,
                                  query: q => 
                                         q.Include(x => x.RoutingInstanceType)
                                          .Include(x => x.PortPool.PortRole),
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

            _attachment.Interfaces = new List<Interface> { iface };
        }

        protected internal virtual void SetIpv4()
        {
            List<SCM.Models.RequestModels.Ipv4AddressAndMask> ipv4Addresses = null;
            if (_args.ContainsKey(nameof(WithIpv4))) ipv4Addresses = (List<SCM.Models.RequestModels.Ipv4AddressAndMask>)_args[nameof(WithIpv4)];
            var ipv4AddressAndMask = ipv4Addresses?.FirstOrDefault();
            var iface = _attachment.Interfaces.Single();
            iface.IpAddress = ipv4AddressAndMask?.IpAddress;
            iface.SubnetMask = ipv4AddressAndMask?.SubnetMask;
        }

        protected internal virtual async Task CreateContractBandwidthPoolAsync()
        {
            var contractBandwidthMbps = (int)_args[nameof(WithContractBandwidth)];
            var contractBandwidth = (from contractBandwidths in await _unitOfWork.ContractBandwidthRepository.GetAsync(
                                  q =>
                                     q.BandwidthMbps == contractBandwidthMbps,
                                     AsTrackable: true)
                                     select contractBandwidths)
                                    .SingleOrDefault();

            if (contractBandwidth == null) throw new BuilderBadArgumentsException($"The requested contract bandwidth of {contractBandwidthMbps} " +
               $"Mbps is not valid.");

            var contractBandwidthPool = new ContractBandwidthPool
            {
                ContractBandwidthID = contractBandwidth.ContractBandwidthID,
                ContractBandwidth = contractBandwidth,
                TenantID = _attachment.Tenant?.TenantID,
                Name = Guid.NewGuid().ToString("N")
            };

            _attachment.ContractBandwidthPool = contractBandwidthPool;
        }

        protected internal virtual void SetTrustReceivedCosAndDscp()
        {
            if (_attachment.ContractBandwidthPool != null)
            {
                var trustReceivedCosAndDscp = (bool)_args[nameof(WithTrustReceivedCosAndDscp)];
                _attachment.ContractBandwidthPool.TrustReceivedCosAndDscp = trustReceivedCosAndDscp;
            }
        }

        protected internal virtual async Task CreateRoutingInstanceAsync()
        {
            if (_attachment.AttachmentRole.RoutingInstanceType != null)
            {
                var routingInstanceType = (from routingInstanceTypes in await _unitOfWork.RoutingInstanceTypeRepository.GetAsync(
                                        q =>
                                           q.RoutingInstanceTypeID == _attachment.AttachmentRole.RoutingInstanceType.RoutingInstanceTypeID)
                                           select routingInstanceTypes)
                                           .Single();

                var routingInstanceRequest = _args.ContainsKey(nameof(WithRoutingInstance)) ? (RoutingInstanceRequest)_args[nameof(WithRoutingInstance)] : null;
                var routingInstanceDirector = _routingInstanceDirectorFactory(routingInstanceType);
                var routingInstance = await routingInstanceDirector.BuildAsync(deviceId: _attachment.Device.DeviceID,
                                                                               tenantId: _attachment.Tenant?.TenantID,
                                                                               request: routingInstanceRequest);

                _attachment.RoutingInstanceID = null;
                _attachment.RoutingInstance = routingInstance;
            }
        }

        protected internal virtual async Task AssociateExistingRoutingInstanceAsync()
        {
            var routingInstanceName = _args[nameof(UseExistingRoutingInstance)].ToString();
            var existingRoutingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                    x =>
                                           x.Name == routingInstanceName &&
                                           x.TenantID == _attachment.Tenant.TenantID &&
                                           x.DeviceID == _attachment.Device.DeviceID, 
                                           query: q => q.IncludeValidationProperties(),
                                           AsTrackable: true)
                                           select routingInstances)
                                           .SingleOrDefault();

            _attachment.RoutingInstance = existingRoutingInstance ?? throw new BuilderBadArgumentsException("Could not find existing routing " +
                $"instance '{routingInstanceName}' belonging to tenant '{_attachment.Tenant.Name}'.");
            _attachment.RoutingInstanceID = existingRoutingInstance.RoutingInstanceID;
        }

        protected internal virtual async Task AssociateDefaultRoutingInstanceAsync()
        {
            var defaultRoutingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                    x =>
                                           x.DeviceID == _attachment.Device.DeviceID &&
                                           x.RoutingInstanceType.IsDefault,
                                           AsTrackable: true)
                                           select routingInstances)
                                           .SingleOrDefault();

            _attachment.RoutingInstance = defaultRoutingInstance ?? throw new BuilderUnableToCompleteException("Could not find the default routing " +
                $"instance for device '{_attachment.Device.Name}'. Please report this issue to your system administrator.");
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

        protected internal virtual async Task SetDeviceAsync()
        {
            var deviceId = (int)_args[nameof(ForDevice)];
            var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                        x =>
                          x.DeviceID == deviceId,
                          query: q => q.IncludeValidationProperties(),
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _attachment.Device = device ?? throw new BuilderBadArgumentsException($"The device with ID '{deviceId}' was not found.");
        }

        private async Task SetAttachmentAsync()
        {
            var attachmentId = (int)_args[nameof(ForAttachment)];
            var attachment = (from attachments in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                            q.AttachmentID == attachmentId,
                            query: x => x.IncludeValidationProperties(),
                            AsTrackable: true)
                              select attachments)
                             .SingleOrDefault();

            _attachment = attachment ?? throw new BuilderBadArgumentsException($"Could not find the attachment with ID '{attachmentId}'.");
        }

        /// <summary>
        /// Find a device from the supplied set of constraints such as locatio, plane, etc.
        /// </summary>
        /// <returns></returns>
        protected internal virtual async Task FindDeviceFromConstraintsAsync()
        {
            IList<Device> devices;

            // Find a collection of Devices matching the requested Location
            // with a status of 'Production'

            var locationName = _args[nameof(WithLocation)].ToString();
            if (string.IsNullOrEmpty(locationName)) throw new BuilderBadArgumentsException("A location is required but none was supplied.");

            var location = (from locations in await _unitOfWork.LocationRepository.GetAsync(
                        q => 
                            q.SiteName == locationName)
                            select locations)
                            .SingleOrDefault();

            if (location == null) throw new BuilderBadArgumentsException($"The location {locationName} is not valid.");

            // Find all devices with Device Status of 'Production' which are in the requested Location and which suport the 
            // required Attachment Role

            var query = from d in await _unitOfWork.DeviceRepository.GetAsync(
                        q => 
                        q.DeviceStatus.DeviceStatusType == SCM.Models.DeviceStatusTypeEnum.Production
                        && q.LocationID == location.LocationID
                        && q.DeviceRole.DeviceRoleAttachmentRoles
                       .Where(
                            x => 
                            x.AttachmentRoleID == _attachment.AttachmentRole.AttachmentRoleID)
                            .Any(),
                        query: q => q.IncludeValidationProperties(),
                        AsTrackable: true)
                        select d;

            // Filter devices collection to include only those devices which belong to the requested plane (if specified)

            var planeName = _args.ContainsKey(nameof(WithPlane)) ? _args[nameof(WithPlane)].ToString() : "";
            if (!string.IsNullOrEmpty(planeName))
            {
                var plane = (from planes in await _unitOfWork.PlaneRepository.GetAsync(q => q.Name == planeName)
                             select planes)
                             .SingleOrDefault();

                if (plane == null) throw new BuilderBadArgumentsException($"The plane {planeName} is not valid.");
                query = query.Where(q => q.PlaneID == plane.PlaneID).ToList();
            }

            devices = query.ToList();

            // Filter devices collection to only those devices which have the required number of free ports
            // of the required bandwidth and which belong to the requested Port Pool

            devices = devices.Where(q => q.Ports.Where(p => p.PortStatus.PortStatusType == SCM.Models.PortStatusTypeEnum.Free
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
                        (x.Ports.Where(p => p.PortStatus.PortStatusType == SCM.Models.PortStatusTypeEnum.Free
                        && p.PortBandwidth.BandwidthGbps == _portBandwidthRequired)
                        .Count() >
                        current.Ports.Where(p => p.PortStatus.PortStatusType == SCM.Models.PortStatusTypeEnum.Free
                        && p.PortBandwidth.BandwidthGbps == _portBandwidthRequired)
                        .Count() ? x : current));
            }
            else
            {
                // Only one device found
                _attachment.Device = devices.Single();
            }
        }

        protected internal virtual void SetDescription()
        {
            var description = _args[nameof(WithDescription)].ToString();
            _attachment.Description = description;
        }

        protected internal virtual void SetNotes()
        {
            var notes = _args[nameof(WithNotes)].ToString();
            _attachment.Notes = notes;
        }
    }
}

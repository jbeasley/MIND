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
    /// Builder for attachments. The builder exposes a fluent API.
    /// </summary>
    public class AttachmentBuilder : IAttachmentBuilder
    {
        protected internal readonly IUnitOfWork _unitOfWork;
        protected internal readonly IMtuService _mtuService;
        protected internal readonly IAttachmentRoleService _attachmentRoleService;
        protected internal readonly IAttachmentBandwidthService _attachmentBandwidthService;
        protected internal readonly IContractBandwidthService _contractBandwidthService;
        protected internal readonly ILocationService _locationService;
        protected internal readonly IPlaneService _planeService;
        protected internal readonly IRoutingInstanceTypeService _routingInstanceTypeService;
        protected internal IRoutingInstanceBuilder _routingInstanceBuilder;
        protected internal int _portBandwidthRequired;
        protected internal int _numPortsRequired;
        protected internal IEnumerable<Port> _ports;
        protected internal readonly Attachment _attachment;
        protected internal Dictionary<string, object> _args;

        private readonly Func<RoutingInstanceType, IRoutingInstanceBuilder> _routingInstanceBuilderFactory;

        public AttachmentBuilder(IUnitOfWork unitOfWork, IMtuService mtuService, IAttachmentRoleService attachmentRoleService,
            IAttachmentBandwidthService attachmentBandwidthService, IContractBandwidthService contractBandwidthService,
            ILocationService locationService, IPlaneService planeService, IRoutingInstanceTypeService routingInstanceTypeService,
            Func<RoutingInstanceType, IRoutingInstanceBuilder> routingInstanceBuilderFactory)
        {
            _args = new Dictionary<string, object>();
            _attachment = new Attachment
            {
                Created = true,
                ShowCreatedAlert = true
            };

            _unitOfWork = unitOfWork;
            _mtuService = mtuService;
            _attachmentRoleService = attachmentRoleService;
            _attachmentBandwidthService = attachmentBandwidthService;
            _contractBandwidthService = contractBandwidthService;
            _locationService = locationService;
            _planeService = planeService;
            _routingInstanceTypeService = routingInstanceTypeService;
            _routingInstanceBuilderFactory = routingInstanceBuilderFactory;
        }

        public virtual IAttachmentBuilder ForTenant(int tenantId)
        {
            _attachment.TenantID = tenantId;
            return this;
        }

        public virtual IAttachmentBuilder WithAttachmentBandwidth(int? attachmentBandwidthGbps)
        {
            if (attachmentBandwidthGbps == null)
            {
                throw new BuilderBadArgumentsException("A value for attachment bandwidth is required.");
            }

            _args.Add(nameof(attachmentBandwidthGbps), attachmentBandwidthGbps);
            return this;
        }

        public virtual IAttachmentBuilder WithAttachmentRole(string portPoolName, string attachmentRoleName)
        {
            _args.Add(nameof(portPoolName), portPoolName);
            _args.Add(nameof(attachmentRoleName), attachmentRoleName);
            return this;
        }

        public virtual IAttachmentBuilder WithContractBandwidth(int? contractBandwidthMbps, bool? trustReceivedCosDscp = false)
        {
            if (contractBandwidthMbps == null)
            {
                throw new BuilderBadArgumentsException("A value for contract bandwidth is required.");
            }

            _args.Add(nameof(contractBandwidthMbps), contractBandwidthMbps);
            _args.Add(nameof(trustReceivedCosDscp), trustReceivedCosDscp != null ? trustReceivedCosDscp : false);
            return this;
        }

        public virtual IAttachmentBuilder WithInterfaces(List<SCM.Models.RequestModels.Ipv4AddressAndMask> ipv4Addresses)
        {
            _args.Add(nameof(ipv4Addresses), ipv4Addresses);        
            return this;
        }

        public virtual IAttachmentBuilder WithLocation(string locationName)
        {
            _args.Add(nameof(locationName), locationName);
            return this;
        }

        public virtual IAttachmentBuilder WithPlane(string planeName = "")
        {
            _args.Add(nameof(planeName), planeName);
            return this;
        }

        public virtual async Task<Attachment> BuildAsync()
        {
            await CreateAttachmentBandwidthAsync();
            SetNumberOfPortsRequired();
            SetPortBandwidthRequired();
            await CreateAttachmentRoleAsync();
            await AllocatePortsAsync();
            CreateInterfaces();
            await SetMtuAsync();

            if (_args.ContainsKey("contractBandwidthMbps"))
            {
                await CreateContractBandwidthPoolAsync();
            }

            await CreateRoutingInstanceAsync();    
            return _attachment;
        }

        protected internal virtual void SetPortBandwidthRequired()
        {
            _portBandwidthRequired = _attachment.AttachmentBandwidth.BandwidthGbps;
        }

        protected internal virtual void SetNumberOfPortsRequired()
        {
            _numPortsRequired = 1;
        }

        protected internal virtual async Task AllocatePortsAsync()
        {
            _ports = await GetPortsAsync();
            var assignedPortStatusResult = await _unitOfWork.PortStatusRepository.GetAsync(q => q.PortStatusType == PortStatusType.Assigned);
            var assignedPortStatus = assignedPortStatusResult.Single();
            foreach (var port in _ports)
            {
                port.PortStatusID = assignedPortStatus.PortStatusID;
                port.TenantID = _attachment.TenantID;
            }
        }

        protected internal virtual async Task CreateAttachmentBandwidthAsync()
        {
            var attachmentBandwidthGbps = (int)_args["attachmentBandwidthGbps"];
            var attachmentBandwidth = await _attachmentBandwidthService.GetAsync(attachmentBandwidthGbps, asTrackable: true);
            if (attachmentBandwidth == null)
            {
                throw new BuilderBadArgumentsException($"The requested attachment bandwidth is not valid.");
            }

            _attachment.AttachmentBandwidthID = attachmentBandwidth.AttachmentBandwidthID;
            _attachment.AttachmentBandwidth = attachmentBandwidth;
        }

        protected internal virtual async Task CreateAttachmentRoleAsync()
        {
            var attachmentRoleName = _args["attachmentRoleName"].ToString();
            var portPoolName = _args["portPoolName"].ToString();
            var attachmentRole = await _attachmentRoleService.GetByPortPoolAndRoleName(portPoolName, attachmentRoleName, asTrackable: true);
            if (attachmentRole == null)
            {
                throw new BuilderBadArgumentsException($"Could not find an attachment role from the supplied '{attachmentRoleName}' " +
                    $"and '{portPoolName}' arguments.");
            }

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
            if (_attachment.AttachmentRole.IsLayer3Role)
            {
                ipv4Addresses = (List<SCM.Models.RequestModels.Ipv4AddressAndMask>)_args["ipv4Addresses"];
                if (!ipv4Addresses.Any())
                {
                    throw new BuilderBadArgumentsException("An IPv4 address and subnet mask is required in order to create an interface for the attachment.");
                }

                var ipv4AddressAndMask = ipv4Addresses.First();
                iface.IpAddress = ipv4AddressAndMask.IpAddress;
                iface.SubnetMask = ipv4AddressAndMask.SubnetMask;
            }

            _attachment.Interfaces = new List<Interface> { iface };
        }

        protected internal virtual async Task CreateContractBandwidthPoolAsync()
        {
            var contractBandwidthMbps = (int)_args["contractBandwidthMbps"];
            if (contractBandwidthMbps > _attachment.AttachmentBandwidth.BandwidthGbps * 1000)
            {
                throw new BuilderBadArgumentsException($"The requested contract bandwidth of {contractBandwidthMbps} Mbps is greater " +
                    $"than the bandwidth of the attachment which is {_attachment.AttachmentBandwidth.BandwidthGbps} Gbps.");
            }

            var contractBandwidth = await _contractBandwidthService.GetAsync(contractBandwidthMbps);
            if (contractBandwidth == null)
            {
                throw new BuilderBadArgumentsException($"The requested contract bandwidth of {_args["contractBandwidthMbps"]} Mbps is not valid.");
            }

            var trustReceivedCosDscp = (bool)_args["trustReceivedCosDscp"];
            var contractBandwidthPool = new ContractBandwidthPool
            {
                ContractBandwidthID = contractBandwidth.ContractBandwidthID,
                TenantID = _attachment.TenantID,
                Name = Guid.NewGuid().ToString("N"),
                TrustReceivedCosDscp = trustReceivedCosDscp
            };

            _attachment.ContractBandwidthPool = contractBandwidthPool;
        }

        protected internal virtual async Task CreateRoutingInstanceAsync()
        {
            if (_attachment.AttachmentRole.RoutingInstanceTypeID == null)
            {
                throw new BuilderUnableToCompleteException("A routing instance type is required for the attachment role but was not found.");
            }

            var routingInstanceType = await _routingInstanceTypeService.GetByIDAsync(_attachment.AttachmentRole.RoutingInstanceTypeID.Value);
            _routingInstanceBuilder = _routingInstanceBuilderFactory(routingInstanceType);
            _routingInstanceBuilder.Init(_attachment.TenantID.Value, _attachment.Device.DeviceID, routingInstanceType.RoutingInstanceTypeID);
            await _routingInstanceBuilder.Create();
            _attachment.RoutingInstance = _routingInstanceBuilder.GetResult();
        }

        protected internal virtual async Task SetMtuAsync()
        {
            Mtu mtu = null;
            if (_attachment.Device.UseLayer2InterfaceMtu)
            {
                mtu = await _mtuService.GetByValueAsync(1514);
            }
            else
            {
                mtu = await _mtuService.GetByValueAsync(1500);
            }

            if (mtu == null)
            {
                throw new BuilderUnableToCompleteException($"The MTU for the attachment could not be set. MTU values could not be found in the database. " +
                    $"Please contact your system administrator to report this issue.");
            }

            _attachment.MtuID = mtu.MtuID;
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

            var locationName = _args["locationName"].ToString();
            if (string.IsNullOrEmpty(locationName))
            {
                throw new BuilderBadArgumentsException("A location is required but none was supplied.");
            }

            var location = await _locationService.GetByNameAsync(locationName);
            if (location == null)
            {
                throw new BuilderBadArgumentsException($"The location {locationName} is not valid.");
            }

            // Find all devices with Device Status of 'Production' which are in the requested Location and which suport the 
            // required Attachment Role

            var query = from d in await _unitOfWork.DeviceRepository.GetAsync(q => q.DeviceStatus.DeviceStatusType == DeviceStatusType.Production
                && q.LocationID == location.LocationID
                && q.DeviceRole.DeviceRoleAttachmentRoles.Where(x => x.AttachmentRoleID == _attachment.AttachmentRoleID).Any(),
                includeProperties: "Ports.PortBandwidth,Ports.Device,Ports.PortStatus,Attachments", AsTrackable: true)
                        select d;

            // Filter devices collection to include only those devices which belong to the requested plane (if specified)

            var planeName = _args["planeName"] != null ? _args["planeName"].ToString() : "";
            if (!string.IsNullOrEmpty(planeName))
            {
                var plane = await _planeService.GetByNameAsync(planeName);
                if (plane == null)
                {
                    throw new BuilderBadArgumentsException($"The plane {planeName} is not valid.");
                }

                query = query.Where(q => q.PlaneID == plane.PlaneID).ToList();
            }

            devices = query.ToList();
           
            // Filter devices collection to only those devices which have the required number of free ports
            // of the required bandwidth and which belong to the requested Port Pool

            devices = devices.Where(q => q.Ports.Where(p => p.PortStatus.PortStatusType == PortStatusType.Free
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

                _attachment.Device = devices.Aggregate((current, x) =>
                (x.Ports.Where(p => p.PortStatus.PortStatusType == PortStatusType.Free
                && p.PortBandwidth.BandwidthGbps == _portBandwidthRequired)
                .Count() >
                current.Ports.Where(p => p.PortStatus.PortStatusType == PortStatusType.Free
                && p.PortBandwidth.BandwidthGbps == _portBandwidthRequired)
                .Count() ? x : current));
            }
            else
            {
                // Only one device found

                _attachment.Device = devices.Single();
            }

            // Get the ports

            var ports = _attachment.Device.Ports.Where(q => q.PortStatus.PortStatusType == PortStatusType.Free
                  && q.PortBandwidth.BandwidthGbps == _portBandwidthRequired
                  && q.PortPoolID == _attachment.AttachmentRole.PortPoolID);

            return ports.Take(_numPortsRequired);
        }
    }
}

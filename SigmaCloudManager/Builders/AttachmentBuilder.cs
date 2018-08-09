using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Services;
using SCM.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Mind.Builders
{
    public class AttachmentBuilder : IAttachmentBuilder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMtuService _mtuService;
        private readonly IAttachmentRoleService _attachmentRoleService;
        private readonly IAttachmentBandwidthService _attachmentBandwidthService;
        private readonly IContractBandwidthService _contractBandwidthService;
        private readonly ILocationService _locationService;
        private readonly IPlaneService _planeService;
        private readonly IRoutingInstanceTypeService _routingInstanceTypeService;
        private IRoutingInstanceBuilder _routingInstanceBuilder;
        private int _portBandwidthRequired;
        private int _numPortsRequired = 1;
        private Port _port;
        private AttachmentRole _attachmentRole;
        private readonly Attachment _attachment;

        private readonly Func<RoutingInstanceType, IRoutingInstanceBuilder> _routingInstanceBuilderFactory;

        public AttachmentBuilder(IUnitOfWork unitOfWork, IMtuService mtuService, IAttachmentRoleService attachmentRoleService,
            IAttachmentBandwidthService attachmentBandwidthService, IContractBandwidthService contractBandwidthService,
            ILocationService locationService, IPlaneService planeService, IRoutingInstanceTypeService routingInstanceTypeService,
            Func<RoutingInstanceType,IRoutingInstanceBuilder> routingInstanceBuilderFactory)
        {
            _attachment = new Attachment();
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

        public IAttachmentBuilder Init(int tenantId)
        {
            _attachment.TenantID = tenantId;
            _attachment.Created = true;
            _attachment.ShowCreatedAlert = true;

            return this;
        }

        public async Task SetAttachmentBandwidthAsync(int? attachmentBandwidthGbps)
        {
            if (attachmentBandwidthGbps == null)
            {
                throw new BuilderBadArgumentsException("A value for attachment bandwidth is required.");
            }

            var attachmentBandwidth = await _attachmentBandwidthService.GetAsync(attachmentBandwidthGbps.Value);
            if (attachmentBandwidth == null)
            {
                throw new BuilderBadArgumentsException($"The requested attachment bandwidth {attachmentBandwidthGbps} is not valid.");
            }

            _attachment.AttachmentBandwidthID = attachmentBandwidth.AttachmentBandwidthID;

            SetNumPortsRequired(1);
            SetPortBandwidthRequired(attachmentBandwidthGbps.Value);
        }

        public IAttachmentBuilder SetNumPortsRequired(int numPortsRequired)
        {
            _numPortsRequired = numPortsRequired;
            return this;
        }

        public IAttachmentBuilder SetPortBandwidthRequired(int portBandwidthRequired)
        {
            _portBandwidthRequired = portBandwidthRequired;
            return this;
        }

        public async Task SetAttachmentRoleAsync(string portPoolName, string attachmentRoleName)
        {
            _attachmentRole = await _attachmentRoleService.GetByPortPoolAndRoleName(portPoolName, attachmentRoleName, asTrackable: true);
            if (_attachmentRole == null)
            {
                throw new BuilderBadArgumentsException($"Could not find an attachment role from the supplied '{attachmentRoleName}' " +
                    $"and '{portPoolName}' arguments.");
            }

            _attachment.AttachmentRoleID = _attachmentRole.AttachmentRoleID;
            _attachment.AttachmentRole = _attachmentRole;
            _attachment.IsLayer3 = _attachmentRole.IsLayer3Role;
            _attachment.IsTagged = _attachmentRole.IsTaggedRole;
            _attachment.RequiresSync = _attachmentRole.RequireSyncToNetwork;
            _attachment.ShowRequiresSyncAlert = _attachmentRole.RequireSyncToNetwork;
        }

        public async Task SetMtuAsync()
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
                throw new BuilderUnableToCompleteException($"The MTU for the attachment could not be set. " +
                    $"Please contact your system administrator to report this issue.");
            }

            _attachment.MtuID = mtu.MtuID;
        }

        public async Task CreateContractBandwidthPoolAsync(int tenantID, int? contractBandwidthMbps)
        {
            if (contractBandwidthMbps == null)
            {
                throw new BuilderBadArgumentsException("A value for contract bandwidth is required.");
            }

            var contractBandwidth = await _contractBandwidthService.GetAsync(contractBandwidthMbps.Value);
            if (contractBandwidth == null)
            {
                throw new BuilderBadArgumentsException($"The requested contract bandwidth of {contractBandwidthMbps} Mbps is not valid.");
            }

            var contractBandwidthPool = new ContractBandwidthPool
            {
                ContractBandwidthID = contractBandwidth.ContractBandwidthID,
                TenantID = tenantID,
                Name = Guid.NewGuid().ToString("N")
            };

            _attachment.ContractBandwidthPool = contractBandwidthPool;
        }

        public IAttachmentBuilder CreateInterfaces(string ipAddress, string subnetMask)
        {
            var iface = new Interface
            {
                DeviceID = _attachment.Device.DeviceID,
                IpAddress = ipAddress,
                SubnetMask = subnetMask,
                Ports = new List<Port> { _port }
            };

            _attachment.Interfaces = new List<Interface> { iface };

            return this;
        }

        public async Task CreateRoutingInstanceAsync()
        {
            if (_attachmentRole.RoutingInstanceTypeID == null)
            {
                throw new BuilderUnableToCompleteException("A routing instance type is required for the attachment role but was not found.");
            }

            var routingInstanceType = await _routingInstanceTypeService.GetByIDAsync(_attachmentRole.RoutingInstanceTypeID.Value);
            _routingInstanceBuilder = _routingInstanceBuilderFactory(routingInstanceType);
            _routingInstanceBuilder.Init(_attachment.TenantID.Value, _attachment.Device.DeviceID, routingInstanceType.RoutingInstanceTypeID);
            await _routingInstanceBuilder.Create();
            _attachment.RoutingInstance = _routingInstanceBuilder.GetResult();
        }

        public async Task AllocatePortsAsync(string locationName, string planeName)
        {
            var ports = await GetPortsAsync(planeName, locationName);
            _port = ports.Single();
            var assignedPortStatusResult = await _unitOfWork.PortStatusRepository.GetAsync(q => q.PortStatusType == PortStatusType.Assigned);
            var assignedPortStatus = assignedPortStatusResult.Single();
            _port.PortStatusID = assignedPortStatus.PortStatusID;
            _port.TenantID = _attachment.TenantID;
        }

        public Attachment GetResult()
        {
            return _attachment;
        }

        /// <summary>
        /// Get some available ports.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Port>> GetPortsAsync(string planeName = "", string locationName = "")
        {
            IList<Device> devices;

            // Find a collection of Devices matching the requested Location
            // with a status of 'Production'

            if (locationName == null)
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
                && p.PortPoolID == _attachmentRole.PortPoolID
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
                  && q.PortPoolID == _attachmentRole.PortPoolID);

            return ports.Take(_numPortsRequired);
        }
    }
}

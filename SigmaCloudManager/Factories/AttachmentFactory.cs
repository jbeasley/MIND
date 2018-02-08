using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Data;
using SCM.Services;
using System.Collections.Generic;

namespace SCM.Factories
{
    /// <summary>
    /// Factory for creating Attachments.
    /// </summary>
    public class AttachmentFactory : BaseFactory, IAttachmentFactory
    {
        public AttachmentFactory(IMapper mapper,
            IRoutingInstanceTypeService routingInstanceTypeService,
            IRoutingInstanceService routingInstanceService,
            IAttachmentBandwidthService attachmentBandwidthService,
            IMtuService mtuService,
            IAttachmentRoleService attachmentRoleService,
            IUnitOfWork unitOfWork,
            IRoutingInstanceFactory vrfFactory,
            IContractBandwidthPoolFactory contractBandwidthPoolFactory) : base(mapper)
        {
            AttachmentBandwidthService = attachmentBandwidthService;
            RoutingInstanceTypeService = routingInstanceTypeService;
            RoutingInstanceService = routingInstanceService;
            MtuService = mtuService;
            AttachmentRoleService = attachmentRoleService;
            UnitOfWork = unitOfWork;
            RoutingInstanceFactory = vrfFactory;
            ContractBandwidthPoolFactory = contractBandwidthPoolFactory;
        }

        private IAttachmentBandwidthService AttachmentBandwidthService { get; }
        private IRoutingInstanceTypeService RoutingInstanceTypeService { get; }
        private IRoutingInstanceService RoutingInstanceService { get; }
        private IMtuService MtuService { get; }
        private IAttachmentRoleService AttachmentRoleService { get; }
        private IUnitOfWork UnitOfWork { get; }
        private IRoutingInstanceFactory RoutingInstanceFactory { get; }
        private IContractBandwidthPoolFactory ContractBandwidthPoolFactory { get; }

        /// <summary>
        /// Create a new Attachment, Bundle Attachment, or Multi-Port
        /// The request object denotes which type of Attachment to create.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FactoryResult> NewAsync(AttachmentRequest request)
        {
            var bandwidth = await AttachmentBandwidthService.GetByIDAsync(request.BandwidthID);
            request.Bandwidth = bandwidth;

            // Get some ports for the Attachment

            request = await GetPortsAsync(request);

            // Hand off to create the new Attachment

            if (request.BundleRequired)
            {
                return await NewBundleAttachmentAsync(request);
            }
            else if (request.MultiPortRequired)
            {
                return await NewMultiPortAsync(request);
            }
            else
            {
                return await NewAttachmentAsync(request);
            }
        }

        /// <summary>
        /// Create a new Attachment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<FactoryResult> NewAttachmentAsync(AttachmentRequest request)
        {
            var result = await CreateAttachmentAsync(request);
            var iface = new Interface
            {
                DeviceID = request.Device.DeviceID
            };

            if (request.IsLayer3)
            {
                iface.IpAddress = request.IpAddress1;
                iface.SubnetMask = request.SubnetMask1;
            }

            var attachment = (Attachment)result.Item;
            attachment.Interfaces.Add(iface);

            return result;
        }

        /// <summary>
        /// Create a new Bundle Attachment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<FactoryResult> NewBundleAttachmentAsync(AttachmentRequest request)
        {
            var result = await CreateAttachmentAsync(request);
            var attachment = (Attachment)result.Item;
            var iface = new Interface();

            if (request.IsLayer3)
            {
                iface.IpAddress = request.IpAddress1;
                iface.SubnetMask = request.SubnetMask1;
            }

            var port = request.Ports.First();
            iface.DeviceID = port.DeviceID;
            attachment.Interfaces.Add(iface);

            // Find the first unused bundle ID in the range 1, 65535
            // (this is the Cisco IOS - XR allowable range for bundle IDs)

            var usedBundleIDs = port.Device.Attachments.Where(q => q.IsBundle).Select(q => q.ID).Where(q => q != null);
            int? id = Enumerable.Range(1, 65535).Except(usedBundleIDs.Select(q => q.Value)).FirstOrDefault();

            attachment.ID = id ?? throw new FactoryFailureException("Unable to assign a free ID value to the Attachment. "
                    + $"Have all IDs in the range 1 - 65535 for device '{port.Device.Name}' been used?");

            // Default the min links setting for the bundle to the number of ports to be added to the Attachmet.
            // The user can change this setting later once the Attachment is created by editing the Attachment record.

            attachment.BundleMinLinks = request.Ports.Count();

            return result;
        }

        /// <summary>
        /// Create a new Multi-Port
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<FactoryResult> NewMultiPortAsync(AttachmentRequest request)
        {
            var result = await CreateAttachmentAsync(request);
            var attachment = (Attachment)(result.Item);

            // Find the first un-used identifier in the range 1, 65535 (this range is fairly arbitrary - the identifer is not used
            // for network configuration of a multiport. It serves only to create a unique multiport ID which is used
            // in the NSO service models).

            var port = request.Ports.First();
            var usedIds = port.Device.Attachments.Where(q => q.IsMultiPort).Select(q => q.ID).Where(q => q != null);
            int? id = Enumerable.Range(1, 65535).Except(usedIds.Select(q => q.Value)).FirstOrDefault();

            attachment.ID = id ?? throw new FactoryFailureException("Unable to assign a free ID value to the Attachment. "
                    + $"Have all IDs in the range 1 - 65535 for device '{port.Device.Name}' been used?");

            // Create interfaces, one per port in the multi-port group

            var ports = request.Ports.ToList();
            var portCount = ports.Count();

            for (var i = 1; i <= portCount; i++)
            {
                var iface = new Interface
                {
                    DeviceID = request.Device.DeviceID
                };

                if (request.IsLayer3)
                {
                    if (i == 1)
                    {
                        iface.IpAddress = request.IpAddress1;
                        iface.SubnetMask = request.SubnetMask1;
                    }
                    else if (i == 2)
                    {
                        iface.IpAddress = request.IpAddress2;
                        iface.SubnetMask = request.SubnetMask2;
                    }
                    else if (i == 3)
                    {
                        iface.IpAddress = request.IpAddress3;
                        iface.SubnetMask = request.SubnetMask3;
                    }
                    else if (i == 4)
                    {
                        iface.IpAddress = request.IpAddress4;
                        iface.SubnetMask = request.SubnetMask4;
                    }
                }

                attachment.Interfaces.Add(iface);
            }

            return result;
        }

        /// <summary>
        /// Generic helper to create a basic Attachment for use as 
        /// a new Attachment, Bundle Attachment, or Multi-Port
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<FactoryResult> CreateAttachmentAsync(AttachmentRequest request)
        {
            var attachment = Mapper.Map<Attachment>(request);
            var result = new FactoryResult
            {
                IsSuccess = true,
                Item = attachment
            };

            Mtu mtu = null;
            if (request.Device.UseLayer2InterfaceMtu)
            {
                mtu = await MtuService.GetByValueAsync(1514);
            }
            else
            {
                mtu = await MtuService.GetByValueAsync(1500);
            }

            if (mtu == null)
            {
                throw new FactoryFailureException($"The required MTU ({mtu.MtuValue} bytes) was not found.");
            }

            var attachmentRole = await AttachmentRoleService.GetByIDAsync(request.AttachmentRoleID);
            attachment.MtuID = mtu.MtuID;
            attachment.AttachmentBandwidthID = request.BandwidthID;
            attachment.RequiresSync = attachmentRole.RequireSyncToNetwork;
            attachment.Created = true;
            attachment.ShowCreatedAlert = true;
            attachment.ShowRequiresSyncAlert = attachmentRole.RequireSyncToNetwork;

            if (attachmentRole.RoutingInstanceTypeID != null)
            {
                var routingInstanceType = await RoutingInstanceTypeService.GetByIDAsync(attachmentRole.RoutingInstanceTypeID.Value);
                if (routingInstanceType.IsLayer3 && routingInstanceType.IsDefault)
                {
                    var deviceDefaultRoutingInstanceResult = await RoutingInstanceService.GetAllByDeviceIDAsync(attachment.DeviceID, 
                        isDefault: true, isLayer3: true);
                    var deviceDefaultRoutingInstance = deviceDefaultRoutingInstanceResult.SingleOrDefault();

                    if (deviceDefaultRoutingInstance == null)
                    {
                        throw new FactoryFailureException("Could not find the default layer 3 routing instance for device "
                            + $"'{attachment.Device.Name}'");
                    }

                    attachment.RoutingInstanceID = deviceDefaultRoutingInstance.RoutingInstanceID;
                }
                else
                {
                    request.RoutingInstanceTypeID = attachmentRole.RoutingInstanceTypeID;

                    // Call Routing Instance factory to create Routing Instance

                    var vrfResponse = await RoutingInstanceFactory.NewAsync(request);
                    var vrf = (RoutingInstance)vrfResponse.Item;
                    attachment.RoutingInstance = vrf;
                }
            }

            if (attachmentRole.RequireContractBandwidth) {

                // Create Contract Bandwidth Pool and add to Attachment

                var contractBandwidthPool = Mapper.Map<ContractBandwidthPool>(request);
                attachment.ContractBandwidthPool = (ContractBandwidthPool)ContractBandwidthPoolFactory.New(contractBandwidthPool).Item;
            }
        
            return result;
        }

        /// <summary>
        /// Helper to get some ports for a new Attachment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<AttachmentRequest> GetPortsAsync(AttachmentRequest request)
        {

            // Work out the number of ports we need to allocate and 
            // the port bandwidth required

            if (request.BundleRequired || request.MultiPortRequired)
            {
                // For bundles and multiport requests we need at least 2 ports. Work out the number of ports required from 
                // the request data based upon the required bandwidth of the attachment (e.g. 20Gbp/s) and the 
                // per port bandwidth needed to satisfy the required bandwidth (e.g. 10Gb/s)

                request.NumPortsRequired = request.Bandwidth.BandwidthGbps / request.Bandwidth.BundleOrMultiPortMemberBandwidthGbps.Value;
                request.PortBandwidthRequired = request.Bandwidth.BandwidthGbps / request.NumPortsRequired;
            }
            else
            {
                // The request is not for a bundle or multiport so the number of ports required must be 1

                request.NumPortsRequired = 1;
                request.PortBandwidthRequired = request.Bandwidth.BandwidthGbps;
            }

            // Try and find some ports which satisfy the request

            request = await FindPortsAsync(request);

            // Do we have some ports? If not quit. 

            if (request.Ports.Count() == 0)
            {
                return request;
            }

            return request;
        }

        /// <summary>
        ///  Helper to find some ports
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<AttachmentRequest> FindPortsAsync(AttachmentRequest request)
        {
            // First find a Device with some ports

            request = await FindDeviceAsync(request);

            if (request.Device == null)
            {
                // Bad news - no Device matches the request so quit

                return request;
            }

            // Filter to get ports with the required bandwidth and which belong to the requested Port Role

            var ports = request.Device.Ports.Where(q => q.PortStatus.PortStatusType == PortStatusType.Free 
                    && q.PortBandwidth.BandwidthGbps == request.PortBandwidthRequired 
                    && q.PortPoolID == request.PortPoolID);

            // Check if we have at least the number of ports rquired

            if (ports.Count() == 0)
            {
                throw new FactoryFailureException("No ports matching the requested bandwidth parameter could not be found. "
                    + "Please change your request and try again, or contact your system adminstrator and report this issue.");
            }

            if (ports.Count() < request.NumPortsRequired)
            {
                throw new FactoryFailureException($"The number of ports available ({ports.Count()}) is less than the number "
                    + $"required ({request.NumPortsRequired}). "
                    + "Please change your request and try again, or contact your system adminstrator and report this issue.");
            }

            // Take the number of ports we need

            request.Ports = ports.Take(request.NumPortsRequired);

            return request;
        }

        /// <summary>
        /// Find a Device with ports which can be used to satisfy the attachment request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<AttachmentRequest> FindDeviceAsync(AttachmentRequest request)
        {
            IList<Device> devices;

            // If a specific Device has not been requested then find a collection of Devices matching the requested Location
            // with a status of 'Production'

            if (request.DeviceID == null)
            {
                // Find all devices with Device Status of 'Production' which are in the requested Location and which suport the 
                // required Attachment Role

                var query = from d in await UnitOfWork.DeviceRepository.GetAsync(q => q.DeviceStatus.DeviceStatusType == DeviceStatusType.Production
                    && q.LocationID == request.LocationID
                    && q.DeviceRole.DeviceRoleAttachmentRoles.Where(x => x.AttachmentRoleID == request.AttachmentRoleID).Any(),
                    includeProperties: "Ports.PortBandwidth,Ports.Device,Ports.PortStatus,Attachments")
                          select d;

                // Filter devices collection to include only those devices which belong to the requested plane (if specified)

                if (request.PlaneID != null)
                {
                    query = query.Where(q => q.PlaneID == request.PlaneID).ToList();
                }

                devices = query.ToList();
            }
            else
            {
                // A specific Device has been requested so retrieve it, making sure the status of the Device is 'Production'
                // and that the device supports the required Attachment Role

                devices = await UnitOfWork.DeviceRepository.GetAsync(q => q.DeviceID == request.DeviceID 
                    && q.DeviceStatus.DeviceStatusType == DeviceStatusType.Production
                    && q.DeviceRole.DeviceRoleAttachmentRoles.Where(x => x.AttachmentRoleID == request.AttachmentRoleID).Any(),
                    includeProperties: "Ports.PortBandwidth,Ports.Device,Ports.PortStatus,Attachments");
            }

            // Filter devices collection to only those devices which have the required number of free ports
            // of the required bandwidth and which belong to the requested Port Pool

            devices = devices.Where(q => q.Ports.Where(p => p.PortStatus.PortStatusType == PortStatusType.Free 
                && p.PortPoolID == request.PortPoolID
                && p.PortBandwidth.BandwidthGbps == request.PortBandwidthRequired).Count() >= request.NumPortsRequired).ToList();

            if (devices.Count == 0)
            {
                throw new FactoryFailureException("Ports matching the requested "
                    + "parameters could not be found. "
                    + "Please change the input parameters and try again, "
                    + "or contact your system adminstrator to report this issue.");
            }
            else if (devices.Count > 1)
            {
                // Get device with the most free ports of the required Port Bandwidth

                request.Device = devices.Aggregate((current, x) =>
                (x.Ports.Where(p => p.PortStatus.PortStatusType == PortStatusType.Free 
                && p.PortBandwidth.BandwidthGbps == request.Bandwidth.BandwidthGbps)
                .Count() > 
                current.Ports.Where(p => p.PortStatus.PortStatusType == PortStatusType.Free 
                && p.PortBandwidth.BandwidthGbps == request.Bandwidth.BandwidthGbps)
                .Count() ? x : current));
            }
            else
            {
                // Only one device found so return it

                request.Device = devices.Single();
            }

            return request;
        }
    }
}


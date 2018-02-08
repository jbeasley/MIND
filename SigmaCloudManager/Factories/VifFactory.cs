using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Data;
using SCM.Services;

namespace SCM.Factories
{
    /// <summary>
    /// Factory for creating VIFs
    /// </summary>
    public class VifFactory : BaseFactory, IVifFactory
    {
        public VifFactory(IMapper mapper,
            IUnitOfWork unitOfWork,
            IAttachmentService attachmentService,
            IRoutingInstanceTypeService routingInstanceTypeService,
            IRoutingInstanceService routingInstanceService,
            IVifRoleService vifRoleService,
            IRoutingInstanceFactory vrfFactory,
            IContractBandwidthPoolFactory contractBandwidthPoolFactory) : base(mapper)
        {
            UnitOfWork = unitOfWork;
            AttachmentService = attachmentService;
            RoutingInstanceService = routingInstanceService;
            RoutingInstanceTypeService = routingInstanceTypeService;
            VifRoleService = vifRoleService;
            RoutingInstanceFactory = vrfFactory;
            ContractBandwidthPoolFactory = contractBandwidthPoolFactory;
        }

        private IUnitOfWork UnitOfWork { get; }
        private IAttachmentService AttachmentService { get; }
        private IRoutingInstanceService RoutingInstanceService { get; }
        private IRoutingInstanceTypeService RoutingInstanceTypeService { get; }
        private IVifRoleService VifRoleService { get; }
        private IRoutingInstanceFactory RoutingInstanceFactory { get; }
        private IContractBandwidthPoolFactory ContractBandwidthPoolFactory { get; }

        /// <summary>
        /// Create a new Vif
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FactoryResult> NewAsync(VifRequest request)
        {
            var result = await CreateVifAsync(request);
            if (!result.IsSuccess)
            {
                return result;
            }

            var vif = (Vif)result.Item;

            // Create a vlan for each Interface which is associated with the Vif's parent Attachment

            var attachment = await AttachmentService.GetByIDAsync(request.AttachmentID);
            var ifaces = attachment.Interfaces.ToList();
            var ifaceCount = ifaces.Count;

            for (var i = 1; i <= ifaceCount; i++)
            {
                var vlan = new Vlan
                {
                    InterfaceID = ifaces[i - 1].InterfaceID
                };

                // Check to add IP address and subnet mask to the vlan

                if (request.IsLayer3)
                {
                    if (i == 1)
                    {
                        vlan.IpAddress = request.IpAddress1;
                        vlan.SubnetMask = request.SubnetMask1;
                    }
                    else if (i == 2)
                    {
                        vlan.IpAddress = request.IpAddress2;
                        vlan.SubnetMask = request.SubnetMask2;
                    }
                    else if (i == 3)
                    {
                        vlan.IpAddress = request.IpAddress3;
                        vlan.SubnetMask = request.SubnetMask3;
                    }
                    else if (i == 4)
                    {
                        vlan.IpAddress = request.IpAddress4;
                        vlan.SubnetMask = request.SubnetMask4;
                    }
                }

                // Add the new vlan to the Vif's Vlans collection

                vif.Vlans.Add(vlan);
            }

            return result;
        }

        /// <summary>
        /// Generic helper to create a basic Vif for use as 
        /// a new Attachment Vif, Bundle Attachment Vif, or Multi-Port Attachment Vif
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<FactoryResult> CreateVifAsync(VifRequest request)
        {
            var vif = Mapper.Map<Vif>(request);

            var result = new FactoryResult
            {
                IsSuccess = true,
                Item = vif
            };

            if (vif == null)
            {
                throw new FactoryFailureException("The Vif cannot be null.");
            }

            var vifRole = await VifRoleService.GetByIDAsync(request.VifRoleID);

            vif.RequiresSync = vifRole.RequireSyncToNetwork;
            vif.Created = true;
            vif.ShowRequiresSyncAlert = vifRole.RequireSyncToNetwork;
            vif.ShowCreatedAlert = true;

            // Allocate a new Vlan Tag for the VIF

            var allocateVlanTagResult = await AllocateVlanTagAsync(request);
            var vlanTagResult = (VlanTagResult)allocateVlanTagResult.Item;
            vif.VlanTag = vlanTagResult.AllocatedVlanTag;

            if (vlanTagResult.VlanTagRange != null)
            {
                vif.VlanTagRangeID = vlanTagResult.VlanTagRange.VlanTagRangeID;
            }

            var attachment = await AttachmentService.GetByIDAsync(request.AttachmentID);
            request.DeviceID = attachment.DeviceID;

            if (vifRole.RoutingInstanceTypeID != null)
            {

                var routingInstanceType = await RoutingInstanceTypeService.GetByIDAsync(vifRole.RoutingInstanceTypeID.Value);
                if (routingInstanceType.IsLayer3 && routingInstanceType.IsDefault)
                {
                    var deviceDefaultRoutingInstanceResult = await RoutingInstanceService.GetAllByDeviceIDAsync(attachment.DeviceID, 
                        isDefault: true, isLayer3: true);
                    var deviceDefaultRoutingInstance = deviceDefaultRoutingInstanceResult.SingleOrDefault();
                    if (deviceDefaultRoutingInstance == null)
                    {
                        throw new FactoryFailureException("Could not find the default layer 3 routing instance "
                            + $"for device '{attachment.Device.Name}'");
                    }

                    vif.RoutingInstanceID = deviceDefaultRoutingInstance.RoutingInstanceID;
                }
                else
                {
                    request.RoutingInstanceTypeID = vifRole.RoutingInstanceTypeID;

                    // Call Routing Instance factory to create Routing Instance

                    var vrfResponse = await RoutingInstanceFactory.NewAsync(request);
                    var vrf = (RoutingInstance)vrfResponse.Item;
                    vif.RoutingInstance = vrf;
                }
            }

            if (request.ContractBandwidthID != null)
            {
                // Create Contract Bandwidth Pool and add to Vif

                var contractBandwidthPool = Mapper.Map<ContractBandwidthPool>(request);
                vif.ContractBandwidthPool = (ContractBandwidthPool)ContractBandwidthPoolFactory.New(contractBandwidthPool).Item;
            }

            return result;
        }

        /// <summary>
        /// Allocate a vlan tag for a new VIF.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<FactoryResult> AllocateVlanTagAsync(VifRequest request)
        {
            var result = new FactoryResult
            {
                IsSuccess = true
            };

            var attachment = await AttachmentService.GetByIDAsync(request.AttachmentID);
            var currentVifs = attachment.Vifs;

            if (request.AutoAllocateVlanTag)
            {
                var dbResult = await UnitOfWork.VlanTagRangeRepository.GetAsync(q => q.Name == "Default");
                var vlanTagRange = dbResult.SingleOrDefault();

                if (vlanTagRange == null)
                {
                    throw new FactoryFailureException("The default vlan tag range was not found.");
                }

                // Allocate a new unused vlan tag from the vlan tag range

                int? newVlanTag = Enumerable.Range(vlanTagRange.VlanTagRangeStart, vlanTagRange.VlanTagRangeCount)
                    .Except(currentVifs.Select(v => v.VlanTag)).FirstOrDefault();

                if (newVlanTag == null)
                {
                    throw new FactoryFailureException("Failed to allocate a free vlan tag. "
                        + "Please contact your administrator, or try another range.");
                }

                var vlanTagResult = new VlanTagResult
                {
                    VlanTagRange = vlanTagRange,
                    AllocatedVlanTag = newVlanTag.Value
                };

                result.Item = vlanTagResult;

                return result;
            }
            else
            {
                if (request.RequestedVlanTag == null)
                {
                    throw new FactoryFailureException("A requested vlan tag must be specified, "
                        + "or select the auto-allocate vlan tag option.");
                }

                if (currentVifs.Where(q => q.VlanTag == request.RequestedVlanTag).Count() > 0)
                {
                    throw new FactoryFailureException("The requested vlan tag is already assigned. " 
                        + "Try again with a different vlan tag.");
                }
                else
                {
                    var vlanTagResult = new VlanTagResult
                    {
                        AllocatedVlanTag = request.RequestedVlanTag.Value
                    };

                    result.Item = vlanTagResult;
                    return result;
                }
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SCM.Models.RequestModels;
using AutoMapper;

namespace SCM.Services
{
    public class RoutingInstanceService : BaseService, IRoutingInstanceService
    {
        public RoutingInstanceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        private string Properties { get; } = "Tenant,"
                + "RoutingInstanceType,"
                + "Attachments.Interfaces,"
                + "Vifs.Vlans,"
                + "Device.Location.SubRegion.Region,"
                + "AttachmentSetRoutingInstances.AttachmentSet.VpnAttachmentSets.Vpn,"
                + "AttachmentSetRoutingInstances.AttachmentSet.Tenant,"
                + "BgpPeers.VpnTenantIpNetworksOut.TenantIpNetwork,"
                + "BgpPeers.VpnTenantIpNetworksOut.AttachmentSet.VpnAttachmentSets.Vpn,"
                + "BgpPeers.VpnTenantCommunitiesOut.TenantCommunity,"
                + "BgpPeers.VpnTenantCommunitiesOut.AttachmentSet.VpnAttachmentSets.Vpn,"
                + "BgpPeers.VpnTenantIpNetworksIn.TenantIpNetwork,"
                + "BgpPeers.VpnTenantIpNetworksIn.AttachmentSet.VpnAttachmentSets.Vpn,"
                + "BgpPeers.VpnTenantCommunitiesIn.TenantCommunity,"
                + "BgpPeers.VpnTenantCommunitiesIn.AttachmentSet.VpnAttachmentSets.Vpn,"
                + "VpnTenantIpNetworkRoutingInstances.AttachmentSet.VpnAttachmentSets.Vpn,"
                + "VpnTenantCommunityRoutingInstances.TenantCommunity,"
                + "VpnTenantCommunityRoutingInstances.AttachmentSet.VpnAttachmentSets.Vpn,"
                + "VpnTenantIpNetworkStaticRouteRoutingInstances.TenantIpNetwork,"
                + "VpnTenantIpNetworkStaticRouteRoutingInstances.AttachmentSet.VpnAttachmentSets.Vpn";


        public async Task<RoutingInstance> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.RoutingInstanceRepository.GetAsync(q => q.RoutingInstanceID == id, 
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<IEnumerable<RoutingInstance>> GetAllByNameAsync(string name, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.RoutingInstanceRepository.GetAsync(q => q.Name == name,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<IEnumerable<RoutingInstance>> GetAllByRouteDistinguisherRangeTypeAsync(RouteDistinguisherRangeTypeEnum rdRangeType, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.RoutingInstanceRepository.GetAsync(q => q.RouteDistinguisherRange.Type == rdRangeType,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<IEnumerable<RoutingInstance>> GetAllByDeviceIDAsync(int deviceID, int? tenantID = null, 
            bool? isDefault = null, bool? isLayer3 = null, bool? isTenantFacingVrf = null, bool? isInfrastructureVrf = false, 
            bool includeProperties = true)
        {
            var p = includeProperties ? Properties : "RoutingInstanceType";
            var query = from routingInstances in await UnitOfWork.RoutingInstanceRepository.GetAsync(q => q.DeviceID == deviceID,
                includeProperties: p,
                AsTrackable: false)
                        select routingInstances;
            
            if (tenantID != null)
            {
                query = query.Where(x => x.TenantID == tenantID);
            }

            if (isDefault != null)
            {
                query = query.Where(x => x.RoutingInstanceType.IsDefault == isDefault.Value);
            }
            
            if (isLayer3 != null)
            {
                query = query.Where(x => x.RoutingInstanceType.IsLayer3 == isLayer3.Value);
            }

            if (isTenantFacingVrf != null)
            {
                query = query.Where(x => x.RoutingInstanceType.IsTenantFacingVrf == isTenantFacingVrf.Value);
            }

            if (isInfrastructureVrf != null)
            {
                query = query.Where(x => x.RoutingInstanceType.IsInfrastructureVrf == isInfrastructureVrf.Value);
            }

            return query.ToList();        }

        public async Task<IEnumerable<RoutingInstance>> GetAllByAttachmentSetIDAsync(int attachmentSetID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.RoutingInstanceRepository.GetAsync(q => q.AttachmentSetRoutingInstances
                                                                 .Where(x => x.AttachmentSetID == attachmentSetID)
                                                                 .Any(),
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<int> UpdateAsync(RoutingInstance routingInstance)
        {
            var rdRange = await UnitOfWork.RouteDistinguisherRangeRepository.GetAsync(q => q.AdministratorSubField == routingInstance.AdministratorSubField);
            routingInstance.RouteDistinguisherRangeID = rdRange.Single().RouteDistinguisherRangeID;
            UnitOfWork.RoutingInstanceRepository.Update(routingInstance);

            // Update the Requires Sync state of the Device. If VRF name has changed then a 
            // sync to the Device is required to update the network

            var dbResult = await UnitOfWork.DeviceRepository.GetAsync(q => q.DeviceID == routingInstance.DeviceID, 
                includeProperties: "DeviceRole");
            var device = dbResult.Single();

            device.RequiresSync = device.DeviceRole.RequireSyncToNetwork;
            device.ShowRequiresSyncAlert = device.DeviceRole.RequireSyncToNetwork;
            UnitOfWork.DeviceRepository.Update(device);

            return await UnitOfWork.SaveAsync();
        }
    }
}
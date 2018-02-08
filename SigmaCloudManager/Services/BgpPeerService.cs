using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class BgpPeerService : BaseService, IBgpPeerService
    {
        public BgpPeerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "RoutingInstance.Tenant,"
                  + "VpnTenantCommunitiesIn.TenantCommunity,"
                  + "VpnTenantNetworksIn.TenantNetwork,"
                  + "VpnTenantCommunitiesIn.AttachmentSet.VpnAttachmentSets.Vpn,"
                  + "VpnTenantNetworksIn.AttachmentSet.VpnAttachmentSets.Vpn,"
                  + "VpnTenantCommunitiesOut.TenantCommunity,"
                  + "VpnTenantNetworksOut.TenantNetwork,"
                  + "VpnTenantCommunitiesOut.AttachmentSet.VpnAttachmentSets.Vpn,"
                  + "VpnTenantNetworksOut.AttachmentSet.VpnAttachmentSets.Vpn,"
                  + "VpnTenantCommunitiesIn.AttachmentSet.VpnAttachmentSets.AttachmentSet.Tenant,"
                  + "VpnTenantNetworksIn.AttachmentSet.VpnAttachmentSets.AttachmentSet.Tenant,"
                  + "VpnTenantCommunitiesOut.AttachmentSet.VpnAttachmentSets.AttachmentSet.Tenant,"
                  + "VpnTenantNetworksOut.AttachmentSet.VpnAttachmentSets.AttachmentSet.Tenant";

        public async Task<IEnumerable<BgpPeer>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.BgpPeerRepository.GetAsync(includeProperties: p,
                AsTrackable: false);
        }

        public async Task<IEnumerable<BgpPeer>> GetAllByRoutingInstanceIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return  await this.UnitOfWork.BgpPeerRepository.GetAsync(q => q.RoutingInstanceID == id, 
                includeProperties: p,
                AsTrackable:false);

        }

        public async Task<BgpPeer> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.BgpPeerRepository.GetAsync(q => q.BgpPeerID == id, 
                includeProperties: p,
                AsTrackable:false);

            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(BgpPeer bgpPeer)
        {
            this.UnitOfWork.BgpPeerRepository.Insert(bgpPeer);
            await UpdateRequiresSync(bgpPeer.RoutingInstanceID);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(BgpPeer bgpPeer)
        {
            this.UnitOfWork.BgpPeerRepository.Update(bgpPeer);
            await UpdateRequiresSync(bgpPeer.RoutingInstanceID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(BgpPeer bgpPeer)
        {
            this.UnitOfWork.BgpPeerRepository.Delete(bgpPeer);
            await UpdateRequiresSync(bgpPeer.RoutingInstanceID);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Helper to update 'RequiresSync' property of the Vif(s) or Attachment(s) which a routing instance 
        /// is associated with and the associated Device to indicate that sync to network is required.
        /// </summary>
        /// <param name="routingInstanceID"></param>
        /// <returns></returns>
        private async Task UpdateRequiresSync(int routingInstanceID)
        {
            var vifs = await UnitOfWork.VifRepository.GetAsync(q => q.RoutingInstance.RoutingInstanceID == routingInstanceID,
                includeProperties: "VifRole,Attachment.AttachmentRole,Attachment.Device.DeviceRole");

            foreach (var vif in vifs)
            {
                vif.RequiresSync = vif.VifRole.RequireSyncToNetwork;
                vif.ShowRequiresSyncAlert = vif.VifRole.RequireSyncToNetwork;
                UnitOfWork.VifRepository.Update(vif);
                vif.Attachment.RequiresSync = vif.Attachment.AttachmentRole.RequireSyncToNetwork;
                vif.Attachment.ShowRequiresSyncAlert = vif.Attachment.AttachmentRole.RequireSyncToNetwork;
                UnitOfWork.AttachmentRepository.Update(vif.Attachment);
                vif.Attachment.Device.RequiresSync = vif.Attachment.Device.DeviceRole.RequireSyncToNetwork;
                vif.Attachment.Device.ShowRequiresSyncAlert = vif.Attachment.Device.DeviceRole.RequireSyncToNetwork;
                UnitOfWork.DeviceRepository.Update(vif.Attachment.Device);
            }

            var attachments = await UnitOfWork.AttachmentRepository.GetAsync(q => q.RoutingInstanceID == routingInstanceID,
                    includeProperties: "AttachmentRole,Device.DeviceRole");

            foreach (var attachment in attachments)
            {
                attachment.RequiresSync = attachment.AttachmentRole.RequireSyncToNetwork;
                attachment.ShowRequiresSyncAlert = attachment.AttachmentRole.RequireSyncToNetwork;
                UnitOfWork.AttachmentRepository.Update(attachment);
                attachment.Device.RequiresSync = attachment.Device.DeviceRole.RequireSyncToNetwork;
                attachment.Device.ShowRequiresSyncAlert = attachment.Device.DeviceRole.RequireSyncToNetwork;
                UnitOfWork.DeviceRepository.Update(attachment.Device);
            }
        }
    }
}

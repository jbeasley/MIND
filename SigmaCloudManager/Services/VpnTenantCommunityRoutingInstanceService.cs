using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnTenantCommunityRoutingInstanceService : BaseService, IVpnTenantCommunityRoutingInstanceService
    {
        public VpnTenantCommunityRoutingInstanceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "AttachmentSet.Tenant,"
                + "TenantCommunity.Tenant,"
                + "TenantCommunitySet.Tenant,"
                + "RoutingInstance";

        /// <summary>
        /// Get all VPN Tenant Communities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantCommunityRoutingInstanceRepository.GetAsync(includeProperties:p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Communities for a given Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantCommunityRoutingInstanceRepository.GetAsync(q => q.AttachmentSetID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllByTenantCommunitySetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantCommunityRoutingInstanceRepository.GetAsync(q => q.TenantCommunitySetID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Communities for a given VPN and Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantCommunityRoutingInstanceRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == vpnID 
            && x.AttachmentSetID == attachmentSetID).Any(),
                includeProperties: p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get a single VPN Tenant Community.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantCommunityRoutingInstance> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantCommunityRoutingInstanceRepository.GetAsync(q => q.VpnTenantCommunityRoutingInstanceID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get a single VPN Tenant Community for a given Attachment Set.
        /// </summary>
        /// <returns></returns>
        public async Task<VpnTenantCommunityRoutingInstance> GetOneAsync(int attachmentSetID, int tenantCommunityID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantCommunityRoutingInstanceRepository.GetAsync(q => q.AttachmentSetID == attachmentSetID
            && q.TenantCommunityID == tenantCommunityID,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get all VPN Tenant Communities for a given Tenant Community.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantCommunityRoutingInstanceRepository.GetAsync(q => q.TenantCommunityID == id,
                includeProperties: p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Communities for a given VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllByVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantCommunityRoutingInstanceRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == id).Any(),
                includeProperties: p, 
                AsTrackable: false);
        }

        public async Task<int> AddAsync(VpnTenantCommunityRoutingInstance vpnTenantCommunityRoutingInstance)
        {
            UnitOfWork.VpnTenantCommunityRoutingInstanceRepository.Insert(vpnTenantCommunityRoutingInstance);
            await UpdateVpnSyncStateAsync(vpnTenantCommunityRoutingInstance.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(VpnTenantCommunityRoutingInstance vpnTenantCommunityRoutingInstance)
        {
            this.UnitOfWork.VpnTenantCommunityRoutingInstanceRepository.Update(vpnTenantCommunityRoutingInstance);
            await UpdateVpnSyncStateAsync(vpnTenantCommunityRoutingInstance.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTenantCommunityRoutingInstance vpnTenantCommunityRoutingInstance)
        {
            this.UnitOfWork.VpnTenantCommunityRoutingInstanceRepository.Delete(vpnTenantCommunityRoutingInstance);
            await UpdateVpnSyncStateAsync(vpnTenantCommunityRoutingInstance.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update the 'RequiresSync' property of the VPN associated with a given 
        /// AttachmentSet.
        /// </summary>
        /// <param name="attachmentSetID"></param>
        /// <returns></returns>
        private async Task UpdateVpnSyncStateAsync(int attachmentSetID)
        {
            var vpns = await UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                                         .Where(x => x.AttachmentSetID == attachmentSetID)
                                                                         .Any());
            foreach (var vpn in vpns)
            {
                vpn.RequiresSync = true;
                this.UnitOfWork.VpnRepository.Update(vpn);
            }
        }
    }
}
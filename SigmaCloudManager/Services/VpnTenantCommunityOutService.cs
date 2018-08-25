using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnTenantCommunityOutService : BaseService, IVpnTenantCommunityOutService
    {
        public VpnTenantCommunityOutService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "AttachmentSet.Tenant,"
                + "TenantCommunity.Tenant,"
                + "BgpPeer.RoutingInstance";

        /// <summary>
        /// Get all VPN Tenant Communities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityOut>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantCommunityOutRepository.GetAsync(includeProperties:p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Communities for a given Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityOut>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantCommunityOutRepository.GetAsync(q => q.AttachmentSetID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Communities for a given VPN and Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityOut>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantCommunityOutRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
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
        public async Task<VpnTenantCommunityOut> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantCommunityOutRepository.GetAsync(q => q.VpnTenantCommunityOutID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get a single VPN Tenant Community for a given Attachment Set
        /// </summary>
        /// <returns></returns>
        public async Task<VpnTenantCommunityOut> GetOneAsync(int attachmentSetID, int tenantCommunityID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantCommunityOutRepository.GetAsync(q => q.AttachmentSetID == attachmentSetID 
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
        public async Task<IEnumerable<VpnTenantCommunityOut>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantCommunityOutRepository.GetAsync(q => q.TenantCommunityID == id,
                includeProperties: p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Communities for a given VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityOut>> GetAllByVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantCommunityOutRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == id).Any(),
                includeProperties: p, 
                AsTrackable: false);
        }

        public async Task<int> AddAsync(VpnTenantCommunityOut vpnTenantCommunityOut)
        {
            UnitOfWork.VpnTenantCommunityOutRepository.Insert(vpnTenantCommunityOut);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(VpnTenantCommunityOut vpnTenantCommunityOut)
        {
            this.UnitOfWork.VpnTenantCommunityOutRepository.Update(vpnTenantCommunityOut);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTenantCommunityOut vpnTenantCommunityOut)
        {
            this.UnitOfWork.VpnTenantCommunityOutRepository.Delete(vpnTenantCommunityOut);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnTenantCommunityInService : BaseService, IVpnTenantCommunityInService
    {
        public VpnTenantCommunityInService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "AttachmentSet.Tenant,"
                + "AttachmentSet.VpnAttachmentSets.Vpn.ExtranetVpns.ExtranetVpn,"
                + "TenantCommunity.Tenant,"
                + "BgpPeer.RoutingInstance";

        /// <summary>
        /// Get all VPN Tenant Communities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityIn>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantCommunityInRepository.GetAsync(includeProperties:p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Communities for a given Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityIn>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantCommunityInRepository.GetAsync(q => q.AttachmentSetID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Communities for a given VPN and Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityIn>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantCommunityInRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.AttachmentSetID == attachmentSetID && x.VpnID == vpnID).Any(),
                includeProperties: p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get a single VPN Tenant Community.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantCommunityIn> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantCommunityInRepository.GetAsync(q => q.VpnTenantCommunityInID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get a single VPN Tenant Community for a given Attachment Set
        /// </summary>
        /// <returns></returns>
        public async Task<VpnTenantCommunityIn> GetOneAsync(int attachmentSetID, int tenantCommunityID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantCommunityInRepository.GetAsync(q => q.AttachmentSetID == attachmentSetID 
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
        public async Task<IEnumerable<VpnTenantCommunityIn>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantCommunityInRepository.GetAsync(q => q.TenantCommunityID == id,
                includeProperties: p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Communities for a given VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityIn>> GetAllByVpnIDAsync(int vpnID, int? tenantID = null, bool? extranet = null, 
            bool includeProperties = true)
        {
            var p = includeProperties ? Properties : "TenantCommunity";
            var query = from vpnTenantCommunities in await UnitOfWork.VpnTenantCommunityInRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == vpnID).Any(),
                includeProperties: p,
                AsTrackable: false)
            select vpnTenantCommunities;

            if (tenantID != null)
            {
                query = query.Where(x => x.TenantCommunity.TenantID == tenantID);
            }

            if (extranet != null)
            {
                query = query.Where(x => x.TenantCommunity.AllowExtranet == extranet);
            }

            return query.ToList();
        }

        public async Task<int> AddAsync(VpnTenantCommunityIn vpnTenantCommunityIn)
        {
            UnitOfWork.VpnTenantCommunityInRepository.Insert(vpnTenantCommunityIn);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(VpnTenantCommunityIn vpnTenantCommunityIn)
        {
            this.UnitOfWork.VpnTenantCommunityInRepository.Update(vpnTenantCommunityIn);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTenantCommunityIn vpnTenantCommunityIn)
        {
            this.UnitOfWork.VpnTenantCommunityInRepository.Delete(vpnTenantCommunityIn);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
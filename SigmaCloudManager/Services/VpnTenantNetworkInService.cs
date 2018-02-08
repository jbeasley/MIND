using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnTenantNetworkInService : BaseService, IVpnTenantNetworkInService
    {
        public VpnTenantNetworkInService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "AttachmentSet.Tenant,"
                + "AttachmentSet.VpnAttachmentSets.Vpn.ExtranetVpns.ExtranetVpn,"
                + "BgpPeer.RoutingInstance,"
                + "TenantNetwork.Tenant";

        /// <summary>
        /// Get all VPN Tenant IN Networks.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkIn>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkInRepository.GetAsync(includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkIn>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkInRepository.GetAsync(q => q.AttachmentSetID == id, 
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given VPN and Attachment Set.
        /// </summary>
        /// <param name="attachmentSetID"></param>
        /// <param name="vpnID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkIn>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkInRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == vpnID && q.AttachmentSetID == attachmentSetID).Any(),
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkIn>> GetAllByTenantNetworkIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantNetworkInRepository.GetAsync(q => q.TenantNetworkID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkIn>> GetAllByVpnIDAsync(int vpnID, int? tenantID = null, bool? extranet = null,
            bool includeProperties = true)
        {
            var p = includeProperties ? Properties : "TenantNetwork";
            var query = from vpnTenantNetworks in await UnitOfWork.VpnTenantNetworkInRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == vpnID).Any(),
                includeProperties: p,
                AsTrackable: false)
            select vpnTenantNetworks;

            if (tenantID != null)
            {
                query = query.Where(x => x.TenantNetwork.TenantID == tenantID);
            }

            if (extranet != null)
            {
                query = query.Where(x => x.TenantNetwork.AllowExtranet == extranet);
            }

            return query.ToList();
        }

        /// <summary>
        /// Get a single VPN Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantNetworkIn> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantNetworkInRepository.GetAsync(q => q.VpnTenantNetworkInID == id, 
                includeProperties: p,
                AsTrackable:false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get a single VPN Tenant Network for a given Attachment Set.
        /// </summary>
        /// <param name="attachmentSetID"></param>
        /// <param name="tenantNetworkID"></param>
        /// <returns></returns>
        public async Task<VpnTenantNetworkIn> GetOneAsync(int attachmentSetID, int tenantNetworkID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantNetworkInRepository.GetAsync(q => q.AttachmentSetID == attachmentSetID 
            && q.TenantNetworkID == tenantNetworkID,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(VpnTenantNetworkIn vpnTenantNetworkIn)
        {
            this.UnitOfWork.VpnTenantNetworkInRepository.Insert(vpnTenantNetworkIn);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkIn.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(VpnTenantNetworkIn vpnTenantNetworkIn)
        {
            this.UnitOfWork.VpnTenantNetworkInRepository.Update(vpnTenantNetworkIn);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkIn.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTenantNetworkIn vpnTenantNetworkIn)
        {
            this.UnitOfWork.VpnTenantNetworkInRepository.Delete(vpnTenantNetworkIn);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkIn.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update the 'RequiresSync' property of all VPNs associated with a given 
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
using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnTenantNetworkOutService : BaseService, IVpnTenantNetworkOutService
    {
        public VpnTenantNetworkOutService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "AttachmentSet.Tenant,"
                + "BgpPeer.RoutingInstance,"
                + "TenantNetwork.Tenant";

        /// <summary>
        /// Get all VPN Tenant Networks.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkOut>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkOutRepository.GetAsync(includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkOut>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkOutRepository.GetAsync(q => q.AttachmentSetID == id, 
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given VPN and Attachment Set.
        /// </summary>
        /// <param name="attachmentSetID"></param>
        /// <param name="vpnID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkOut>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkOutRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == vpnID && x.AttachmentSetID == attachmentSetID).Any(),
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkOut>> GetAllByTenantNetworkIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantNetworkOutRepository.GetAsync(q => q.TenantNetworkID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkOut>> GetAllByVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantNetworkOutRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == id).Any(),
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get a single VPN Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantNetworkOut> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantNetworkOutRepository.GetAsync(q => q.VpnTenantNetworkOutID == id, 
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
        public async Task<VpnTenantNetworkOut> GetOneAsync(int attachmentSetID, int tenantNetworkID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantNetworkOutRepository.GetAsync(q => q.AttachmentSetID == attachmentSetID 
            && q.TenantNetworkID == tenantNetworkID,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(VpnTenantNetworkOut vpnTenantNetwork)
        {
            this.UnitOfWork.VpnTenantNetworkOutRepository.Insert(vpnTenantNetwork);
            await UpdateVpnSyncStateAsync(vpnTenantNetwork.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(VpnTenantNetworkOut vpnTenantNetwork)
        {
            this.UnitOfWork.VpnTenantNetworkOutRepository.Update(vpnTenantNetwork);
            await UpdateVpnSyncStateAsync(vpnTenantNetwork.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTenantNetworkOut vpnTenantNetwork)
        {
            this.UnitOfWork.VpnTenantNetworkOutRepository.Delete(vpnTenantNetwork);
            await UpdateVpnSyncStateAsync(vpnTenantNetwork.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update the 'RequiresSync' property of all VPN associated with a given 
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
using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnTenantNetworkRoutingInstanceService : BaseService, IVpnTenantNetworkRoutingInstanceService
    {
        public VpnTenantNetworkRoutingInstanceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "AttachmentSet.Tenant,"
                + "TenantNetwork.Tenant,"
                + "RoutingInstance";

        /// <summary>
        /// Get all VPN Tenant Networks.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkRoutingInstance>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkRoutingInstanceRepository.GetAsync(includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkRoutingInstanceRepository.GetAsync(q => q.AttachmentSetID == id, 
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given VPN and Attachment Set.
        /// </summary>
        /// <param name="attachmentSetID"></param>
        /// <param name="vpnID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkRoutingInstance>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkRoutingInstanceRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == vpnID && x.AttachmentSetID == attachmentSetID).Any(),
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkRoutingInstance>> GetAllByTenantNetworkIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantNetworkRoutingInstanceRepository.GetAsync(q => q.TenantNetworkID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkRoutingInstance>> GetAllByVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantNetworkRoutingInstanceRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == id).Any(),
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get a single VPN Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantNetworkRoutingInstance> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantNetworkRoutingInstanceRepository.GetAsync(q => q.VpnTenantNetworkRoutingInstanceID == id, 
                includeProperties: p,
                AsTrackable:false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get a single VPN Tenant Network for a given Attachment Set
        /// </summary>
        /// <param name="attachmentSetID"></param>
        /// <param name="tenantNetworkID"></param>
        /// <returns></returns>
        public async Task<VpnTenantNetworkRoutingInstance> GetOneAsync(int attachmentSetID, int tenantNetworkID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantNetworkRoutingInstanceRepository.GetAsync(q => q.AttachmentSetID == attachmentSetID
            && q.TenantNetworkID == tenantNetworkID,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(VpnTenantNetworkRoutingInstance vpnTenantNetworkRoutingInstance)
        {
            this.UnitOfWork.VpnTenantNetworkRoutingInstanceRepository.Insert(vpnTenantNetworkRoutingInstance);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkRoutingInstance.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(VpnTenantNetworkRoutingInstance vpnTenantNetworkRoutingInstance)
        {
            this.UnitOfWork.VpnTenantNetworkRoutingInstanceRepository.Update(vpnTenantNetworkRoutingInstance);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkRoutingInstance.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTenantNetworkRoutingInstance vpnTenantNetworkRoutingInstance)
        {
            this.UnitOfWork.VpnTenantNetworkRoutingInstanceRepository.Delete(vpnTenantNetworkRoutingInstance);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkRoutingInstance.AttachmentSetID);
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
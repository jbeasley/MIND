using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnTenantNetworkStaticRouteRoutingInstanceService : BaseService, IVpnTenantNetworkStaticRouteRoutingInstanceService
    {
        public VpnTenantNetworkStaticRouteRoutingInstanceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "AttachmentSet.Tenant,"
                + "RoutingInstance,"
                + "TenantNetwork.Tenant";

        /// <summary>
        /// Get all VPN Tenant Network Static Routes.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkStaticRouteRoutingInstance>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkStaticRouteRoutingInstanceRepository.GetAsync(includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Network Static Routes for a given Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkStaticRouteRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkStaticRouteRoutingInstanceRepository.GetAsync(q => q.AttachmentSetID == id, 
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Network Static Routes for a given VPN and Attachment Set.
        /// </summary>
        /// <param name="attachmentSetID"></param>
        /// <param name="vpnID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkStaticRouteRoutingInstance>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkStaticRouteRoutingInstanceRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == vpnID && x.AttachmentSetID == attachmentSetID).Any(),
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Network Static Routes for a given Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkStaticRouteRoutingInstance>> GetAllByTenantNetworkIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantNetworkStaticRouteRoutingInstanceRepository.GetAsync(q => q.TenantNetworkID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Network Static Routes for a given VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkStaticRouteRoutingInstance>> GetAllByVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantNetworkStaticRouteRoutingInstanceRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == id).Any(),
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get a single VPN Tenant Network Static Route.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantNetworkStaticRouteRoutingInstance> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantNetworkStaticRouteRoutingInstanceRepository.GetAsync(q => q.VpnTenantNetworkStaticRouteRoutingInstanceID == id, 
                includeProperties: p,
                AsTrackable:false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get a single VPN Tenant Network Static Route for a given Attachment Set
        /// </summary>
        /// <param name="attachmentSetID"></param>
        /// <param name="tenantNetworkID"></param>
        /// <returns></returns>
        public async Task<VpnTenantNetworkStaticRouteRoutingInstance> GetOneAsync(int attachmentSetID, int tenantNetworkID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantNetworkStaticRouteRoutingInstanceRepository.GetAsync(q => q.AttachmentSetID == attachmentSetID
            && q.TenantNetworkID == tenantNetworkID,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(VpnTenantNetworkStaticRouteRoutingInstance vpnTenantNetworkStaticRouteRoutingInstance)
        {
            this.UnitOfWork.VpnTenantNetworkStaticRouteRoutingInstanceRepository.Insert(vpnTenantNetworkStaticRouteRoutingInstance);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(VpnTenantNetworkStaticRouteRoutingInstance vpnTenantNetworkStaticRouteRoutingInstance)
        {
            this.UnitOfWork.VpnTenantNetworkStaticRouteRoutingInstanceRepository.Update(vpnTenantNetworkStaticRouteRoutingInstance);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTenantNetworkStaticRouteRoutingInstance vpnTenantNetworkStaticRouteRoutingInstance)
        {
            this.UnitOfWork.VpnTenantNetworkStaticRouteRoutingInstanceRepository.Delete(vpnTenantNetworkStaticRouteRoutingInstance);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID);
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
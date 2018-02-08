using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnTenantNetworkCommunityInService : BaseService, IVpnTenantNetworkCommunityInService
    {
        public VpnTenantNetworkCommunityInService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "VpnTenantNetworkIn.TenantNetwork.Tenant,"
                + "VpnTenantNetworkIn.AttachmentSet.Tenant,"
                + "TenantCommunity";

        /// <summary>
        /// Get all VPN Tenant Network Community records.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkCommunityIn>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantNetworkCommunityInRepository.GetAsync(includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Network Communities for a given VPN Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkCommunityIn>> GetAllByVpnTenantNetworkInIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantNetworkCommunityInRepository.GetAsync(q => q.VpnTenantNetworkInID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Network Communities for a given Tenant Communities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantNetworkCommunityIn>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantNetworkCommunityInRepository.GetAsync(q => q.TenantCommunityID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get a single VPN Tenant Network Community
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantNetworkCommunityIn> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnTenantNetworkCommunityInRepository.GetAsync(q => q.VpnTenantNetworkCommunityInID == id, 
                includeProperties: p,
                AsTrackable:false);

            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(VpnTenantNetworkCommunityIn vpnTenantNetworkCommunityIn)
        {
            this.UnitOfWork.VpnTenantNetworkCommunityInRepository.Insert(vpnTenantNetworkCommunityIn);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkCommunityIn);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(VpnTenantNetworkCommunityIn vpnTenantNetworkCommunityIn)
        {
            this.UnitOfWork.VpnTenantNetworkCommunityInRepository.Update(vpnTenantNetworkCommunityIn);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkCommunityIn);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTenantNetworkCommunityIn vpnTenantNetworkCommunityIn)
        {
            this.UnitOfWork.VpnTenantNetworkCommunityInRepository.Delete(vpnTenantNetworkCommunityIn);
            await UpdateVpnSyncStateAsync(vpnTenantNetworkCommunityIn);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update the 'RequiresSync' property of all VPNs associated with a given 
        /// Vpn Tenant Network Community.
        /// </summary>
        /// <param name="vpnTenantNetworkCommunityIn"></param>
        /// <returns></returns>
        private async Task UpdateVpnSyncStateAsync(VpnTenantNetworkCommunityIn vpnTenantNetworkCommunityIn)
        {
            var vpns = await UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
            .Select(x => x.AttachmentSet)
            .SelectMany(x => x.VpnTenantNetworksIn)
            .Where(z => z.VpnTenantNetworkInID == vpnTenantNetworkCommunityIn.VpnTenantNetworkInID)
            .Any());

            foreach (var vpn in vpns)
            {
                vpn.RequiresSync = true;
                this.UnitOfWork.VpnRepository.Update(vpn);
            }
        }
    }
}
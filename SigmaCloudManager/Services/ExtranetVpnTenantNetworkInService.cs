using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class ExtranetVpnTenantNetworkInService : BaseService, IExtranetVpnTenantNetworkInService
    {
        public ExtranetVpnTenantNetworkInService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "ExtranetVpnMember.MemberVpn,"
            + "ExtranetVpnMember.ExtranetVpn,"
            + "VpnTenantNetworkIn.TenantNetwork.Tenant,"
            + "VpnTenantNetworkIn.AttachmentSet.Tenant";

        /// <summary>
        /// Get all Extrant VPN Tenant Communities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ExtranetVpnTenantNetworkIn>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.ExtranetVpnTenantNetworkInRepository.GetAsync(includeProperties:p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get a single Extranet VPN Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ExtranetVpnTenantNetworkIn> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.ExtranetVpnTenantNetworkInRepository.GetAsync(q => q.ExtranetVpnTenantNetworkInID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get all Extranet VPN Tenant Communities for a given Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ExtranetVpnTenantNetworkIn>> GetAllByTenantNetworkIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.ExtranetVpnTenantNetworkInRepository.GetAsync(q => q.VpnTenantIpNetworkIn.TenantIpNetworkID == id,
                includeProperties: p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get all Extranet VPN Tenant Communities for a given Extranet VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ExtranetVpnTenantNetworkIn>> GetAllByExtranetVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.ExtranetVpnTenantNetworkInRepository.GetAsync(q => q.ExtranetVpnMember.ExtranetVpn.VpnID == id,
                includeProperties: p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get all Extranet VPN Tenant Networks for a given Extranet Member VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ExtranetVpnTenantNetworkIn>> GetAllByExtranetVpnMemberIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.ExtranetVpnTenantNetworkInRepository.GetAsync(q => q.ExtranetVpnMemberID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<int> AddAsync(ExtranetVpnTenantNetworkIn extranetVpnTenantNetworkIn)
        {
            UnitOfWork.ExtranetVpnTenantNetworkInRepository.Insert(extranetVpnTenantNetworkIn);
            await UpdateVpnSyncStateAsync(extranetVpnTenantNetworkIn.ExtranetVpnTenantNetworkInID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(ExtranetVpnTenantNetworkIn extranetVpnTenantNetworkIn)
        {
            this.UnitOfWork.ExtranetVpnTenantNetworkInRepository.Update(extranetVpnTenantNetworkIn);
            await UpdateVpnSyncStateAsync(extranetVpnTenantNetworkIn.ExtranetVpnTenantNetworkInID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(ExtranetVpnTenantNetworkIn extranetVpnTenantNetworkIn)
        {
            this.UnitOfWork.ExtranetVpnTenantNetworkInRepository.Delete(extranetVpnTenantNetworkIn);
            await UpdateVpnSyncStateAsync(extranetVpnTenantNetworkIn.ExtranetVpnTenantNetworkInID);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update the 'RequiresSync' property of the Extranet VPN associated with a given 
        /// Extranet VPN Tenant Network.
        /// </summary>
        /// <param name="extranetVpnTenantNetworkID"></param>
        /// <returns></returns>
        private async Task UpdateVpnSyncStateAsync(int extranetVpnTenantNetworkID)
        {
            var extranetVpnResult = await UnitOfWork.VpnRepository.GetAsync(q => q.ExtranetVpnMembers
                                                                         .Where(x => x.ExtranetVpnTenantNetworksIn
                                                                         .Where(y => y.ExtranetVpnTenantNetworkInID == extranetVpnTenantNetworkID)
                                                                         .Any())
                                                                         .Any());

            var extranetVpn = extranetVpnResult.SingleOrDefault();
            if (extranetVpn != null)
            {
                extranetVpn.RequiresSync = true;
                this.UnitOfWork.VpnRepository.Update(extranetVpn);
            }
        }
    }
}
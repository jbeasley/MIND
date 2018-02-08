using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class TenantCommunitySetCommunityService : BaseService, ITenantCommunitySetCommunityService
    {
        public TenantCommunitySetCommunityService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "TenantCommunity,"
            + "TenantCommunitySet.Tenant";

        public async Task<IEnumerable<TenantCommunitySetCommunity>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.TenantCommunitySetCommunityRepository.GetAsync(includeProperties: p, AsTrackable: false);
        }

        public async Task<TenantCommunitySetCommunity> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.TenantCommunitySetCommunityRepository.GetAsync(q => q.TenantCommunitySetCommunityID == id,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<IEnumerable<TenantCommunitySetCommunity>> GetAllByTenantCommunitySetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.TenantCommunitySetCommunityRepository.GetAsync(q => q.TenantCommunitySetID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<int> AddAsync(TenantCommunitySetCommunity tenantCommunitySetCommunity)
        {
            this.UnitOfWork.TenantCommunitySetCommunityRepository.Insert(tenantCommunitySetCommunity);
            
            // Update the 'RequiresSync' property of all VPNs associated with the Tenant Community Set Community

            await UpdateVpnSyncStateAsync(tenantCommunitySetCommunity);

            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(TenantCommunitySetCommunity tenantCommunitySetCommunity)
        {
            this.UnitOfWork.TenantCommunitySetCommunityRepository.Delete(tenantCommunitySetCommunity);

            // Update the 'RequiresSync' property of all VPNs associated with the Tenant Community Set Community

            await UpdateVpnSyncStateAsync(tenantCommunitySetCommunity);

            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Helper to set the RequiresSync state of VPNs affected by an addtion or deletion of a 
        /// Tenant Community within a Tenant Community Set.
        /// </summary>
        /// <param name="tenantCommunitySetCommunity"></param>
        /// <returns></returns>
        private async Task UpdateVpnSyncStateAsync(TenantCommunitySetCommunity tenantCommunitySetCommunity)
        {
            var vpns = await UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantCommunitiesRoutingInstance)
                                                        .Where(x => x.TenantCommunitySetID == tenantCommunitySetCommunity.TenantCommunitySetID)
                                                        .Any());

            foreach (var vpn in vpns)
            {
                vpn.RequiresSync = true;
                vpn.ShowRequiresSyncAlert = true;
                this.UnitOfWork.VpnRepository.Update(vpn);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class TenantCommunitySetService : BaseService, ITenantCommunitySetService
    {
        public TenantCommunitySetService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "Tenant,RoutingPolicyMatchOption";
        
        public async Task<IEnumerable<TenantCommunitySet>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.TenantCommunitySetRepository.GetAsync(includeProperties: p, AsTrackable: false);
        }

        public async Task<IEnumerable<TenantCommunitySet>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.TenantCommunitySetRepository.GetAsync(q => q.TenantCommunitySetCommunities
                                                                  .Where(x => x.TenantCommunityID == id)
                                                                  .Any(),
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<TenantCommunitySet> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.TenantCommunitySetRepository.GetAsync(q => q.TenantCommunitySetID == id, 
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<TenantCommunitySet> GetByNameAsync(string name, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.TenantCommunitySetRepository.GetAsync(q => q.Name == name,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<IEnumerable<TenantCommunitySet>> GetAllByTenantIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.TenantCommunitySetRepository.GetAsync(q => q.TenantID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<int> AddAsync(TenantCommunitySet tenantCommunitySet)
        {
            this.UnitOfWork.TenantCommunitySetRepository.Insert(tenantCommunitySet);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(TenantCommunitySet tenantCommunitySet)
        {
            this.UnitOfWork.TenantCommunitySetRepository.Update(tenantCommunitySet);

            // Update the 'RequiresSync' property of all VPNs associated with the Tenant Community

            var vpns = await UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantCommunitiesRoutingInstance)
                                                        .Where(x => x.TenantCommunitySetID == tenantCommunitySet.TenantCommunitySetID)
                                                        .Any());

            foreach (var vpn in vpns)
            {
                vpn.RequiresSync = true;
                vpn.ShowRequiresSyncAlert = true;
                this.UnitOfWork.VpnRepository.Update(vpn);
            }

            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(TenantCommunitySet tenantCommunitySet)
        {
            this.UnitOfWork.TenantCommunitySetRepository.Delete(tenantCommunitySet);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}

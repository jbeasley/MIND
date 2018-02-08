using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class TenantCommunityService : BaseService, ITenantCommunityService
    {
        public TenantCommunityService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "Tenant,"
            + "VpnTenantCommunitiesIn.AttachmentSet,"
            + "VpnTenantCommunitiesOut.AttachmentSet";

        public async Task<IEnumerable<TenantCommunity>> GetAllAsync(string searchString = "", bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            if (string.IsNullOrEmpty(searchString))
            {
                return await this.UnitOfWork.TenantCommunityRepository.GetAsync(includeProperties: p,
                    AsTrackable: false);
            }
            else
            {
                return await this.UnitOfWork.TenantCommunityRepository.GetAsync(q => q.Name.Contains(searchString), 
                    includeProperties: p,
                    AsTrackable: false);
            }
        }

        public async Task<IEnumerable<TenantCommunity>> GetAllByTenantIDAsync(int id, string searchString = "", bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            if (string.IsNullOrEmpty(searchString))
            {

                return await this.UnitOfWork.TenantCommunityRepository.GetAsync(q => q.TenantID == id,
                    includeProperties: p,
                    AsTrackable: false);
            }
            else
            {
                return await this.UnitOfWork.TenantCommunityRepository.GetAsync(q => q.TenantID == id && q.Name.Contains(searchString),
                    includeProperties: p,
                    AsTrackable: false);
            }
        }

        public async Task<IEnumerable<TenantCommunity>> GetAllByTenantCommunitySetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.TenantCommunityRepository.GetAsync(q => q.TenantCommunitySets
                                                                  .Where(x => x.TenantCommunitySetID == id)
                                                                  .Any(),
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<TenantCommunity> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.TenantCommunityRepository.GetAsync(q => q.TenantCommunityID == id, 
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get a Tenant Community from the Autonomous System and Number constituent components
        /// of the Tenant Community.
        /// </summary>
        /// <param name="asNumber"></param>
        /// <param name="number"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<TenantCommunity> GetByCommunityAsync(int asNumber, int number, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.TenantCommunityRepository.GetAsync(q => q.AutonomousSystemNumber == asNumber && q.Number == number,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(TenantCommunity tenantCommunity)
        {
            this.UnitOfWork.TenantCommunityRepository.Insert(tenantCommunity);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(TenantCommunity tenantCommunity)
        {
            this.UnitOfWork.TenantCommunityRepository.Update(tenantCommunity);

            // Update the 'RequiresSync' property of all VPNs associated with the Tenant Community

            var vpns = await UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantCommunitiesIn)
                                                        .Select(y => y.TenantCommunity)
                                                        .Concat(q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantCommunitiesOut)
                                                        .Select(y => y.TenantCommunity)
                                                        .Concat(q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantCommunitiesRoutingInstance)
                                                        .Where(x => x.TenantCommunity != null)
                                                        .Select(y => y.TenantCommunity)
                                                        .Concat(q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantCommunitiesRoutingInstance)
                                                        .SelectMany(x => x.TenantCommunitySet.TenantCommunitySetCommunities)
                                                        .Where(x => x.TenantCommunitySet != null)
                                                        .Select(y => y.TenantCommunity)
                                                        .Concat(q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantNetworksIn)
                                                        .SelectMany(y => y.VpnTenantNetworkCommunitiesIn)
                                                        .Select(z => z.TenantCommunity)))))
                                                        .Where(z => z.TenantCommunityID == tenantCommunity.TenantCommunityID)
                                                        .GroupBy(x => x.TenantCommunityID)
                                                        .Select(group => group.First())
                                                        .Any());

            foreach (var vpn in vpns.GroupBy(x => x.VpnID).Select(group => group.First()))
            {
                vpn.RequiresSync = true;
                vpn.ShowRequiresSyncAlert = true;
                this.UnitOfWork.VpnRepository.Update(vpn);
            }

            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(TenantCommunity tenantCommunity)
        {
            this.UnitOfWork.TenantCommunityRepository.Delete(tenantCommunity);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class ExtranetVpnTenantCommunityInService : BaseService, IExtranetVpnTenantCommunityInService
    {
        public ExtranetVpnTenantCommunityInService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "ExtranetVpnMember.MemberVpn,"
            + "ExtranetVpnMember.ExtranetVpn,"
            + "VpnTenantCommunityIn.TenantCommunity.Tenant,"
            + "VpnTenantCommunityIn.AttachmentSet.Tenant";

        /// <summary>
        /// Get all Extrant VPN Tenant Communities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ExtranetVpnTenantCommunityIn>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.ExtranetVpnTenantCommunityInRepository.GetAsync(includeProperties:p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get a single Extranet VPN Tenant Community.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ExtranetVpnTenantCommunityIn> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.ExtranetVpnTenantCommunityInRepository.GetAsync(q => q.ExtranetVpnTenantCommunityInID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get all Extranet VPN Tenant Communities for a given Tenant Community.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ExtranetVpnTenantCommunityIn>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.ExtranetVpnTenantCommunityInRepository.GetAsync(q => q.VpnTenantCommunityIn.TenantCommunityID == id,
                includeProperties: p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get all Extranet VPN Tenant Communities for a given Extranet VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ExtranetVpnTenantCommunityIn>> GetAllByExtranetVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.ExtranetVpnTenantCommunityInRepository.GetAsync(q => q.ExtranetVpnMember.ExtranetVpn.VpnID == id,
                includeProperties: p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get all Extranet VPN Tenant Communities for a given Extranet Member VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ExtranetVpnTenantCommunityIn>> GetAllByExtranetVpnMemberIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.ExtranetVpnTenantCommunityInRepository.GetAsync(q => q.ExtranetVpnMemberID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<int> AddAsync(ExtranetVpnTenantCommunityIn extranetVpnTenantCommunityIn)
        {
            UnitOfWork.ExtranetVpnTenantCommunityInRepository.Insert(extranetVpnTenantCommunityIn);
            await UpdateVpnSyncStateAsync(extranetVpnTenantCommunityIn.ExtranetVpnTenantCommunityInID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(ExtranetVpnTenantCommunityIn extranetVpnTenantCommunityIn)
        {
            this.UnitOfWork.ExtranetVpnTenantCommunityInRepository.Update(extranetVpnTenantCommunityIn);
            await UpdateVpnSyncStateAsync(extranetVpnTenantCommunityIn.ExtranetVpnTenantCommunityInID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(ExtranetVpnTenantCommunityIn extranetVpnTenantCommunityIn)
        {
            this.UnitOfWork.ExtranetVpnTenantCommunityInRepository.Delete(extranetVpnTenantCommunityIn);
            await UpdateVpnSyncStateAsync(extranetVpnTenantCommunityIn.ExtranetVpnTenantCommunityInID);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update the 'RequiresSync' property of the Extranet VPN associated with a given 
        /// Extranet VPN Tenant Community.
        /// </summary>
        /// <param name="extranetVpnTenantCommunityID"></param>
        /// <returns></returns>
        private async Task UpdateVpnSyncStateAsync(int extranetVpnTenantCommunityID)
        {
            var extranetVpnResult = await UnitOfWork.VpnRepository.GetAsync(q => q.ExtranetVpnMembers
                                                                         .Where(x => x.ExtranetVpnTenantCommunitiesIn
                                                                         .Where(y => y.ExtranetVpnTenantCommunityInID == extranetVpnTenantCommunityID)
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
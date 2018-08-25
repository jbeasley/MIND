using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnTenantIpNetworkCommunityInService : BaseService, IVpnTenantIpNetworkCommunityInService
    {
        public VpnTenantIpNetworkCommunityInService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private readonly string _properties = "VpnTenantIpNetworkIn.TenantIpNetwork,"
                + "VpnTenantIpNetworkIn.AttachmentSet,"
                + "TenantCommunity";

        /// <summary>
        /// Get all VPN Tenant IP Network Communities for a given VPN Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkCommunityIn>> GetAllByVpnTenantIpNetworkInIDAsync(int id, bool? deep = false,bool asTrackable= false)
        {
            return await UnitOfWork.VpnTenantIpNetworkCommunityInRepository.GetAsync(q => q.VpnTenantIpNetworkInID == id,
                includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single VPN Tenant IP Network Community
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkCommunityIn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnTenantIpNetworkCommunityInRepository.GetAsync(q => q.VpnTenantIpNetworkCommunityInID == id,
                    includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<VpnTenantIpNetworkCommunityIn> AddAsync(VpnTenantIpNetworkCommunityIn VpnTenantIpNetworkCommunityIn)
        {
            this.UnitOfWork.VpnTenantIpNetworkCommunityInRepository.Insert(VpnTenantIpNetworkCommunityIn);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(VpnTenantIpNetworkCommunityIn.VpnTenantIpNetworkCommunityInID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkCommunityIn> UpdateAsync(VpnTenantIpNetworkCommunityIn VpnTenantIpNetworkCommunityIn)
        {
            this.UnitOfWork.VpnTenantIpNetworkCommunityInRepository.Update(VpnTenantIpNetworkCommunityIn);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(VpnTenantIpNetworkCommunityIn.VpnTenantIpNetworkCommunityInID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int VpnTenantIpNetworkCommunityInID)
        {
            await this.UnitOfWork.VpnTenantIpNetworkCommunityInRepository.DeleteAsync(VpnTenantIpNetworkCommunityInID);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
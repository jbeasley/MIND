using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using Mind.Builders;
using Microsoft.EntityFrameworkCore;

namespace SCM.Services
{
    public class TenantDomainCommunityOutboundPolicyService : BaseService, ITenantDomainCommunityOutboundPolicyService
    {
        private readonly ITenantDomainCommunityOutboundPolicyDirector _director;
        private readonly ITenantDomainCommunityOutboundPolicyUpdateDirector _updateDirector;

        public TenantDomainCommunityOutboundPolicyService(IUnitOfWork unitOfWork, ITenantDomainCommunityOutboundPolicyDirector director, 
            ITenantDomainCommunityOutboundPolicyUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        /// <summary>
        /// Get all tenant community outbound policies for a given device
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityOut>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantCommunityOutRepository.GetAsync(
                    q => 
                        q.BgpPeer.RoutingInstance.DeviceID == id,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantCommunity),
                        AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single tenant community outbound policy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<VpnTenantCommunityOut> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnTenantCommunityOutRepository.GetAsync(
                q => 
                    q.VpnTenantCommunityOutID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantCommunity),
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<VpnTenantCommunityOut> AddAsync(int deviceId, TenantDomainCommunityOutboundPolicyRequest request)
        {
            var vpnTenantCommunityOut = await _director.BuildAsync(deviceId, request);  
            this.UnitOfWork.VpnTenantCommunityOutRepository.Insert(vpnTenantCommunityOut);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantCommunityOut.VpnTenantCommunityOutID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantCommunityOut> UpdateAsync(int vpnTenantCommunityOutId, TenantDomainCommunityOutboundPolicyUpdate update)
        {
            await _updateDirector.UpdateAsync(vpnTenantCommunityOutId, update);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantCommunityOutId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantNetworkInId)
        {
            await this.UnitOfWork.VpnTenantCommunityOutRepository.DeleteAsync(vpnTenantNetworkInId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
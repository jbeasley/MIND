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
    public class TenantDomainCommunityInboundPolicyService : BaseService, ITenantDomainCommunityInboundPolicyService
    {
        private readonly ITenantDomainCommunityInboundPolicyDirector _director;
        private readonly ITenantDomainCommunityInboundPolicyUpdateDirector _updateDirector;

        public TenantDomainCommunityInboundPolicyService(IUnitOfWork unitOfWork, ITenantDomainCommunityInboundPolicyDirector director, 
            ITenantDomainCommunityInboundPolicyUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        /// <summary>
        /// Get all tenant community inbound policies for a given device
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityIn>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantCommunityInRepository.GetAsync(
                    q => 
                        q.BgpPeer.RoutingInstance.DeviceID == id,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantCommunity),
                        AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single tenant community inbound policy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<VpnTenantCommunityIn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnTenantCommunityInRepository.GetAsync(
                q => 
                    q.VpnTenantCommunityInID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantCommunity),
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<VpnTenantCommunityIn> AddAsync(int deviceId, TenantDomainCommunityInboundPolicyRequest request)
        {
            var vpnTenantCommunityIn = await _director.BuildAsync(deviceId, request);  
            this.UnitOfWork.VpnTenantCommunityInRepository.Insert(vpnTenantCommunityIn);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantCommunityIn.VpnTenantCommunityInID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantCommunityIn> UpdateAsync(int vpnTenantCommunityInId, TenantDomainCommunityInboundPolicyUpdate update)
        {
            await _updateDirector.UpdateAsync(vpnTenantCommunityInId, update);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantCommunityInId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantNetworkInId)
        {
            await this.UnitOfWork.VpnTenantCommunityInRepository.DeleteAsync(vpnTenantNetworkInId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
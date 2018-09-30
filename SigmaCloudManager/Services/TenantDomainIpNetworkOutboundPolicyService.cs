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
    public class TenantDomainIpNetworkOutboundPolicyService : BaseService, ITenantDomainIpNetworkOutboundPolicyService
    {
        private readonly ITenantDomainIpNetworkOutboundPolicyDirector _director;
        private readonly ITenantDomainIpNetworkOutboundPolicyUpdateDirector _updateDirector;

        public TenantDomainIpNetworkOutboundPolicyService(IUnitOfWork unitOfWork, ITenantDomainIpNetworkOutboundPolicyDirector director, 
            ITenantDomainIpNetworkOutboundPolicyUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        /// <summary>
        /// Get all tenant IP network outbound policies for a given device
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkOut>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantIpNetworkOutRepository.GetAsync(
                    q => 
                        q.BgpPeer.RoutingInstance.DeviceID == id,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                        AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single tenant IP network outbound policy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkOut> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnTenantIpNetworkOutRepository.GetAsync(
                q => 
                    q.VpnTenantIpNetworkOutID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<VpnTenantIpNetworkOut> AddAsync(int deviceId, TenantDomainIpNetworkOutboundPolicyRequest request)
        {
            var vpnTenantIpNetworkOut = await _director.BuildAsync(deviceId, request);  
            this.UnitOfWork.VpnTenantIpNetworkOutRepository.Insert(vpnTenantIpNetworkOut);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkOut.VpnTenantIpNetworkOutID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkOut> UpdateAsync(int vpnTenantIpNetworkOutId, TenantDomainIpNetworkOutboundPolicyUpdate update)
        {
            await _updateDirector.UpdateAsync(vpnTenantIpNetworkOutId, update);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkOutId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantNetworkInId)
        {
            await this.UnitOfWork.VpnTenantIpNetworkOutRepository.DeleteAsync(vpnTenantNetworkInId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
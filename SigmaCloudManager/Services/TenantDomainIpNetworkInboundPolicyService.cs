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
    public class TenantDomainIpNetworkInboundPolicyService : BaseService, ITenantDomainIpNetworkInboundPolicyService
    {
        private readonly ITenantDomainIpNetworkInboundPolicyDirector _director;
        private readonly ITenantDomainIpNetworkInboundPolicyUpdateDirector _updateDirector;

        public TenantDomainIpNetworkInboundPolicyService(IUnitOfWork unitOfWork, ITenantDomainIpNetworkInboundPolicyDirector director, 
            ITenantDomainIpNetworkInboundPolicyUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        /// <summary>
        /// Get all tenant IP network inbound policies for a given device
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkIn>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantIpNetworkInRepository.GetAsync(
                    q => 
                        q.BgpPeer.RoutingInstance.DeviceID == id,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                        AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single tenant IP network inbound policy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkIn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnTenantIpNetworkInRepository.GetAsync(
                q => 
                    q.VpnTenantIpNetworkInID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<VpnTenantIpNetworkIn> AddAsync(int deviceId, TenantDomainIpNetworkInboundPolicyRequest request)
        {
            var vpnTenantIpNetworkIn = await _director.BuildAsync(deviceId, request);  
            this.UnitOfWork.VpnTenantIpNetworkInRepository.Insert(vpnTenantIpNetworkIn);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkIn.VpnTenantIpNetworkInID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkIn> UpdateAsync(int vpnTenantIpNetworkInId, TenantDomainIpNetworkInboundPolicyUpdate update)
        {
            await _updateDirector.UpdateAsync(vpnTenantIpNetworkInId, update);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkInId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantNetworkInId)
        {
            await this.UnitOfWork.VpnTenantIpNetworkInRepository.DeleteAsync(vpnTenantNetworkInId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
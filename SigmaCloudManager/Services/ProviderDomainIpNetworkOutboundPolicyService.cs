using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using Mind.Builders;
using Mind.Models.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace SCM.Services
{
    public class ProviderDomainIpNetworkOutboundPolicyService : BaseService, IProviderDomainIpNetworkOutboundPolicyService
    {
        private readonly IProviderDomainIpNetworkOutboundPolicyDirector _director;
        private readonly IProviderDomainIpNetworkOutboundPolicyUpdateDirector _updateDirector;

        public ProviderDomainIpNetworkOutboundPolicyService(IUnitOfWork unitOfWork, IProviderDomainIpNetworkOutboundPolicyDirector director,
            IProviderDomainIpNetworkOutboundPolicyUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        /// <summary>
        /// Get all provider domain IP network outbound policies for a given attachment set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkOut>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantIpNetworkOutRepository.GetAsync(
                    q => 
                        q.AttachmentSetID == id,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                        AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get all provider domain IP network outbound policies for a given vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkOut>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await UnitOfWork.VpnTenantIpNetworkOutRepository.GetAsync(
                      q => 
                         q.AttachmentSet.VpnAttachmentSets
                         .Where(x => x.VpnID == id).Any(),
                         query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                         AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single provider domain IP network outbound policy.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
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

        public async Task<VpnTenantIpNetworkOut> AddAsync(int attachmentSetId, VpnTenantIpNetworkOutRequest request)
        {
            var vpnTenantIpNetworkOut = await _director.BuildAsync(attachmentSetId, request);
            this.UnitOfWork.VpnTenantIpNetworkOutRepository.Insert(vpnTenantIpNetworkOut);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkOut.VpnTenantIpNetworkOutID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkOut> UpdateAsync(int vpnTenantIpNetworkOutId, VpnTenantIpNetworkOutUpdate update)
        {
            await _updateDirector.UpdateAsync(vpnTenantIpNetworkOutId, update);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkOutId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantIpNetworkOutId)
        {
            await this.UnitOfWork.VpnTenantIpNetworkOutRepository.DeleteAsync(vpnTenantIpNetworkOutId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
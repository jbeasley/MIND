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
    public class ProviderDomainCommunityOutboundPolicyService : BaseService, IProviderDomainCommunityOutboundPolicyService
    {
        private readonly IProviderDomainCommunityOutboundPolicyDirector _director;
        private readonly IProviderDomainCommunityOutboundPolicyUpdateDirector _updateDirector;

        public ProviderDomainCommunityOutboundPolicyService(IUnitOfWork unitOfWork, IProviderDomainCommunityOutboundPolicyDirector director,
            IProviderDomainCommunityOutboundPolicyUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        /// <summary>
        /// Get all provider domain community outbound policies for a given attachment set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityOut>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantCommunityOutRepository.GetAsync(
                    q => 
                        q.AttachmentSetID == id,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantCommunity),
                        AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get all provider domain community outbound policies for a given vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityOut>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await UnitOfWork.VpnTenantCommunityOutRepository.GetAsync(
                      q => 
                         q.AttachmentSet.VpnAttachmentSets
                         .Where(x => x.VpnID == id).Any(),
                         query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantCommunity),
                         AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single provider domain community outbound policy.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
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

        public async Task<VpnTenantCommunityOut> AddAsync(int attachmentSetId, VpnTenantCommunityOutRequest request)
        {
            var vpnTenantCommunityOut = await _director.BuildAsync(attachmentSetId, request);
            this.UnitOfWork.VpnTenantCommunityOutRepository.Insert(vpnTenantCommunityOut);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantCommunityOut.VpnTenantCommunityOutID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantCommunityOut> UpdateAsync(int vpnTenantCommunityOutId, VpnTenantCommunityOutUpdate update)
        {
            await _updateDirector.UpdateAsync(vpnTenantCommunityOutId, update);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantCommunityOutId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantCommunityOutId)
        {
            await this.UnitOfWork.VpnTenantCommunityOutRepository.DeleteAsync(vpnTenantCommunityOutId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
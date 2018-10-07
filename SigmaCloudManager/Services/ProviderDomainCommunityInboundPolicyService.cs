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
    public class ProviderDomainCommunityInboundPolicyService : BaseService, IProviderDomainCommunityInboundPolicyService
    {
        private readonly IProviderDomainCommunityInboundPolicyDirector _director;
        private readonly IProviderDomainCommunityInboundPolicyUpdateDirector _updateDirector;

        public ProviderDomainCommunityInboundPolicyService(IUnitOfWork unitOfWork, IProviderDomainCommunityInboundPolicyDirector director, 
            IProviderDomainCommunityInboundPolicyUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        /// <summary>
        /// Get all community inbound policies for a given Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityIn>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantCommunityInRepository.GetAsync(
                    q => 
                        q.AttachmentSetID == id,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantCommunity),
                        AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get all community inbound policies for a given tenant which are associated with a given VPN 
        /// </summary>
        /// <param name="vpnId"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <param name="extranet"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantCommunityIn>> GetAllByVpnIDAsync(int vpnId, int? tenantId = null, bool extranet = false, bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.VpnTenantCommunityInRepository.GetAsync(
                  q =>
                        q.AttachmentSet.VpnAttachmentSets.Select(x =>
                        x.VpnID == vpnId).Any(),
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantCommunity),
                        AsTrackable: asTrackable)
                         select result);

            if (tenantId != null) query = query.Where(q => q.TenantCommunity.TenantID == tenantId);
            return query.ToList();
        }

        /// <summary>
        /// Get a single community inbound policy.
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

        public async Task<VpnTenantCommunityIn> AddAsync(int attachmentSetId, VpnTenantCommunityInRequest request)
        {
            var vpnTenantCommunityIn = await _director.BuildAsync(attachmentSetId, request);  
            this.UnitOfWork.VpnTenantCommunityInRepository.Insert(vpnTenantCommunityIn);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantCommunityIn.VpnTenantCommunityInID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantCommunityIn> UpdateAsync(int vpnTenantCommunityInId, VpnTenantCommunityInUpdate update)
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
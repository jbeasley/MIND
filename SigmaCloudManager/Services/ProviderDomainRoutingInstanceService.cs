using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SCM.Models.RequestModels;
using AutoMapper;

namespace SCM.Services
{
    public class ProviderDomainRoutingInstanceService : BaseService, IProviderDomainRoutingInstanceService
    {
        public ProviderDomainRoutingInstanceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<RoutingInstance> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.RoutingInstanceRepository.GetAsync(
                q =>
                    q.RoutingInstanceID == id &&
                    q.RoutingInstanceType.IsTenantFacingVrf,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<IEnumerable<RoutingInstance>> GetAllByAttachmentSetIDAsync(int attachmentSetId, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.RoutingInstanceRepository.GetAsync(
                q =>
                    q.AttachmentSetRoutingInstances
                    .Where(
                        x => 
                        x.AttachmentSetID == attachmentSetId)
                    .Any() &&
                    q.RoutingInstanceType.IsTenantFacingVrf,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: false)
                    select result)
                    .ToList();
        }

        public async Task<IEnumerable<RoutingInstance>> GetAllByTenantIDAsync(int tenantId, bool? deep = false, bool asTrackable = false, 
            string providerDomainLocationName = "")
        {
            var query = (from result in await UnitOfWork.RoutingInstanceRepository.GetAsync(
                   q =>
                       q.TenantID == tenantId &&
                       q.RoutingInstanceType.IsTenantFacingVrf,
                       query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                       AsTrackable: false)
                         select result);

            if (!string.IsNullOrEmpty(providerDomainLocationName)) query = query.Where(x => x.Device.Location.SiteName == providerDomainLocationName);

            return query.ToList();
        }
    }
}
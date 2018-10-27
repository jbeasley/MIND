using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mind.Services;
using SCM.Services;
using Mind.Models.RequestModels;
using Mind.Builders;

namespace Mind.Services
{
    public class AttachmentSetRoutingInstanceService : BaseService, IAttachmentSetRoutingInstanceService
    {
        private readonly IAttachmentSetRoutingInstanceDirector _director;

        public AttachmentSetRoutingInstanceService(IUnitOfWork unitOfWork, IAttachmentSetRoutingInstanceDirector director) : base(unitOfWork)
        {
            _director = director;
        }

        public async Task<IEnumerable<AttachmentSetRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(
                q =>
                    q.AttachmentSetID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable);
        }

        public async Task<AttachmentSetRoutingInstance> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(
                q => 
                    q.AttachmentSetRoutingInstanceID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select result)
                   .SingleOrDefault();
        }

        public async Task<AttachmentSetRoutingInstance> GetByAttachmentSetIDAndRoutingInstanceIDAsync(int attachmentSetId, int routingInstanceId,
            bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(
                q => 
                    q.AttachmentSetID == attachmentSetId
                    && q.RoutingInstanceID == routingInstanceId,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select result)
                   .SingleOrDefault();
        }

        public async Task<AttachmentSetRoutingInstance> AddAsync(int attachmentSetId, RoutingInstanceForAttachmentSetRequest request)
        {
            var attachmentSetRoutingInstance = await _director.BuildAsync(attachmentSetId, request);
            UnitOfWork.AttachmentSetRoutingInstanceRepository.Insert(attachmentSetRoutingInstance);
            await UnitOfWork.SaveAsync();

            return await GetByIDAsync(attachmentSetRoutingInstance.AttachmentSetRoutingInstanceID, deep: true, asTrackable: false);
        }

        public async Task<AttachmentSetRoutingInstance> UpdateAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance)
        {
            this.UnitOfWork.AttachmentSetRoutingInstanceRepository.Update(attachmentSetRoutingInstance);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(attachmentSetRoutingInstance.AttachmentSetRoutingInstanceID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int attachmentSetId, int routingInstanceId)
        {
            var attachmentSetRoutingInstance = (from result in await UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(
                                             q =>
                                                q.AttachmentSetID == attachmentSetId && 
                                                q.RoutingInstanceID == routingInstanceId,
                                                query: q => q.IncludeDeleteValidationProperties(),
                                                AsTrackable: true)
                                                select result)
                                                .Single();

            attachmentSetRoutingInstance.ValidateDelete();
            this.UnitOfWork.AttachmentSetRoutingInstanceRepository.Delete(attachmentSetRoutingInstance);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
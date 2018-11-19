using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mind.Services;
using Mind.Models.RequestModels;
using Mind.Builders;
using AutoMapper;

namespace SCM.Services
{
    public class AttachmentSetService : BaseService, IAttachmentSetService
    {
        private readonly IAttachmentSetDirector _director;
        private readonly IAttachmentSetUpdateDirector _updateDirector;

        public AttachmentSetService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IAttachmentSetDirector director,
            IAttachmentSetUpdateDirector updateDirector) : base(unitOfWork, mapper)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        public async Task<IEnumerable<AttachmentSet>> GetAllByTenantIDAsync(int tenantId, bool? deep = false, bool asTrackable = false)
        {
            return (from attachmentSets in await this.UnitOfWork.AttachmentSetRepository.GetAsync(q =>
                    q.TenantID == tenantId,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select attachmentSets)
                    .ToList();
        }

        public async Task<AttachmentSet> GetByIDAsync(int id, bool? deep = false, bool asTrackable = true)
        {
            return (from attachmentSets in await UnitOfWork.AttachmentSetRepository.GetAsync(q => q.AttachmentSetID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select attachmentSets)
                    .SingleOrDefault();
        }

        public async Task<AttachmentSet> AddAsync(int tenantId, AttachmentSetRequest request)
        {
            var attachmentSet = await _director.BuildAsync(tenantId, request);
            UnitOfWork.AttachmentSetRepository.Insert(attachmentSet);
            await UnitOfWork.SaveAsync();

            return await GetByIDAsync(attachmentSet.AttachmentSetID, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Obsolete method - REMOVE
        /// </summary>
        /// <param name="attachmentSet"></param>
        /// <returns></returns>
        public async Task<AttachmentSet> AddAsync(AttachmentSet attachmentSet)
        {
            attachmentSet.Name = Guid.NewGuid().ToString("N");
            this.UnitOfWork.AttachmentSetRepository.Insert(attachmentSet);
            await this.UnitOfWork.SaveAsync();

            return attachmentSet;
        }

        /// <summary>
        /// Obsolete method - REMOVE
        /// </summary>
        /// <param name="attachmentSet"></param>
        /// <returns></returns>
        public async Task<AttachmentSet> UpdateAsync(AttachmentSet attachmentSet)
        {
            this.UnitOfWork.AttachmentSetRepository.Update(attachmentSet);
            await this.UnitOfWork.SaveAsync();

            return attachmentSet;
        }

        public async Task<AttachmentSet> UpdateAsync(int attachmentSetId, AttachmentSetUpdate update)
        {
            // Perform updates to the attachment set and save
            await _updateDirector.UpdateAsync(attachmentSetId, update);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(attachmentSetId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int attachmentSetId)
        {
            var attachmentSet = (from result in await UnitOfWork.AttachmentSetRepository.GetAsync(
                              q =>
                                q.AttachmentSetID == attachmentSetId,
                                query: q => q.IncludeDeleteValidationProperties(),
                                AsTrackable: true)
                                select result)
                                .Single();

            attachmentSet.ValidateDelete();
            this.UnitOfWork.AttachmentSetRepository.Delete(attachmentSet);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
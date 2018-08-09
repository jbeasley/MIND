using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Factories;
using SCM.Validators;
using Mind.Builders;

namespace SCM.Services
{
    /// <summary>
    /// Service logic for Provider Domain Attachments
    /// </summary>
    public class ProviderDomainAttachmentService : BaseAttachmentService, IProviderDomainAttachmentService
    {
        protected readonly IProviderDomainAttachmentDirector _director;


        public ProviderDomainAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IAttachmentValidator validator,
            IProviderDomainAttachmentDirector director) : base(unitOfWork, mapper, validator)
        {
            _director = director;
        }

        /// <summary>
        /// Create a new Provider Domain Attachment
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Attachment> AddAsync(int tenantId, ProviderDomainAttachmentRequest request)
        { 
            var attachment = await _director.BuildAsync(tenantId, request);
            UnitOfWork.AttachmentRepository.Insert(attachment);
            await UnitOfWork.SaveAsync();

            return await base.GetByIDAsync(attachment.AttachmentID, deep: true, asTrackable: false);
        }
    }
}
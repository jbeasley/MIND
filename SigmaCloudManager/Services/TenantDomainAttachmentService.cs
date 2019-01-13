using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Services;
using Mind.Builders;
using Mind.Models;

namespace Mind.Services
{
    /// <summary>
    /// Service logic for tenant domain attachments
    /// </summary>
    public class TenantDomainAttachmentService : BaseAttachmentService, ITenantDomainAttachmentService
    {
        private readonly Func<TenantDomainAttachmentRequest, AttachmentRole, ITenantDomainAttachmentDirector> _createDirectorFactory;
        private readonly Func<Attachment, ITenantDomainAttachmentDirector> _attachmentDirectorFactory;

        public TenantDomainAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            Func<TenantDomainAttachmentRequest, AttachmentRole, ITenantDomainAttachmentDirector> directorFactory,
            Func<Attachment, ITenantDomainAttachmentDirector> attachmentDirectorFactory) : base(unitOfWork, mapper)
        {
            _createDirectorFactory = directorFactory;
            _attachmentDirectorFactory = attachmentDirectorFactory;
        }

        /// <summary>
        /// Get a tenant domain attachment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<Attachment> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from attachments in await UnitOfWork.AttachmentRepository.GetAsync(
                 q =>
                     q.AttachmentID == id &&
                     q.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.ProviderFacing ||
                     q.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.TenantInfrastructure,
                     query: q => deep.GetValueOrDefault() ? q.IncludeDeepProperties() : q,
                     AsTrackable: asTrackable)
                    select attachments)
                     .SingleOrDefault();
        }

        /// <summary>
        /// Get all tenant domain attachments for a given device
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<List<Attachment>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from attachments in await UnitOfWork.AttachmentRepository.GetAsync(
                q =>
                    q.DeviceID == id &&
                     q.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.ProviderFacing ||
                     q.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.TenantInfrastructure,
                    query: q => deep.GetValueOrDefault() ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select attachments)
                    .ToList();
        }

        /// <summary>
        /// Create a new tenant domain attachment
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Attachment> AddAsync(int deviceId, TenantDomainAttachmentRequest request)
        {
            var attachmentRole = (from result in await UnitOfWork.AttachmentRoleRepository.GetAsync(
                                x =>
                                  x.Name == request.AttachmentRoleName)
                                  select result)
                                  .SingleOrDefault();

            if (attachmentRole == null) throw new ServiceBadArgumentsException($"Could not find attachment role with name '{request.AttachmentRoleName}'."); 
            var director = _createDirectorFactory(request, attachmentRole);
            var attachment = await director.BuildAsync(deviceId, request);
            UnitOfWork.AttachmentRepository.Insert(attachment);
            await UnitOfWork.SaveAsync();

            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.TenantInfrastructure, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Update an existing tenant domain attachment
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Attachment> UpdateAsync(int attachmentId, TenantDomainAttachmentUpdate update)
        {
            // Get the current attachment as a non-tracked entity.
            var attachment = await UnitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);

            var director = _attachmentDirectorFactory(attachment);
            var updatedAttachment = await director.UpdateAsync(attachment, update);

            await UnitOfWork.SaveAsync();
            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.TenantInfrastructure, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Delete a tenant domain attachment
        /// </summary>
        /// <param name="attachmentId"></param>
        public async Task DeleteAsync(int attachmentId)
        {
            var attachment = await UnitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);
            var director = _attachmentDirectorFactory(attachment);

            // Destroy the attachment
            await director.DestroyAsync(attachment);

            await UnitOfWork.SaveAsync();
        }
    }
}
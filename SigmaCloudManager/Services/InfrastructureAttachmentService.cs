using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
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
    /// Service logic for infrastructure attachments
    /// </summary>
    public class InfrastructureAttachmentService : BaseAttachmentService, IInfrastructureAttachmentService
    {
        private readonly Func<InfrastructureAttachmentRequest, AttachmentRole, IInfrastructureAttachmentDirector> _createDirectorFactory;
        private readonly Func<Attachment, IInfrastructureAttachmentDirector> _attachmentDirectorFactory;

        public InfrastructureAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            Func<InfrastructureAttachmentRequest, AttachmentRole, IInfrastructureAttachmentDirector> directorFactory,
            Func<Attachment, IInfrastructureAttachmentDirector> attachmentDirectorFactory) : base(unitOfWork, mapper)
        {
            _createDirectorFactory = directorFactory;
            _attachmentDirectorFactory = attachmentDirectorFactory;
        }

        /// <summary>
        /// Get a infrastructure attachment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<Attachment> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await base.GetByIDAsync(id, Mind.Models.PortRoleTypeEnum.ProviderInfrastructure, deep, asTrackable);
        }

        /// <summary>
        /// Get all infrastructure attachments for a given device
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<List<Attachment>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await base.GetAllByDeviceIDAsync(id, Mind.Models.PortRoleTypeEnum.ProviderInfrastructure, deep, asTrackable);
        }

        /// <summary>
        /// Create a new infrastructure attachment
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Attachment> AddAsync(int deviceId, InfrastructureAttachmentRequest request)
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

            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.ProviderInfrastructure, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Update an existing infrastructure attachment
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Attachment> UpdateAsync(int attachmentId, InfrastructureAttachmentUpdate update)
        {
            // Get the current attachment as a non-tracked entity.
            var attachment = await UnitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);

            var director = _attachmentDirectorFactory(attachment);
            var updatedAttachment = await director.UpdateAsync(attachment, update);

            await UnitOfWork.SaveAsync();
            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.ProviderInfrastructure, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Delete an infrastructure attachment
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
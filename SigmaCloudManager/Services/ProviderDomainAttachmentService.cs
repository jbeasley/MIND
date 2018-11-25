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
using IO.Swagger.Api;
using Mind.Directors;

namespace Mind.Services
{
    /// <summary>
    /// Service logic for provider domain attachments
    /// </summary>
    public class ProviderDomainAttachmentService : BaseAttachmentService, IProviderDomainAttachmentService
    {
        private readonly Func<ProviderDomainAttachmentRequest, AttachmentRole, IProviderDomainAttachmentDirector> _createAttachmentDirectorFactory;
        private readonly Func<Attachment, IProviderDomainAttachmentDirector> _attachmentDirectorFactory;
        private readonly Func<Attachment, INetworkSynchronizable<Attachment>> _networkSyncAttachmentDirectorFactory;
        private readonly IDataApi _novaApiClient;

        public ProviderDomainAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            Func<ProviderDomainAttachmentRequest, AttachmentRole, IProviderDomainAttachmentDirector> createAttachmentDirectorFactory,
            Func<Attachment, IProviderDomainAttachmentDirector> attachmentDirectorFactory,
            Func<Attachment, INetworkSynchronizable<Attachment>> networkSyncAttachmentDirectorFactory,
            IDataApi novaApiClient) : base(unitOfWork, mapper)
        {
            _createAttachmentDirectorFactory = createAttachmentDirectorFactory;
            _attachmentDirectorFactory = attachmentDirectorFactory;
            _networkSyncAttachmentDirectorFactory = networkSyncAttachmentDirectorFactory;

            _novaApiClient = novaApiClient;
        }

        /// <summary>
        /// Get a provider domain attachment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<Attachment> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await base.GetByIDAsync(id, SCM.Models.PortRoleTypeEnum.TenantFacing, deep, asTrackable);
        }

        /// <summary>
        /// Get all provider domain attachments for a given tenant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<List<Attachment>> GetAllByTenantIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await base.GetAllByTenantIDAsync(id, SCM.Models.PortRoleTypeEnum.TenantFacing, deep, asTrackable);
        }

        /// <summary>
        /// Create a new provider domain attachment
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Attachment> AddAsync(int tenantId, ProviderDomainAttachmentRequest request)
        {
            var attachmentRole = (from result in await UnitOfWork.AttachmentRoleRepository.GetAsync(
                                x =>
                                  x.Name == request.AttachmentRoleName)
                                  select result)
                                  .SingleOrDefault();

            if (attachmentRole == null) throw new ServiceBadArgumentsException($"Could not find attachment role with name '{request.AttachmentRoleName}'.");

            var director = _createAttachmentDirectorFactory(request, attachmentRole);

            // Create the attachment, and add to the network for non-bundle, non-multiport tagged attachments only
            var attachment = await director.BuildAsync(tenantId, request, attachmentRole.IsTaggedRole && 
                                                       !request.BundleRequired.GetValueOrDefault() && 
                                                       !request.MultiportRequired.GetValueOrDefault());

            UnitOfWork.AttachmentRepository.Insert(attachment);

            // Save changes to the db
            await UnitOfWork.SaveAsync();
            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.TenantFacing, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Update an existing provider domain attachment
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Attachment> UpdateAsync(int attachmentId, ProviderDomainAttachmentUpdate update)
        {
            // Get the current attachment as a non-tracked entity.
            var attachment = await UnitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);
            var director = _attachmentDirectorFactory(attachment);

            // Update the attachment, and update the network if the attachment is a non-bundle, non-multiport tagged attachment
            var updatedAttachment = await director.UpdateAsync(attachment, update, attachment.IsTagged && !attachment.IsBundle && !attachment.IsMultiPort);

            await UnitOfWork.SaveAsync();
            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.TenantFacing, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Delete a provider domain attachment
        /// </summary>
        /// <param name="attachmentId"></param>
        public async Task DeleteAsync(int attachmentId)
        {
            var attachment = await UnitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);
            var director = _attachmentDirectorFactory(attachment);

            // Destroy the attachment
            await director.DestroyAsync(attachment, cleanUpNetwork: !attachment.IsBundle && !attachment.IsMultiPort);
            await UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Sync an attachment to the network
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="attachmentId">The ID of the attachment</param>
        public async Task SyncToNetworkAsync(int attachmentId)
        {
            var attachment = await UnitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);
            var director = _networkSyncAttachmentDirectorFactory(attachment);
            if (director != null) await director.SyncToNetworkAsync(attachment);
        }
    }
}
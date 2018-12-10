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
using Mind.Directors;
using IO.NovaAttSwagger.Client;

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

        public ProviderDomainAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            Func<ProviderDomainAttachmentRequest, AttachmentRole, IProviderDomainAttachmentDirector> createAttachmentDirectorFactory,
            Func<Attachment, IProviderDomainAttachmentDirector> attachmentDirectorFactory,
            Func<Attachment, INetworkSynchronizable<Attachment>> networkSyncAttachmentDirectorFactory) : base(unitOfWork, mapper)
        {
            _createAttachmentDirectorFactory = createAttachmentDirectorFactory;
            _attachmentDirectorFactory = attachmentDirectorFactory;
            _networkSyncAttachmentDirectorFactory = networkSyncAttachmentDirectorFactory;
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
        /// <param name="syncToNetwork"></param>
        /// <returns></returns>
        public async Task<Attachment> AddAsync(int tenantId, ProviderDomainAttachmentRequest request, bool syncToNetwork = false)
        {
            var attachmentRole = (from result in await UnitOfWork.AttachmentRoleRepository.GetAsync(
                                x =>
                                  x.Name == request.AttachmentRoleName)
                                  select result)
                                  .SingleOrDefault();

            if (attachmentRole == null)
            {
                throw new ServiceBadArgumentsException($"Could not find attachment role with name '{request.AttachmentRoleName}'.");
            }

            // Check sync to network is allowed
            CheckAllowNetworkSync(attachmentRole.IsTaggedRole, 
                        request.BundleRequired.GetValueOrDefault(), request.MultiportRequired.GetValueOrDefault(), syncToNetwork);

            var director = _createAttachmentDirectorFactory(request, attachmentRole);
            var attachment = await director.BuildAsync(tenantId, request, syncToNetwork);

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
        /// <param name="syncToNetwork"></param>
        /// <returns></returns>
        public async Task<Attachment> UpdateAsync(int attachmentId, ProviderDomainAttachmentUpdate update, bool syncToNetwork = false)
        {
            // Get the current attachment as a non-tracked entity.
            var attachment = await UnitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);

            // Check sync to network is allowed
            CheckAllowNetworkSync(attachment.IsTagged, attachment.IsBundle, attachment.IsMultiPort, syncToNetwork);

            var director = _attachmentDirectorFactory(attachment);
            var updatedAttachment = await director.UpdateAsync(attachment, update, syncToNetwork);

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
        /// Sync an attachment to the network with a put operation.
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="attachmentId">The ID of the attachment</param>
        public async Task SyncToNetworkPutAsync(int attachmentId)
        {
            var attachment = await UnitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);
            var director = _networkSyncAttachmentDirectorFactory(attachment);

            try
            {
                await director.SyncToNetworkPutAsync(attachment);
            }

            catch (ApiException)
            {
                // Rethrow the exception to be caught further up the stack
                throw;
            }

            finally
            {
                // Save network status change for the attachment
                await UnitOfWork.SaveAsync();
            }
        }

        /// <summary>
        /// Checks the allow stage and network sync.
        /// </summary>
        /// <param name="tagged">If set to <c>true</c> tagged.</param>
        /// <param name="bundle">If set to <c>true</c> bundle.</param>
        /// <param name="multiport">If set to <c>true</c> multiport.</param>
        /// <param name="syncToNetwork">If set to <c>true</c> sync to network.</param>
        private void CheckAllowNetworkSync(bool tagged, bool bundle, bool multiport, bool syncToNetwork)
        {
            var allowNetworkSync = tagged && !bundle && !multiport;                                                     
            if (syncToNetwork && !allowNetworkSync)
            {
                throw new ServiceBadArgumentsException($"The attachment cannot be synchronised with the network. Currently only tagged attachments " +
                    "which are not bundles or multiports support network sync.");
            }
        }
    }
}
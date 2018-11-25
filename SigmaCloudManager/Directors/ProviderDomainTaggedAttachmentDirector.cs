using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Directors;

namespace Mind.Builders
{
    public class ProviderDomainTaggedAttachmentDirector<TAttachmentBuilder> : IProviderDomainAttachmentDirector, INetworkSynchronizable<Attachment>
        where TAttachmentBuilder: AttachmentBuilder<TAttachmentBuilder>
    {
        private readonly Func<ProviderDomainAttachmentRequest, IAttachmentBuilder<TAttachmentBuilder>> _builderRequestFactory;
        private readonly Func<Attachment, IAttachmentBuilder<TAttachmentBuilder>> _builderAttachmentFactory;

        public ProviderDomainTaggedAttachmentDirector(Func<ProviderDomainAttachmentRequest, IAttachmentBuilder<TAttachmentBuilder>> builderRequestFactory,
                                                      Func<Attachment, IAttachmentBuilder<TAttachmentBuilder>> builderAttachmentFactory)
        {
            _builderRequestFactory = builderRequestFactory;
            _builderAttachmentFactory = builderAttachmentFactory;
        }

        /// <summary>
        /// Create a new attachment
        /// </summary>
        /// <returns>An instance of Attachment</returns>
        /// <param name="tenantId">The ID of the tenant for which the attachment will be created</param>
        /// <param name="request">Request object containing parameters to create the new attachment</param>
        /// <param name="syncToNetwork">If set to <c>true</c> sync the attachment with the network.</param>
        public async Task<SCM.Models.Attachment> BuildAsync(int tenantId, ProviderDomainAttachmentRequest request, bool syncToNetwork = true)
        {
            var builder = _builderRequestFactory(request);
            return await builder.ForTenant(tenantId)
                                .WithAttachmentRole(request.AttachmentRoleName)
                                .WithPortPool(request.PortPoolName)
                                .WithAttachmentBandwidth(request.AttachmentBandwidthGbps)
                                .WithLocation(request.LocationName)
                                .WithPlane(request.PlaneName.ToString())
                                .WithDescription(request.Description)
                                .WithNotes(request.Notes)
                                .SyncToNetwork(syncToNetwork)
                                .BuildAsync();
        }

        /// <summary>
        /// Updates an attachment
        /// </summary>
        /// <returns>An instance of Attachment for the updated attachment</returns>
        /// <param name="attachment">The attachment to update</param>
        /// <param name="update">Update object containing parameters to perform the update</param>
        /// <param name="syncToNetwork">If set to <c>true</c> add the updated attachment to the network.</param>
        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, ProviderDomainAttachmentUpdate update, bool syncToNetwork = true)
        {
            var builder = _builderAttachmentFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .WithJumboMtu(update.UseJumboMtu)
                                .WithTrustReceivedCosAndDscp(update.TrustReceivedCosAndDscp)
                                .WithDescription(update.Description)
                                .WithNotes(update.Notes)
                                .SyncToNetwork(syncToNetwork)
                                .BuildAsync();
        }

        /// <summary>
        /// Destroy an attachment
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="attachment">Attachment.</param>
        /// <param name="cleanUpNetwork">If set to <c>true</c> clean up network.</param>   
        public async Task DestroyAsync(Attachment attachment, bool cleanUpNetwork = true)
        {
            var builder = _builderAttachmentFactory(attachment);
            await builder.ForAttachment(attachment.AttachmentID)
                         .CleanUpNetwork(cleanUpNetwork)
                         .ReleasePorts()
                         .DestroyAsync();
        }

        /// <summary>
        /// Destroy a collection of attachments.
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="attachments">Attachments to destroy</param>
        /// <param name="cleanUpNetwork">If set to <c>true</c> clean up network.</param>
        public async Task DestroyAsync(List<Attachment> attachments, bool cleanUpNetwork = true)
        {
            var tasks = attachments.Select(
                async attachment => 
                    await DestroyAsync(attachment, cleanUpNetwork));

            await Task.WhenAll(tasks);
        }

        ///<summary>
        /// Sync an attachment to the network
        /// </summary>
        /// <returns>An instance of Attachment for the updated attachment</returns>
        /// <param name="attachment">The attachment to update</param>
        public async Task<Attachment> SyncToNetworkAsync(SCM.Models.Attachment attachment)
        {
            var builder = _builderAttachmentFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .SyncToNetworkAsync();
        }
    }
}

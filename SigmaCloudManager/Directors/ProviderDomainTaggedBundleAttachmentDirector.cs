using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainTaggedBundleAttachmentDirector : IProviderDomainAttachmentDirector
    {
        private readonly Func<ProviderDomainAttachmentRequest, IBundleAttachmentBuilder> _builderFactory;
        private readonly Func<Attachment, IBundleAttachmentBuilder> _builderAttachmentFactory;

        public ProviderDomainTaggedBundleAttachmentDirector(Func<ProviderDomainAttachmentRequest, IBundleAttachmentBuilder> builderFactory,
                                                            Func<Attachment, IBundleAttachmentBuilder> builderAttachmentFactory)
        {
            _builderFactory = builderFactory;
            _builderAttachmentFactory = builderAttachmentFactory;
        }

        public async Task<SCM.Models.Attachment> BuildAsync(int tenantId, ProviderDomainAttachmentRequest request, bool addToNetwork = false)
        {
            var builder = _builderFactory(request);
            return await builder.ForTenant(tenantId)
                                .WithAttachmentRole(request.AttachmentRoleName)
                                .WithPortPool(request.PortPoolName)
                                .WithAttachmentBandwidth(request.AttachmentBandwidthGbps)
                                .WithLocation(request.LocationName)
                                .WithPlane(request.PlaneName.ToString())
                                .WithBundleLinks(request.BundleMinLinks, request.BundleMaxLinks)
                                .WithDescription(request.Description)
                                .WithNotes(request.Notes)                               
                                .BuildAsync();
        }

        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, ProviderDomainAttachmentUpdate update, bool syncToNetwork = false)
        {
            var builder = _builderAttachmentFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .WithJumboMtu(update.UseJumboMtu)
                                .WithBundleLinks(update.BundleMinLinks, update.BundleMaxLinks)
                                .WithDescription(update.Description)
                                .WithNotes(update.Notes)
                                .BuildAsync();
        }

        /// <summary>
        /// Destroy an attachment
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="attachment">Attachment.</param>
        /// <param name="cleanUpNetwork">If set to <c>true</c> clean up network.</param>   
        public async Task DestroyAsync(Attachment attachment, bool cleanUpNetwork = false)
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
    }
}

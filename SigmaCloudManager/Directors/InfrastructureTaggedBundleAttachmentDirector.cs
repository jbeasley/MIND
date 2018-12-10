using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public class InfrastructureTaggedBundleAttachmentDirector : IInfrastructureAttachmentDirector
    {
        private readonly Func<InfrastructureAttachmentRequest, IBundleAttachmentBuilder> _builderRequestFactory;
        private readonly Func<Attachment, IBundleAttachmentBuilder> _builderAttachmentFactory;

        public InfrastructureTaggedBundleAttachmentDirector(Func<InfrastructureAttachmentRequest, IBundleAttachmentBuilder> builderRequestFactory,
                                                            Func<Attachment, IBundleAttachmentBuilder> builderAttachmentFactory)
        {
            _builderRequestFactory = builderRequestFactory;
            _builderAttachmentFactory = builderAttachmentFactory;
        }

        public async Task<SCM.Models.Attachment> BuildAsync(int deviceId, InfrastructureAttachmentRequest request)
        {
            var builder = _builderRequestFactory(request);
            return await builder.ForDevice(deviceId)
                                .WithAttachmentRole(request.AttachmentRoleName)
                                .WithPortPool(request.PortPoolName)
                                .WithAttachmentBandwidth(request.AttachmentBandwidthGbps)
                                .WithBundleLinks(request.BundleMinLinks, request.BundleMaxLinks)
                                .WithDescription(request.Description)
                                .WithNotes(request.Notes)
                                .BuildAsync();
        }

        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, InfrastructureAttachmentUpdate update)
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
        public async Task DestroyAsync(Attachment attachment)
        {
            var builder = _builderAttachmentFactory(attachment);
            await builder.ForAttachment(attachment.AttachmentID)
                         .ReleasePorts()
                         .DestroyAsync();
        }

        /// <summary>
        /// Destroy a collection of attachments.
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="attachments">Attachments to destroy</param>
        public async Task DestroyAsync(List<Attachment> attachments)
        {
            var tasks = attachments.Select(
                async attachment =>
                    await DestroyAsync(attachment));

            await Task.WhenAll(tasks);
        }
    }
}

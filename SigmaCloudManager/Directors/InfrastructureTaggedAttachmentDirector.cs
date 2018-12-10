using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public class InfrastructureTaggedAttachmentDirector<TAttachmentBuilder> : IInfrastructureAttachmentDirector 
        where TAttachmentBuilder: AttachmentBuilder<TAttachmentBuilder>
    {
        private readonly Func<InfrastructureAttachmentRequest, IAttachmentBuilder<TAttachmentBuilder>> _builderRequestFactory;
        private readonly Func<Attachment, IAttachmentBuilder<TAttachmentBuilder>> _builderAttachmentFactory;

        public InfrastructureTaggedAttachmentDirector(Func<InfrastructureAttachmentRequest, IAttachmentBuilder<TAttachmentBuilder>> builderRequestFactory,
                                                      Func<Attachment, IAttachmentBuilder<TAttachmentBuilder>> builderAttachmentFactory)
        {
            _builderRequestFactory = builderRequestFactory;
            _builderAttachmentFactory = builderAttachmentFactory;
        }

        public async Task<Attachment> BuildAsync(int deviceId, InfrastructureAttachmentRequest request)
        {
            var builder = _builderRequestFactory(request);
            return await builder.ForDevice(deviceId)
                                .WithAttachmentRole(request.AttachmentRoleName)
                                .WithPortPool(request.PortPoolName)
                                .WithAttachmentBandwidth(request.AttachmentBandwidthGbps)
                                .WithDescription(request.Description)
                                .WithNotes(request.Notes)
                                .BuildAsync();
        }

        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, InfrastructureAttachmentUpdate update)
        {
            var builder = _builderAttachmentFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .WithJumboMtu(update.UseJumboMtu)
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

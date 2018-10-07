using SCM.Models.RequestModels;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class InfrastructureUntaggedAttachmentUpdateDirector<TAttachmentBuilder> : IInfrastructureAttachmentUpdateDirector 
        where TAttachmentBuilder: IAttachmentUpdateBuilder<TAttachmentBuilder>
    {
        private readonly Func<Attachment, IAttachmentUpdateBuilder<TAttachmentBuilder>> _builderFactory;

        public InfrastructureUntaggedAttachmentUpdateDirector(Func<Attachment, IAttachmentUpdateBuilder<TAttachmentBuilder>> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, InfrastructureAttachmentUpdate update)
        {
            var builder = _builderFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .WithJumboMtu(update.UseJumboMtu)
                                .UpdateAsync();
        }
    }
}

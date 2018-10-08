using SCM.Models.RequestModels;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainTaggedAttachmentUpdateDirector<TAttachmentBuilder> : ITenantDomainAttachmentUpdateDirector 
        where TAttachmentBuilder: IAttachmentBuilder<TAttachmentBuilder>
    {
        private readonly Func<Attachment, IAttachmentBuilder<TAttachmentBuilder>> _builderFactory;

        public TenantDomainTaggedAttachmentUpdateDirector(Func<Attachment, IAttachmentBuilder<TAttachmentBuilder>> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, TenantDomainAttachmentUpdate update)
        {
            var builder = _builderFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .WithJumboMtu(update.UseJumboMtu)
                                .BuildAsync();
        }
    }
}

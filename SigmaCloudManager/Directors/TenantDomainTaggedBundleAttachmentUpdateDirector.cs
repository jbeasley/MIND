using SCM.Models.RequestModels;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainTaggedBundleAttachmentUpdateDirector: ITenantDomainAttachmentUpdateDirector
    {
        private readonly Func<Attachment, IBundleAttachmentUpdateBuilder> _builderFactory;

        public TenantDomainTaggedBundleAttachmentUpdateDirector(Func<Attachment, IBundleAttachmentUpdateBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, TenantDomainAttachmentUpdate update)
        {
            var builder = _builderFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .WithJumboMtu(update.UseJumboMtu)
                                .WithBundleLinks(update.BundleMinLinks, update.BundleMaxLinks)
                                .UpdateAsync();
        }
    }
}

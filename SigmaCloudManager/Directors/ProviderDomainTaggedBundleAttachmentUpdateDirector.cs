﻿using SCM.Models.RequestModels;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainTaggedBundleAttachmentUpdateDirector : IProviderDomainAttachmentUpdateDirector
    {
        private readonly Func<Attachment, IBundleAttachmentBuilder> _builderFactory;

        public ProviderDomainTaggedBundleAttachmentUpdateDirector(Func<Attachment, IBundleAttachmentBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, ProviderDomainAttachmentUpdate update)
        {
            var builder = _builderFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .WithJumboMtu(update.UseJumboMtu)
                                .WithBundleLinks(update.BundleMinLinks, update.BundleMaxLinks)
                                .BuildAsync();
        }
    }
}

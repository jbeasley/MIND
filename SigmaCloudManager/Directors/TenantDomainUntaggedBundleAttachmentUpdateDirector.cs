using SCM.Models.RequestModels;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainUntaggedBundleAttachmentUpdateDirector : ITenantDomainAttachmentUpdateDirector
    {
        private readonly Func<Attachment, IBundleAttachmentBuilder> _builderFactory;

        public TenantDomainUntaggedBundleAttachmentUpdateDirector(Func<Attachment, IBundleAttachmentBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, TenantDomainAttachmentUpdate update)
        {
            var builder = _builderFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .WithJumboMtu(update.UseJumboMtu)
                                .WithContractBandwidth(update.ContractBandwidthMbps)
                                .WithTrustReceivedCosAndDscp(update.TrustReceivedCosAndDscp)
                                .WithBundleLinks(update.BundleMinLinks, update.BundleMaxLinks)
                                .WithDescription(update.Description)
                                .WithNotes(update.Notes)
                                .BuildAsync();
        }
    }
}

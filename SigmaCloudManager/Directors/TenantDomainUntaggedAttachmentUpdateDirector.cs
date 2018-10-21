using SCM.Models.RequestModels;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainUntaggedAttachmentUpdateDirector<TAttachmentBuilder> : ITenantDomainAttachmentUpdateDirector 
        where TAttachmentBuilder: IAttachmentBuilder<TAttachmentBuilder>
    {
        private readonly Func<Attachment, IAttachmentBuilder<TAttachmentBuilder>> _builderFactory;

        public TenantDomainUntaggedAttachmentUpdateDirector(Func<Attachment, IAttachmentBuilder<TAttachmentBuilder>> builderFactory)
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
                                .WithDescription(update.Description)
                                .WithNotes(update.Notes)
                                .BuildAsync();
        }
    }
}

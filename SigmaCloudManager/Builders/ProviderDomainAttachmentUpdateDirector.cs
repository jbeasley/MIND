using SCM.Models.RequestModels;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainAttachmentUpdateDirector<TAttachmentBuilder> : IProviderDomainAttachmentUpdateDirector 
        where TAttachmentBuilder: IAttachmentUpdateBuilder<TAttachmentBuilder>
    {
        private readonly Func<Attachment, IAttachmentUpdateBuilder<TAttachmentBuilder>> _builderFactory;

        public ProviderDomainAttachmentUpdateDirector(Func<Attachment, IAttachmentUpdateBuilder<TAttachmentBuilder>> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, ProviderDomainAttachmentUpdate update)
        {
            var builder = _builderFactory(attachment);
            return await builder.ForAttachment(attachment)
                                .WithExistingRoutingInstance(update.ExistingRoutingInstanceName)
                                .WithNewRoutingInstance(update.CreateNewRoutingInstance)
                                .WithContractBandwidth(update.ContractBandwidthMbps)
                                .WithJumboMtu(update.UseJumboMtu)
                                .WithTrustReceivedCosAndDscp(update.TrustReceivedCosAndDscp)
                                .UpdateAsync();
        }
    }
}

using SCM.Models.RequestModels;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainUntaggedAttachmentUpdateDirector<TAttachmentBuilder> : IProviderDomainAttachmentUpdateDirector 
        where TAttachmentBuilder: IAttachmentBuilder<TAttachmentBuilder>
    {
        private readonly Func<Attachment, IAttachmentBuilder<TAttachmentBuilder>> _builderFactory;

        public ProviderDomainUntaggedAttachmentUpdateDirector(Func<Attachment, IAttachmentBuilder<TAttachmentBuilder>> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, ProviderDomainAttachmentUpdate update, bool addToNetwork = false)
        {
            var builder = _builderFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .UseExistingRoutingInstance(update.ExistingRoutingInstanceName)
                                .WithNewRoutingInstance(update.CreateNewRoutingInstance)
                                .WithRoutingInstance(update.RoutingInstance)
                                .WithContractBandwidth(update.ContractBandwidthMbps)
                                .WithJumboMtu(update.UseJumboMtu)
                                .WithTrustReceivedCosAndDscp(update.TrustReceivedCosAndDscp)
                                .WithDescription(update.Description)
                                .WithNotes(update.Notes)
                                .WithIpv4(update.Ipv4Addresses)
                                .BuildAsync();
        }
    }
}

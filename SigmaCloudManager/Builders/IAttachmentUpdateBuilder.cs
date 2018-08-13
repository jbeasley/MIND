using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IAttachmentUpdateBuilder<TAttachmentBuilder>
    {
        IAttachmentUpdateBuilder<TAttachmentBuilder> WithContractBandwidth(int? contractBandwidthMbps, bool? trustReceivedCosDscp = false);
        IAttachmentUpdateBuilder<TAttachmentBuilder> WithExistingRoutingInstance(string routingInstanceName);
        IAttachmentUpdateBuilder<TAttachmentBuilder> WithNewRoutingInstance(bool? newRoutingInstance = false);
        IAttachmentUpdateBuilder<TAttachmentBuilder> WithJumboMtu(bool? useJumboMtu = false);
        IAttachmentUpdateBuilder<TAttachmentBuilder> WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp = false);
        IAttachmentUpdateBuilder<TAttachmentBuilder> ForAttachment(Attachment attachment);
        Task<Attachment> UpdateAsync();
    }
}

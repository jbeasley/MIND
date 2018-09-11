using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IBundleAttachmentUpdateBuilder
    {
        IBundleAttachmentUpdateBuilder WithContractBandwidth(int? contractBandwidthMbps);
        IBundleAttachmentUpdateBuilder WithJumboMtu(bool? useJumboMtu = false);
        IBundleAttachmentUpdateBuilder WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp = false);
        IBundleAttachmentUpdateBuilder WithExistingRoutingInstance(string routingInstanceName);
        IBundleAttachmentUpdateBuilder WithNewRoutingInstance(bool? newRoutingInstance = false);
        IBundleAttachmentUpdateBuilder ForAttachment(int? attachmentId);
        IBundleAttachmentUpdateBuilder WithBundleLinks(int? minLinks, int? maxLinks);
        Task<Attachment> UpdateAsync();
    }
}

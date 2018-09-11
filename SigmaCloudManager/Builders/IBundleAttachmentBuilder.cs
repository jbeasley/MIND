using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IBundleAttachmentBuilder : IAttachmentBuilder<BundleAttachmentBuilder>
    {
        new IBundleAttachmentBuilder ForTenant(int? tenantId);
        new IBundleAttachmentBuilder WithIpv4(List<Ipv4AddressAndMask> ipv4Addresses);
        new IBundleAttachmentBuilder WithAttachmentRole(string attachmentRoleName);
        new IBundleAttachmentBuilder WithPortPool(string portPoolName);
        new IBundleAttachmentBuilder WithAttachmentBandwidth(int? attachmentBandwidthGbps);
        new IBundleAttachmentBuilder WithLocation(string locationName);
        new IBundleAttachmentBuilder WithPlane(string planeName = "");
        new IBundleAttachmentBuilder WithContractBandwidth(int? contractBandwidthMbps);
        new IBundleAttachmentBuilder WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp);
        new IBundleAttachmentBuilder WithJumboMtu(bool? useJumboMtu = false);
        IBundleAttachmentBuilder WithBundleLinks(int? minLinks, int? maxLinks);
    }
}

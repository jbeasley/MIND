﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IBundleAttachmentBuilder : IAttachmentBuilder<BundleAttachmentBuilder>
    {
        new IBundleAttachmentBuilder ForTenant(int tenantId);
        new IBundleAttachmentBuilder WithInterfaces(List<Ipv4AddressAndMask> ipv4Addresses);
        new IBundleAttachmentBuilder WithAttachmentRole(string portPoolName, string attachmentRoleName);
        new IBundleAttachmentBuilder WithAttachmentBandwidth(int? attachmentBandwidthGbps);
        new IBundleAttachmentBuilder WithLocation(string locationName);
        new IBundleAttachmentBuilder WithPlane(string planeName = "");
        new IBundleAttachmentBuilder WithContractBandwidth(int? contractBandwidthMbps, bool? trustReceivedCosDscp = false);
        new IBundleAttachmentBuilder WithJumboMtu(bool? useJumboMtu = false);
        IBundleAttachmentBuilder WithBundleLinks(int? minLinks, int? maxLinks);
    }
}

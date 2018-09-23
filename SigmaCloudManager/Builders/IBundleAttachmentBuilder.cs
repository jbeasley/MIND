﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IBundleAttachmentBuilder
    {
        IBundleAttachmentBuilder ForTenant(int? tenantId);
        IBundleAttachmentBuilder WithIpv4(List<Ipv4AddressAndMask> ipv4Addresses);
        IBundleAttachmentBuilder WithAttachmentRole(string attachmentRoleName);
        IBundleAttachmentBuilder WithPortPool(string portPoolName);
        IBundleAttachmentBuilder WithAttachmentBandwidth(int? attachmentBandwidthGbps);
        IBundleAttachmentBuilder WithLocation(string locationName);
        IBundleAttachmentBuilder WithPlane(string planeName = "");
        IBundleAttachmentBuilder WithContractBandwidth(int? contractBandwidthMbps);
        IBundleAttachmentBuilder WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp);
        IBundleAttachmentBuilder WithJumboMtu(bool? useJumboMtu = false);
        IBundleAttachmentBuilder WithBundleLinks(int? minLinks, int? maxLinks);
        Task<Attachment> BuildAsync();
    }
}

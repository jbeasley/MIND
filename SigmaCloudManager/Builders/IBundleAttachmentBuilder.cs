﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IBundleAttachmentBuilder
    {
        IBundleAttachmentBuilder ForTenant(int? tenantId);
        IBundleAttachmentBuilder ForDevice(int? deviceId);
        IBundleAttachmentBuilder ForAttachment(int? attachmentId);
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
        IBundleAttachmentBuilder UseExistingRoutingInstance(string routingInstanceName);
        IBundleAttachmentBuilder WithNewRoutingInstance(bool? newRoutingInstance = false);
        IBundleAttachmentBuilder WithRoutingInstance(RoutingInstanceRequest routingInstanceRequest);
        IBundleAttachmentBuilder UseDefaultRoutingInstance(bool? useDefaultRoutingInstance);
        IBundleAttachmentBuilder WithDescription(string description);
        IBundleAttachmentBuilder WithNotes(string notes);
        IBundleAttachmentBuilder CleanUpRoutingInstance();
        IBundleAttachmentBuilder ReleasePorts();
        IBundleAttachmentBuilder SyncToNetworkPut(bool? syncToNetworkPut);
        IBundleAttachmentBuilder CleanUpNetwork(bool? cleanUpNetwork);
        IBundleAttachmentBuilder Stage(bool? stage);

        Task<Attachment> BuildAsync();
        Task DestroyAsync();
    }
}

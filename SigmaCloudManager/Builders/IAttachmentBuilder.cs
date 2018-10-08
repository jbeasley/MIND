using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IAttachmentBuilder<TAttachmentBuilder>
    {
        IAttachmentBuilder<TAttachmentBuilder> ForTenant(int? tenantId);
        IAttachmentBuilder<TAttachmentBuilder> ForDevice(int? deviceId);
        IAttachmentBuilder<TAttachmentBuilder> ForAttachment(int? attachmentId);
        IAttachmentBuilder<TAttachmentBuilder> WithIpv4(List<Ipv4AddressAndMask> ipv4Addresses);
        IAttachmentBuilder<TAttachmentBuilder> WithAttachmentRole(string attachmentRoleName);
        IAttachmentBuilder<TAttachmentBuilder> WithPortPool(string portPoolName);
        IAttachmentBuilder<TAttachmentBuilder> WithAttachmentBandwidth(int? attachmentBandwidthGbps);
        IAttachmentBuilder<TAttachmentBuilder> WithLocation(string locationName);
        IAttachmentBuilder<TAttachmentBuilder> WithPlane(string planeName = "");
        IAttachmentBuilder<TAttachmentBuilder> WithContractBandwidth(int? contractBandwidthMbps);
        IAttachmentBuilder<TAttachmentBuilder> WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp);
        IAttachmentBuilder<TAttachmentBuilder> WithJumboMtu(bool? useJumboMtu = false);
        IAttachmentBuilder<TAttachmentBuilder> UseExistingRoutingInstance(string routingInstanceName);
        IAttachmentBuilder<TAttachmentBuilder> WithNewRoutingInstance(bool? newRoutingInstance = false);
        IAttachmentBuilder<TAttachmentBuilder> UseDefaultRoutingInstance(bool? useDefaultRoutingInstance);
        Task<Attachment> BuildAsync();
    }
}

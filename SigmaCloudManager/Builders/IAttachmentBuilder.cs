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
        IAttachmentBuilder<TAttachmentBuilder> ForTenant(int tenantId);
        IAttachmentBuilder<TAttachmentBuilder> WithInterfaces(List<Ipv4AddressAndMask> ipv4Addresses);
        IAttachmentBuilder<TAttachmentBuilder> WithAttachmentRole(string portPoolName, string attachmentRoleName);
        IAttachmentBuilder<TAttachmentBuilder> WithAttachmentBandwidth(int? attachmentBandwidthGbps);
        IAttachmentBuilder<TAttachmentBuilder> WithLocation(string locationName);
        IAttachmentBuilder<TAttachmentBuilder> WithPlane(string planeName = "");
        IAttachmentBuilder<TAttachmentBuilder> WithContractBandwidth(int? contractBandwidthMbps, bool? trustReceivedCosDscp = false);
        IAttachmentBuilder<TAttachmentBuilder> WithJumboMtu(bool? useJumboMtu = false);
        Task<Attachment> BuildAsync();
    }
}

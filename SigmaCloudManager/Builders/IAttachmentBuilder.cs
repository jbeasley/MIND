using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IAttachmentBuilder
    {
        IAttachmentBuilder ForTenant(int tenantId);
        IAttachmentBuilder WithInterfaces(List<Ipv4AddressAndMask> ipv4Addresses);
        IAttachmentBuilder WithAttachmentRole(string portPoolName, string attachmentRoleName);
        IAttachmentBuilder WithAttachmentBandwidth(int? attachmentBandwidthGbps);
        IAttachmentBuilder WithLocation(string locationName);
        IAttachmentBuilder WithPlane(string planeName = "");
        IAttachmentBuilder WithContractBandwidth(int? contractBandwidthMbps, bool? trustReceivedCosDscp = false);
        Task<Attachment> BuildAsync();

    }
}

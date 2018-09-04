using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVifBuilder
    {
        IVifBuilder ForAttachment(int attachmentId);
        IVifBuilder ForTenant(int tenantId);
        IVifBuilder WithVifRole(string roleName);
        IVifBuilder AutoAllocateVlanTag(bool? autoAllocateVlanTag, string vlanTagRangeName = "Default");
        IVifBuilder WithRequestedVlanTag(int? vlanTag);
        IVifBuilder WithContractBandwidth(int? contractBandwidthMbps);
        IVifBuilder WithExistingRoutingInstance(string existingRoutingInstanceName);
        IVifBuilder WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp);
        IVifBuilder WithIpv4(List<Ipv4AddressAndMask> ipv4AddressesAndMask);
        IVifBuilder WithJumboMtu(bool? useJumboMtu);
        Task<Vif> BuildAsync();
    }
}

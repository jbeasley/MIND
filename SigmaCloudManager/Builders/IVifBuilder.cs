using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVifBuilder
    {
        IVifBuilder ForAttachment(int? attachmentId);
        IVifBuilder ForTenant(int? tenantId);
        IVifBuilder ForVif(int? vifId);
        IVifBuilder WithVifRole(string roleName);
        IVifBuilder AutoAllocateVlanTag(bool? autoAllocateVlanTag, string vlanTagRangeName = "Default");
        IVifBuilder WithRequestedVlanTag(int? vlanTag);
        IVifBuilder WithContractBandwidth(int? contractBandwidthMbps);
        IVifBuilder WithExistingContractBandwidthPool(string existingContractBandwidthPoolName);
        IVifBuilder UseExistingRoutingInstance(string existingRoutingInstanceName);
        IVifBuilder WithNewRoutingInstance(bool? createNewRoutingInstanceName);
        IVifBuilder WithRoutingInstance(RoutingInstanceRequest routingInstanceRequest);
        IVifBuilder WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp);
        IVifBuilder WithIpv4(List<Ipv4AddressAndMask> ipv4AddressesAndMask);
        IVifBuilder WithJumboMtu(bool? useJumboMtu);
        IVifBuilder CleanUpRoutingInstance();
        IVifBuilder CleanUpContractBandwidthPool();
        IVifBuilder SyncToNetworkPut(bool? syncToNetworkPut);
        IVifBuilder CleanUpNetwork(bool? cleanUpNetwork);

        Task<Vif> BuildAsync();
        Task DestroyAsync();
        Task<Vif> SyncToNetworkPutAsync();
    }
}

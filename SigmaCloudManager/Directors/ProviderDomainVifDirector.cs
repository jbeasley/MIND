using Mind.Models.RequestModels;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainVifDirector : IProviderDomainVifDirector
    {
        private readonly IVifBuilder _builder;

        public ProviderDomainVifDirector(IVifBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.Vif> BuildAsync(int attachmentId, ProviderDomainVifRequest request)
        {
            return await _builder.ForAttachment(attachmentId)
                                 .ForTenant(request.TenantId.Value)
                                 .WithRequestedVlanTag(request.RequestedVlanTag)
                                 .WithTrustReceivedCosAndDscp(request.TrustReceivedCosAndDscp)
                                 .WithVifRole(request.VifRoleName)
                                 .UseExistingRoutingInstance(request.ExistingRoutingInstanceName)
                                 .WithContractBandwidth(request.ContractBandwidthMbps)
                                 .WithExistingContractBandwidthPool(request.ExistingContractBandwidthPoolName)
                                 .WithIpv4(request.Ipv4Addresses)
                                 .BuildAsync();
        }
    }
}

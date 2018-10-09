using Mind.Models.RequestModels;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainVifUpdateDirector : IProviderDomainVifUpdateDirector
    {
        private readonly IVifBuilder _builder;

        public ProviderDomainVifUpdateDirector(IVifBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.Vif> UpdateAsync(int vifId, Mind.Models.RequestModels.ProviderDomainVifUpdate update)
        {
            return await _builder.ForVif(vifId)
                                 .WithNewRoutingInstance(update.CreateNewRoutingInstance)
                                 .UseExistingRoutingInstance(update.ExistingRoutingInstanceName)
                                 .WithContractBandwidth(update.ContractBandwidthMbps)
                                 .WithExistingContractBandwidthPool(update.ExistingContractBandwidthPoolName)
                                 .WithTrustReceivedCosAndDscp(update.TrustReceivedCosAndDscp)
                                 .WithIpv4(update.Ipv4Addresses)
                                 .WithJumboMtu(update.UseJumboMtu)
                                 .BuildAsync();
        }
    }
}

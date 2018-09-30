using Mind.Models.RequestModels;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainVifUpdateDirector : ITenantDomainVifUpdateDirector
    {
        private readonly IVifUpdateBuilder _builder;

        public TenantDomainVifUpdateDirector(IVifUpdateBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.Vif> UpdateAsync(int vifId, Mind.Models.RequestModels.TenantDomainVifUpdate update)
        {
            return await _builder.ForVif(vifId)
                                 .WithContractBandwidth(update.ContractBandwidthMbps)
                                 .WithExistingContractBandwidthPool(update.ExistingContractBandwidthPoolName)
                                 .WithTrustReceivedCosAndDscp(update.TrustReceivedCosAndDscp)
                                 .WithIpv4(update.Ipv4Addresses)
                                 .WithJumboMtu(update.UseJumboMtu)
                                 .UpdateAsync();
        }
    }
}

﻿using Mind.Models.RequestModels;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainVifUpdateDirector : IProviderDomainVifUpdateDirector
    {
        private readonly IVifUpdateBuilder _builder;

        public ProviderDomainVifUpdateDirector(IVifUpdateBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.Vif> UpdateAsync(int vifId, Mind.Models.RequestModels.ProviderDomainVifUpdate update)
        {
            return await _builder.ForVif(vifId)
                                 .WithExistingRoutingInstance(update.ExistingRoutingInstanceName)
                                 .WithContractBandwidth(update.ContractBandwidthMbps)
                                 .WithTrustReceivedCosAndDscp(update.TrustReceivedCosAndDscp)
                                 .WithIpv4(update.Ipv4Addresses)
                                 .WithJumboMtu(update.UseJumboMtu)
                                 .UpdateAsync();
        }
    }
}

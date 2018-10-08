using Mind.Models.RequestModels;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class InfrastructureVifUpdateDirector : IInfrastructureVifUpdateDirector
    {
        private readonly IVifUpdateBuilder _builder;

        public InfrastructureVifUpdateDirector(IVifUpdateBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.Vif> UpdateAsync(int vifId, Mind.Models.RequestModels.InfrastructureVifUpdate update)
        {
            return await _builder.ForVif(vifId)
                                 .WithNewRoutingInstance(update.CreateNewRoutingInstance)
                                 .UseExistingRoutingInstance(update.ExistingRoutingInstanceName)
                                 .WithContractBandwidth(update.ContractBandwidthMbps)
                                 .WithExistingContractBandwidthPool(update.ExistingContractBandwidthPoolName)
                                 .WithIpv4(update.Ipv4Addresses)
                                 .WithJumboMtu(update.UseJumboMtu)
                                 .UpdateAsync();
        }
    }
}

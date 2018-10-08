using Mind.Models.RequestModels;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class InfrastructureVifDirector : IInfrastructureVifDirector
    {
        private readonly IVifBuilder _builder;

        public InfrastructureVifDirector(IVifBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.Vif> BuildAsync(int attachmentId, InfrastructureVifRequest request)
        {
            return await _builder.ForAttachment(attachmentId)
                                 .WithRequestedVlanTag(request.RequestedVlanTag)
                                 .WithVifRole(request.VifRoleName)
                                 .WithContractBandwidth(request.ContractBandwidthMbps)
                                 .WithExistingContractBandwidthPool(request.ExistingContractBandwidthPoolName)
                                 .WithIpv4(request.Ipv4Addresses)
                                 .BuildAsync();
        }
    }
}

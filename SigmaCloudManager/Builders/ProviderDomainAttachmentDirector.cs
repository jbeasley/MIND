using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainAttachmentDirector : IProviderDomainAttachmentDirector
    {
        private readonly Func<ProviderDomainAttachmentRequest, IAttachmentBuilder> _builderFactory;

        public ProviderDomainAttachmentDirector(Func<ProviderDomainAttachmentRequest, IAttachmentBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Attachment> BuildAsync(int tenantId, ProviderDomainAttachmentRequest request)
        {
            var builder = _builderFactory(request);
            return await builder.ForTenant(tenantId)
                                .WithAttachmentRole(request.PortPoolName, request.AttachmentRoleName)
                                .WithAttachmentBandwidth(request.AttachmentBandwidthGbps)
                                .WithLocation(request.LocationName)
                                .WithPlane(request.PlaneName.ToString())
                                .WithInterfaces(request.Ipv4Addresses)
                                .WithContractBandwidth(request.ContractBandwidthMbps, request.TrustReceivedCosDscp)
                                .BuildAsync();
        }
    }
}

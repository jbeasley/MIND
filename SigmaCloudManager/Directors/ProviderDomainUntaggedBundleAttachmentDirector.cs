using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainUntaggedBundleAttachmentDirector : IProviderDomainAttachmentDirector
    {
        private readonly Func<ProviderDomainAttachmentRequest, IBundleAttachmentBuilder> _builderFactory;

        public ProviderDomainUntaggedBundleAttachmentDirector(Func<ProviderDomainAttachmentRequest, IBundleAttachmentBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Attachment> BuildAsync(int tenantId, ProviderDomainAttachmentRequest request)
        {
            var builder = _builderFactory(request);
            return await builder.ForTenant(tenantId)
                                .WithAttachmentRole(request.AttachmentRoleName)
                                .WithPortPool(request.PortPoolName)
                                .WithAttachmentBandwidth(request.AttachmentBandwidthGbps)
                                .WithLocation(request.LocationName)
                                .WithPlane(request.PlaneName.ToString())
                                .WithIpv4(request.Ipv4Addresses)
                                .WithContractBandwidth(request.ContractBandwidthMbps)
                                .WithTrustReceivedCosAndDscp(request.TrustReceivedCosAndDscp)
                                .WithBundleLinks(request.BundleMinLinks, request.BundleMaxLinks)
                                .BuildAsync();
        }
    }
}

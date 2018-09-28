using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainUntaggedAttachmentDirector<TAttachmentBuilder> : ITenantDomainAttachmentDirector 
        where TAttachmentBuilder: AttachmentBuilder<TAttachmentBuilder>
    {
        private readonly Func<TenantDomainAttachmentRequest, IAttachmentBuilder<TAttachmentBuilder>> _builderFactory;

        public TenantDomainUntaggedAttachmentDirector(Func<TenantDomainAttachmentRequest, IAttachmentBuilder<TAttachmentBuilder>> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Attachment> BuildAsync(int deviceId, TenantDomainAttachmentRequest request)
        {
            var builder = _builderFactory(request);
            return await builder.ForDevice(deviceId)
                                .WithAttachmentRole(request.AttachmentRoleName)
                                .WithPortPool(request.PortPoolName)
                                .WithAttachmentBandwidth(request.AttachmentBandwidthGbps)
                                .WithIpv4(request.Ipv4Addresses)
                                .UseDefaultRoutingInstance(true)
                                .WithContractBandwidth(request.ContractBandwidthMbps)
                                .WithTrustReceivedCosAndDscp(request.TrustReceivedCosAndDscp)
                                .BuildAsync();
        }
    }
}

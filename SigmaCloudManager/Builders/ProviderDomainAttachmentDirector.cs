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
            builder.Init(tenantId);
            await builder.SetAttachmentRoleAsync(request.PortPoolName, request.AttachmentRoleName);
            await builder.SetAttachmentBandwidthAsync(request.AttachmentBandwidthGbps);
            await builder.AllocatePortsAsync(request.LocationName, request.PlaneName.ToString());
            await builder.SetMtuAsync();
            builder.CreateInterfaces(request.IpAddress1, request.SubnetMask1);
            var attachmentRole = builder.GetResult().AttachmentRole;
            await builder.CreateRoutingInstanceAsync();
            if (attachmentRole.RequireContractBandwidth) {
                await builder.CreateContractBandwidthPoolAsync(tenantId, request.ContractBandwidthMbps);
            }

            return builder.GetResult();
        }
    }
}

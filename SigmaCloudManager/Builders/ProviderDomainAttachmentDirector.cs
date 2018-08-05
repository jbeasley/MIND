using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainAttachmentDirector
    {
        public ProviderDomainAttachmentDirector()
        {
        }

        public async Task<SCM.Models.Attachment> Build(int tenantId, ProviderDomainAttachmentRequest request, IAttachmentBuilder builder)
        {
            builder.Init(tenantId)
                 .CreateInterfaces(request.IpAddress1, request.SubnetMask1);

            await builder.SetMtuAsync();
            await builder.SetAttachmentRoleAsync(request.PortPoolName, request.AttachmentRoleName);
            var attachmentRole = builder.GetResult().AttachmentRole;
            if (attachmentRole.RoutingInstanceType.IsVrf)
            {
                builder.UseRoutingInstanceBuilder<VrfRoutingInstanceBuilder>();
            }
            await builder.SetAttachmentBandwidthAsync(request.AttachmentBandwidthGbps);
            await builder.AllocatePortsAsync(request.LocationName, request.PlaneName.ToString());
            await builder.CreateRoutingInstanceAsync();
            if (attachmentRole.RequireContractBandwidth) {
                await builder.CreateContractBandwidthPoolAsync(tenantId, request.ContractBandwidthMbps);
            }

            return builder.GetResult();
        }
    }
}

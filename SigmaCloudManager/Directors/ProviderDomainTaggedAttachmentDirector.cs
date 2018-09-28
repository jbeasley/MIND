using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainTaggedAttachmentDirector<TAttachmentBuilder> : IProviderDomainAttachmentDirector 
        where TAttachmentBuilder: AttachmentBuilder<TAttachmentBuilder>
    {
        private readonly Func<ProviderDomainAttachmentRequest, IAttachmentBuilder<TAttachmentBuilder>> _builderFactory;

        public ProviderDomainTaggedAttachmentDirector(Func<ProviderDomainAttachmentRequest, IAttachmentBuilder<TAttachmentBuilder>> builderFactory)
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
                                .BuildAsync();
        }
    }
}

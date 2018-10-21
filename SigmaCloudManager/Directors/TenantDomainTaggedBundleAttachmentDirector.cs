using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainTaggedBundleAttachmentDirector : ITenantDomainAttachmentDirector
    {
        private readonly Func<TenantDomainAttachmentRequest, IBundleAttachmentBuilder> _builderFactory;

        public TenantDomainTaggedBundleAttachmentDirector(Func<TenantDomainAttachmentRequest, IBundleAttachmentBuilder> builderFactory)
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
                                .WithBundleLinks(request.BundleMinLinks, request.BundleMaxLinks)
                                .WithDescription(request.Description)
                                .WithNotes(request.Notes)
                                .BuildAsync();
        }
    }
}

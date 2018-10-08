using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class InfrastructureTaggedBundleAttachmentDirector : IInfrastructureAttachmentDirector
    {
        private readonly Func<InfrastructureAttachmentRequest, IBundleAttachmentBuilder> _builderFactory;

        public InfrastructureTaggedBundleAttachmentDirector(Func<InfrastructureAttachmentRequest, IBundleAttachmentBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Attachment> BuildAsync(int deviceId, InfrastructureAttachmentRequest request)
        {
            var builder = _builderFactory(request);
            return await builder.ForDevice(deviceId)
                                .WithAttachmentRole(request.AttachmentRoleName)
                                .WithPortPool(request.PortPoolName)
                                .WithAttachmentBandwidth(request.AttachmentBandwidthGbps)
                                .WithBundleLinks(request.BundleMinLinks, request.BundleMaxLinks)
                                .BuildAsync();
        }
    }
}

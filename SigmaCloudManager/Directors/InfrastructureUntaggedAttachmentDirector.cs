using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class InfrastructureUntaggedAttachmentDirector<TAttachmentBuilder> : IInfrastructureAttachmentDirector 
        where TAttachmentBuilder: AttachmentBuilder<TAttachmentBuilder>
    {
        private readonly Func<InfrastructureAttachmentRequest, IAttachmentBuilder<TAttachmentBuilder>> _builderFactory;

        public InfrastructureUntaggedAttachmentDirector(Func<InfrastructureAttachmentRequest, IAttachmentBuilder<TAttachmentBuilder>> builderFactory)
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
                                .WithIpv4(request.Ipv4Addresses)
                                .UseDefaultRoutingInstance(true)
                                .WithDescription(request.Description)
                                .WithNotes(request.Notes)
                                .BuildAsync();
        }
    }
}

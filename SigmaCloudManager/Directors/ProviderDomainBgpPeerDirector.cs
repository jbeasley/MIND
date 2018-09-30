using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainBgpPeerDirector : IProviderDomainBgpPeerDirector
    {
        private readonly IBgpPeerBuilder _builder;

        public ProviderDomainBgpPeerDirector(IBgpPeerBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.BgpPeer> BuildAsync(int routingInstanceId, BgpPeerRequest request)
        {
            return await _builder.ForRoutingInstance(routingInstanceId)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .WithPeer2ByteAutonomousSystem(request.Peer2ByteAutonomousSystem)
                                 .WithBfd(request.IsBfdEnabled)
                                 .WithMultiHop(request.IsMultiHop)
                                 .WithPeerPassword(request.PeerPassword)
                                 .WithMaximumRoutes(request.MaximumRoutes) 
                                 .BuildAsync();
        }
    }
}

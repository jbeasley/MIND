using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class BgpPeerUpdateDirector : IBgpPeerUpdateDirector
    {
        private readonly IBgpPeerUpdateBuilder _builder;

        public BgpPeerUpdateDirector(IBgpPeerUpdateBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.BgpPeer> UpdateAsync(int bgpPeerId, BgpPeerRequest request)
        {
            return await _builder.ForBgpPeer(bgpPeerId)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .WithPeer2ByteAutonomousSystem(request.Peer2ByteAutonomousSystem)
                                 .WithBfd(request.IsBfdEnabled)
                                 .WithMultiHop(request.IsMultiHop)
                                 .WithPeerPassword(request.PeerPassword)
                                 .WithMaximumRoutes(request.MaximumRoutes) 
                                 .UpdateAsync();
        }
    }
}

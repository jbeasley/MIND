using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public interface IBgpPeerBuilder
    {
        IBgpPeerBuilder ForRoutingInstance(int routingInstanceId);
        IBgpPeerBuilder WithIpv4PeerAddress(string ipv4PeerAddess);
        IBgpPeerBuilder WithPeer2ByteAutonomousSystem(int? peer2ByteASNumber);
        IBgpPeerBuilder WithMaximumRoutes(int? maxRoutes);
        IBgpPeerBuilder WithBfd(bool? isBfdEnabled);
        IBgpPeerBuilder WithMultiHop(bool? isMultiHopEnabled);
        IBgpPeerBuilder WithPeerPassword(string password);
        Task<BgpPeer> BuildAsync();
    }
}

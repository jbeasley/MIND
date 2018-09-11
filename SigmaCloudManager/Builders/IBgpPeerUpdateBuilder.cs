using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public interface IBgpPeerUpdateBuilder
    {
        IBgpPeerUpdateBuilder ForBgpPeer(int? bgpPeerId);
        IBgpPeerUpdateBuilder WithIpv4PeerAddress(string ipv4PeerAddess);
        IBgpPeerUpdateBuilder WithPeer2ByteAutonomousSystem(int? peer2ByteASNumber);
        IBgpPeerUpdateBuilder WithMaximumRoutes(int? maxRoutes);
        IBgpPeerUpdateBuilder WithBfd(bool? isBfdEnabled);
        IBgpPeerUpdateBuilder WithMultiHop(bool? isMultiHopEnabled);
        IBgpPeerUpdateBuilder WithPeerPassword(string password);
        Task<BgpPeer> UpdateAsync();
    }
}

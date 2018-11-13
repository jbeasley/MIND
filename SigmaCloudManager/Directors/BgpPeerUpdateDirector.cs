using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public class BgpPeerUpdateDirector : IBgpPeerUpdateDirector
    {
        private readonly Func<IBgpPeerBuilder> _builderFactory;

        public BgpPeerUpdateDirector(Func<IBgpPeerBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<BgpPeer> UpdateAsync(int bgpPeerId, BgpPeerRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForBgpPeer(bgpPeerId)
                                .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                .WithPeer2ByteAutonomousSystem(request.Peer2ByteAutonomousSystem)
                                .WithBfd(request.IsBfdEnabled)
                                .WithMultiHop(request.IsMultiHop)
                                .WithPeerPassword(request.PeerPassword)
                                .WithMaximumRoutes(request.MaximumRoutes) 
                                .BuildAsync();
        }

        public async Task<List<BgpPeer>> UpdateAsync(List<BgpPeerRequest> requests)
        {
            var bgpPeers = new List<BgpPeer>();
            var tasks = requests.Select(
                            async request =>
                            {
                                if (request.BgpPeerId.HasValue)
                                {
                                    bgpPeers.Add(await UpdateAsync(request.BgpPeerId.Value, request));
                                }
                            });

            await Task.WhenAll(tasks);
            return bgpPeers;
        }
    }
}

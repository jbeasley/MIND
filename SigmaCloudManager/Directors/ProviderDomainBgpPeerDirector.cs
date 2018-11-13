using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public class ProviderDomainBgpPeerDirector : IProviderDomainBgpPeerDirector
    {
        // Factory for the bgp builder - the factory ensures we get a unique instance of the builder
        // for each bgp peer request which is necessary when constructing a collection of bgp peers
        private readonly Func<IBgpPeerBuilder> _builderFactory;

        public ProviderDomainBgpPeerDirector(Func<IBgpPeerBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<BgpPeer> BuildAsync(int routingInstanceId, BgpPeerRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForRoutingInstance(routingInstanceId)
                                .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                .WithPeer2ByteAutonomousSystem(request.Peer2ByteAutonomousSystem)
                                .WithBfd(request.IsBfdEnabled)
                                .WithMultiHop(request.IsMultiHop)
                                .WithPeerPassword(request.PeerPassword)
                                .WithMaximumRoutes(request.MaximumRoutes) 
                                .BuildAsync();
        }

        public async Task<BgpPeer> BuildAsync(RoutingInstance routingInstance, BgpPeerRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForRoutingInstance(routingInstance)
                                .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                .WithPeer2ByteAutonomousSystem(request.Peer2ByteAutonomousSystem)
                                .WithBfd(request.IsBfdEnabled)
                                .WithMultiHop(request.IsMultiHop)
                                .WithPeerPassword(request.PeerPassword)
                                .WithMaximumRoutes(request.MaximumRoutes)
                                .BuildAsync();
        }

        public async Task<List<BgpPeer>> BuildAsync(RoutingInstance routingInstance, List<BgpPeerRequest> requests)
        {
            var bgpPeers = new List<BgpPeer>();
            var tasks = requests.Select(
                            async request =>
                               bgpPeers.Add(await BuildAsync(routingInstance, request))
                            );

            await Task.WhenAll(tasks);
            return bgpPeers;
        }
    }
}

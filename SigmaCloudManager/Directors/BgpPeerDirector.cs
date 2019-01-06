using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public class BgpPeerDirector : IBgpPeerDirector
    {
        // Factory for the bgp builder - the factory ensures we get a unique instance of the builder
        // for each bgp peer request which is necessary when constructing a collection of bgp peers
        private readonly Func<IBgpPeerBuilder> _builderFactory;

        public BgpPeerDirector(Func<IBgpPeerBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<BgpPeer> BuildForRoutingInstanceAsync(int routingInstanceId, BgpPeerRequest request)
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

        public async Task<BgpPeer> BuildForDeviceAsync(int deviceId, BgpPeerRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForDevice(deviceId)
                                .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                .WithPeer2ByteAutonomousSystem(request.Peer2ByteAutonomousSystem)
                                .WithBfd(request.IsBfdEnabled)
                                .WithMultiHop(request.IsMultiHop)
                                .WithPeerPassword(request.PeerPassword)
                                .WithMaximumRoutes(request.MaximumRoutes)
                                .BuildAsync();
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

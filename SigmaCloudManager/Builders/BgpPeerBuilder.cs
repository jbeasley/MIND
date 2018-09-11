using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SCM.Data;
using SCM.Models;

namespace Mind.Builders
{
    public class BgpPeerBuilder :BaseBuilder, IBgpPeerBuilder
    {
        protected internal BgpPeer _bgpPeer;

        public BgpPeerBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _bgpPeer = new BgpPeer
            {
                VpnTenantCommunitiesOut = new List<VpnTenantCommunityOut>(),
                VpnTenantIpNetworksOut = new List<VpnTenantIpNetworkOut>(),
                VpnTenantCommunitiesIn = new List<VpnTenantCommunityIn>(),
                VpnTenantIpNetworksIn = new List<VpnTenantIpNetworkIn>()
            };
        }

        public IBgpPeerBuilder ForRoutingInstance(int routingInstanceId)
        {
            _args.Add(nameof(ForRoutingInstance), routingInstanceId);
            return this;
        }

        public IBgpPeerBuilder WithBfd(bool? isBfdEnabled)
        {
            if (isBfdEnabled.HasValue) _args.Add(nameof(WithBfd), isBfdEnabled);
            return this;
        }

        public IBgpPeerBuilder WithIpv4PeerAddress(string ipv4PeerAddess)
        {
            if (!string.IsNullOrEmpty(ipv4PeerAddess)) _args.Add(nameof(WithIpv4PeerAddress), ipv4PeerAddess);
            return this;
        }

        public IBgpPeerBuilder WithMaximumRoutes(int? maxRoutes)
        {
            if (maxRoutes.HasValue) _args.Add(nameof(WithMaximumRoutes), maxRoutes);
            return this;
        }

        public IBgpPeerBuilder WithMultiHop(bool? isMultiHopEnabled)
        {
            if (isMultiHopEnabled.HasValue) _args.Add(nameof(WithMultiHop), isMultiHopEnabled);
            return this;
        }

        public IBgpPeerBuilder WithPeer2ByteAutonomousSystem(int? peer2ByteASNumber)
        {
            if (peer2ByteASNumber.HasValue) _args.Add(nameof(WithPeer2ByteAutonomousSystem), peer2ByteASNumber);
            return this;
        }

        public IBgpPeerBuilder WithPeerPassword(string password)
        {
            if (!string.IsNullOrEmpty(password)) _args.Add(nameof(WithPeerPassword), password);
            return this;
        }

        public async Task<BgpPeer> BuildAsync()
        {
            await SetRoutingInstanceAsync();
            if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) _bgpPeer.Ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            if (_args.ContainsKey(nameof(WithMaximumRoutes))) _bgpPeer.MaximumRoutes = (int)_args[nameof(WithMaximumRoutes)];
            if (_args.ContainsKey(nameof(WithBfd))) _bgpPeer.IsBfdEnabled = (bool)_args[nameof(WithBfd)];
            if (_args.ContainsKey(nameof(WithMultiHop))) _bgpPeer.IsMultiHop = (bool)_args[nameof(WithMultiHop)];
            if (_args.ContainsKey(nameof(WithPeer2ByteAutonomousSystem))) _bgpPeer.Peer2ByteAutonomousSystem = (int)_args[nameof(WithPeer2ByteAutonomousSystem)];
            if (_args.ContainsKey(nameof(WithPeerPassword))) _bgpPeer.PeerPassword = _args[nameof(WithPeerPassword)].ToString();

            Validate();
            return _bgpPeer;
        }

        protected internal virtual async Task SetRoutingInstanceAsync()
        {
            var routingInstanceId = (int)_args[nameof(ForRoutingInstance)];
            var routingInstance = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                              x =>
                                x.RoutingInstanceID == routingInstanceId,
                                AsTrackable: true,
                                includeProperties: "Vifs.Vlans.Vif.Attachment.Interfaces.Ports, " +
                                "Attachments.Interfaces.Attachment.Interfaces.Ports")
                                select result)
                                .SingleOrDefault();

            _bgpPeer.RoutingInstance = routingInstance;
        }

        protected internal virtual void Validate()
        {
            if (_bgpPeer.RoutingInstance == null) throw new BuilderIllegalStateException("A routing instance for the BGP peer is required.");
            if (_bgpPeer.Peer2ByteAutonomousSystem < 1 || _bgpPeer.Peer2ByteAutonomousSystem > 65535)
                throw new BuilderIllegalStateException("The 2 byte autonomous system number requested is not valid. The number must be between " +
                    "1 and 65535.");

            if (!IPAddress.TryParse(_bgpPeer.Ipv4PeerAddress, out IPAddress peerIpv4Address))
                throw new BuilderIllegalStateException("The peer address is not a valid IPv4 address");
            
            // For non-multihop peers, the peer IP address must be reachable from at least one vif or attachment which
            // belongs to the routing instance
            if (!_bgpPeer.IsMultiHop)
            {
                _bgpPeer.RoutingInstance.Vifs.SelectMany(
                    x => 
                     x.Vlans)
                    .ToList()
                    .ForEach(
                        x =>
                        {
                            var network = IPNetwork.Parse(x.IpAddress, x.SubnetMask);
                            if (!network.Contains(peerIpv4Address))
                            {
                                throw new BuilderIllegalStateException($"IP address '{_bgpPeer.Ipv4PeerAddress}' is not contained by the network " +
                                $"assigned to vif '{x.Vif.Name}' ({network.Network.ToString()}/{network.Cidr.ToString()}).");
                            }
                        });


                _bgpPeer.RoutingInstance.Attachments.SelectMany(x => x.Interfaces)
                    .ToList()
                    .ForEach(x =>
                    {
                        var network = IPNetwork.Parse(x.IpAddress, x.SubnetMask);
                        if (!network.Contains(peerIpv4Address))
                        {
                            throw new BuilderIllegalStateException($"IP address '{_bgpPeer.Ipv4PeerAddress}' is not contained by the network " +
                                $"assigned to attachment '{x.Attachment.Name}' ({network.Network.ToString()}/{network.Cidr.ToString()}).");
                        }
                    });
            }
        }
    }
}

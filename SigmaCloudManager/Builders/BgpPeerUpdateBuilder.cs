using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SCM.Data;
using SCM.Models;

namespace Mind.Builders
{
    public class BgpPeerUpdateBuilder :BgpPeerBuilder, IBgpPeerUpdateBuilder
    {
        public BgpPeerUpdateBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IBgpPeerUpdateBuilder ForBgpPeer(int? bgpPeerId)
        {
            if (bgpPeerId.HasValue) _args.Add(nameof(ForBgpPeer), bgpPeerId);
            return this;
        }

        IBgpPeerUpdateBuilder IBgpPeerUpdateBuilder.WithBfd(bool? isBfdEnabled)
        {
            base.WithBfd(isBfdEnabled);
            return this;
        }

        IBgpPeerUpdateBuilder IBgpPeerUpdateBuilder.WithIpv4PeerAddress(string ipv4PeerAddess)
        {
            base.WithIpv4PeerAddress(ipv4PeerAddess);
            return this;
        }

        IBgpPeerUpdateBuilder IBgpPeerUpdateBuilder.WithMaximumRoutes(int? maxRoutes)
        {
            base.WithMaximumRoutes(maxRoutes);
            return this;
        }

        IBgpPeerUpdateBuilder IBgpPeerUpdateBuilder.WithMultiHop(bool? isMultiHopEnabled)
        {
            base.WithMultiHop(isMultiHopEnabled);
            return this;
        }

        IBgpPeerUpdateBuilder IBgpPeerUpdateBuilder.WithPeer2ByteAutonomousSystem(int? peer2ByteASNumber)
        {
            base.WithPeer2ByteAutonomousSystem(peer2ByteASNumber);
            return this;
        }

        IBgpPeerUpdateBuilder IBgpPeerUpdateBuilder.WithPeerPassword(string password)
        {
            base.WithPeerPassword(password);
            return this;
        }

        public async Task<BgpPeer> UpdateAsync()
        {
            await SetBgpPeerAsync();
            if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) _bgpPeer.Ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            if (_args.ContainsKey(nameof(WithMaximumRoutes))) _bgpPeer.MaximumRoutes = (int)_args[nameof(WithMaximumRoutes)];
            if (_args.ContainsKey(nameof(WithBfd))) _bgpPeer.IsBfdEnabled = (bool)_args[nameof(WithBfd)];
            if (_args.ContainsKey(nameof(WithMultiHop))) _bgpPeer.IsMultiHop = (bool)_args[nameof(WithMultiHop)];
            if (_args.ContainsKey(nameof(WithPeer2ByteAutonomousSystem))) _bgpPeer.Peer2ByteAutonomousSystem = (int)_args[nameof(WithPeer2ByteAutonomousSystem)];
            if (_args.ContainsKey(nameof(WithPeerPassword))) _bgpPeer.PeerPassword = _args[nameof(WithPeerPassword)].ToString();

            Validate();
            return _bgpPeer;
        }

        protected internal virtual async Task SetBgpPeerAsync()
        {
            var bgpPeerId = (int)_args[nameof(ForBgpPeer)];
            var bgpPeer = (from result in await _unitOfWork.BgpPeerRepository.GetAsync(
                              x =>
                                x.BgpPeerID == bgpPeerId,
                                AsTrackable: true,
                                includeProperties: "RoutingInstance," +
                                "VpnTenantCommunitiesIn.AttachmentSet," +
                                "VpnTenantCommunitiesOut.AttachmentSet," +
                                "VpnTenantIpNetworksIn.AttachmentSet," +
                                "VpnTenantIpNetworksOut.AttachmentSet," +
                                "VpnTenantCommunitiesIn.TenantCommunity," +
                                "VpnTenantCommunitiesOut.TenantCommunity," +
                                "VpnTenantIpNetworksIn.TenantIpNetwork," +
                                "VpnTenantIpNetworksOut.TenantIpNetwork")
                                select result)
                                .SingleOrDefault();

            base._bgpPeer = bgpPeer ?? throw new BuilderUnableToCompleteException($"Could not find the BGP peer with ID '{bgpPeerId}'.");
        }

        protected override internal void Validate()
        {
            base.Validate();
        }
    }
}

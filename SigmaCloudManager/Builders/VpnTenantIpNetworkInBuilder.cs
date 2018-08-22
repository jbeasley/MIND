using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for vpn tenant IP networks. The buidler exposes a fluent UI
    /// </summary>
    public class VpnTenantIpNetworkInBuilder : BaseBuilder, IVpnTenantIpNetworkInBuilder
    {
        protected VpnTenantIpNetworkIn _vpnTenantIpNetworkIn;

        public VpnTenantIpNetworkInBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _vpnTenantIpNetworkIn = new VpnTenantIpNetworkIn();
        }

        public virtual IVpnTenantIpNetworkInBuilder ForAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId != null) _args.Add(nameof(ForAttachmentSet), attachmentSetId);
            return this;
        }

        public virtual IVpnTenantIpNetworkInBuilder WithTenantIpNetwork(int? tenantIpNetworkId)
        {
            if (tenantIpNetworkId != null) _args.Add(nameof(WithTenantIpNetwork), tenantIpNetworkId);
            return this;
        }

        public virtual IVpnTenantIpNetworkInBuilder AddToAllBgpPeersInAttachmentSet(bool? addToAllBgpPeersInAttachmentSet)
        {
            if (addToAllBgpPeersInAttachmentSet.HasValue) _args.Add(nameof(AddToAllBgpPeersInAttachmentSet), addToAllBgpPeersInAttachmentSet);
            return this;
        }

        public virtual IVpnTenantIpNetworkInBuilder WithIpv4PeerAddress(string ipv4PeerAddress)
        {
            if (!string.IsNullOrEmpty(ipv4PeerAddress)) _args.Add(nameof(WithIpv4PeerAddress), ipv4PeerAddress);
            return this;
        }

        public virtual IVpnTenantIpNetworkInBuilder WithLocalIpRoutingPreference(int? localIpRoutingPreference)
        {
            if (localIpRoutingPreference != null) _args.Add(nameof(WithLocalIpRoutingPreference), localIpRoutingPreference);
            return this;
        }

        public async Task<VpnTenantIpNetworkIn> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForAttachmentSet)))
            {
                _vpnTenantIpNetworkIn.AttachmentSetID = (int)_args[nameof(ForAttachmentSet)];
            }
            if (_args.ContainsKey(nameof(WithTenantIpNetwork)))
            {
                _vpnTenantIpNetworkIn.TenantIpNetworkID = (int)_args[nameof(WithTenantIpNetwork)];
            }
            if (_args.ContainsKey(nameof(WithLocalIpRoutingPreference))) { 
                _vpnTenantIpNetworkIn.LocalIpRoutingPreference = (int)_args[nameof(WithLocalIpRoutingPreference)];
            }
            if (_args.ContainsKey(nameof(AddToAllBgpPeersInAttachmentSet)))
            {
                _vpnTenantIpNetworkIn.AddToAllBgpPeersInAttachmentSet = (bool)_args[nameof(AddToAllBgpPeersInAttachmentSet)];
            }
            if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerAsync();

            return _vpnTenantIpNetworkIn;
        }

        protected virtual internal async Task SetIpv4BgpPeerAsync()
        {
            var ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            var bgpPeer = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                          q =>
                          q.AttachmentSetID == _vpnTenantIpNetworkIn.AttachmentSetID,
                          includeProperties: "AttachmentSetRoutingInstances.RoutingInstance.BgpPeers",
                          AsTrackable: false)
                           from attachmentSetRoutingInstance in result.AttachmentSetRoutingInstances
                           from bgpPeers in attachmentSetRoutingInstance.RoutingInstance.BgpPeers
                           select bgpPeers)
                                    .SingleOrDefault(x => x.Ipv4PeerAddress == ipv4PeerAddress);

            if (bgpPeer == null) throw new BuilderBadArgumentsException("Unable to create a new tenant IP network association an the attachment set using " +
                $"the given arguments. The BGP peer address '{ipv4PeerAddress}' does not exist within any routing instance which belongs to " +
                $"the attachment set.");

            _vpnTenantIpNetworkIn.BgpPeerID = bgpPeer.BgpPeerID;
        }
    }
}

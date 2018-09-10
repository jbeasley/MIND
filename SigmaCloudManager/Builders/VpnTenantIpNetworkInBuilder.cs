using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for tenant IP network associations with the inbound policy of attachment sets.
    /// The builder exposes a fluent UI
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

        public virtual IVpnTenantIpNetworkInBuilder WithTenant(int? tenantId)
        {
            if (tenantId != null) _args.Add(nameof(WithTenant), tenantId);
            return this;
        }

        public virtual IVpnTenantIpNetworkInBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName)
        {
            if (!string.IsNullOrEmpty(tenantIpNetworkCidrName)) _args.Add(nameof(WithTenantIpNetworkCidrName), tenantIpNetworkCidrName);
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

            if (_args.ContainsKey(nameof(WithTenantIpNetworkCidrName))) await SetTenantIpNetworkAsync();

            if (_args.ContainsKey(nameof(WithLocalIpRoutingPreference)))
            {
                _vpnTenantIpNetworkIn.LocalIpRoutingPreference = (int)_args[nameof(WithLocalIpRoutingPreference)];
            }

            if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerAsync();

            if (_args.ContainsKey(nameof(AddToAllBgpPeersInAttachmentSet))) SetAddToAllBgpPeersInAttachmentSet();

            return _vpnTenantIpNetworkIn;
        }

        protected virtual internal async Task SetAttachmentSetAsync()
        {
            var attachmentSetId = (int)_args[nameof(ForAttachmentSet)];
            var attachmentSet = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                q => q.AttachmentSetID == attachmentSetId, AsTrackable: true)
                                 select result)
                                 .SingleOrDefault();

            _vpnTenantIpNetworkIn.AttachmentSet = attachmentSet;
        }

        protected virtual internal async Task SetTenantIpNetworkAsync()
        {
            var tenantIpNetworkCidrName = _args[nameof(WithTenantIpNetworkCidrName)].ToString();
            var tenantId = (int)_args[nameof(WithTenant)];

            var tenantIpNetwork = (from result in await _unitOfWork.TenantIpNetworkRepository.GetAsync(
                              q =>
                                q.TenantID == tenantId
                                && q.CidrNameIncludingIpv4LessThanOrEqualToLength == tenantIpNetworkCidrName,
                                AsTrackable: true)
                                   select result)
                                   .SingleOrDefault();

            _vpnTenantIpNetworkIn.TenantIpNetwork = tenantIpNetwork;
        }

        protected virtual internal async Task SetIpv4BgpPeerAsync()
        {
            var ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            var bgpPeer = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                       q =>
                          q.AttachmentSetID == _vpnTenantIpNetworkIn.AttachmentSetID,
                          includeProperties: "AttachmentSetRoutingInstances.RoutingInstance.BgpPeers",
                          AsTrackable: true)
                           from attachmentSetRoutingInstance in result.AttachmentSetRoutingInstances
                           from bgpPeers in attachmentSetRoutingInstance.RoutingInstance.BgpPeers
                           select bgpPeers)
                           .SingleOrDefault(
                                x =>
                                    x.Ipv4PeerAddress == ipv4PeerAddress);

            _vpnTenantIpNetworkIn.AddToAllBgpPeersInAttachmentSet = false;
            _vpnTenantIpNetworkIn.BgpPeer = bgpPeer ?? throw new BuilderBadArgumentsException("Unable to create a new tenant IP network association " +
                $"with the attachment set using the given arguments. The BGP peer address '{ipv4PeerAddress}' does not " +
                $"exist within any routing instance which belongs to the attachment set.");
        }

        protected virtual internal void SetAddToAllBgpPeersInAttachmentSet()
        {
            var addToAllBgpPeersInAttachmentSet = (bool)_args[nameof(AddToAllBgpPeersInAttachmentSet)];
            if (_vpnTenantIpNetworkIn.BgpPeerID != null)
            {
                _vpnTenantIpNetworkIn.AddToAllBgpPeersInAttachmentSet = false;
            }
            else
            {
                _vpnTenantIpNetworkIn.AddToAllBgpPeersInAttachmentSet = true;
            }
        }

        /// <summary>
        /// Validate the state of the vpn tenant IP network.
        /// </summary>
        protected virtual internal void Validate()
        {
            if (_vpnTenantIpNetworkIn.AttachmentSet == null) throw new BuilderIllegalStateException("An attachment set association with the " +
                "tenant IP network is required but was not found.");
        
            if (_vpnTenantIpNetworkIn.TenantIpNetwork == null)
                throw new BuilderIllegalStateException("Unable to create a new tenant IP network association with the " +
                $"attachment set using the given arguments. The tenant IP network CIDR block '{_args[nameof(WithTenantIpNetworkCidrName)].ToString()}' " +
                $"does not exist for the given tenant with ID of '{(int)_args[nameof(WithTenant)]}'.");

            if (_vpnTenantIpNetworkIn.AddToAllBgpPeersInAttachmentSet)
            {
                if (_vpnTenantIpNetworkIn.BgpPeer != null)
                {
                    throw new BuilderIllegalStateException($"A BGP peer association with the tenant IP network '{_vpnTenantIpNetworkIn.TenantIpNetwork.CidrName}' " +
                        "was found but is not required because the request is to add the tenant IP network to all bgp peers in attachment set " +
                        $"'{_vpnTenantIpNetworkIn.AttachmentSet.Name}'.");
                }
            }
            else
            {
                if (_vpnTenantIpNetworkIn.BgpPeer == null)
                {
                    throw new BuilderIllegalStateException($"A BGP peer association with the tenant IP network '{_vpnTenantIpNetworkIn.TenantIpNetwork.CidrName}' " +
                        "is required because the request is to add the tenant IP network to a specific bgp peer in attachment set " +
                        $"'{_vpnTenantIpNetworkIn.AttachmentSet.Name}'.");
                }
            }
        }
    }
}


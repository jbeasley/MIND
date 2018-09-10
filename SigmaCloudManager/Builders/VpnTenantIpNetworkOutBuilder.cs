﻿using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for tenant IP network associations with the outbound policy of attachment sets.
    /// The buidler exposes a fluent UI
    /// </summary>
    public class VpnTenantIpNetworkOutBuilder : BaseBuilder, IVpnTenantIpNetworkOutBuilder
    {
        protected VpnTenantIpNetworkOut _vpnTenantIpNetworkOut;

        public VpnTenantIpNetworkOutBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _vpnTenantIpNetworkOut = new VpnTenantIpNetworkOut();
        }

        public virtual IVpnTenantIpNetworkOutBuilder ForAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId != null) _args.Add(nameof(ForAttachmentSet), attachmentSetId);
            return this;
        }

        public virtual IVpnTenantIpNetworkOutBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName)
        {
            if (!string.IsNullOrEmpty(tenantIpNetworkCidrName)) _args.Add(nameof(WithTenantIpNetworkCidrName), tenantIpNetworkCidrName);
            return this;
        }

        public virtual IVpnTenantIpNetworkOutBuilder WithIpv4PeerAddress(string ipv4PeerAddress)
        {
            if (!string.IsNullOrEmpty(ipv4PeerAddress)) _args.Add(nameof(WithIpv4PeerAddress), ipv4PeerAddress);
            return this;
        }

        public virtual IVpnTenantIpNetworkOutBuilder WithAdvertisedIpRoutingPreference(int? advertisedIpRoutingPreference)
        {
            if (advertisedIpRoutingPreference != null) _args.Add(nameof(WithAdvertisedIpRoutingPreference), advertisedIpRoutingPreference);
            return this;
        }

        public async Task<VpnTenantIpNetworkOut> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForAttachmentSet))) await SetAttachmentSetAsync();

            if (_args.ContainsKey(nameof(WithTenantIpNetworkCidrName))) await SetTenantIpNetworkAsync();

            if (_args.ContainsKey(nameof(WithAdvertisedIpRoutingPreference))) { 
                _vpnTenantIpNetworkOut.AdvertisedIpRoutingPreference = (int)_args[nameof(WithAdvertisedIpRoutingPreference)];
            }

            if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerAsync();

            return _vpnTenantIpNetworkOut;
        }

        protected virtual internal async Task SetAttachmentSetAsync()
        {
            var attachmentSetId = (int)_args[nameof(ForAttachmentSet)];
            var attachmentSet = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                q => q.AttachmentSetID == attachmentSetId, AsTrackable: true)
                                 select result)
                                 .SingleOrDefault();

            _vpnTenantIpNetworkOut.AttachmentSet = attachmentSet;
        }

        protected virtual internal async Task SetTenantIpNetworkAsync()
        {
            var tenantIpNetworkCidrName = _args[nameof(WithTenantIpNetworkCidrName)].ToString();
            var tenantIpNetwork = (from result in await _unitOfWork.TenantIpNetworkRepository.GetAsync(
                              q =>
                              q.TenantID == _vpnTenantIpNetworkOut.AttachmentSet.TenantID &&
                              q.CidrNameIncludingIpv4LessThanOrEqualToLength == tenantIpNetworkCidrName,
                              AsTrackable: true)
                                   select result)
                                 .SingleOrDefault();

            _vpnTenantIpNetworkOut.TenantIpNetwork = tenantIpNetwork ?? throw new BuilderBadArgumentsException("Unable to create a new tenant IP network association with the attachment set using " +
                $"the given arguments. The tenant IP network CIDR name '{tenantIpNetworkCidrName}' does not exist.");
        }

        protected virtual internal async Task SetIpv4BgpPeerAsync()
        {
            var ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            var bgpPeer = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                          q =>
                          q.AttachmentSetID == _vpnTenantIpNetworkOut.AttachmentSetID,
                          includeProperties: "AttachmentSetRoutingInstances.RoutingInstance.BgpPeers",
                          AsTrackable: false)
                           from attachmentSetRoutingInstance in result.AttachmentSetRoutingInstances
                           from bgpPeers in attachmentSetRoutingInstance.RoutingInstance.BgpPeers
                           select bgpPeers)
                                    .SingleOrDefault(x => x.Ipv4PeerAddress == ipv4PeerAddress);

            _vpnTenantIpNetworkOut.BgpPeer = bgpPeer ?? throw new BuilderBadArgumentsException("Unable to create a new tenant IP network association with the attachment set using " +
                $"the given arguments. The BGP peer address '{ipv4PeerAddress}' does not exist within any routing instance which belongs to " +
                $"the attachment set.");
        }

        /// <summary>
        /// Validate the state of the vpn tenant IP network.
        /// </summary>
        protected virtual internal void Validate()
        {
            if (_vpnTenantIpNetworkOut.AttachmentSet == null) throw new BuilderIllegalStateException("An attachment set association with the " +
                "tenant IP network is required but was not found.");

            if (_vpnTenantIpNetworkOut.TenantIpNetwork == null)
                throw new BuilderIllegalStateException("Unable to create a new tenant IP network association with the " +
                $"attachment set using the given arguments. The tenant IP network CIDR block '{_args[nameof(WithTenantIpNetworkCidrName)].ToString()}' " +
                $"could not be found.");

            if (_vpnTenantIpNetworkOut.BgpPeer == null)
            {
                throw new BuilderIllegalStateException($"A BGP peer association with the tenant IP network '{_vpnTenantIpNetworkOut.TenantIpNetwork.CidrName}' " +
                    "was not found.");
            }
        }
    }
}

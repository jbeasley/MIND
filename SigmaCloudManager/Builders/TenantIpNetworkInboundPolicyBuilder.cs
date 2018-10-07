using Microsoft.EntityFrameworkCore;
using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for tenant IP network associations with inbound routing policies
    /// The builder exposes a fluent UI
    /// </summary>
    public class TenantIpNetworkInboundPolicyBuilder : BaseBuilder, ITenantIpNetworkInboundPolicyBuilder
    {
        protected VpnTenantIpNetworkIn _vpnTenantIpNetworkIn;

        public TenantIpNetworkInboundPolicyBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _vpnTenantIpNetworkIn = new VpnTenantIpNetworkIn();
        }

        public virtual ITenantIpNetworkInboundPolicyBuilder ForAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId != null) _args.Add(nameof(ForAttachmentSet), attachmentSetId);
            return this;
        }

        public virtual ITenantIpNetworkInboundPolicyBuilder ForDevice(int? deviceId)
        {
            if (deviceId != null) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        public ITenantIpNetworkInboundPolicyBuilder ForTenantIpNetworkInboundPolicy(int vpnTenantIpNetworkInId)
        {
            _args.Add(nameof(ForTenantIpNetworkInboundPolicy), vpnTenantIpNetworkInId);
            return this;
        }

        /// <summary>
        /// The owning tenant of the tenant IP network. Multiple tenants may be allocated the same CIDR block so the 
        /// tenant owner is required in order to identify the correct CIDR block.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public virtual ITenantIpNetworkInboundPolicyBuilder WithTenantOwner(int? tenantId)
        {
            if (tenantId != null) _args.Add(nameof(WithTenantOwner), tenantId);
            return this;
        }

        public virtual ITenantIpNetworkInboundPolicyBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName)
        {
            if (!string.IsNullOrEmpty(tenantIpNetworkCidrName)) _args.Add(nameof(WithTenantIpNetworkCidrName), tenantIpNetworkCidrName);
            return this;
        }

        public virtual ITenantIpNetworkInboundPolicyBuilder AddToAllBgpPeersInAttachmentSet(bool? addToAllBgpPeersInAttachmentSet)
        {
            if (addToAllBgpPeersInAttachmentSet.HasValue) _args.Add(nameof(AddToAllBgpPeersInAttachmentSet), addToAllBgpPeersInAttachmentSet);
            return this;
        }

        public virtual ITenantIpNetworkInboundPolicyBuilder WithIpv4PeerAddress(string ipv4PeerAddress)
        {
            if (!string.IsNullOrEmpty(ipv4PeerAddress)) _args.Add(nameof(WithIpv4PeerAddress), ipv4PeerAddress);
            return this;
        }

        public virtual ITenantIpNetworkInboundPolicyBuilder WithLocalIpRoutingPreference(int? localIpRoutingPreference)
        {
            if (localIpRoutingPreference != null) _args.Add(nameof(WithLocalIpRoutingPreference), localIpRoutingPreference);
            return this;
        }

        public virtual async Task<VpnTenantIpNetworkIn> BuildAsync()
        {
            // Check to build components for an attachment set
            if (_args.ContainsKey(nameof(ForAttachmentSet)))
            {
                await SetAttachmentSetAsync();
                if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerForAttachmentSetAsync();
                if (_args.ContainsKey(nameof(AddToAllBgpPeersInAttachmentSet))) SetAddToAllBgpPeersInAttachmentSet();
            }

            // Check to update components for an existing inbound policy
            if (_args.ContainsKey(nameof(ForTenantIpNetworkInboundPolicy)))
            {
                await SetTenantIpNetworkInboundPolicyAsync();
                if (_vpnTenantIpNetworkIn.AttachmentSet == null)
                {
                    if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerForDeviceAsync();
                }
                else
                {
                    if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerForAttachmentSetAsync();
                    if (_args.ContainsKey(nameof(AddToAllBgpPeersInAttachmentSet))) SetAddToAllBgpPeersInAttachmentSet();
                }           
            }

            // Check to build components for a device - i.e. there should be no attachment set association
            if (_args.ContainsKey(nameof(ForDevice)))
            {
                if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerForDeviceAsync();
            }

            if (_args.ContainsKey(nameof(WithTenantIpNetworkCidrName)) && _args.ContainsKey(nameof(WithTenantOwner))) await SetTenantIpNetworkAsync();
            if (_args.ContainsKey(nameof(WithLocalIpRoutingPreference)))
                _vpnTenantIpNetworkIn.LocalIpRoutingPreference = (int)_args[nameof(WithLocalIpRoutingPreference)];
           
            _vpnTenantIpNetworkIn.Validate();
            return _vpnTenantIpNetworkIn;
        }

        protected virtual internal async Task SetAttachmentSetAsync()
        {
            var attachmentSetId = (int)_args[nameof(ForAttachmentSet)];
            var attachmentSet = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                                    q => 
                                        q.AttachmentSetID == attachmentSetId, 
                                        query: q => q.IncludeValidationProperties(),
                                        AsTrackable: true)
                                select result)
                                .SingleOrDefault();

            _vpnTenantIpNetworkIn.AttachmentSet = attachmentSet;
        }

        private async Task SetTenantIpNetworkInboundPolicyAsync()
        {
            var vpnTenantIpNetworkInId = (int)_args[nameof(ForTenantIpNetworkInboundPolicy)];
            var vpnTenantIpNetworkIn = (from result in await _unitOfWork.VpnTenantIpNetworkInRepository.GetAsync(
                                      q =>
                                        q.VpnTenantIpNetworkInID == vpnTenantIpNetworkInId,
                                        query: x => x.IncludeValidationProperties(),
                                        AsTrackable: true)
                                        select result)
                                       .SingleOrDefault();

            _vpnTenantIpNetworkIn = vpnTenantIpNetworkIn ?? throw new BuilderUnableToCompleteException("The tenant IP network inbound policy with ID " +
                $"'{vpnTenantIpNetworkInId}' was not found.");
        }

        protected virtual internal async Task SetIpv4BgpPeerForDeviceAsync()
        {
            // Try to find the device ID, first from supplied args, then from an existing BGP peer
            var deviceId = _args.ContainsKey(nameof(ForDevice)) ? (int)_args[nameof(ForDevice)] : _vpnTenantIpNetworkIn.BgpPeer?.RoutingInstance?.DeviceID;
            if (!deviceId.HasValue) throw new BuilderBadArgumentsException("Unable to complete creating the inbound policy. A device was not found.");

            var ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            var bgpPeer = (from result in await _unitOfWork.BgpPeerRepository.GetAsync(
                                   q =>
                                      q.RoutingInstance.DeviceID == deviceId &&
                                      q.Ipv4PeerAddress == ipv4PeerAddress,
                                      query: q => q.IncludeValidationProperties(),
                                      AsTrackable: true)
                           select result)
                           .SingleOrDefault();

            _vpnTenantIpNetworkIn.BgpPeer = bgpPeer;
        }

        protected virtual internal async Task SetTenantIpNetworkAsync()
        {
            var tenantIpNetworkCidrName = _args[nameof(WithTenantIpNetworkCidrName)].ToString();
            var tenantOwnerId = (int)_args[nameof(WithTenantOwner)];
            var tenantIpNetwork = (from result in await _unitOfWork.TenantIpNetworkRepository.GetAsync(
                              q =>
                                   q.TenantID == tenantOwnerId && 
                                   q.CidrNameIncludingIpv4LessThanOrEqualToLength == tenantIpNetworkCidrName,
                                   AsTrackable: true)
                                   select result)
                                   .SingleOrDefault();

            _vpnTenantIpNetworkIn.TenantIpNetwork = tenantIpNetwork;
        }

        protected virtual internal async Task SetIpv4BgpPeerForAttachmentSetAsync()
        {
            var ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            var bgpPeer = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                       q =>
                          q.AttachmentSetID == _vpnTenantIpNetworkIn.AttachmentSet.AttachmentSetID,
                          query: q => q.IncludeValidationProperties(),
                          AsTrackable: true)
                           from attachmentSetRoutingInstance in result.AttachmentSetRoutingInstances
                           from bgpPeers in attachmentSetRoutingInstance.RoutingInstance.BgpPeers
                           select bgpPeers)
                           .SingleOrDefault(
                                x =>
                                    x.Ipv4PeerAddress == ipv4PeerAddress);

            _vpnTenantIpNetworkIn.BgpPeer = bgpPeer;
        }

        protected virtual internal void SetAddToAllBgpPeersInAttachmentSet()
        {
            var addToAllBgpPeersInAttachmentSet = (bool)_args[nameof(AddToAllBgpPeersInAttachmentSet)];
            _vpnTenantIpNetworkIn.AddToAllBgpPeersInAttachmentSet = addToAllBgpPeersInAttachmentSet;
            if (addToAllBgpPeersInAttachmentSet)
            {
                // Clear any previously set bgp peer
                _vpnTenantIpNetworkIn.BgpPeer = null;
                _vpnTenantIpNetworkIn.BgpPeerID = null;
            }
        }
    }
}


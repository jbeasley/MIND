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
    /// Builder for tenant community associations with inbound routing policies
    /// The builder exposes a fluent UI
    /// </summary>
    public class TenantCommunityInboundPolicyBuilder : BaseBuilder, ITenantCommunityInboundPolicyBuilder
    {
        protected VpnTenantCommunityIn _vpnTenantCommunityIn;

        public TenantCommunityInboundPolicyBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _vpnTenantCommunityIn = new VpnTenantCommunityIn();
        }

        public virtual ITenantCommunityInboundPolicyBuilder ForAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId != null) _args.Add(nameof(ForAttachmentSet), attachmentSetId);
            return this;
        }

        public virtual ITenantCommunityInboundPolicyBuilder ForDevice(int? deviceId)
        {
            if (deviceId != null) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        public ITenantCommunityInboundPolicyBuilder ForTenantCommunityInboundPolicy(int vpnTenantCommunityInId)
        {
            _args.Add(nameof(ForTenantCommunityInboundPolicy), vpnTenantCommunityInId);
            return this;
        }

        /// <summary>
        /// The owning tenant of the tenant community. Multiple tenants may be allocated the same community so the 
        /// tenant owner is required in order to identify the correct one.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public virtual ITenantCommunityInboundPolicyBuilder WithTenantOwner(int? tenantId)
        {
            if (tenantId != null) _args.Add(nameof(WithTenantOwner), tenantId);
            return this;
        }

        public virtual ITenantCommunityInboundPolicyBuilder WithTenantCommunityName(string tenantCommunityName)
        {
            if (!string.IsNullOrEmpty(tenantCommunityName)) _args.Add(nameof(WithTenantCommunityName), tenantCommunityName);
            return this;
        }

        public virtual ITenantCommunityInboundPolicyBuilder AddToAllBgpPeersInAttachmentSet(bool? addToAllBgpPeersInAttachmentSet)
        {
            if (addToAllBgpPeersInAttachmentSet.HasValue) _args.Add(nameof(AddToAllBgpPeersInAttachmentSet), addToAllBgpPeersInAttachmentSet);
            return this;
        }

        public virtual ITenantCommunityInboundPolicyBuilder WithIpv4PeerAddress(string ipv4PeerAddress)
        {
            if (!string.IsNullOrEmpty(ipv4PeerAddress)) _args.Add(nameof(WithIpv4PeerAddress), ipv4PeerAddress);
            return this;
        }

        public virtual ITenantCommunityInboundPolicyBuilder WithLocalIpRoutingPreference(int? localIpRoutingPreference)
        {
            if (localIpRoutingPreference != null) _args.Add(nameof(WithLocalIpRoutingPreference), localIpRoutingPreference);
            return this;
        }

        public virtual async Task<VpnTenantCommunityIn> BuildAsync()
        {
            // Check to build components for an attachment set
            if (_args.ContainsKey(nameof(ForAttachmentSet)))
            {
                await SetAttachmentSetAsync();
                if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerForAttachmentSetAsync();
                if (_args.ContainsKey(nameof(AddToAllBgpPeersInAttachmentSet))) SetAddToAllBgpPeersInAttachmentSet();
            }

            // Check to update components for an existing inbound policy
            if (_args.ContainsKey(nameof(ForTenantCommunityInboundPolicy)))
            {
                await SetTenantCommunityInboundPolicyAsync();
                if (_vpnTenantCommunityIn.AttachmentSet == null)
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

            if (_args.ContainsKey(nameof(WithTenantCommunityName)) && _args.ContainsKey(nameof(WithTenantOwner))) await SetTenantCommunityAsync();
            if (_args.ContainsKey(nameof(WithLocalIpRoutingPreference)))
                _vpnTenantCommunityIn.LocalIpRoutingPreference = (int)_args[nameof(WithLocalIpRoutingPreference)];
           
            _vpnTenantCommunityIn.Validate();
            return _vpnTenantCommunityIn;
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

            _vpnTenantCommunityIn.AttachmentSet = attachmentSet;
        }

        private async Task SetTenantCommunityInboundPolicyAsync()
        {
            var vpnTenantCommunityInId = (int)_args[nameof(ForTenantCommunityInboundPolicy)];
            var vpnTenantCommunityIn = (from result in await _unitOfWork.VpnTenantCommunityInRepository.GetAsync(
                                      q =>
                                        q.VpnTenantCommunityInID == vpnTenantCommunityInId,
                                        query: x => x.IncludeValidationProperties(),
                                        AsTrackable: true)
                                        select result)
                                       .SingleOrDefault();

            _vpnTenantCommunityIn = vpnTenantCommunityIn ?? throw new BuilderUnableToCompleteException("The tenant community inbound policy with ID " +
                $"'{vpnTenantCommunityInId}' was not found.");
        }

        protected virtual internal async Task SetIpv4BgpPeerForDeviceAsync()
        {
            // Try to find the device ID, first from supplied args, then from an existing BGP peer
            var deviceId = _args.ContainsKey(nameof(ForDevice)) ? (int)_args[nameof(ForDevice)] : _vpnTenantCommunityIn.BgpPeer?.RoutingInstance?.DeviceID;
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

            _vpnTenantCommunityIn.BgpPeer = bgpPeer;
        }

        protected virtual internal async Task SetTenantCommunityAsync()
        {
            var tenantCommunityName = _args[nameof(WithTenantCommunityName)].ToString();
            var tenantOwnerId = (int)_args[nameof(WithTenantOwner)];
            var tenantCommunity = (from result in await _unitOfWork.TenantCommunityRepository.GetAsync(
                              q =>
                                   q.TenantID == tenantOwnerId && 
                                   q.Name == tenantCommunityName,
                                   AsTrackable: true)
                                   select result)
                                   .SingleOrDefault();

            _vpnTenantCommunityIn.TenantCommunity = tenantCommunity;
        }

        protected virtual internal async Task SetIpv4BgpPeerForAttachmentSetAsync()
        {
            var ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            var bgpPeer = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                       q =>
                          q.AttachmentSetID == _vpnTenantCommunityIn.AttachmentSet.AttachmentSetID,
                          query: q => q.IncludeValidationProperties(),
                          AsTrackable: true)
                           from attachmentSetRoutingInstance in result.AttachmentSetRoutingInstances
                           from bgpPeers in attachmentSetRoutingInstance.RoutingInstance.BgpPeers
                           select bgpPeers)
                           .SingleOrDefault(
                                x =>
                                    x.Ipv4PeerAddress == ipv4PeerAddress);

            _vpnTenantCommunityIn.BgpPeer = bgpPeer;
        }

        protected virtual internal void SetAddToAllBgpPeersInAttachmentSet()
        {
            var addToAllBgpPeersInAttachmentSet = (bool)_args[nameof(AddToAllBgpPeersInAttachmentSet)];
            _vpnTenantCommunityIn.AddToAllBgpPeersInAttachmentSet = addToAllBgpPeersInAttachmentSet;
            if (addToAllBgpPeersInAttachmentSet)
            {
                // Clear any previously set bgp peer
                _vpnTenantCommunityIn.BgpPeer = null;
                _vpnTenantCommunityIn.BgpPeerID = null;
            }
        }
    }
}


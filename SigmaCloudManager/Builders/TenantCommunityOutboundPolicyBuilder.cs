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
    /// Builder for tenant community associations with the outbound policy of attachment sets.
    /// The buidler exposes a fluent UI
    /// </summary>
    public class TenantCommunityOutboundPolicyBuilder : BaseBuilder, ITenantCommunityOutboundPolicyBuilder
    {
        protected VpnTenantCommunityOut _vpnTenantCommunityOut;

        public TenantCommunityOutboundPolicyBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _vpnTenantCommunityOut = new VpnTenantCommunityOut();
        }

        public virtual ITenantCommunityOutboundPolicyBuilder ForAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId != null) _args.Add(nameof(ForAttachmentSet), attachmentSetId);
            return this;
        }

        public virtual ITenantCommunityOutboundPolicyBuilder ForDevice(int? deviceId)
        {
            if (deviceId != null) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        public virtual ITenantCommunityOutboundPolicyBuilder ForTenantCommunityOutboundPolicy(int VpnTenantCommunityOutId)
        {
            _args.Add(nameof(ForTenantCommunityOutboundPolicy), VpnTenantCommunityOutId);
            return this;
        }

        public virtual ITenantCommunityOutboundPolicyBuilder WithTenantCommunityName(string tenantCommunityName)
        {
            if (!string.IsNullOrEmpty(tenantCommunityName)) _args.Add(nameof(WithTenantCommunityName), tenantCommunityName);
            return this;
        }

        public virtual ITenantCommunityOutboundPolicyBuilder WithIpv4PeerAddress(string ipv4PeerAddress)
        {
            if (!string.IsNullOrEmpty(ipv4PeerAddress)) _args.Add(nameof(WithIpv4PeerAddress), ipv4PeerAddress);
            return this;
        }

        public virtual ITenantCommunityOutboundPolicyBuilder WithAdvertisedIpRoutingPreference(int? advertisedIpRoutingPreference)
        {
            if (advertisedIpRoutingPreference != null) _args.Add(nameof(WithAdvertisedIpRoutingPreference), advertisedIpRoutingPreference);
            return this;
        }

        public virtual async Task<VpnTenantCommunityOut> BuildAsync()
        {
            // Check to build components for an attachment set
            if (_args.ContainsKey(nameof(ForAttachmentSet)))
            {
                await SetAttachmentSetAsync();
                if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerForAttachmentSetAsync();
                if (_args.ContainsKey(nameof(WithTenantCommunityName))) await SetTenantCommunityForAttachmentSetAsync();
            }

            // Check to update components for an existing outbound policy
            if (_args.ContainsKey(nameof(ForTenantCommunityOutboundPolicy)))
            {
                await SetTenantCommunityOutboundPolicyAsync();
                if (_vpnTenantCommunityOut.AttachmentSet == null)
                {
                    if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerForDeviceAsync();
                }
                else
                {
                    if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerForAttachmentSetAsync();
                }
            }

            // Check to build components for a device - i.e. there should be no attachment set association
            if (_args.ContainsKey(nameof(ForDevice)))
            {
                if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerForDeviceAsync();
                if (_args.ContainsKey(nameof(WithTenantCommunityName))) await SetTenantCommunityForBgpPeerAsync();
            }

            if (_args.ContainsKey(nameof(ForTenantCommunityOutboundPolicy))) await SetTenantCommunityOutboundPolicyAsync();

            if (_args.ContainsKey(nameof(WithAdvertisedIpRoutingPreference))) { 
                _vpnTenantCommunityOut.AdvertisedIpRoutingPreference = (int)_args[nameof(WithAdvertisedIpRoutingPreference)];
            }

            _vpnTenantCommunityOut.Validate();

            return _vpnTenantCommunityOut;
        }

        protected virtual internal async Task SetAttachmentSetAsync()
        {
            var attachmentSetId = (int)_args[nameof(ForAttachmentSet)];
            var attachmentSet = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                            q => 
                                 q.AttachmentSetID == attachmentSetId, 
                                 AsTrackable: true)
                                 select result)
                                 .SingleOrDefault();

            _vpnTenantCommunityOut.AttachmentSet = attachmentSet;
        }

        private async Task SetTenantCommunityOutboundPolicyAsync()
        {
            var vpnTenantCommunityOutId = (int)_args[nameof(ForTenantCommunityOutboundPolicy)];
            var vpnTenantCommunityOut = (from result in await _unitOfWork.VpnTenantCommunityOutRepository.GetAsync(
                                      q =>
                                        q.VpnTenantCommunityOutID == vpnTenantCommunityOutId,
                                        query: x => x.IncludeValidationProperties(),
                                        AsTrackable: true)
                                         select result)
                                        .SingleOrDefault();

            if (_vpnTenantCommunityOut == null) throw new BuilderUnableToCompleteException("The tenant community outbound policy with ID " +
                $"'{vpnTenantCommunityOutId}' was not found.");

            _vpnTenantCommunityOut = vpnTenantCommunityOut;
        }

        protected virtual internal async Task SetTenantCommunityForAttachmentSetAsync()
        {
            var tenantCommunityName = _args[nameof(WithTenantCommunityName)].ToString();
            var tenantCommunity = (from result in await _unitOfWork.TenantCommunityRepository.GetAsync(
                              q =>
                              q.TenantID == _vpnTenantCommunityOut.AttachmentSet.TenantID &&
                              q.Name == tenantCommunityName,
                              AsTrackable: true)
                                   select result)
                                 .SingleOrDefault();

            _vpnTenantCommunityOut.TenantCommunity = tenantCommunity ?? throw new BuilderBadArgumentsException("Unable to create a tenant community association with the attachment set using " +
                $"the given arguments. The tenant community name '{tenantCommunityName}' does not exist.");
        }

        protected virtual internal async Task SetTenantCommunityForBgpPeerAsync()
        {
            var tenantCommunityCidrName = _args[nameof(WithTenantCommunityName)].ToString();
            var tenantCommunity = (from result in await _unitOfWork.TenantCommunityRepository.GetAsync(
                              q =>
                              q.TenantID == _vpnTenantCommunityOut.BgpPeer.RoutingInstance.Device.TenantID &&
                              q.Name == tenantCommunityCidrName,
                              AsTrackable: true)
                                   select result)
                                 .SingleOrDefault();

            _vpnTenantCommunityOut.TenantCommunity = tenantCommunity ?? throw new BuilderBadArgumentsException("Unable to create a tenant community association with the BGP peer using " +
                $"the given arguments. The tenant community name '{tenantCommunityCidrName}' does not exist.");
        }

        protected virtual internal async Task SetIpv4BgpPeerForAttachmentSetAsync()
        {
            var ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            var bgpPeer = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                          q =>
                          q.AttachmentSetID == _vpnTenantCommunityOut.AttachmentSet.AttachmentSetID,
                          query: q => q.IncludeValidationProperties(),
                          AsTrackable: true)
                           from attachmentSetRoutingInstance in result.AttachmentSetRoutingInstances
                           from bgpPeers in attachmentSetRoutingInstance.RoutingInstance.BgpPeers
                           select bgpPeers)
                                    .SingleOrDefault(x => x.Ipv4PeerAddress == ipv4PeerAddress);

            _vpnTenantCommunityOut.BgpPeer = bgpPeer ?? throw new BuilderBadArgumentsException("Unable to create a tenant community association with the attachment set using " +
                $"the given arguments. The BGP peer address '{ipv4PeerAddress}' does not exist within any routing instance which belongs to " +
                $"the attachment set.");
        }

        protected virtual internal async Task SetIpv4BgpPeerForDeviceAsync()
        {
            // Try to find the device ID, first from supplied args, then from an existing BGP peer
            var deviceId = _args.ContainsKey(nameof(ForDevice)) ? (int)_args[nameof(ForDevice)] : _vpnTenantCommunityOut.BgpPeer?.RoutingInstance?.DeviceID;
            if (!deviceId.HasValue) throw new BuilderBadArgumentsException("Unable to complete creating the outbound policy. A device was not found.");

            var ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            var bgpPeer = (from result in await _unitOfWork.BgpPeerRepository.GetAsync(
                                   q =>
                                      q.RoutingInstance.DeviceID == deviceId &&
                                      q.Ipv4PeerAddress == ipv4PeerAddress,
                                      query: q => q.IncludeValidationProperties(),
                                      AsTrackable: true)
                           select result)
                           .SingleOrDefault();

            _vpnTenantCommunityOut.BgpPeer = bgpPeer ?? throw new BuilderBadArgumentsException("Unable to create a tenant community association with the" +
              $"the given arguments. The BGP peer address '{ipv4PeerAddress}' does not exist within any routing instance which belongs to " +
              "the given device.");
        }
    }
}

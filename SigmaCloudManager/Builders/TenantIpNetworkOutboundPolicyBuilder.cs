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
    /// Builder for tenant IP network associations with the outbound policy of attachment sets.
    /// The buidler exposes a fluent UI
    /// </summary>
    public class TenantIpNetworkOutboundPolicyBuilder : BaseBuilder, ITenantIpNetworkOutboundPolicyBuilder
    {
        protected VpnTenantIpNetworkOut _vpnTenantIpNetworkOut;

        public TenantIpNetworkOutboundPolicyBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _vpnTenantIpNetworkOut = new VpnTenantIpNetworkOut();
        }

        public virtual ITenantIpNetworkOutboundPolicyBuilder ForAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId != null) _args.Add(nameof(ForAttachmentSet), attachmentSetId);
            return this;
        }

        public virtual ITenantIpNetworkOutboundPolicyBuilder ForAttachmentSet(AttachmentSet attachmentSet)
        {
            if (attachmentSet != null) _args.Add(nameof(ForAttachmentSet), attachmentSet);
            return this;
        }

        public virtual ITenantIpNetworkOutboundPolicyBuilder ForDevice(int? deviceId)
        {
            if (deviceId != null) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        public virtual ITenantIpNetworkOutboundPolicyBuilder ForTenantIpNetworkOutboundPolicy(int? vpnTenantIpNetworkOutId)
        {
            if (vpnTenantIpNetworkOutId.HasValue) _args.Add(nameof(ForTenantIpNetworkOutboundPolicy), vpnTenantIpNetworkOutId);
            return this;
        }

        public virtual ITenantIpNetworkOutboundPolicyBuilder WithTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(WithTenant), tenantId);
            return this;
        }

        public virtual ITenantIpNetworkOutboundPolicyBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName)
        {
            if (!string.IsNullOrEmpty(tenantIpNetworkCidrName)) _args.Add(nameof(WithTenantIpNetworkCidrName), tenantIpNetworkCidrName);
            return this;
        }

        public virtual ITenantIpNetworkOutboundPolicyBuilder WithIpv4PeerAddress(string ipv4PeerAddress)
        {
            if (!string.IsNullOrEmpty(ipv4PeerAddress)) _args.Add(nameof(WithIpv4PeerAddress), ipv4PeerAddress);
            return this;
        }

        public virtual ITenantIpNetworkOutboundPolicyBuilder AddToAllBgpPeersInAttachmentSet(bool? addToAllBgpPeersInAttachmentSet)
        {
            if (addToAllBgpPeersInAttachmentSet.HasValue) _args.Add(nameof(AddToAllBgpPeersInAttachmentSet), addToAllBgpPeersInAttachmentSet);
            return this;
        }

        public virtual ITenantIpNetworkOutboundPolicyBuilder WithAdvertisedIpRoutingPreference(int? advertisedIpRoutingPreference)
        {
            if (advertisedIpRoutingPreference != null) _args.Add(nameof(WithAdvertisedIpRoutingPreference), advertisedIpRoutingPreference);
            return this;
        }

        public virtual async Task<VpnTenantIpNetworkOut> BuildAsync()
        {
            // Check to build components for an attachment set
            if (_args.ContainsKey(nameof(ForAttachmentSet)))
            {
                await SetAttachmentSetAsync();
                if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerForAttachmentSetAsync();
                if (_args.ContainsKey(nameof(AddToAllBgpPeersInAttachmentSet))) SetAddToAllBgpPeersInAttachmentSet();
                if (_args.ContainsKey(nameof(WithTenantIpNetworkCidrName))) await SetTenantIpNetworkForAttachmentSetAsync();
            }

            // Check to update components for an existing outbound policy
            if (_args.ContainsKey(nameof(ForTenantIpNetworkOutboundPolicy)))
            {
                await SetTenantIpNetworkOutboundPolicyAsync();
                if (_vpnTenantIpNetworkOut.AttachmentSet == null)
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
                if (_args.ContainsKey(nameof(WithTenantIpNetworkCidrName))) await SetTenantIpNetworkForBgpPeerAsync();
            }

            if (_args.ContainsKey(nameof(ForTenantIpNetworkOutboundPolicy))) await SetTenantIpNetworkOutboundPolicyAsync();

            if (_args.ContainsKey(nameof(WithAdvertisedIpRoutingPreference))) { 
                _vpnTenantIpNetworkOut.AdvertisedIpRoutingPreference = (int)_args[nameof(WithAdvertisedIpRoutingPreference)];
            }

            _vpnTenantIpNetworkOut.Validate();

            return _vpnTenantIpNetworkOut;
        }

        protected virtual internal async Task SetAttachmentSetAsync()
        {
            if (_args[nameof(ForAttachmentSet)].GetType() == typeof(AttachmentSet))
            {
                var attachmentSet = (AttachmentSet)_args[nameof(ForAttachmentSet)];
                _vpnTenantIpNetworkOut.AttachmentSet = attachmentSet;
            }
            else
            {
                var attachmentSetId = (int)_args[nameof(ForAttachmentSet)];
                var attachmentSet = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                                q =>
                                     q.AttachmentSetID == attachmentSetId,
                                     AsTrackable: true)
                                     select result)
                                     .SingleOrDefault();

                _vpnTenantIpNetworkOut.AttachmentSet = attachmentSet;
            }
        }

        private async Task SetTenantIpNetworkOutboundPolicyAsync()
        {
            var vpnTenantIpNetworkOutId = (int)_args[nameof(ForTenantIpNetworkOutboundPolicy)];
            var vpnTenantIpNetworkOut = (from result in await _unitOfWork.VpnTenantIpNetworkOutRepository.GetAsync(
                                      q =>
                                        q.VpnTenantIpNetworkOutID == vpnTenantIpNetworkOutId,
                                        query: x => x.IncludeValidationProperties(),
                                        AsTrackable: true)
                                         select result)
                                        .SingleOrDefault();

            if (_vpnTenantIpNetworkOut == null) throw new BuilderUnableToCompleteException("The tenant IP network outbound policy with ID " +
                $"'{vpnTenantIpNetworkOutId}' was not found.");

            _vpnTenantIpNetworkOut = vpnTenantIpNetworkOut;
        }

        protected virtual internal async Task SetTenantIpNetworkForAttachmentSetAsync()
        {
            if (!_args.ContainsKey(nameof(WithTenant))) throw new BuilderBadArgumentsException("Unable to create a tenant IP network association with the attachment set using " +
                $"the given arguments. A tenant ID argument is required but was not supplied.");

            var tenantId = (int)_args[nameof(WithTenant)];
            var tenantIpNetworkCidrName = _args[nameof(WithTenantIpNetworkCidrName)].ToString();
            var tenantIpNetwork = (from result in await _unitOfWork.TenantIpNetworkRepository.GetAsync(
                              q =>
                              q.TenantID == tenantId &&
                              q.CidrNameIncludingIpv4LessThanOrEqualToLength == tenantIpNetworkCidrName,
                              AsTrackable: true)
                                   select result)
                                 .SingleOrDefault();

            _vpnTenantIpNetworkOut.TenantIpNetwork = tenantIpNetwork ?? throw new BuilderBadArgumentsException("Unable to create a tenant IP network association with the attachment set using " +
                $"the given arguments. The tenant IP network CIDR name '{tenantIpNetworkCidrName}' does not exist.");
        }

        protected virtual internal async Task SetTenantIpNetworkForBgpPeerAsync()
        {
            var tenantIpNetworkCidrName = _args[nameof(WithTenantIpNetworkCidrName)].ToString();
            var tenantIpNetwork = (from result in await _unitOfWork.TenantIpNetworkRepository.GetAsync(
                              q =>
                              q.TenantID == _vpnTenantIpNetworkOut.BgpPeer.RoutingInstance.Device.TenantID &&
                              q.CidrNameIncludingIpv4LessThanOrEqualToLength == tenantIpNetworkCidrName,
                              AsTrackable: true)
                                   select result)
                                 .SingleOrDefault();

            _vpnTenantIpNetworkOut.TenantIpNetwork = tenantIpNetwork ?? throw new BuilderBadArgumentsException("Unable to create a tenant IP network association with the BGP peer using " +
                $"the given arguments. The tenant IP network CIDR name '{tenantIpNetworkCidrName}' does not exist.");
        }

        protected virtual internal async Task SetIpv4BgpPeerForAttachmentSetAsync()
        {
            var ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            var routingInstanceNames = _vpnTenantIpNetworkOut.AttachmentSet.AttachmentSetRoutingInstances
                           .Select(
                               attachmentSetRoutingInstance =>
                               attachmentSetRoutingInstance.RoutingInstance.Name);

            var bgpPeer = (from result in await _unitOfWork.BgpPeerRepository.GetAsync(
                           q =>
                           routingInstanceNames.Contains(q.RoutingInstance.Name),
                           query: q => q.IncludeValidationProperties(),
                           AsTrackable: true)
                           select result)
                          .SingleOrDefault(
                            x =>
                            x.Ipv4PeerAddress == ipv4PeerAddress);

            _vpnTenantIpNetworkOut.BgpPeer = bgpPeer;
        }

        protected virtual internal async Task SetIpv4BgpPeerForDeviceAsync()
        {
            // Try to find the device ID, first from supplied args, then from an existing BGP peer
            var deviceId = _args.ContainsKey(nameof(ForDevice)) ? (int)_args[nameof(ForDevice)] : _vpnTenantIpNetworkOut.BgpPeer?.RoutingInstance?.DeviceID;
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

            _vpnTenantIpNetworkOut.BgpPeer = bgpPeer ?? throw new BuilderBadArgumentsException("Unable to create a tenant IP network association with " +
              $"the given arguments. The BGP peer address '{ipv4PeerAddress}' does not exist within any routing instance which belongs to " +
              "the given device.");
        }

        protected virtual internal void SetAddToAllBgpPeersInAttachmentSet()
        {
            var addToAllBgpPeersInAttachmentSet = (bool)_args[nameof(AddToAllBgpPeersInAttachmentSet)];
            _vpnTenantIpNetworkOut.AddToAllBgpPeersInAttachmentSet = addToAllBgpPeersInAttachmentSet;
            if (addToAllBgpPeersInAttachmentSet)
            {
                // Clear any previously set bgp peer
                _vpnTenantIpNetworkOut.BgpPeer = null;
                _vpnTenantIpNetworkOut.BgpPeerID = null;
            }
        }
    }
}

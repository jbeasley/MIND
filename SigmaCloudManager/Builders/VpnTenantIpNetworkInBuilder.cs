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

        /// <summary>
        /// The owning tenant of the tenant IP network. Multiple tenants may be allocated the same CIDR block so the 
        /// tenant owner is required in order to identify the correct CIDR block.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public virtual IVpnTenantIpNetworkInBuilder WithTenantOwner(int? tenantId)
        {
            if (tenantId != null) _args.Add(nameof(WithTenantOwner), tenantId);
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
            if (_args.ContainsKey(nameof(ForAttachmentSet))) await SetAttachmentSetAsync();
            if (_args.ContainsKey(nameof(WithTenantIpNetworkCidrName)) && _args.ContainsKey(nameof(WithTenantOwner))) await SetTenantIpNetworkAsync();
            if (_args.ContainsKey(nameof(WithLocalIpRoutingPreference)))
                _vpnTenantIpNetworkIn.LocalIpRoutingPreference = (int)_args[nameof(WithLocalIpRoutingPreference)];
            
            if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await SetIpv4BgpPeerAsync();
            if (_args.ContainsKey(nameof(AddToAllBgpPeersInAttachmentSet))) SetAddToAllBgpPeersInAttachmentSet();

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

        protected virtual internal async Task SetIpv4BgpPeerAsync()
        {
            var ipv4PeerAddress = _args[nameof(WithIpv4PeerAddress)].ToString();
            var bgpPeer = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                       q =>
                          q.AttachmentSetID == _vpnTenantIpNetworkIn.AttachmentSet.AttachmentSetID,
                          query: q => q.Include(x => x.AttachmentSetRoutingInstances)
                                       .ThenInclude(x => x.RoutingInstance.BgpPeers),
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
        }
    }
}


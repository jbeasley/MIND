using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class VpnTenantIpNetworkOutUpdateBuilder : VpnTenantIpNetworkOutBuilder, IVpnTenantIpNetworkOutUpdateBuilder
    {
        public VpnTenantIpNetworkOutUpdateBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IVpnTenantIpNetworkOutUpdateBuilder ForVpnTenantIpNetworkOut(int VpnTenantIpNetworkOutId)
        {
            _args.Add(nameof(ForVpnTenantIpNetworkOut), VpnTenantIpNetworkOutId);
            return this;
        }

        IVpnTenantIpNetworkOutUpdateBuilder IVpnTenantIpNetworkOutUpdateBuilder.WithIpv4PeerAddress(string peerIpv4Address)
        {
            base.WithIpv4PeerAddress(peerIpv4Address);
            return this;
        }

        IVpnTenantIpNetworkOutUpdateBuilder IVpnTenantIpNetworkOutUpdateBuilder.WithAdvertisedIpRoutingPreference(int? advertisedIpRoutingPreference)
        {
            base.WithAdvertisedIpRoutingPreference(advertisedIpRoutingPreference);
            return this;
        }

        public async Task<VpnTenantIpNetworkOut> UpdateAsync()
        {
            if (_args.ContainsKey(nameof(ForVpnTenantIpNetworkOut))) await SetVpnTenantIpNetworkOut();        
            if (_args.ContainsKey(nameof(WithAdvertisedIpRoutingPreference)))
                _vpnTenantIpNetworkOut.AdvertisedIpRoutingPreference = (int)_args[nameof(WithAdvertisedIpRoutingPreference)];
            
            if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await base.SetIpv4BgpPeerAsync();

            _vpnTenantIpNetworkOut.Validate();

            return _vpnTenantIpNetworkOut;
        }

        private async Task SetVpnTenantIpNetworkOut()
        {
            var vpnTenantIpNetworkOutId = (int)_args[nameof(ForVpnTenantIpNetworkOut)];
            var vpnTenantIpNetworkOut = (from result in await _unitOfWork.VpnTenantIpNetworkOutRepository.GetAsync(
                                      q =>
                                        q.VpnTenantIpNetworkOutID == vpnTenantIpNetworkOutId,
                                        query: x => x.IncludeValidationProperties(),
                                        AsTrackable: true)
                                        select result)
                                        .SingleOrDefault();

            if (_vpnTenantIpNetworkOut == null) throw new BuilderUnableToCompleteException("The tenant IP network attachment set association with ID " +
                $"'{vpnTenantIpNetworkOutId}' was not found.");

            _vpnTenantIpNetworkOut = vpnTenantIpNetworkOut;
        }
    }
}

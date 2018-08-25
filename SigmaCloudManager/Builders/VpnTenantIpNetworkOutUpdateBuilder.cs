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

        public IVpnTenantIpNetworkOutUpdateBuilder ForVpnTenantIpNetworkOut(VpnTenantIpNetworkOut VpnTenantIpNetworkOut)
        {
            if (VpnTenantIpNetworkOut != null) _args.Add(nameof(ForVpnTenantIpNetworkOut), VpnTenantIpNetworkOut);
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
            if (_args.ContainsKey(nameof(ForVpnTenantIpNetworkOut)))
            {
                _vpnTenantIpNetworkOut = (VpnTenantIpNetworkOut)_args[nameof(ForVpnTenantIpNetworkOut)];
            }
            if (_args.ContainsKey(nameof(WithAdvertisedIpRoutingPreference)))
            {
                _vpnTenantIpNetworkOut.AdvertisedIpRoutingPreference = (int)_args[nameof(WithAdvertisedIpRoutingPreference)];
            }
            if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await base.SetIpv4BgpPeerAsync();

            return _vpnTenantIpNetworkOut;
        }
    }
}

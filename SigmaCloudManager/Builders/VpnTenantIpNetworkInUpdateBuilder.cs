using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class VpnTenantIpNetworkInUpdateBuilder : VpnTenantIpNetworkInBuilder, IVpnTenantIpNetworkInUpdateBuilder
    {
        public VpnTenantIpNetworkInUpdateBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IVpnTenantIpNetworkInUpdateBuilder ForVpnTenantIpNetworkIn(VpnTenantIpNetworkIn vpnTenantIpNetworkIn)
        {
            if (vpnTenantIpNetworkIn != null) _args.Add(nameof(ForVpnTenantIpNetworkIn), vpnTenantIpNetworkIn);
            return this;
        }

        IVpnTenantIpNetworkInUpdateBuilder IVpnTenantIpNetworkInUpdateBuilder.AddToAllBgpPeersInAttachmentSet(bool? addToAllBgpPeersInAttachmentSet)
        {
            base.AddToAllBgpPeersInAttachmentSet(addToAllBgpPeersInAttachmentSet);
            return this;
        }

        IVpnTenantIpNetworkInUpdateBuilder IVpnTenantIpNetworkInUpdateBuilder.WithIpv4PeerAddress(string peerIpv4Address)
        {
            base.WithIpv4PeerAddress(peerIpv4Address);
            return this;
        }

        IVpnTenantIpNetworkInUpdateBuilder IVpnTenantIpNetworkInUpdateBuilder.WithLocalIpRoutingPreference(int? localIpRoutingPreference)
        {
            base.WithLocalIpRoutingPreference(localIpRoutingPreference);
            return this;
        }

        public async Task<VpnTenantIpNetworkIn> UpdateAsync()
        {
            if (_args.ContainsKey(nameof(ForVpnTenantIpNetworkIn)))
            {
                _vpnTenantIpNetworkIn = (VpnTenantIpNetworkIn)_args[nameof(ForVpnTenantIpNetworkIn)];
            }
            if (_args.ContainsKey(nameof(WithLocalIpRoutingPreference)))
            {
                _vpnTenantIpNetworkIn.LocalIpRoutingPreference = (int)_args[nameof(WithLocalIpRoutingPreference)];
            }
            if (_args.ContainsKey(nameof(AddToAllBgpPeersInAttachmentSet)))
            {
                _vpnTenantIpNetworkIn.AddToAllBgpPeersInAttachmentSet = (bool)_args[nameof(AddToAllBgpPeersInAttachmentSet)];
            }
            if (_args.ContainsKey(nameof(WithIpv4PeerAddress))) await base.SetIpv4BgpPeerAsync();

            return _vpnTenantIpNetworkIn;
        }
    }
}

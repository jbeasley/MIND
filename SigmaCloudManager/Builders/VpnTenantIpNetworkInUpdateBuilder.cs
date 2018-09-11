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

        public IVpnTenantIpNetworkInUpdateBuilder ForVpnTenantIpNetworkIn(int vpnTenantIpNetworkInId)
        {
            _args.Add(nameof(ForVpnTenantIpNetworkIn), vpnTenantIpNetworkInId);
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
                await SetVpnTenantIpNetworkIn();
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

            base.Validate();

            return _vpnTenantIpNetworkIn;
        }

        private async Task SetVpnTenantIpNetworkIn()
        {
            var vpnTenantIpNetworkInId = (int)_args[nameof(ForVpnTenantIpNetworkIn)];
            var vpnTenantIpNetworkIn = (from result in await _unitOfWork.VpnTenantIpNetworkInRepository.GetAsync(
                                      q =>
                                        q.VpnTenantIpNetworkInID == vpnTenantIpNetworkInId,
                                        includeProperties: "AttachmentSet," +
                                        "TenantIpNetwork," +
                                        "BgpPeer",
                                        AsTrackable: true)
                                        select result)
                                        .SingleOrDefault();

            if (_vpnTenantIpNetworkIn == null) throw new BuilderUnableToCompleteException("The tenant IP network attachment set association with ID " +
                $"'{vpnTenantIpNetworkInId}' was not found.");

            _vpnTenantIpNetworkIn = vpnTenantIpNetworkIn;
        }
    }
}

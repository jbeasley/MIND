using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class VpnTenantIpNetworkInDirector : IVpnTenantIpNetworkInDirector
    {
        private readonly IVpnTenantIpNetworkInBuilder _builder;

        public VpnTenantIpNetworkInDirector(IVpnTenantIpNetworkInBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantIpNetworkIn> BuildAsync(int attachmentSetId, int tenantIpNetworkId, VpnTenantIpNetworkInRequest request)
        {
            return await _builder.ForAttachmentSet(attachmentSetId)
                                 .WithLocalIpRoutingPreference(request.LocalIpRoutingPreference)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .WithTenantIpNetwork(tenantIpNetworkId)
                                 .AddToAllBgpPeersInAttachmentSet(request.AddToAllBgpPeersInAttachmentSet)
                                 .BuildAsync();
        }
    }
}

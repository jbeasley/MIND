using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class VpnTenantIpNetworkOutDirector : IVpnTenantIpNetworkOutDirector
    {
        private readonly IVpnTenantIpNetworkOutBuilder _builder;

        public VpnTenantIpNetworkOutDirector(IVpnTenantIpNetworkOutBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantIpNetworkOut> BuildAsync(int attachmentSetId, VpnTenantIpNetworkOutRequest request)
        {
            return await _builder.ForAttachmentSet(attachmentSetId)
                                 .WithAdvertisedIpRoutingPreference(request.AdvertisedIpRoutingPreference)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .WithTenantIpNetworkCidrName(request.TenantIpNetworkCidrName)
                                 .BuildAsync();
        }
    }
}

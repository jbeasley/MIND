using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public class ProviderDomainIpNetworkOutboundPolicyDirector : IProviderDomainIpNetworkOutboundPolicyDirector
    {
        private readonly Func<ITenantIpNetworkOutboundPolicyBuilder> _builderFactory;

        public ProviderDomainIpNetworkOutboundPolicyDirector(Func<ITenantIpNetworkOutboundPolicyBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<VpnTenantIpNetworkOut> BuildAsync(int attachmentSetId, VpnTenantIpNetworkOutRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForAttachmentSet(attachmentSetId)
                                .WithAdvertisedIpRoutingPreference(request.AdvertisedIpRoutingPreference)
                                .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                .AddToAllBgpPeersInAttachmentSet(request.AddToAllBgpPeersInAttachmentSet)
                                .WithTenant(request.TenantId)
                                .WithTenantIpNetworkCidrName(request.TenantIpNetworkCidrName)
                                .BuildAsync();
        }

        /// <summary>
        /// Build a single VpnTenantIpNetworkOut object
        /// </summary>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkOut> BuildAsync(AttachmentSet attachmentSet, VpnTenantIpNetworkOutRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForAttachmentSet(attachmentSet)
                                .WithAdvertisedIpRoutingPreference(request.AdvertisedIpRoutingPreference)
                                .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                .AddToAllBgpPeersInAttachmentSet(request.AddToAllBgpPeersInAttachmentSet)
                                .WithTenant(request.TenantId)
                                .WithTenantIpNetworkCidrName(request.TenantIpNetworkCidrName)
                                .BuildAsync();
        }

        /// <summary>
        /// Build a collection of VpnTenantIpNetworkOut objects
        /// </summary>
        /// <returns></returns>
        public async Task<List<VpnTenantIpNetworkOut>> BuildAsync(AttachmentSet attachmentSet, List<VpnTenantIpNetworkOutRequest> requests)
        {
            var vpnTenantIpNetworksOut = new List<VpnTenantIpNetworkOut>();
            var tasks = requests.Select(
                                 async request =>
                                 {
                                     // Each vpnTenantIpNetworkOut object will be built from a distinct instance of the builder
                                     vpnTenantIpNetworksOut.Add(await BuildAsync(attachmentSet, request));
                                 });

            await Task.WhenAll(tasks);

            return vpnTenantIpNetworksOut;
        }
    }
}

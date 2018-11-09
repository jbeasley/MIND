using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public class ProviderDomainIpNetworkInboundPolicyDirector : IProviderDomainIpNetworkInboundPolicyDirector
    {
        private readonly Func<ITenantIpNetworkInboundPolicyBuilder> _builderFactory;

        public ProviderDomainIpNetworkInboundPolicyDirector(Func<ITenantIpNetworkInboundPolicyBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        /// <summary>
        /// Build a single VpnTenantIpNetworkIn object
        /// </summary>
        /// <returns></returns>
        public async Task<SCM.Models.VpnTenantIpNetworkIn> BuildAsync(int attachmentSetId, VpnTenantIpNetworkInRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForAttachmentSet(attachmentSetId)
                                .WithTenantOwner(request.TenantId)
                                .WithLocalIpRoutingPreference(request.LocalIpRoutingPreference)
                                .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                .WithTenantIpNetworkCidrName(request.TenantIpNetworkCidrName)
                                .AddToAllBgpPeersInAttachmentSet(request.AddToAllBgpPeersInAttachmentSet)
                                .BuildAsync();
        }

        /// <summary>
        /// Build a single VpnTenantIpNetworkIn object
        /// </summary>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkIn> BuildAsync(AttachmentSet attachmentSet, VpnTenantIpNetworkInRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForAttachmentSet(attachmentSet)
                                .WithTenantOwner(request.TenantId)
                                .WithLocalIpRoutingPreference(request.LocalIpRoutingPreference)
                                .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                .WithTenantIpNetworkCidrName(request.TenantIpNetworkCidrName)
                                .AddToAllBgpPeersInAttachmentSet(request.AddToAllBgpPeersInAttachmentSet)
                                .BuildAsync();
        }

        /// <summary>
        /// Build a collection of VpnTenantIpNetworkIn objects
        /// </summary>
        /// <returns></returns>
        public async Task<List<VpnTenantIpNetworkIn>> BuildAsync(AttachmentSet attachmentSet, List<VpnTenantIpNetworkInRequest> requests)
        {
            var vpnTenantIpNetworksIn = new List<VpnTenantIpNetworkIn>();
            var tasks = requests.Select(
                                 async request =>
                                 {
                                     // Each vpnTenantIpNetworkIn object will be built from a distinct instance of the builder
                                     vpnTenantIpNetworksIn.Add(await BuildAsync(attachmentSet, request));
                                 });

            await Task.WhenAll(tasks);

            return vpnTenantIpNetworksIn;
        }
    }
}

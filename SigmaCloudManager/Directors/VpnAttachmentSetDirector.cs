using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class VpnAttachmentSetDirector : IVpnAttachmentSetDirector
    {
        // Factory for the vpn attachment set builder - the factory ensures we get a unique instance of the builder
        // for each request which is necessary when constructing a collection of vpn attachment sets
        private readonly Func<IVpnAttachmentSetBuilder> _builderFactory;

        public VpnAttachmentSetDirector(Func<IVpnAttachmentSetBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.VpnAttachmentSet> BuildAsync(int vpnId, VpnAttachmentSetRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForVpn(vpnId)
                                .WithAttachmentSet(request.AttachmentSetName)
                                .WithHub(request.IsHub)
                                .WithMulticastDirectlyIntegrated(request.IsMulticastDirectlyIntegrated)
                                .BuildAsync();
        }

        public async Task<VpnAttachmentSet> BuildAsync(Vpn vpn, VpnAttachmentSetRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForVpn(vpn)
                                .WithAttachmentSet(request.AttachmentSetName)
                                .WithHub(request.IsHub)
                                .WithMulticastDirectlyIntegrated(request.IsMulticastDirectlyIntegrated)
                                .BuildAsync();
        }

        public async Task<List<VpnAttachmentSet>> BuildAsync(Vpn vpn, List<VpnAttachmentSetRequest> requests)
        {
            var vpnAttachmentSets = new List<SCM.Models.VpnAttachmentSet>();
            var tasks = requests.Select(
                            async request =>
                               vpnAttachmentSets.Add(await BuildAsync(vpn, request))
                            );

            await Task.WhenAll(tasks);
            return vpnAttachmentSets;
        }
    }
}

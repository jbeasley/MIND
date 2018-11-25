using Mind.Models.RequestModels;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Directors;

namespace Mind.Builders
{
    public class TenantDomainVifDirector : ITenantDomainVifDirector, IDestroyable<SCM.Models.Vif>
    {
        private readonly Func<IVifBuilder> _builderFactory;

        public TenantDomainVifDirector(Func<IVifBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.Vif> BuildAsync(int attachmentId, TenantDomainVifRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForAttachment(attachmentId)
                                .WithRequestedVlanTag(request.RequestedVlanTag)
                                .WithTrustReceivedCosAndDscp(request.TrustReceivedCosAndDscp)
                                .WithVifRole(request.VifRoleName)
                                .WithContractBandwidth(request.ContractBandwidthMbps)
                                .WithExistingContractBandwidthPool(request.ExistingContractBandwidthPoolName)
                                .WithIpv4(request.Ipv4Addresses)
                                .BuildAsync();
        }

        /// <summary>
        /// Destroys a vif
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="vif">The vif to destroy</param>
        /// <param name="cleanUpNetwork">If true clean up the network. Default is false.</param>
        public async Task DestroyAsync(SCM.Models.Vif vif, bool cleanUpNetwork = false)
        {
            var builder = _builderFactory();
            await builder.ForVif(vif.VifID)
                         .CleanUpRoutingInstance()
                         .CleanUpContractBandwidthPool()
                         .CleanUpNetwork(cleanUpNetwork)
                         .DestroyAsync();
        }

        /// <summary>
        /// Destroys a collection of vifs
        /// </summary>
        /// <param name="vifs">The collection of vifs to destroy</param>
        /// <param name="cleanUpNetwork">If true clean up the network. Default is false.</param>
        /// <returns>An awaitable task</returns>
        public async Task DestroyAsync(List<SCM.Models.Vif> vifs, bool cleanUpNetwork = false)
        {
            var tasks = vifs.Select(
                async vif =>
                    await DestroyAsync(vif, cleanUpNetwork));

            await Task.WhenAll(tasks);
        }
    }
}

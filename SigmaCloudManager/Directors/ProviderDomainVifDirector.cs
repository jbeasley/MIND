using Mind.Models.RequestModels;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Directors;
using SCM.Models;

namespace Mind.Builders
{
    public class ProviderDomainVifDirector : IProviderDomainVifDirector, IDestroyable<SCM.Models.Vif>, INetworkSynchronizable<SCM.Models.Vif>
    {
        private readonly Func<IVifBuilder> _builderFactory;

        public ProviderDomainVifDirector(Func<IVifBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        /// <summary>
        /// Builds a new vif
        /// </summary>
        /// <returns>An instance of vif</returns>
        /// <param name="attachmentId">The ID of the parent attachement for which the vif will built</param>
        /// <param name="request">Request object containing parameters with which to create the vif</param>
        /// <param name="syncToNetwork">Add the attachment to the network with a put operation</param>
        /// <param name="stage">If set to <c>true</c> the vif network status will be set to <c>staged</c></param>
        public async Task<SCM.Models.Vif> BuildAsync(int attachmentId, ProviderDomainVifRequest request, bool stage = true, bool syncToNetwork = false)
        {
            var builder = _builderFactory();
            return await builder.ForAttachment(attachmentId)
                                .ForTenant(request.TenantId.Value)
                                .WithRequestedVlanTag(request.RequestedVlanTag)
                                .WithTrustReceivedCosAndDscp(request.TrustReceivedCosAndDscp)
                                .WithVifRole(request.VifRoleName)
                                .UseExistingRoutingInstance(request.ExistingRoutingInstanceName)
                                .WithRoutingInstance(request.RoutingInstance)
                                .WithContractBandwidth(request.ContractBandwidthMbps)
                                .WithExistingContractBandwidthPool(request.ExistingContractBandwidthPoolName)
                                .WithIpv4(request.Ipv4Addresses)
                                .Stage(stage)
                                .SyncToNetworkPut(syncToNetwork)
                                .BuildAsync();
        }

        /// <summary>
        /// Updates the vif.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="vifId">The ID of the vif.</param>
        /// <param name="update">Update object containing property values with which to update the vif</param>
        /// <param name="stage">If set to <c>true</c> the vif network status will be set to <c>staged</c></param>
        /// <param name="syncToNetwork">If set to <c>true</c> sync to network.</param>
        public async Task<SCM.Models.Vif> UpdateAsync(int vifId, Mind.Models.RequestModels.ProviderDomainVifUpdate update, bool stage = true, bool syncToNetwork = false)
        {
            var builder = _builderFactory();
            return await builder.ForVif(vifId)
                                .WithNewRoutingInstance(update.CreateNewRoutingInstance)
                                .WithRoutingInstance(update.RoutingInstance)
                                .UseExistingRoutingInstance(update.ExistingRoutingInstanceName)
                                .WithContractBandwidth(update.ContractBandwidthMbps)
                                .WithExistingContractBandwidthPool(update.ExistingContractBandwidthPoolName)
                                .WithTrustReceivedCosAndDscp(update.TrustReceivedCosAndDscp)
                                .WithIpv4(update.Ipv4Addresses)
                                .WithJumboMtu(update.UseJumboMtu)
                                .Stage(stage)
                                .SyncToNetworkPut(syncToNetwork)
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

        public async Task<Vif> SyncToNetworkPutAsync(Vif vif)
        {
            var builder = _builderFactory();
            return await builder.ForVif(vif.VifID)
                                .SyncToNetworkPutAsync();
        }
    }
}

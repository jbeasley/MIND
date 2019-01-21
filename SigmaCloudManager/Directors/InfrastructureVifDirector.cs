﻿using Mind.Models.RequestModels;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Directors;

namespace Mind.Builders
{
    public class InfrastructureVifDirector : IInfrastructureVifDirector, IDestroyable<SCM.Models.Vif>
    {
        private readonly Func<IVifBuilder> _builderFactory;

        public InfrastructureVifDirector(Func<IVifBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        /// <summary>
        /// Builds a new vif
        /// </summary>
        /// <returns>The new vif</returns>
        /// <param name="attachmentId">Attachment identifier referring to the parent attachment of the new vif</param>
        /// <param name="request">Request object to build the vif</param>
        public async Task<SCM.Models.Vif> BuildAsync(int attachmentId, InfrastructureVifRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForAttachment(attachmentId)
                                .WithRequestedVlanTag(request.RequestedVlanTag)
                                .WithVifRole(request.VifRoleName)
                                .WithContractBandwidth(request.ContractBandwidthMbps)
                                .WithExistingContractBandwidthPool(request.ExistingContractBandwidthPoolName)
                                .UseExistingRoutingInstance(request.ExistingRoutingInstanceName)
                                .WithRoutingInstance(request.RoutingInstance)
                                .WithIpv4(request.Ipv4Addresses)
                                .BuildAsync();
        }

        /// <summary>
        /// Updates a vif
        /// </summary>
        /// <returns>The updated vif</returns>
        /// <param name="vifId">Vif identifier.</param>
        /// <param name="update">Updates to apply to the vif</param>
        public async Task<SCM.Models.Vif> UpdateAsync(int vifId, Mind.Models.RequestModels.InfrastructureVifUpdate update)
        {
            var builder = _builderFactory();
            return await builder.ForVif(vifId)
                                .WithNewRoutingInstance(update.CreateNewRoutingInstance)
                                .UseExistingRoutingInstance(update.ExistingRoutingInstanceName)
                                .WithRoutingInstance(update.RoutingInstance)
                                .WithContractBandwidth(update.ContractBandwidthMbps)
                                .WithExistingContractBandwidthPool(update.ExistingContractBandwidthPoolName)
                                .WithIpv4(update.Ipv4Addresses)
                                .WithJumboMtu(update.UseJumboMtu)
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
        public async Task DestroyAsync(List<SCM.Models.Vif> vifs, bool cleanUpNetwork = false)
        {
            var tasks = vifs.Select(
                async vif =>
                    await DestroyAsync(vif, cleanUpNetwork));

            await Task.WhenAll(tasks);
        }
    }
}

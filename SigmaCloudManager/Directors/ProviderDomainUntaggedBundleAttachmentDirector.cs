using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public class ProviderDomainUntaggedBundleAttachmentDirector : IProviderDomainAttachmentDirector
    {
        private readonly Func<ProviderDomainAttachmentRequest, IBundleAttachmentBuilder> _builderFactory;
        private readonly Func<Attachment, IBundleAttachmentBuilder> _builderAttachmentFactory;

        public ProviderDomainUntaggedBundleAttachmentDirector(Func<ProviderDomainAttachmentRequest, IBundleAttachmentBuilder> builderFactory, 
                                                              Func<Attachment, IBundleAttachmentBuilder> builderAttachmentFactory)
        {
            _builderFactory = builderFactory;
            _builderAttachmentFactory = builderAttachmentFactory;
        }

        public async Task<SCM.Models.Attachment> BuildAsync(int tenantId, ProviderDomainAttachmentRequest request, bool stage = true, bool syncToNetwork = false)
        {
            var builder = _builderFactory(request);
            return await builder.ForTenant(tenantId)
                                .WithAttachmentRole(request.AttachmentRoleName)
                                .WithPortPool(request.PortPoolName)
                                .WithAttachmentBandwidth(request.AttachmentBandwidthGbps)
                                .WithLocation(request.LocationName)
                                .WithPlane(request.PlaneName.ToString())
                                .WithIpv4(request.Ipv4Addresses)
                                .WithContractBandwidth(request.ContractBandwidthMbps)
                                .WithTrustReceivedCosAndDscp(request.TrustReceivedCosAndDscp)
                                .WithBundleLinks(request.BundleMinLinks, request.BundleMaxLinks)
                                .WithRoutingInstance(request.RoutingInstance)
                                .WithDescription(request.Description)
                                .WithNotes(request.Notes)
                                .BuildAsync();
        }

        public async Task<SCM.Models.Attachment> UpdateAsync(SCM.Models.Attachment attachment, ProviderDomainAttachmentUpdate update, bool stage = true, bool syncToNetwork = false)
        {
            var builder = _builderAttachmentFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .UseExistingRoutingInstance(update.ExistingRoutingInstanceName)
                                .WithNewRoutingInstance(update.CreateNewRoutingInstance)
                                .WithRoutingInstance(update.RoutingInstance)
                                .WithContractBandwidth(update.ContractBandwidthMbps)
                                .WithJumboMtu(update.UseJumboMtu)
                                .WithTrustReceivedCosAndDscp(update.TrustReceivedCosAndDscp)
                                .WithBundleLinks(update.BundleMinLinks, update.BundleMaxLinks)
                                .WithDescription(update.Description)
                                .WithNotes(update.Notes)
                                .WithIpv4(update.Ipv4Addresses)
                                .BuildAsync();
        }

        /// <summary>
        /// Destroy an attachment
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="attachment">Attachment.</param>
        /// <param name="cleanUpNetwork">If set to <c>true</c> clean up network.</param>   
        public async Task DestroyAsync(Attachment attachment, bool cleanUpNetwork = true)
        {
            var builder = _builderAttachmentFactory(attachment);
            await builder.ForAttachment(attachment.AttachmentID)
                         .CleanUpRoutingInstance()
                         .CleanUpNetwork(cleanUpNetwork)
                         .ReleasePorts()
                         .DestroyAsync();
        }

        /// <summary>
        /// Destroy a collection of attachments.
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="attachments">Attachments to destroy</param>
        /// <param name="cleanUpNetwork">If set to <c>true</c> clean up network.</param>
        public async Task DestroyAsync(List<Attachment> attachments, bool cleanUpNetwork = true)
        {
            var tasks = attachments.Select(
                async attachment =>
                    await DestroyAsync(attachment, cleanUpNetwork));

            await Task.WhenAll(tasks);
        }
    }
}

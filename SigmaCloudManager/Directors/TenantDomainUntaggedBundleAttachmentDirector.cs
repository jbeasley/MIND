﻿using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public class TenantDomainUntaggedBundleAttachmentDirector : ITenantDomainAttachmentDirector 
    {
        private readonly Func<TenantDomainAttachmentRequest, IBundleAttachmentBuilder> _builderRequestFactory;
        private readonly Func<Attachment, IBundleAttachmentBuilder> _builderAttachmentFactory;

        public TenantDomainUntaggedBundleAttachmentDirector(Func<TenantDomainAttachmentRequest, IBundleAttachmentBuilder> builderRequestFactory,
                                                            Func<Attachment, IBundleAttachmentBuilder> builderAttachmentFactory)
        {
            _builderRequestFactory = builderRequestFactory;
            _builderAttachmentFactory = builderAttachmentFactory;
        }

        public async Task<SCM.Models.Attachment> BuildAsync(int deviceId, TenantDomainAttachmentRequest request)
        {
            var builder = _builderRequestFactory(request);
            return await builder.ForDevice(deviceId)
                                .WithAttachmentRole(request.AttachmentRoleName)
                                .WithPortPool(request.PortPoolName)
                                .WithAttachmentBandwidth(request.AttachmentBandwidthGbps)
                                .WithIpv4(request.Ipv4Addresses)
                                .UseDefaultRoutingInstance(true)
                                .WithContractBandwidth(request.ContractBandwidthMbps)
                                .WithTrustReceivedCosAndDscp(request.TrustReceivedCosAndDscp)
                                .WithBundleLinks(request.BundleMinLinks, request.BundleMaxLinks)
                                .WithDescription(request.Description)
                                .WithNotes(request.Notes)
                                .BuildAsync();
        }

        public async Task<Attachment> UpdateAsync(SCM.Models.Attachment attachment, TenantDomainAttachmentUpdate update)
        {
            var builder = _builderAttachmentFactory(attachment);
            return await builder.ForAttachment(attachment.AttachmentID)
                                .WithJumboMtu(update.UseJumboMtu)
                                .WithContractBandwidth(update.ContractBandwidthMbps)
                                .WithTrustReceivedCosAndDscp(update.TrustReceivedCosAndDscp)
                                .WithBundleLinks(update.BundleMinLinks, update.BundleMaxLinks)
                                .WithDescription(update.Description)
                                .WithNotes(update.Notes)
                                .BuildAsync();
        }

        /// <summary>
        /// Destroy an attachment
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="attachment">Attachment.</param>
        public async Task DestroyAsync(Attachment attachment)
        {
            var builder = _builderAttachmentFactory(attachment);
            await builder.ForAttachment(attachment.AttachmentID)
                         .ReleasePorts()
                         .DestroyAsync();
        }

        /// <summary>
        /// Destroy a collection of attachments.
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="attachments">Attachments to destroy</param>
        public async Task DestroyAsync(List<Attachment> attachments)
        {
            var tasks = attachments.Select(
                async attachment =>
                    await DestroyAsync(attachment));

            await Task.WhenAll(tasks);
        }
    }
}

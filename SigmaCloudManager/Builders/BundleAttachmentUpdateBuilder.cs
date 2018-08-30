using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for updating bundle attachments. The builder exposes a fluent API.
    /// </summary>
    public class BundleAttachmentUpdateBuilder : BundleAttachmentBuilder, IBundleAttachmentUpdateBuilder
    {
        public BundleAttachmentUpdateBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceDirector> routingInstanceDirectorFactory) :
            base(unitOfWork, routingInstanceDirectorFactory)
        {
        }

        public IBundleAttachmentUpdateBuilder ForAttachment(Attachment attachment)
        {
            _attachment = attachment;
            return this;
        }

        public IBundleAttachmentUpdateBuilder WithNewRoutingInstance(bool? newRoutingInstance = false)
        {
            base._args.Add(nameof(WithNewRoutingInstance), newRoutingInstance != null ? newRoutingInstance : false);
            return this;
        }

        IBundleAttachmentUpdateBuilder IBundleAttachmentUpdateBuilder.WithContractBandwidth(int? contractBandwidthMbps)
        {
            base.WithContractBandwidth(contractBandwidthMbps);
            return this;
        }

        IBundleAttachmentUpdateBuilder IBundleAttachmentUpdateBuilder.WithExistingRoutingInstance(string routingInstanceName)
        {
            base.WithExistingRoutingInstance(routingInstanceName);
            return this;
        }

        IBundleAttachmentUpdateBuilder IBundleAttachmentUpdateBuilder.WithJumboMtu(bool? useJumboMtu)
        {
            base.WithJumboMtu(useJumboMtu);
            return this;
        }

        IBundleAttachmentUpdateBuilder IBundleAttachmentUpdateBuilder.WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp)
        {
            base.WithTrustReceivedCosAndDscp(trustReceivedCosAndDscp);
            return this;
        }

        IBundleAttachmentUpdateBuilder IBundleAttachmentUpdateBuilder.WithBundleLinks(int? minLinks, int? maxLinks)
        {
            base.WithBundleLinks(minLinks, maxLinks);
            return this;
        }

        /// <summary>
        /// Update the attachment
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Attachment> UpdateAsync()
        {
            await SetMtuAsync();
            if (_attachment.AttachmentRole.RequireContractBandwidth)
            {
                if (_args.ContainsKey("contractBandwidthMbps") && _args["contractBandwidthMbps"] != null) await CreateContractBandwidthPoolAsync();
            }
            if (!base._attachment.AttachmentRole.IsTaggedRole)
            {
                if (base._args.ContainsKey(nameof(WithNewRoutingInstance)) &&
                    (bool)base._args[nameof(WithNewRoutingInstance)])
                {
                    await base.CreateRoutingInstanceAsync();
                }
                else if (base._args.ContainsKey(nameof(WithExistingRoutingInstance)) && base._args[nameof(WithExistingRoutingInstance)] != null)
                {
                    await AssociateExistingRoutingInstanceAsync();
                }
            }
            if (base._args.ContainsKey(nameof(WithTrustReceivedCosAndDscp))) base.SetTrustReceivedCosAndDscp();
            base.SetBundleLinks();

            return _attachment;
        }
    }
}

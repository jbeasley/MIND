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
        public BundleAttachmentUpdateBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IVrfRoutingInstanceDirector> routingInstanceDirectorFactory) :
            base(unitOfWork, routingInstanceDirectorFactory)
        {
        }

        public IBundleAttachmentUpdateBuilder ForAttachment(int? attachmentId)
        {
            if (attachmentId.HasValue) _args.Add(nameof(ForAttachment), attachmentId);
            return this;
        }

        public IBundleAttachmentUpdateBuilder WithNewRoutingInstance(bool? newRoutingInstance)
        {
            if (newRoutingInstance.HasValue) base._args.Add(nameof(WithNewRoutingInstance), newRoutingInstance);
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
            if (_args.ContainsKey(nameof(ForAttachment))) await SetAttachmentAsync();
            await SetMtuAsync();

            if (_args.ContainsKey(nameof(WithContractBandwidth))) await CreateContractBandwidthPoolAsync();

            if (_args.ContainsKey(nameof(WithNewRoutingInstance)) && (bool)_args[nameof(WithNewRoutingInstance)])
            {
                await base.CreateRoutingInstanceAsync();
            }
            else if (_args.ContainsKey(nameof(WithExistingRoutingInstance)))
            {
                await AssociateExistingRoutingInstanceAsync();
            }

            if (base._args.ContainsKey(nameof(WithTrustReceivedCosAndDscp))) base.SetTrustReceivedCosAndDscp();
            base.SetBundleLinks();

            _attachment.Validate();

            return _attachment;
        }

        private async Task SetAttachmentAsync()
        {
            var attachmentId = (int)_args[nameof(ForAttachment)];
            var attachment = (from attachments in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                            q.AttachmentID == attachmentId,
                            query: q => q.IncludeValidationProperties(),
                            AsTrackable: true)
                            select attachments)
                           .SingleOrDefault();

            base._attachment = attachment ?? throw new BuilderBadArgumentsException($"The attachment with ID '{attachmentId}'.");
        }
    }
}

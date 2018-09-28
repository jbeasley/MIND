using SCM.Data;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for updating single attachments. The builder exposes a fluent API.
    /// </summary>
    public class SingleAttachmentUpdateBuilder : SingleAttachmentBuilder, IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder>
    {
        public SingleAttachmentUpdateBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IVrfRoutingInstanceDirector> routingInstanceDirectorFactory) : 
            base(unitOfWork, routingInstanceDirectorFactory)
        {
        }

        public IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder> ForAttachment(int? attachmentId)
        {
            if (attachmentId.HasValue) _args.Add(nameof(ForAttachment), attachmentId);
            return this;
        }

        IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder> IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder>.WithContractBandwidth(int? contractBandwidthMbps)
        {
            base.WithContractBandwidth(contractBandwidthMbps);
            return this;
        }

        IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder> IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder>.UseExistingRoutingInstance(string routingInstanceName)
        {
            base.UseExistingRoutingInstance(routingInstanceName);
            return this;
        }

        IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder> IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder>.WithJumboMtu(bool? useJumboMtu)
        {
            base.WithJumboMtu(useJumboMtu);
            return this;
        }

        IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder> IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder>.WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp)
        {
            base.WithTrustReceivedCosAndDscp(trustReceivedCosAndDscp);
            return this;
        }

        public IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder> WithNewRoutingInstance(bool? newRoutingInstance)
        {
            base._args.Add(nameof(WithNewRoutingInstance), newRoutingInstance != null ? newRoutingInstance : false);
            return this;
        }

        async Task<Attachment> IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder>.UpdateAsync()
        {
            if (_args.ContainsKey(nameof(ForAttachment))) await SetAttachmentAsync();
            if (_args.ContainsKey(nameof(WithJumboMtu))) await SetMtuAsync();
            if (_args.ContainsKey(nameof(WithContractBandwidth))) await CreateContractBandwidthPoolAsync();
            if (base._args.ContainsKey(nameof(WithNewRoutingInstance)) && (bool)base._args[nameof(WithNewRoutingInstance)])
            {
                await base.CreateRoutingInstanceAsync();
            }
            else if (base._args.ContainsKey(nameof(UseExistingRoutingInstance)))
            {
                await AssociateExistingRoutingInstanceAsync();
            }
            if (base._args.ContainsKey(nameof(WithTrustReceivedCosAndDscp))) SetTrustReceivedCosAndDscp();

            _attachment.Validate();

            return _attachment;
        }

        private async Task SetAttachmentAsync()
        {
            var attachmentId = (int)_args[nameof(ForAttachment)];
            var attachment = (from attachments in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                            q.AttachmentID == attachmentId,
                            query: x => x.IncludeValidationProperties(),
                            AsTrackable: true)
                              select attachments)
                             .SingleOrDefault();

            base._attachment = attachment ?? throw new BuilderBadArgumentsException($"Could not find the attachment with ID '{attachmentId}'.");
        }
    }
}

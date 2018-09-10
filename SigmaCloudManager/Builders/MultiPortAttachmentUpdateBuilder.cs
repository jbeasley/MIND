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
    /// Builder for updating multi-port attachments. The builder exposes a fluent API.
    /// </summary>
    public class MultiPortAttachmentUpdateBuilder : MultiPortAttachmentBuilder, IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder>
    {
        public MultiPortAttachmentUpdateBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceDirector> routingInstanceDirectorFactory) : 
            base(unitOfWork, routingInstanceDirectorFactory)
        {
        }

        public IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder> ForAttachment(int attachmentId)
        {
            _args.Add(nameof(ForAttachment), attachmentId);
            return this;
        }

        IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder> IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder>.WithContractBandwidth(int? contractBandwidthMbps)
        {
            base.WithContractBandwidth(contractBandwidthMbps);
            return this;
        }

        IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder> IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder>.WithExistingRoutingInstance(string routingInstanceName)
        {
            base.WithExistingRoutingInstance(routingInstanceName);
            return this;
        }

        IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder> IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder>.WithJumboMtu(bool? useJumboMtu)
        {
            base.WithJumboMtu(useJumboMtu);
            return this;
        }

        IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder> IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder>.WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp)
        {
            base.WithTrustReceivedCosAndDscp(trustReceivedCosAndDscp);
            return this;
        }

        public IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder> WithNewRoutingInstance(bool? newRoutingInstance)
        {
            base._args.Add(nameof(WithNewRoutingInstance), newRoutingInstance != null ? newRoutingInstance : false);
            return this;
        }

        async Task<Attachment> IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder>.UpdateAsync()
        {
            await SetAttachmentAsync();
            await SetMtuAsync();
            if (_args.ContainsKey("contractBandwidthMbps")) await CreateContractBandwidthPoolAsync();

            if (base._args.ContainsKey(nameof(WithNewRoutingInstance)) &&
                (bool)base._args[nameof(WithNewRoutingInstance)])
            {
                await base.CreateRoutingInstanceAsync();
            }
            else if (base._args.ContainsKey(nameof(WithExistingRoutingInstance)) && base._args[nameof(WithExistingRoutingInstance)] != null)
            {
                await AssociateExistingRoutingInstanceAsync();
            }
            if (base._args.ContainsKey(nameof(WithTrustReceivedCosAndDscp))) SetTrustReceivedCosAndDscp();

            Validate();
            return _attachment;
        }

        private async Task SetAttachmentAsync()
        {
            var attachmentId = (int)_args[nameof(ForAttachment)];
            var attachment = (from attachments in await _unitOfWork.AttachmentRepository.GetAsync(q => q.AttachmentID == attachmentId,
               includeProperties: "Tenant," +
               "Device," +
               "RoutingInstance.Attachments," +
               "RoutingInstance.Vifs," +
               "ContractBandwidthPool," +
               "AttachmentRole.PortPool.PortRole," +
               "AttachmentBandwidth," +
               "Interfaces.Ports," +
               "Vifs", AsTrackable: true)
                              select attachments)
                             .Single();

            base._attachment = attachment;
        }
    }
}

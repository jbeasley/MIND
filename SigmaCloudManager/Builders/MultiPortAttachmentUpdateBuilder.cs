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
        public MultiPortAttachmentUpdateBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceBuilder> routingInstanceBuilderFactory) : 
            base(unitOfWork, routingInstanceBuilderFactory)
        {
        }

        public IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder> ForAttachment(Attachment attachment)
        {
            _attachment = attachment;
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
            await SetMtuAsync();
            if (_attachment.AttachmentRole.RequireContractBandwidth)
            {
                if (_args.ContainsKey("contractBandwidthMbps")) await CreateContractBandwidthPoolAsync();
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
            if (base._args.ContainsKey(nameof(WithTrustReceivedCosAndDscp))) SetTrustReceivedCosAndDscp();

            return _attachment;
        }
    }
}

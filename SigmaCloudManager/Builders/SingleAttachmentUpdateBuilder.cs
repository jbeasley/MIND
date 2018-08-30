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
        public SingleAttachmentUpdateBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceDirector> routingInstanceDirectorFactory) : 
            base(unitOfWork, routingInstanceDirectorFactory)
        {
        }

        public IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder> ForAttachment(Attachment attachment)
        {
            _attachment = attachment;
            return this;
        }

        IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder> IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder>.WithContractBandwidth(int? contractBandwidthMbps)
        {
            base.WithContractBandwidth(contractBandwidthMbps);
            return this;
        }

        IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder> IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder>.WithExistingRoutingInstance(string routingInstanceName)
        {
            base.WithExistingRoutingInstance(routingInstanceName);
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

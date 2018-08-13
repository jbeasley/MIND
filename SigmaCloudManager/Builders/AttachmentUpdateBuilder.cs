using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Services;
using SCM.Data;

namespace Mind.Builders
{
    /// <summary>
    /// Abstract builder for updating existing attachments. The builder exposes a fluent API.
    /// </summary>
    public abstract class AttachmentUpdateBuilder<TAttachmentBuilder> : AttachmentBuilder<TAttachmentBuilder>, IAttachmentUpdateBuilder<TAttachmentBuilder>
        where TAttachmentBuilder : AttachmentUpdateBuilder<TAttachmentBuilder>
    {

        public AttachmentUpdateBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceBuilder> routingInstanceBuilderFactory) :
            base(unitOfWork, routingInstanceBuilderFactory)
        {
        }

        public virtual IAttachmentUpdateBuilder<TAttachmentBuilder> ForAttachment(Attachment attachment)
        {
            _attachment = attachment;
            return _builder;
        }

        public virtual IAttachmentUpdateBuilder<TAttachmentBuilder> WithExistingRoutingInstance(string existingRoutingInstanceName)
        {
            _args.Add(nameof(existingRoutingInstanceName), existingRoutingInstanceName);
            return _builder;
        }

        public virtual IAttachmentUpdateBuilder<TAttachmentBuilder> WithNewRoutingInstance(bool? newRoutingInstance = false)
        {
            _args.Add(nameof(WithNewRoutingInstance), newRoutingInstance != null ? newRoutingInstance : false);
            return _builder;
        }

        public virtual IAttachmentUpdateBuilder<TAttachmentBuilder> WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp = false)
        {
            _args.Add(nameof(WithTrustReceivedCosAndDscp), trustReceivedCosAndDscp != null ? trustReceivedCosAndDscp : false);
            return _builder;
        }

        IAttachmentUpdateBuilder<TAttachmentBuilder> IAttachmentUpdateBuilder<TAttachmentBuilder>.WithContractBandwidth(int? contractBandwidthMbps, bool? trustReceivedCosDscp)
        {
            base.WithContractBandwidth(contractBandwidthMbps, trustReceivedCosDscp);
            return _builder;
        }

        IAttachmentUpdateBuilder<TAttachmentBuilder> IAttachmentUpdateBuilder<TAttachmentBuilder>.WithJumboMtu(bool? useJumboMtu)
        {
            base.WithJumboMtu(useJumboMtu);
            return _builder;
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
                else if (base._args.ContainsKey("existingRoutingInstanceName"))
                {
                    await AssociateExistingRoutingInstanceAsync();
                }
            }
            if (base._args.ContainsKey(nameof(WithTrustReceivedCosAndDscp))) SetTrustReceivedCosAndDscp();

            return _attachment;
        }

        protected internal virtual async Task AssociateExistingRoutingInstanceAsync()
        {
            var routingInstanceName = _args["existingRoutingInstanceName"] != null ? _args["existingRoutingInstanceName"].ToString() : null;
            if (routingInstanceName != null)
            {
                var existingRoutingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(x => x.Name == routingInstanceName,
                                               AsTrackable: true)
                                               select routingInstances)
                                               .SingleOrDefault();

                _attachment.RoutingInstance = existingRoutingInstance ?? throw new BuilderBadArgumentsException("Could not find existing routing " +
                    $"instance '{routingInstanceName}.'");
                _attachment.RoutingInstanceID = existingRoutingInstance.RoutingInstanceID;
            }
        }

        protected internal virtual void SetTrustReceivedCosAndDscp()
        {
            if (_attachment.ContractBandwidthPool != null)
            {
                var trustReceivedCosAndDscp = _args[nameof(WithTrustReceivedCosAndDscp)] != null ? (bool)_args[nameof(WithTrustReceivedCosAndDscp)] : false;
                _attachment.ContractBandwidthPool.TrustReceivedCosDscp = trustReceivedCosAndDscp;
            }
        }

        protected override internal void SetPortBandwidthRequired()
        {
            throw new NotImplementedException();
        }

        protected override internal void SetNumberOfPortsRequired()
        {
           throw new NotImplementedException();
        }
    }
}

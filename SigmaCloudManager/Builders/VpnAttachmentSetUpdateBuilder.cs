using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for updating a vpn attachment sets. The builder exposes a fluent api.
    /// </summary>
    public class VpnAttachmentSetUpdateBuilder : VpnAttachmentSetBuilder, IVpnAttachmentSetUpdateBuilder
    {
        public VpnAttachmentSetUpdateBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        IVpnAttachmentSetUpdateBuilder IVpnAttachmentSetUpdateBuilder.ForVpn(int? vpnId)
        {
            base.ForVpn(vpnId);
            return this;
        }

        public IVpnAttachmentSetUpdateBuilder WithAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId.HasValue) _args.Add(nameof(WithAttachmentSet), attachmentSetId);
            return this;
        }

        IVpnAttachmentSetUpdateBuilder IVpnAttachmentSetUpdateBuilder.WithHub(bool? isHub)
        {
            base.WithHub(isHub);
            return this;
        }

        IVpnAttachmentSetUpdateBuilder IVpnAttachmentSetUpdateBuilder.WithMulticastDirectlyIntegrated(bool? isMulticastDirectlyIntegrated)
        {
            base.WithMulticastDirectlyIntegrated(isMulticastDirectlyIntegrated);
            return this;
        }

        public virtual async Task<VpnAttachmentSet> UpdateAsync()
        {
            if (_args.ContainsKey(nameof(ForVpn)) && _args.ContainsKey(nameof(WithAttachmentSet))) await SetVpnAttachmentSetAsync();
            if (_args.ContainsKey(nameof(WithHub))) SetIsHub();
            if (_args.ContainsKey(nameof(WithMulticastDirectlyIntegrated))) SetMulticastDirectlyIntegrated();

            _vpnAttachmentSet.Validate();
            return base._vpnAttachmentSet;
        }

        protected internal virtual async Task SetVpnAttachmentSetAsync()
        {
            var attachmentSetId = (int)_args[nameof(WithAttachmentSet)];
            var vpnId = (int)_args[nameof(ForVpn)];
            var vpnAttachmentSet = (from result in await _unitOfWork.VpnAttachmentSetRepository.GetAsync(
                            q =>
                                q.VpnID == vpnId && q.AttachmentSetID == attachmentSetId,
                                AsTrackable: true,
                                query: x => x.IncludeValidationProperties())
                                select result)
                                .SingleOrDefault();

            _vpnAttachmentSet = vpnAttachmentSet ?? throw new BuilderBadArgumentsException($"The vpn attachment set for vpn with ID '{vpnId}' and " +
                $"attachment set with ID '{attachmentSetId}' was not found.");
        }
    }
}

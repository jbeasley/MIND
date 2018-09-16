using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for vpn attachment sets. The builder exposes a fluent api.
    /// </summary>
    public class VpnAttachmentSetBuilder : BaseBuilder, IVpnAttachmentSetBuilder
    {
        protected internal VpnAttachmentSet _vpnAttachmentSet;

        public VpnAttachmentSetBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _vpnAttachmentSet = new VpnAttachmentSet();
        }

        public virtual IVpnAttachmentSetBuilder ForVpn(int? vpnId)
        {
            if (vpnId.HasValue) _args.Add(nameof(ForVpn), vpnId);
            return this;
        }

        public virtual IVpnAttachmentSetBuilder WithAttachmentSet(string attachmentSetName)
        {
            if (!string.IsNullOrEmpty(attachmentSetName)) _args.Add(nameof(WithAttachmentSet), attachmentSetName);
            return this;
        }

        public IVpnAttachmentSetBuilder WithHub(bool? isHub)
        {
            if (isHub.HasValue) _args.Add(nameof(WithHub), isHub);
            return this;
        }

        public IVpnAttachmentSetBuilder WithMulticastDirectlyIntegrated(bool? isMulticastDirectlyIntegrated)
        {
            if (isMulticastDirectlyIntegrated.HasValue) _args.Add(nameof(WithMulticastDirectlyIntegrated), isMulticastDirectlyIntegrated);
            return this;
        }

        public virtual async Task<VpnAttachmentSet> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForVpn))) await SetVpnAsync();
            if (_args.ContainsKey(nameof(WithAttachmentSet))) await SetAttachmentSetAsync();
            if (_args.ContainsKey(nameof(WithHub))) SetIsHub();
            if (_args.ContainsKey(nameof(WithMulticastDirectlyIntegrated))) SetMulticastDirectlyIntegrated();

            _vpnAttachmentSet.Validate();
            return _vpnAttachmentSet;
        }

        protected internal virtual async Task SetAttachmentSetAsync()
        {
            var attachmentSetName = _args[nameof(WithAttachmentSet)].ToString();
            var attachmentSet = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                            q =>
                                q.Name == attachmentSetName,
                                query: q => 
                                       q.Include(x => x.AttachmentRedundancy)
                                        .Include(x => x.AttachmentSetRoutingInstances)
                                        .ThenInclude(x => x.RoutingInstance.Device.Plane)
                                        .Include(x => x.AttachmentSetRoutingInstances)
                                        .ThenInclude(x => x.RoutingInstance.Attachments)
                                        .Include(x => x.AttachmentSetRoutingInstances)
                                        .ThenInclude(x => x.RoutingInstance.Vifs)
                                        .ThenInclude(x => x.Attachment)
                                        .Include(x => x.VpnTenantMulticastGroups)
                                        .ThenInclude(x => x.TenantMulticastGroup)
                                        .Include(x => x.MulticastVpnDomainType),
                                AsTrackable: true)
                                select result)
                                .SingleOrDefault();

            _vpnAttachmentSet.AttachmentSet = attachmentSet ?? throw new BuilderBadArgumentsException($"The attachment set with name '{attachmentSetName}' " +
                $"was not found.");
        }


        protected internal virtual async Task SetVpnAsync()
        {
            var vpnId = (int)_args[nameof(ForVpn)];
            var vpn = (from result in await _unitOfWork.VpnRepository.GetAsync(
                    q =>
                       q.VpnID == vpnId,
                       AsTrackable: true,
                       query: q => 
                              q.Include(x => x.Plane)
                               .Include(x => x.MulticastVpnDirectionType)
                               .Include(x => x.MulticastVpnServiceType)
                               .Include(x => x.VpnTopologyType))
                       select result)
                       .SingleOrDefault();

            _vpnAttachmentSet.Vpn = vpn ?? throw new BuilderBadArgumentsException($"The vpn with ID '{vpnId}' " +
                $"was not found.");
        }

        protected virtual internal void SetIsHub()
        {
            _vpnAttachmentSet.IsHub = (bool)_args[nameof(WithHub)];
        }

        protected virtual internal void SetMulticastDirectlyIntegrated()
        {
            _vpnAttachmentSet.IsMulticastDirectlyIntegrated = (bool)_args[nameof(WithMulticastDirectlyIntegrated)];
        }
    }
}

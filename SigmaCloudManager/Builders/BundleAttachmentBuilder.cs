using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Data;
using SCM.Services;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for bundle attachments. The builder exposes a fluent API.
    /// </summary>
    public class BundleAttachmentBuilder : AttachmentBuilder<BundleAttachmentBuilder>, IBundleAttachmentBuilder
    {
        public BundleAttachmentBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceDirector> routingInstanceDirectorFactory) : 
            base(unitOfWork, routingInstanceDirectorFactory)
        {
        }

        public IBundleAttachmentBuilder WithBundleLinks(int? minLinks, int? maxLinks)
        {
            if (minLinks != null) _args.Add(nameof(minLinks), minLinks);
            if (maxLinks != null) _args.Add(nameof(maxLinks), maxLinks);
           
            return this;
        }

        IBundleAttachmentBuilder IBundleAttachmentBuilder.ForTenant(int tenantId)
        {
            base.ForTenant(tenantId);
            return this;
        }

        IBundleAttachmentBuilder IBundleAttachmentBuilder.WithIpv4(List<Ipv4AddressAndMask> ipv4Addresses)
        {
            base.WithIpv4(ipv4Addresses);
            return this;
        }

        IBundleAttachmentBuilder IBundleAttachmentBuilder.WithAttachmentRole(string portPoolName, string attachmentRoleName)
        {
            base.WithAttachmentRole(portPoolName, attachmentRoleName);
            return this;
        }

        IBundleAttachmentBuilder IBundleAttachmentBuilder.WithAttachmentBandwidth(int? attachmentBandwidthGbps)
        {
            base.WithAttachmentBandwidth(attachmentBandwidthGbps);
            return this;
        }

        IBundleAttachmentBuilder IBundleAttachmentBuilder.WithLocation(string locationName)
        {
            base.WithLocation(locationName);
            return this;
        }

        IBundleAttachmentBuilder IBundleAttachmentBuilder.WithPlane(string planeName)
        {
            base.WithPlane(planeName);
            return this;
        }

        IBundleAttachmentBuilder IBundleAttachmentBuilder.WithContractBandwidth(int? contractBandwidthMbps)
        {
            base.WithContractBandwidth(contractBandwidthMbps);
            return this;
        }

        IBundleAttachmentBuilder IBundleAttachmentBuilder.WithTrustReceivedCosAndDscp(bool? trustReceivedCosAndDscp)
        {
            base.WithTrustReceivedCosAndDscp(trustReceivedCosAndDscp);
            return this;
        }

        IBundleAttachmentBuilder IBundleAttachmentBuilder.WithJumboMtu(bool? useJumboMtu)
        {
            base.WithJumboMtu(useJumboMtu);
            return this;
        }

        public override async Task<Attachment> BuildAsync()
        {
            await base.BuildAsync();
            _attachment.IsBundle = true;
            SetBundleLinks();
            await CreateBundleIdAsync();
            Validate();

            return _attachment;
        }

        protected internal override void SetNumberOfPortsRequired()
        {
            var attachmentBandwidth = _attachment.AttachmentBandwidth;
            _numPortsRequired = attachmentBandwidth.BandwidthGbps / attachmentBandwidth.BundleOrMultiPortMemberBandwidthGbps.Value;
        }

        protected internal override void SetPortBandwidthRequired()
        {
            _portBandwidthRequired = _attachment.AttachmentBandwidth.BundleOrMultiPortMemberBandwidthGbps.Value;
        }

        protected internal virtual void SetBundleLinks()
        {
            var numPorts = _attachment.Interfaces.SelectMany(x => x.Ports).Count();
            var minLinks = _args.ContainsKey("minLinks") ? (int)_args["minLinks"] : numPorts;
            var maxLinks = _args.ContainsKey("maxLinks") ? (int)_args["maxLinks"] : numPorts;
            _attachment.BundleMinLinks = minLinks;
            _attachment.BundleMaxLinks = maxLinks;
        }

        protected override internal void Validate()
        {
            base.Validate();
            if (!_attachment.AttachmentRole.SupportedByBundle) throw new BuilderIllegalStateException($"The requested attachment role " +
            $"'{_attachment.AttachmentRole.Name}' is not supported with a bundle attachment.");

            if (!_attachment.AttachmentBandwidth.SupportedByBundle) throw new BuilderIllegalStateException($"The requested attachment " +
            $"bandwidth '{_attachment.AttachmentBandwidth.BandwidthGbps} Gbps' is not supported with a bundle attachment.");

            var numPorts = _attachment.Interfaces.SelectMany(x => x.Ports).Count();
            if (_attachment.BundleMinLinks > numPorts) throw new BuilderIllegalStateException($"The min links parameter for the bundle " +
                $"({_attachment.BundleMinLinks}) must be " +
                $"less than or equal to the total number of ports required for the bundle ({numPorts}).");

            if (_attachment.BundleMaxLinks > numPorts) throw new BuilderIllegalStateException($"The max links parameter for the bundle " +
                $"({_attachment.BundleMaxLinks}) must be " +
                $"less than or equal to the total number of ports required for the bundle ({numPorts}).");

            if (_attachment.BundleMinLinks > _attachment.BundleMaxLinks) throw new BuilderIllegalStateException($"The min links parameter for the bundle " +
                $"({_attachment.BundleMinLinks}) must be less then " +
                $"or equal to the max links parameter for the bundle ({_attachment.BundleMaxLinks})");
        }

        private async Task CreateBundleIdAsync()
        {
            var usedBundleIds = (from attachments in await _unitOfWork.AttachmentRepository.GetAsync(
                            q => 
                                q.DeviceID == _attachment.DeviceID && q.IsBundle)
                                select attachments)
                                .Select(q => q.ID).Where(q => q != null)
                                .ToList();

            int? id = Enumerable.Range(1, 65535).Except(usedBundleIds.Select(q => q.Value)).FirstOrDefault();

            _attachment.ID = id ?? throw new BuilderUnableToCompleteException("Unable to assign an ID value to the bundle attachment. "
                    + $"It seems that all IDs in the range 1 - 65535 for device '{_attachment.Device.Name}' have been used. " +
                    $"Contact your system administrator to report this issue.");
        }
    }
}

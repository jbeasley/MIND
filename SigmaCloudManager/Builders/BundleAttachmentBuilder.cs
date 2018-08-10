using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Data;
using SCM.Services;
using SCM.Models;

namespace Mind.Builders
{
    public class BundleAttachmentBuilder : AttachmentBuilder, IAttachmentBuilder
    {
        public BundleAttachmentBuilder(IUnitOfWork unitOfWork, IMtuService mtuService, IAttachmentRoleService attachmentRoleService,
            IAttachmentBandwidthService attachmentBandwidthService, IContractBandwidthService contractBandwidthService,
            ILocationService locationService, IPlaneService planeService, IRoutingInstanceTypeService routingInstanceTypeService,
            Func<RoutingInstanceType, IRoutingInstanceBuilder> routingInstanceBuilderFactory) : base(unitOfWork, mtuService, attachmentRoleService, 
                attachmentBandwidthService, contractBandwidthService, locationService, planeService, routingInstanceTypeService, routingInstanceBuilderFactory)
        {
            _attachment.IsBundle = true;
        }
        
        public IAttachmentBuilder WithBundleMinLinks(int minLinks)
        {
            _args.Add(nameof(minLinks), minLinks);
            return this;
        }

        public IAttachmentBuilder WithBundleMaxLinks(int maxLinks)
        {
            _args.Add(nameof(maxLinks), maxLinks);
            return this;
        }

        public override async Task<Attachment> BuildAsync()
        {
            await base.BuildAsync();
            SetBundleMaxLink();
            SetBundleMinLinks();

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

        private void SetBundleMinLinks()
        {
            var minLinks = _args.ContainsKey("minLinks") ? (int)_args["minLinks"] : _numPortsRequired;
            _attachment.BundleMinLinks = minLinks;
        }

        private void SetBundleMaxLink()
        {
            var maxLinks = _args.ContainsKey("maxLinks") ? (int)_args["maxLinks"] : _numPortsRequired;
            _attachment.BundleMaxLinks = maxLinks;
        }
    }
}

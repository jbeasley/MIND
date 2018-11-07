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
    /// Builder for single attachments. The builder exposes a fluent API.
    /// </summary>
    public class SingleAttachmentBuilder : AttachmentBuilder<SingleAttachmentBuilder>, IAttachmentBuilder<SingleAttachmentBuilder>
    {
        public SingleAttachmentBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IVrfRoutingInstanceDirector> routingInstanceDirectorFactory) : 
            base(unitOfWork, routingInstanceDirectorFactory)
        {
        }

        public async override Task<Attachment> BuildAsync()
        {
            await base.BuildAsync();
            base._attachment.Validate();
            return base._attachment;
        }

        /// <summary>
        /// Set the port bandwidth required. The port bandwidth is equal to the requested attachment
        /// bandwidth for single attachments.
        /// </summary>
        protected override internal void SetPortBandwidthRequired()
        {
            var attachmentBandwidth = _attachment.AttachmentBandwidth;
            if (attachmentBandwidth.MustBeBundleOrMultiPort)
            {
                throw new BuilderBadArgumentsException("The requested attachment bandwidth is only supported by a bundle or multiport.");
            }
            _portBandwidthRequired = attachmentBandwidth.BandwidthGbps;
        }

        /// <summary>
        /// Set the number of ports required. The number of ports is 1 for single attachments.
        /// </summary>
        protected override internal void SetNumberOfPortsRequired()
        {
            _numPortsRequired = 1;
        }

        /// <summary>
        /// Checks that the attachment role is compatible with a single attachment  
        /// </summary>
        protected internal override void CheckAttachmentRoleIsValid()
        {
            // No check here is required
        }
    }
}

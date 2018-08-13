using SCM.Data;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class SingleAttachmentBuilder : AttachmentBuilder<SingleAttachmentBuilder>, IAttachmentBuilder<SingleAttachmentBuilder>
    {
        public SingleAttachmentBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceBuilder> routingInstanceBuilderFactory) : 
            base(unitOfWork, routingInstanceBuilderFactory)
        {
            _builder = this;
        }

        /// <summary>
        /// Set the port bandwidth required. The port bandwidth is equal to the requested attachment
        /// bandwidth for single attachments.
        /// </summary>
        protected override internal void SetPortBandwidthRequired()
        {
            _portBandwidthRequired = _attachment.AttachmentBandwidth.BandwidthGbps;
        }

        /// <summary>
        /// Set the number of ports required. The number of ports is 1 for single attachments.
        /// </summary>
        protected override internal void SetNumberOfPortsRequired()
        {
            _numPortsRequired = 1;
        }
    }
}

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AttachmentSetUpdate
    {
        /// <summary>
        /// The sub-region within which the attachment set operates
        /// </summary>
        /// <value>A string value for the subregion within which the attachment set operates</value>
        public string SubRegion { get; set; }

        /// <summary>
        /// Determines the type of attachment redundancy supported by the attachment set
        /// </summary>
        /// <value>An enum member for the attachment redundancy supported by the attachment set</value>
        public AttachmentRedundancyEnum? AttachmentRedundancy { get; set; }

        /// <summary>
        /// Determines the type of multicast domain supported by the attachment set
        /// </summary>
        /// <value>An enum member for the multicast domain supported by the attachment set</value>
        public MulticastVpnDomainTypeEnum? MulticastVpnDomainType { get; set; }

        /// <summary>
        /// A list of routing instances which are to be associated with the attachment set
        /// </summary>
        /// <value>A list of RoutingInstanceForAttachmentSetRequest objects</value>
        public List<RoutingInstanceForAttachmentSetRequest> AttachmentSetRoutingInstances { get; set; }

        /// <summary>
        /// The bgp IP network inbound policy of the attachment set
        /// </summary>
        /// <value>An instance of BgpIpNetworkInboundPolicyRequest</value>
        public BgpIpNetworkInboundPolicyRequest BgpIpNetworkInboundPolicy { get; set; }

        /// <summary>
        /// The bgp IP network outbound policy of the attachment set
        /// </summary>
        /// <value>An instance of BgpIpNetworkOutboundPolicyRequest</value>
        public BgpIpNetworkOutboundPolicyRequest BgpIpNetworkOutboundPolicy { get; set; }
    }
}
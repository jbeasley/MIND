﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Models.RequestModels
{
    public class AttachmentSetRequest
    {
        /// <summary>
        /// The geographic region within which the attachment set operates
        /// </summary>
        /// <value>An enum member denoting the region within which the attachment set operates</value>
        public RegionEnum? Region { get; set; }

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
        /// Determines if the attachment set should be enabled for layer 3
        /// </summary>
        /// <value>Boolean denoting the layer 3 enablement state</value>
        public bool? IsLayer3 { get; set; }

        /// <summary>
        /// Requests for routing instances to be added to the attachment set
        /// </summary>
        /// <value>A list of RoutingInstanceForAttachmentSetRequest objects</value>
        public List<RoutingInstanceForAttachmentSetRequest> AttachmentSetRoutingInstances { get; set; }

        /// <summary>
        /// Requests for tenant IP networks to to be added to the BGP inbound policy of the attachment set
        /// </summary>
        /// <value>A list of VpnTenantIpNetworkInRequest objects</value>
        public List<VpnTenantIpNetworkInRequest> BgpIpNetworkInboundPolicy{ get; set; }

        /// <summary>
        /// Requests for tenant IP networks to to be added to the BGP outbound policy of the attachment set
        /// </summary>
        /// <value>A list of VpnTenantIpNetworkOutRequest objects</value>
        public List<VpnTenantIpNetworkOutRequest> BgpIpNetworkOutboundPolicy { get; set; }

        /// <summary>
        /// Determines the type of multicast domain supported by the attachment set
        /// </summary>
        /// <value>An enum member for the multicast domain supported by the attachment set</value>
        public MulticastVpnDomainTypeEnum? MulticastVpnDomainType { get; set; }
    }
}

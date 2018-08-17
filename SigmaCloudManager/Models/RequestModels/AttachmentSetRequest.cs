using System;
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
        /// <value>The geographic region within which the attachment set operates</value>
        public enum RegionEnum
        {
            /// <summary>
            /// Enum for EMEA
            /// </summary>
            EMEA = 1,

            /// <summary>
            /// Enum for ASIAPAC
            /// </summary>
            ASIAPAC = 2,

            /// <summary>
            /// Enum for AMERS
            /// </summary>
            AMERS = 3
        }

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
        /// <value>An enumerated list of attachment redundancy options</value>
        public enum AttachmentRedundancyEnum
        {
            /// <summary>
            /// Enum for Bronze
            /// </summary>
            Bronze = 1,

            /// <summary>
            /// Enum for Silver
            /// </summary>
            Silver = 2,

            /// <summary>
            /// Enum for Gold
            /// </summary>
            Gold = 3,

            /// <summary>
            /// Enum for Custom
            /// </summary>
            Custom = 4
        }

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
        public List<RoutingInstanceForAttachmentSetRequest> RoutingInstances { get; set; }

        /// <summary>
        /// Determines the type of multicast domain supported by the attachment set
        /// </summary>
        /// <value>An enumerated list of multicast domain options</value>
        public enum MulticastVpnDomainTypeEnum
        {
            /// <summary>
            /// Enum for Sender-Only
            /// </summary>
            SenderOnly = 1,

            /// <summary>
            /// Enum for Receiver-Only
            /// </summary>
            ReceiverOnly = 2,

            /// <summary>
            /// Enum for Sender-and-Receiver
            /// </summary>
            SenderAndReceiver = 3
        }

        /// <summary>
        /// Determines the type of multicast domain supported by the attachment set
        /// </summary>
        /// <value>An enum member for the multicast domain supported by the attachment set</value>
        public MulticastVpnDomainTypeEnum? MulticastVpnDomainType { get; set; }
    }
}

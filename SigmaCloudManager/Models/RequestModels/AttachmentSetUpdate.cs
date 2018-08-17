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
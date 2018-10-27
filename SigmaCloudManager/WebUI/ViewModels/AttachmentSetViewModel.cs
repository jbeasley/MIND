
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

namespace Mind.WebUI.Models
{ 
    /// <summary>
    /// Model of an attachment set
    /// </summary>
    public class AttachmentSetViewModel
    { 
        /// <summary>
        /// ID of the attachment set
        /// </summary>
        /// <value>Integer value for the ID of the attachment set</value>
        /// <example>11001</example>
        [Display(Name="Attachment Set ID")]
        public int AttachmentSetId { get; private set; }

        /// <summary>
        /// MIND System-generated name of the attachment set
        /// </summary>
        /// <value>String value for the name of the attachment set</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        [Display(Name="Name")]
        public string Name { get; private set; }

        /// <summary>
        /// The geographic region within which the attachment set operates.
        /// </summary>
        /// <value>String value denoting the geographic region within which the attachment set operates</value>
        /// <example>EMEA</example>
        [Display(Name="Region")]
        public string Region { get; private set; }

        /// <summary>
        /// The geographic sub-region within which the attachment set operates
        /// </summary>
        /// <value>String value denoting the geographic sub-region within which the attachment set operates</value>
        /// <example>UK</example>
        [Display(Name="SubRegion")]
        public string SubRegion { get; private set; }

        /// <summary>
        /// Denotes the attachment redundancy level supported by the attachment set
        /// </summary>
        /// <value>String value denoting the attachment redundancy level</value>
        /// <example>Silver</example>
        [Display(Name="Attachment Redundancy")]
        public string AttachmentRedundancy { get; private set; }

        /// <summary>
        /// Denotes the multicast vpn domain type of the attachment set
        /// </summary>
        /// <value>String value denoting the multicast vpn domain type</value>
        /// <example>Sender-Only</example>
        [Display(Name = "Multicast VPN Domain Type")]
        public string MulticastVpnDomainType { get; private set; }

        /// <summary>
        /// Denotees whether the attachment set is enabled for layer 3
        /// </summary>
        /// <value>Boolean value denoting if the attachment set is enabled for layer 3</value>
        /// <example>true</example>
        [Display(Name = "Layer 3")]
        public bool IsLayer3 { get; private set; }

        /// <summary>
        /// The list of routing instances which belong to the attachment Set
        /// </summary>
        /// <value>A list of AttachmentSetRoutingInstance objects</value>
        [Display(Name="Attachment Set Routing Instances")]
        public List<AttachmentSetRoutingInstanceViewModel> AttachmentSetRoutingInstances { get; private set; }
    }
}

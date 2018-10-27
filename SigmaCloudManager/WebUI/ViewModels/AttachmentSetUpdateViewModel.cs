
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
    /// Model for updating an attachment set
    /// </summary>
    public class AttachmentSetUpdateViewModel : IModifiableResource
    {
        /// <summary>
        /// ID of the attachment set
        /// </summary>
        /// <value>Integer value for the ID of the attachment set</value>
        /// <example>11001</example>
        [Display(Name = "Attachment Set ID")]
        public int AttachmentSetId { get; private set; }

        /// <summary>
        /// MIND System-generated name of the attachment set
        /// </summary>
        /// <value>String value for the name of the attachment set</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        [Display(Name = "Name")]
        public string Name { get; private set; }

        /// <summary>
        /// The sub-region within which the attachment set operates
        /// </summary>
        /// <value>A string value for the subregion within which the attachment set operates</value>
        /// <example>UK</example>
        [Display(Name = "SubRegion")]
        public string SubRegion { get; set; } = "None";

        /// <summary>
        /// Determines the ttachment redundancy level supported by the attachment set
        /// </summary>
        /// <value>An enum member for the attachment redundancy supported by the attachment set</value>
        /// <example>Silver</example>
        [Display(Name="Attachment Redundancy")]
        public AttachmentRedundancyEnum? AttachmentRedundancy { get; set; }

        /// <summary>
        /// A list of names of routing instances to be associated with the attachment set.
        /// Each routing instance must exist and belong to an attachment which is owned by the tenant owner requesting the attachment set.
        /// The names passed as a list of strings to/from the web UI for simple integration with a drop-down list defined in the view.
        /// </summary>
        /// <value>A list of strings with each string denoting the name of a routing instance</value>
        [Display(Name = "Routing Instances")]
        public List<string> AttachmentSetRoutingInstanceNames { get; set; }

        /// <summary>
        /// Determines the multicast domain type supported by the attachment set
        /// </summary>
        /// <value>An enum member for the multicast domain supported by the attachment set</value>
        /// <example>Sender-and-Receiver</example>
        [Display(Name = "Multicast VPN Domain Type")]
        public MulticastVpnDomainTypeEnum? MulticastVpnDomainType { get; set; }

        /// <summary>
        /// A list of routing instances routing instance request objects to be associated with the attachment set.
        /// This list is created on submission of the edit form from the web UI from the list of routing instance names given
        /// in the AttachmentSetRoutingInstanceNames property of this model. Each routing instance name in the list is mapped into an
        /// instance of AttachmentSetRoutingInstanceRequest and added this list. This step makes it easy to integrate the edit method
        /// of the controller with the service layer of the application.
        /// </summary>
        public List<AttachmentSetRoutingInstanceRequestViewModel> AttachmentSetRoutingInstances { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

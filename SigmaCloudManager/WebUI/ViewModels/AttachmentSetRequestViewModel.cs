
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
    /// Model for requesting a new attachment set
    /// </summary>
    public class AttachmentSetRequestViewModel
    {
        /// <summary>
        /// The ID of the tenant for which the new attachment set will be created.
        /// </summary>
        /// <value>Integer value denoting the ID of the tenant</value>
        /// <example>9001</example>
        [Required]
        public int? TenantId { get; set; }

        /// <summary>
        /// The major geographic region within which the attachment set operates
        /// </summary>
        /// <value>An enum member denoting the region within which the attachment set operates</value>
        /// <example>EMEA</example>
        [Display(Name = "Region")]
        [Required(ErrorMessage = "A region must be selected")]
        public RegionEnum? Region { get; set; }

        /// <summary>
        /// The geographic sub-region within which the attachment set operates
        /// </summary>
        /// <value>A string value denoting the subregion within which the attachment set operates</value>
        /// <example>UK</example>
        [Display(Name = "SubRegion")]
        public string SubRegion { get; set; }

        /// <summary>
        /// Determines the attachment redundancy level supported by the attachment set
        /// </summary>
        /// <value>An enum member for the attachment redundancy supported by the attachment set</value>
        /// <example>Silver</example>
        [Display(Name = "Attachment Redundancy")]
        [Required(ErrorMessage = "A redundancy option must be selected")]
        public AttachmentRedundancyEnum? AttachmentRedundancy { get; set; }

        /// <summary>
        /// Determines if the attachment set should be enabled for layer 3
        /// </summary>
        /// <value>Boolean denoting the layer 3 enablement state</value>
        /// <example>true</example>
        [Display(Name = "Layer 3")]
        public bool IsLayer3 { get; set; } = true;

        /// <summary>
        /// A list of names of routing instances to be associated with the attachment set.
        /// Each routing instance must exist and belong to an attachment which is owned by the tenant owner requesting the attachment set.
        /// The names are passed as a list of strings to/from the web UI for simple integration with a drop-down list defined in the view.
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
        /// A list of routing instances request objects to be associated with the attachment set.
        /// This list is created on submission of the create form from the web UI from the list of routing instance names given
        /// in the AttachmentSetRoutingInstanceNames property of this model. Each routing instance name in the list is mapped into an
        /// instance of AttachmentSetRoutingInstanceRequest and added this list. This step makes it easy to integrate the create method
        /// of the controller with the service layer of the application.
        /// </summary>
        public List<AttachmentSetRoutingInstanceRequestViewModel> AttachmentSetRoutingInstances { get; set; }

        /// <summary>
        /// A list of tenant IP network associations with the bgp inbound policy of the attachment set
        /// </summary>
        /// <value>A list of VpnTenantIpNetworkInRequestViewModel objects</value>
        [Display(Name = "BGP IP Network Inbound Policy")]
        public List<VpnTenantIpNetworkInRequestViewModel> BgpIpNetworkInboundPolicy { get; set; }

        /// <summary>
        /// A list of tenant IP network associations with the bgp outbound policy of the attachment set
        /// </summary>
        /// <value>A list of VpnTenantIpNetworkInRequestViewModel objects</value>
        [Display(Name = "BGP IP Network Outbound Policy")]
        public List<VpnTenantIpNetworkOutRequestViewModel> BgpIpNetworkOutboundPolicy { get; set; }
    }     
}

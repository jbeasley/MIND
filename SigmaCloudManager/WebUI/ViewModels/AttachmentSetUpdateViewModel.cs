
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
        public int AttachmentSetId { get; set; }

        /// <summary>
        /// The ID of the tenant owner of the attachment set
        /// </summary>
        /// <value>Integer value denoting the ID of the tenant</value>
        /// <example>9001</example>
        public int? TenantId { get; set; }

        /// <summary>
        /// MIND System-generated name of the attachment set
        /// </summary>
        /// <value>String value for the name of the attachment set</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// The geographic region within which the attachment set operates.
        /// </summary>
        /// <value>String value denoting the geographic region within which the attachment set operates</value>
        /// <example>EMEA</example>
        [Display(Name = "Region")]
        public string Region { get; set; }

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
        /// Determines the multicast domain type supported by the attachment set
        /// </summary>
        /// <value>An enum member for the multicast domain supported by the attachment set</value>
        /// <example>Sender-and-Receiver</example>
        [Display(Name = "Multicast VPN Domain Type")]
        public MulticastVpnDomainTypeEnum? MulticastVpnDomainType { get; set; }

        /// <summary>
        /// A list of routing instances routing instance request objects to be associated with the attachment set.
        /// </summary>
        /// <value>A list of AttachmentSetRoutingInstanceRequestViewModel objects</value>
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
        /// <value>A list of VpnTenantIpNetworkOutRequestViewModel objects</value>
        [Display(Name = "BGP IP Network Outbound Policy")]
        public List<VpnTenantIpNetworkOutRequestViewModel> BgpIpNetworkOutboundPolicy { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

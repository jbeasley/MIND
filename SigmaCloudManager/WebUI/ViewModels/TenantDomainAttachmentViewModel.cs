using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of an attachment in the tenant domain
    /// </summary>
    public class TenantDomainAttachmentViewModel
    {
        /// <summary>
        /// ID of the attachment
        /// </summary>
        /// <value>Integer valude denoting the ID of the attachment</value>
        /// <exmple>6001</exmple>
        public int? AttachmentId { get; private set; }

        /// <summary>
        /// ID of the tenant device
        /// </summary>
        /// <value>Integer valude denoting the ID of the tenant device</value>
        /// <exmple>1001</exmple>
        public int? DeviceId { get; private set; }

        /// <summary>
        /// Gets the name of the attachment.
        /// </summary>
        /// <value>String value denoting the name of the attachment</value>
        public string Name { get; private set; }

        /// <summary>
        /// The attachment is enabled for layer 3
        /// </summary>
        /// <value>Boolean which denotes whether the attachment is enabled for layer 3</value>
        /// <example>true</example>
        [Display(Name = "Layer3")]
        public bool IsLayer3 { get; private set; }

        /// <summary>
        /// The attachment is delivered as a bundle
        /// </summary>
        /// <value>Boolean value which denotes if the attachment is delivered as a bundle</value>
        /// <example>true</example>
        [Display(Name = "Bundle")]
        public bool IsBundle { get; private set; }

        /// <summary>
        /// For bundle attachments, the minimum number of active links in the bundle
        /// </summary>
        /// <value>Integer value denoting the minimum links in the bundle</value>
        /// <example>2</example>
        [Display(Name = "Bundle Min Links")]
        public int? BundleMinLinks { get; private set; }

        /// <summary>
        /// For bundle attachments, the maximum number of active links in the bundle
        /// </summary>
        /// <value>Integer value denoting the maximum links in the bundle</value>
        /// <example>2</example>
        [Display(Name = "Bundle Max Links")]
        public int? BundleMaxLinks { get; private set; }

        /// <summary>
        /// The attachment is delivered as a multiport
        /// </summary>
        /// <value>Boolean denoting if the attachment is delivered as a multiport</value>
        /// <example>true</example>
        [Display(Name = "Multiport")]
        public bool IsMultiport { get; private set; }

        /// <summary>
        /// The attachment is enabled with tagging
        /// </summary>
        /// <value>Boolean value denoting if the attachment is enabled with tagging</value>
        /// <example>true</example>
        [Display(Name = "Tagged")]
        public bool IsTagged { get; private set; }

        /// <summary>
        /// The name of the tenant domain device which terminates the attachment
        /// </summary>
        /// <value>String value denoting the name of the tenant device</value>
        /// <example>UK2-PE1</example>
        [Display(Name = "Tenant Device Name")]
        public string TenantDeviceName { get; private set; }

        /// <summary>
        /// The name of tenant network location within which the tenant device exists
        /// </summary>
        /// <value>String value denoting the name of the tenant network location</value>
        /// <example>DTC</example>
        [Display(Name = "Location Name")]
        public string LocationName { get; private set; }

        /// <summary>
        /// The bandwidth of the attachment in Gbps
        /// </summary>
        /// <value>Integer value denoting the bandwidth of the attachment in Gbps</value>
        /// <example>10</example>
        [Display(Name = "Attachment Bandwidth (Gbps)")]
        public int? AttachmentBandwidthGbps { get; private set; }

        /// <summary>
        /// The contract bandwidth pool created for the attachment
        /// </summary>
        /// <value>String value denoting the name of the contract bandwidth pool</value>
        [Display(Name = "Contract Bandwidth Name")]
        public string ContractBandwidthPoolName { get; private set; }

        /// <summary>
        /// The routing instance name for the attachment
        /// </summary>
        /// <value>String value denoting the name of the routing instance</value>
        [Display(Name = "Routing Instance Name")]
        public string RoutingInstanceName { get; private set; }

        /// <summary>
        /// The name of the attachment role
        /// </summary>
        /// <value>String value denoting the name of an attachment role</value>
        /// <example>CE-LAN-UNTAGGED</example>
        [Display(Name = "Attachment Role Name")]
        public string AttachmentRoleName { get; private set; }

        /// <summary>
        /// A list of interfaces created for the attachment
        /// </summary>
        /// <value>A list of InterfaceViewModel objects</value>
        [Display(Name = "Interfaces")]
        public List<InterfaceViewModel> Interfaces { get; private set; }

        /// <summary>
        /// The maximum transmission unit supported by the attachment
        /// </summary>
        /// <value>The MTU in bytes</value>
        [Display(Name = "MTU")]
        public int? Mtu { get; private set; }

        /// <summary>
        /// The routing instance for the attachment
        /// </summary>
        /// <value>An object of type RoutingInstanceViewModel</value>
        public RoutingInstanceViewModel RoutingInstance { get; private set; }

        /// <summary>
        /// The description of the attachment
        /// </summary>
        /// <value>String value denoting the description</value>
        /// <example>A description of the attachment</example>
        public string Description { get; private set; }

        /// <summary>
        /// Notes for the attachment
        /// </summary>
        /// <value>String value for the notes for the attachment</value>
        /// <example>Some notes about the attachment</example>
        public string Notes { get; private set; }

    }
}

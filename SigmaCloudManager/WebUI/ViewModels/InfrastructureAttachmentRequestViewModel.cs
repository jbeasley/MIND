using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// View Model for requesting an infrastructure attachment
    /// </summary>
    public class InfrastructureAttachmentRequestViewModel
    {
        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>Integer value denoting the device identifier.</value>
        public int DeviceId { get; set; }

        /// <summary>
        /// Determines if a bundle style of attachment is required
        /// </summary>
        /// <value>Boolean value which denotes if a bundle style of attachment is required</value>
        /// <example>true</example>
        [Display(Name = "Bundle Required")]
        public bool BundleRequired { get; set; }

        /// <summary>
        /// The minimum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the minimum links in the bundle</value>
        /// <example>2</example>
        [Display(Name = "Bundle Min Links")]
        [Range(1, 8)]
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the maximum links in the bundle</value>
        /// <example>2</example>
        [Display(Name = "Bundle Max Links")]
        [Range(1, 8)]
        public int? BundleMaxLinks { get; set; }

        /// <summary>
        /// The name of a port pool from which ports for the new attachment will be allocated from
        /// </summary>
        /// <value>String value denoting the name of a port pool</value>
        /// <example>Core</example>
        [Required]
        [Display(Name = "Port Pool Name")]
        public string PortPoolName { get; set; }

        /// The name of an attachment role. This argument sets certain constraints on how the attachment must be configured such
        /// as whether the attachment requires contract bandwidth to be defined.
        /// <value>String value denoting the name of an attachment role</value>
        /// <example>PE-P</example>
        [Required]
        [Display(Name = "Attachment Role Name")]
        public string AttachmentRoleName { get; set; }

        /// <summary>
        /// The required bandwidth of the attachment in Gbps
        /// </summary>
        /// <value>Integer value denoting the required attachment bandwidth in Gbps</value>
        /// <example>10</example>
        [Display(Name = "Attachment Bandwidth (Gbps)")]
        [Required]
        public int? AttachmentBandwidthGbps { get; set; }

        /// <summary>
        /// A list of IPv4 addresses to be assigned to the interfaces of the attachment.
        /// </summary>
        /// <value>A list of Ipv4AddressAndMask objects</value>
        [Display(Name = "IPv4 Addresses")]
        public List<Ipv4AddressAndMaskViewModel> Ipv4Addresses { get; set; }

        /// <summary>
        /// A description for the new attachment
        /// </summary>
        /// <value>String value for the description</value>
        /// <example>Connectivity between UK2-P1 and UK2-PE1</example>
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Notes for the new attachment
        /// </summary>
        /// <value>String value for notes</value>
        /// <example>Some user notes which help explain the purpose of the attachment</example>
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Optional parameters for creating a routing instance to be associated with the new attachment.
        /// </summary>
        /// <value>An object of type RoutingInstanceRequestViewModel</value>
        public RoutingInstanceRequestViewModel RoutingInstance { get; set; }

    }
}

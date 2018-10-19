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
    /// Model for requesting tenant attachment to the provider domain
    /// </summary>
    public class ProviderDomainAttachmentRequestViewModel
    {
        /// <summary>
        /// The ID of the tenant for which the new attachment will be allocated.
        /// </summary>
        /// <value>Integer value denoting the ID of the tenant</value>
        /// <example>9001</example>
        [Required]
        public int? TenantId { get; set; }

        /// <summary>
        /// Determines if a bundle style of attachment is required
        /// </summary>
        /// <value>Boolean value which denotes if a bundle style of attachment is required</value>
        /// <example>true</example>
        [Display(Name ="Bundle Required")]
        public bool BundleRequired { get; set; }

        /// <summary>
        /// The minimum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the minimum links in the bundle</value>
        /// <example>2</example>
        [Range(1, 8)]
        [Display(Name = "Bundle Min Links")]
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the maximum links in the bundle</value>
        /// <example>2</example>
        [Range(1, 8)]
        [Display(Name = "Bundle Max Links")]
        public int? BundleMaxLinks { get; set; }

        /// <summary>
        /// Determines if a multi port style of attachment is required
        /// </summary>
        /// <value>Boolean value which denotes if a multi port style of attachment is required</value>
        public bool MultiportRequired { get; set; }

        /// <summary>
        /// The ID of the region within which the new attachment will be provisioned
        /// </summary>
        /// <value>Integer value denoting the ID of the region</value>
        /// <example>9001</example>
        [Required(ErrorMessage = "A region must be selected")]
        [Display(Name = "Region")]
        public int? RegionId { get; set; }

        /// <summary>
        /// The ID of the subregion within which the new attachment will be provisioned
        /// </summary>
        /// <value>Integer value denoting the ID of the subregion</value>
        /// <example>9001</example>
        [Required(ErrorMessage = "A subregion must be selected")]
        [Display(Name = "SubRegion")]
        public int? SubRegionId { get; set; }

        /// <summary>
        /// The name of a provider network location within which the new attachment will be provisioned
        /// </summary>
        /// <value>String value denoting the name of a provider network location</value>
        /// <example>UK2</example>
        [Required(AllowEmptyStrings = false, ErrorMessage = "A location must be selected")]
        [Display(Name = "Location")]
        public string LocationName { get; set; }

        /// <summary>
        /// The name of a port pool from which ports for the new attachment will be allocated from
        /// </summary>
        /// <value>String value denoting the name of a port pool</value>
        /// <example>General</example>
        [Required(AllowEmptyStrings = false, ErrorMessage = "A port pool must be selected")]
        [Display(Name = "Port Pool")]
        public string PortPoolName { get; set; }

        /// <summary>
        /// The name of an attachment role which sets certain constraints on how the attachment must be configuted
        /// </summary>
        /// <value>String value denoting the name of an attachment role</value>
        /// <example>PE-CE-UNTAGGED</example>
        [Required(AllowEmptyStrings = false, ErrorMessage = "An attachment role must be selected")]
        [Display(Name = "Attachment Role")]
        public string AttachmentRoleName { get; set; }

        /// <summary>
        /// Optional name of the provider network plane within which the attachment will be provisioned
        /// </summary>
        /// <value>A member of the PlaneEnum enumeration</value>
        /// <example>Red</example>
        [Display(Name ="Plane")]
        public PlaneEnum? PlaneName { get; set; }

        /// <summary>
        /// The required bandwidth of the attachment in Gbps
        /// </summary>
        /// <value>Integer value denoting the required attachment bandwidth in Gbps</value>
        /// <example>10</example>
        [Required(ErrorMessage = "An attachment bandwidth value must be selected")]
        [Display(Name = "Attachment Bandwidth")]
        public int? AttachmentBandwidthGbps { get; set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        /// <example>100</example>
        [Display(Name = "Contract Bandwidth")]
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets received from the tenant domain should be trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the required trust state</value>
        /// <example>false</example>
        [Display(Name = "Trust CoS and DSCP")]
        public bool TrustReceivedCosAndDscp { get; set; }

        /// <summary>
        /// A description for the new attachment
        /// </summary>
        /// <value>String value for the description</value>
        /// <example>Provider connetivity to the DTC data-center</example>
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
        /// A list of IPv4 addresses to be assigned to the interfaces of the attachment.
        /// </summary>
        /// <value>A list of Ipv4AddressAndMask objcets</value>
        public List<Ipv4AddressAndMaskViewModel> Ipv4Addresses { get; set; }

        /// <summary>
        /// Optional parameters for creating a routing instances to be associated with the new attachment.
        /// </summary>
        /// <value>An object of type RoutingInstanceRequest</value>
        public RoutingInstanceRequestViewModel RoutingInstance { get; set; }

    }
}

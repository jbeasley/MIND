using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{ 
    /// <summary>
    /// Model for requesting a provider domain logical interface
    /// </summary>
    public class ProviderDomainLogicalInterfaceRequestViewModel
    {
        /// <summary>
        /// A description of the logical interface.
        /// </summary>
        /// <value>A string value denoting the description to apply to the logical interface</value>
        /// <example>Loopback interface for multi-hop BGP peering</example>
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// The type of logical interface required.
        /// </summary>
        /// <value>Enum value denoting the type of logical interface</value>
        /// <example>Loopback</example>
        [Required]
        [Display(Name = "Logical Interface Type")]
        public LogicalInterfaceTypeEnum? LogicalInterfaceType { get; set; }

        /// <summary>
        /// IPv4 address and mask to be assigned to the logical interface
        /// </summary>
        /// <value>An instance of Ipv4AddressAndMask</value>
        [Display(Name="IPv4 Addressing")]
        public Ipv4AddressAndMaskViewModel Ipv4Address { get; set; }
    }
}

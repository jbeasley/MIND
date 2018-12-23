
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
using System.ComponentModel;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for requesting a vpn
    /// </summary>
    public class VpnRequestViewModel : IValidatableObject
    {
        public VpnRequestViewModel()
        {
            // Default value for VPN should be to enable Nova
            IsNovaVpn = true;
        }

        /// <summary>
        /// The ID of the tenant for which the vpn will be created
        /// </summary>
        /// <value>Integer value for the ID of the tenant</value>
        /// <example>1001</example>
        public int? TenantId { get; set; }

        /// <summary>
        /// The name of the vpn
        /// </summary>
        /// <value>String value denoting the name of the vpn</value>
        /// <example>cloud-connectivity-vpn</example>
        [Required]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The vpn name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        public string Name { get; set; }

        /// <summary>
        /// A description of the VPN
        /// </summary>
        /// <value>String value denoting the vpn description</value>
        /// <example>vpn for providing IP connectivity between hosts running in public and private clouds</example>
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// The geographical region which the vpn operates within. If no region is chosen then the vpn should be made available in all regions
        /// </summary>
        /// <value>Enum value denoting the region</value>
        /// <example>EMEA</example>
        [Display(Name = "Region")]
        public RegionEnum? Region { get; set; }

        /// <summary>
        /// The provider plane which the vpn should operate. If no plane is chosen then the vpn should operate in both the red and blue planes.
        /// </summary>
        /// <value>Enum member denoting the provider plane which the VPN operates within</value>
        /// <example>Red</example>
        [Display(Name = "Plane")]
        public PlaneEnum? Plane { get; set; }

        /// <summary>
        /// The tenancy type of the vpn. If the tenancy type is single then only the owner of the vpn can participate in the vpn. 
        /// If the tenancy type is multi then any tenant can participate in the vpn.
        /// </summary>
        /// <value>Enum value denoting the tenancy type of the vpn</value>
        /// <example>Single</example>
        [Required]
        [Display(Name = "Tenancy Type")]
        public TenancyTypeEnum? TenancyType { get; set; }

        /// <summary>
        /// The protocol type of the vpn.
        /// </summary>
        /// <value>Enum value denoting the protocol type of the vpn</value>
        /// <example>IP</example>
        [Required]
        [Display(Name = "Protocol Type")]
        public ProtocolTypeEnum? ProtocolType { get; set; } = ProtocolTypeEnum.IP;

        /// <summary>
        /// The topology type of the VPN. A meshed VPN allows any endpoint to communicate with any other endpoint. 
        /// A hub-and-spoke VPN allows spoke endpoints to communicate with hub endpoints but not with other spoke endpoints. 
        /// </summary>
        /// <value>Enum value denoting the topology type of the vpn.</value>
        /// <example>Meshed</example>
        [Required]
        [Display(Name = "Topology Type")]
        public TopologyTypeEnum? TopologyType { get; set; }

        /// <summary>
        /// The address family of the VPN. Currently only IPv4 is available. 
        /// </summary>
        /// <value>Enum value denoting the address family of the vpn.</value>
        /// <example>IPv4</example>
        [Required]
        [Display(Name = "Address Family")]
        public AddressFamilyEnum? AddressFamily { get; set; }

        /// <summary>
        /// Determines if the VPN is launched as a standard 'Nova' implemented vpn. If this option is disabled the vpn may be customised.
        /// </summary>
        /// <value>Boolean denoting whether the vpn is launched as a standard Nova VPN.</value>
        [Required]
        [Display(Name = "Nova VPN")]
        public bool IsNovaVpn { get; set; }

        /// <summary>
        /// Determines if the vpn supports extranet connectivity
        /// </summary>
        /// <value>Boolean denoting whether the vpn supports extranet</value>
        /// <example>true</example>
        [Display(Name = "Extranet")]
        public bool IsExtranet { get; set; }

        /// <summary>
        /// Determines if the VPN supports IP multicast
        /// </summary>
        /// <value>Boolean denoting whether the vpn supports IP multicast.</value>
        /// <example>true</example>
        [Display(Name = "Multicast VPN")]
        public bool IsMulticastVpn { get; set; }

        /// <summary>
        /// The multicast service type of the VPN. 
        /// </summary>
        /// <value>Enum value denoting the multicast service type of the vpn.</value>
        /// <example>ssm</example>
        [Display(Name = "Multicast VPN Service Type")]
        public MulticastVpnServiceTypeEnum? MulticastVpnServiceType { get; set; }

        /// <summary>
        /// The multicast direction type of the VPN. 
        /// </summary>
        /// <value>Enum value denoting the multicast direction type of the vpn.</value>
        /// <example>unidirectional</example>
        [Display(Name = "Multicast VPN Direction Type")]
        public MulticastVpnDirectionTypeEnum? MulticastVpnDirectionType { get; set; }

        /// <summary>
        /// The route target range. Route targets will be allocated from the specified range.
        /// </summary>
        /// <value>String value denoting the name of the route target range</value>
        /// <example>default</example>
        [Display(Name = "Route Target Range")]
        public RouteTargetRangeEnum? RouteTargetRange { get; set; } = RouteTargetRangeEnum.Default;

        /// <summary>
        /// A list of requested route targets to be assigned to the vpn
        /// </summary>
        /// <value>A list of RouteTargetRequest objects</value>
        [Display(Name = "Route Target Requests")]
        public List<RouteTargetRequestViewModel> RouteTargetRequests { get; set; }

        /// <summary>
        /// A list of vpn attachment set request objects denoting attachment sets to be associated with the vpn.
        /// </summary>
        /// <value>List of VpnAttachmentSetRequest objects</value>
        public List<VpnAttachmentSetRequestViewModel> VpnAttachmentSets { get; set; }

        /// <summary>
        /// Stage the vpn ready for synchronisation with the network
        /// </summary>
        /// <value>Booelan denoting whether the vpn should be staged.</value>
        /// <example>true</example>
        public bool? Stage { get; set; }

        /// <summary>
        /// Synchronise the vpn with the network
        /// </summary>
        /// <value>Boolean denoting whether the vpn should be synchronised with the network</value>
        /// <example>true</example>
        public bool? SyncToNetwork { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!RouteTargetRange.HasValue)
            {
                if (RouteTargetRequests != null && RouteTargetRequests.Any())
                {
                    yield return new ValidationResult("A route target range cannot be specified concurrently with requested route targets. " +
                        "Remove either the route target range or the list of requested route targets from the request.");
                }
            }
        }
    }
}

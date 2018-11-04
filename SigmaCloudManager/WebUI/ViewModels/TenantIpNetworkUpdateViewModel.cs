
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
    /// Model for updating a tenant IP network - an IP network which is owned by (associated with) a tenant
    /// </summary>
    public class TenantIpNetworkUpdateViewModel : IModifiableResource
    {
        /// <summary>
        /// The ID of the tenant IP network
        /// </summary>
        public int? TenantIpNetworkId { get; set; }

        /// <summary>
        /// The CIDR representation of the tenant IP network. This does not include the less-than-or-equal-to
        /// parameter if it is defined for this tenant IP network.
        /// </summary>
        /// <value>String value denoting the CIDR name</value>
        /// <example>10.1.0.0/16</example>
        [Display(Name = "CIDR Name")]
        public string CidrName { get; private set; }

        /// <summary>
        /// The CIDR representation of the tenant IP network. This also includes the less-than-or-equal-to
        /// parameter if it is defined for this tenant IP network.
        /// </summary>
        /// <value>String value denoting the CIDR name</value>
        /// <example>10.1.0.0/16 le 24</example>
        [Display(Name = "Full CIDR Name")]
        public string CidrNameIncludingIpv4LessThanOrEqualToLength { get; private set; }

        /// <summary>
        /// The CIDR IPv4 prefix
        /// </summary>
        /// <value>The CIDR IPv4 prefix</value>
        /// <example>10.1.1.0</example>
        [Display(Name = "IPv4 Prefix")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
           ErrorMessage = "A valid IPv4 prefix must be entered, e.g. 192.168.1.0")]
        public string Ipv4Prefix { get; set; }

        /// <summary>
        /// The CIDR length of the IPv4 prefix
        /// </summary>
        /// <value>An integer between 1 and 32 which denotes the CIDR length of the IPv4 prefix</value>
        /// <example>24</example>
        [Display(Name = "IPv4 Length")]
        [Range(1, 32)]
        public int? Ipv4Length { get; set; }

        /// <summary>
        /// The maximum length of IPv4 prefixes which are contained within the CUDR range
        /// </summary>
        /// <value>An intger between 1 and 32 which denotes the maximum length of IPv4 prefixes within the CIDR range</value>
        /// <example>32</example>
        [Display(Name = "IPv4 Less-Than-Or-Equal-To-Length")]
        [Range(1, 32)]
        public int? Ipv4LessThanOrEqualToLength { get; set; }

        /// <summary>
        /// The required IP routing behavior for traffic forwarding towards the tenant IP network
        /// </summary>
        /// <value>Enum member value denoting the required tenant ip routing behavior</value>
        /// <example>BluePlane</example>
        [Display(Name = "IP Routing Behaviour")]
        public TenantIpRoutingBehaviourEnum? IpRoutingBehaviour { get; set; } = TenantIpRoutingBehaviourEnum.AnyPlane;

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Determines whether the tenant network is allowed into any IP Extranet VPNs
        /// </summary>
        /// <value>Boolean value which when true indicates that the tenant network is enabled for extranet</value>
        /// <example>true</example>
        [Display(Name = "Allow Extranet")]
        public bool AllowExtranet { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Ipv4LessThanOrEqualToLength != null)
            {
                if (Ipv4LessThanOrEqualToLength < Ipv4Length)
                {
                    yield return new ValidationResult(
                        "The 'IPv4 Less Than or Equal To Length' value cannot be less than the IPv4 Length value.");
                }
            }
        }
    }
}


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
    /// Model of a tenant IP network - an IP network which is owned (belongs to) a tenant
    /// </summary>
    public class TenantIpNetworkViewModel
    {
        /// <summary>
        /// The ID of the tenant IP network
        /// </summary>
        /// <value>The ID of the tenant IP network</value>
        /// <example>2001</example>
        [Display(Name = "Tenant IP Network ID")]
        public int? TenantIpNetworkId { get; private set; }

        /// <summary>
        /// The name of the tenant owner of the IP network
        /// </summary>
        /// <value>String value denoting the name of the tenant</value>
        /// <example>DCIS</example>
        [Display(Name = "Tenant Name")]
        public string TenantName { get; private set; }

        /// <summary>
        /// The CIDR IPv4 prefix
        /// </summary>
        /// <value>The CIDR IPv4 prefix</value>
        /// <example>10.1.1.0</example>
        [Display(Name = "IPv4 Prefix")]
        public string Ipv4Prefix { get; private set; }

        /// <summary>
        /// The CIDR length of the IPv4 prefix
        /// </summary>
        /// <value>An integer between 1 and 32 which denotes the CIDR length of the IPv4 prefix</value>
        /// <example>24</example>
        [Display(Name = "IPv4 Length")]
        public int? Ipv4Length { get; private set; }

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
        /// The maximum length of IPv4 prefixes which are contained within the CUDR range
        /// </summary>
        /// <value>An intger between 1 and 32 which denotes the maximum length of IPv4 prefixes within the CIDR range</value>
        /// <example>32</example>
        [Display(Name = "IPv4 Less-Than-Or-Equal-To-length")]
        public int? Ipv4LessThanOrEqualToLength { get; private set; }

        /// <summary>
        /// Determines whether the tenant network is allowed into any IP Extranet VPNs
        /// </summary>
        /// <value>Boolean value which when true indicates that the tenant network is enabled for extranet</value>
        /// <example>true</example>
        [Display(Name = "Allow Extranet")]
        public bool AllowExtranet { get; private set; }

        /// <summary>
        /// The ID of the tenant to which the tenant network belongs
        /// </summary>
        /// <value>Integer value for the ID of the tenant</value>
        /// <example>1001</example>
        [Display(Name = "Tenant ID")]
        public int? TenantId { get; private set; }

        /// <summary>
        /// The IP routing behavior for traffic forwarding towards the tenant IP network
        /// </summary>
        /// <value>Enum member value denoting the tenant ip routing behavior</value>
        /// <example>BluePlane</example>
        [Display(Name = "IP Routing Behaviour")]
        public TenantIpRoutingBehaviourEnum? IpRoutingBehaviour { get; private set; }

    }
}

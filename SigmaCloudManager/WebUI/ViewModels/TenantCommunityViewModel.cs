
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
    /// Model for a tenant community - a BGP community which is owned by (associated with) a tenant
    /// </summary>
    public class TenantCommunityViewModel : IModifiableResource
    {
        /// <summary>
        /// The ID of the tenant community
        /// </summary>
        /// <value>Integer value denoting the ID of the tenant community</value>
        /// <example>2001</example>
        public int? TenantCommunityId { get; private set; }

        /// <summary>
        /// The name of the tenant community
        /// </summary>
        /// <value>String value denoting the name of the tenant community</value>
        /// <example>8718:10001</example>
        public string Name { get; private set; }

        /// <summary>
        /// The BGP 2 byte autonomous system number portion of the community
        /// </summary>
        /// <value>An integer denoting the 2 byte autonomous system number portion of the community</value>
        /// <example>8718</example>
        [Display(Name = "Autonomous System Number")]
        [Required]
        [Range(1, 65535)]
        public int? AutonomousSystemNumber { get; set; }

        /// <summary>
        /// The number portion of the community
        /// </summary>
        /// <value>An integer value denoting the number portion of the community</value>
        /// <example>2400</example>
        [Display(Name = "Number")]
        [Required]
        [Range(1, 65535)]
        public int? Number { get; set; }

        /// <summary>
        /// The IP routing behavior for traffic forwarding towards the tenant IP networks which are grouped
        /// or associated with the community
        /// </summary>
        /// <value>Enum member value denoting the required tenant ip routing behavior</value>
        /// <example>BluePlane</example>
        [Display(Name = "IP Routing Behaviour")]
        [Required]
        public TenantIpRoutingBehaviourEnum? IpRoutingBehaviour { get; set; } = TenantIpRoutingBehaviourEnum.AnyPlane;

        /// <summary>
        /// Denotes whether the tenant network is allowed into any IP Extranet VPNs
        /// </summary>
        /// <value>Boolean value which when true indicates that the tenant network is enabled for extranet</value>
        /// <example>true</example>
        [Display(Name = "Allow Extranet")]
        public bool AllowExtranet { get; set; }

        /// <summary>
        /// The ID of the tenant to which the tenant community belongs
        /// </summary>
        /// <value>Integer value for the ID of the tenant</value>
        /// <example>1001</example>
        public int? TenantId { get; private set; }

        /// <summary>
        /// The name of the tenant owner of the IP network
        /// </summary>
        /// <value>String value denoting the name of the tenant</value>
        /// <example>DCIS</example>
        [Display(Name = "Tenant Name")]
        public string TenantName { get; private set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }

    }
}

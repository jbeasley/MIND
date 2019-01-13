using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for updating a tenant community - a BGP community which is owned by (associated with) a tenant
    /// </summary>
    public class TenantCommunityUpdateViewModel : IModifiableResource
    {
        /// <summary>
        /// The ID of the tenant community
        /// </summary>
        /// <value>Integer value for the ID of the tenant community</value>
        /// <example>1001</example>
        public int? TenantCommunityId { get; set; }

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
        /// The required IP routing behavior for traffic forwarding towards the tenant IP networks which are grouped
        /// or associated with the community
        /// </summary>
        /// <value>Enum member value denoting the required tenant ip routing behavior</value>
        [Display(Name = "IP Routing Behaviour")]
        [Required]
        public TenantIpRoutingBehaviourEnum? IpRoutingBehaviour { get; set; }

        /// <summary>
        /// The tenant network environment which the IP network belongs to.
        /// </summary>
        /// <value>Enum member value denoting the tenant environment</value>
        /// <example>Development</example>
        [Display(Name = "Environment")]
        public TenantEnvironmentEnum? TenantEnvironment { get; set; }

        /// <summary>
        /// Determines whether the tenant network is allowed into any IP Extranet VPNs
        /// </summary>
        /// <value>Boolean value which when true indicates that the tenant network is enabled for extranet</value>
        /// <example>true</example>
        [Display(Name = "Allow Extranet")]
        public bool AllowExtranet { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of a routing instance request
    /// </summary>
    public class RoutingInstanceRequestViewModel : IModifiableResource
    {
        public RoutingInstanceRequestViewModel()
        {
            // Instantiate new list of BGP peer requests. In the case where all BGP peers have been removed 
            // using the web UI this will ensure that the corresponding BGP peer records are removed from the database.
            // An empty list will be passed to the application layer which will be processed as a request to remove all 
            // existing BGP peers for the routing instance.
            BgpPeers = new List<BgpPeerRequestViewModel>();
        }

        /// <summary>
        /// Gets or sets the routing instance identifier.
        /// </summary>
        /// <value>Integer denoting the routing instance identifier.</value>
        public int? RoutingInstanceId { get; set; }

        /// <summary>
        /// Denotes if the routing instance is a tenant-facing VRF.
        /// </summary>
        /// <value>Boolean value which is true if the routing instance is a tenant-facing vrf</value>
        public bool? IsTenantFacingVrf { get; set; }

        /// <summary>
        /// Denotes if the routing instance is an infrastructure VRF.
        /// </summary>
        /// <value>Boolean value which is true if the routing instance is an infrastrucfure vrf</value>
        public bool? IsInfrastructureVrf { get; set; }

        /// <summary>
        /// Denotes if the routing instance is a default routing instance
        /// </summary>
        /// <value>Boolean value which is true if the routing instance is a default routing instance</value>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the  device identifier.
        /// </summary>
        /// <value>Integer denoting the device identifier.</value>
        public int? DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the  tenant identifier to which the routing instance is to be associated.
        /// </summary>
        /// <value>Integer denoting the tenant identifier.</value>
        public int? TenantId { get; set; }

        /// <summary>
        /// The requested name of the routing instance
        /// </summary>
        /// <value>A string value denoting the requested name of the routing instance</value>
        /// <example>my-routing-instance</example>
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        public string Name { get; set; }

        /// <summary>
        /// The requested route distinguisher range from which a new route distinguisher should be assigned
        /// </summary>
        /// <value>A member of the RouteDistinguisherRangeTypeEnum enumeration</value>
        /// <example>Default</example>
        public RouteDistinguisherRangeTypeEnum? RangeType { get; set; } = RouteDistinguisherRangeTypeEnum.Default;

        /// <summary>
        /// The requested administrator sub-field of the routing instance
        /// </summary>
        /// <value>An integer value denoting the requested assigned-number sub-field of the routing instance</value>
        /// <example>8718</example>
        [Range(1, 4294967295)]
        public int? AdministratorSubField { get; set; }

        /// <summary>
        /// The requested assigned-number sub-field of the routing instance
        /// </summary>
        /// <value>An integer value denoting the requested assigned-number sub-field of the routing instance</value>
        /// <example>10000</example>
        [Range(1, 4294967295)]
        public int? AssignedNumberSubField { get; set; }

        /// <summary>
        /// A list of BGP peers to be created within the routing instance.
        /// </summary>
        /// <value>A list of BgpPeerRequestViewModel objects</value>
        public List<BgpPeerRequestViewModel> BgpPeers { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AdministratorSubField.HasValue)
            {
                if (!AssignedNumberSubField.HasValue)
                {
                    yield return new ValidationResult(
                        "Both the 'Administrator SubField' and 'Assigned Number SubField' arguments are required in order to set a specific " +
                        "route distinguisher value for the routing instance.");
                }
            }

            if (AssignedNumberSubField.HasValue)
            {
                if (!AdministratorSubField.HasValue)
                {
                    yield return new ValidationResult(
                        "Both the 'Administrator SubField' and 'Assigned Number SubField' arguments are required in order to set a specific " +
                        "route distinguisher value for the routing instance.");
                }
            }
        }
    }
}

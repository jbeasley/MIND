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
    /// Model of a routing instance request
    /// </summary>
    public class RoutingInstanceRequestViewModel
    {
        public RoutingInstanceRequestViewModel()
        {
            // Instantiate new list of BGP peer requests. In the case where all BGP peers have been removed 
            // using the web UI this will ensure that the corresponding BGP peer records are removed from the database.
            // An empty list will be passed to the application layer which will be processed as a request to remove all 
            // existing BGP peers for hte routing instance.
            BgpPeers = new List<BgpPeerRequestViewModel>();
        }

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

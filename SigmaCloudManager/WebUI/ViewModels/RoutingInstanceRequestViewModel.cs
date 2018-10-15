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
    public partial class RoutingInstanceRequestViewModel
    {

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AdministratorSubField.HasValue)
            {
                if (!AssignedNumberSubField.HasValue)
                {
                    yield return new ValidationResult(
                        "Both the 'AdministratorSubField' and 'AssignedNumberSubField' arguments are required in order to set a specific " +
                        "route distinguisher value for the routing instance.");
                }
            }

            if (AssignedNumberSubField.HasValue)
            {
                if (!AdministratorSubField.HasValue)
                {
                    yield return new ValidationResult(
                        "Both the 'AdministratorSubField' and 'AssignedNumberSubField' arguments are required in order to set a specific " +
                        "route distinguisher value for the routing instance.");
                }
            }
        }
    }
}

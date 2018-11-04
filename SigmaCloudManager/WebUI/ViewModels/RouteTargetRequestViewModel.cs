
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
    /// Model for requesting vpn route targets
    /// </summary>
    public class RouteTargetRequestViewModel
    { 
        /// <summary>
        /// The route target range
        /// </summary>
        /// <value>String value denoting the name of the route target range</value>
        /// <example>default</example>
        [Required]
        [Display(Name="Range")]
        public RouteTargetRangeEnum? Range{ get; set; }

        /// <summary>
        /// A requested assigned-number subfield value. The route target will be assigned this value if it is available within the specified range.
        /// </summary>
        /// <value>Integer value for the requested value of the assigned number subfield</value>
        /// <example>9991</example>
        [Range(1, 4294967295)]
        [Display(Name ="Assigned Number SubField")]
        public int? AssignedNumberSubField { get; set; }

        /// <summary>
        /// Auto-allocate a value for the assigned number subfield. If this option is enabled then the 'AssignedNumberSubField' option must be null.
        /// </summary>
        /// <value>Boolean value denoting whether a value for the assigned number subfield should be auto allocated.</value>
        /// <example>true</example>
        [Display(Name ="Auto-Allocate Assigned Number SubField")]
        public bool AutoAllocateAssignedNumberSubField { get; set; }

        /// <summary>
        /// Assign the route target as a hub-export route target for a hub-and-spoke vpn.
        /// </summary>
        /// <value>Boolean value denoting whether the route target should be assigned as a hub export route target.</value>
        /// <example>true</example>
        [Display(Name = "Hub Export")]
        public bool? IsHubExport { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AutoAllocateAssignedNumberSubField)
            {
                if (AssignedNumberSubField != null)
                {
                    yield return new ValidationResult(
                        "The 'Auto-Allocate Assigned Number SubField' option cannot be used concurrently with the 'Assigned Number SubField' option." +
                        "Either remove the 'Auto-Allocate Assigned Number SubField' property or remove the 'Assigned Number SubField' property from " +
                        "the request.");
                }
            }
        }
    }
}

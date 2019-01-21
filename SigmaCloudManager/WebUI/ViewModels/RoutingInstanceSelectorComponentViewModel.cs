using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace Mind.WebUI.Models
{
    public class RoutingInstanceSelectorComponentViewModel
    {
        /// <summary>
        /// Gets or sets the tenant identifier for the tenant to which 
        /// the routing instance belongs.
        /// </summary>
        /// <value>Integer value denoting the tenant identifier.</value>
        public int? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the device identifier for the device to which 
        /// the routing instance is associated.
        /// </summary>
        /// <value>Integer value denoting the device identifier.</value>
        public int? DeviceId { get; set; }

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
        public bool? IsDefaultRoutingInstance { get; set; }

        /// <summary>
        /// Determines if the creation of a new routing instance is required.
        /// </summary>
        /// <value>A boolean which when set to true indicates a new routing instance is required</value>
        /// <example>true</example>
        [Display(Name = "Create New Routing Instance")]
        public bool CreateNewRoutingInstance { get; set; }

        /// <summary>
        /// The name of an existing routing instance.
        /// </summary>
        /// <value>A string value of the name of an existing routing instance</value>
        /// <exanple>db7c48eaa9864cd0b3aa6af08c8370d6</exanple>
        [Display(Name = "Existing Routing Instance Name")]
        public string ExistingRoutingInstanceName { get; set; }
    }
}
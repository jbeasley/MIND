
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
    /// Model for the profile component of a port. The profile includes the port role and pool properties.
    /// </summary>
    public class PortProfileComponentViewModel
    {
        /// <summary>
        /// The role of the port
        /// </summary>
        /// <value>String value denoting the role of the port</value>
        [Required]
        [Display(Name = "Port Role")]
        public string PortRole { get; set; }

        /// <summary>
        /// Pool to which the port is assigned
        /// </summary>
        /// <value>String value denoting the pool to which the port should be assigned</value>
        [Required]
        [Display(Name = "Port Pool")]
        public string PortPool { get; set; }

        /// <summary>
        /// Denotes whether the profile of the port is for a device in the tenant domain.
        /// </summary>
        /// <value>Boolean value denoting whether the port is for a devie in the tenant domain</value>
        public bool? IsTenantDomainRole { get; set; } = null;

        /// <summary>
        /// Denotes whether the profile of the port is for a device in the provider domain
        /// </summary>
        /// <value>Boolean value denoting whether the port is for a devie in the provider domai</value>
        public bool? IsProviderDomainRole { get; set; } = null;
    }
}

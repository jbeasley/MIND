
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
    /// Model for requesting the creation of a device port
    /// </summary>
    public class PortRequestViewModel
    { 
        /// <summary>
        /// The type of the port, e.g. TenGigabitEtheret
        /// </summary>
        /// <value>String denoting the type of the port</value>
        /// <example>TenGigabitEtheret</example>
        [Required]
        [Display(Name="Type")]
        public string Type { get; set; }

        /// <summary>
        /// The name of the port, e.g. 1/0
        /// </summary>
        /// <value>String denoting the name of the port</value>
        /// <example>1/0</example>
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        /// <summary>
        /// Small Form-Factor Pluggable optic for the port
        /// </summary>
        /// <value>String denoting the small form-factor pluggable optic for the port</value>
        /// <example>SFP-10G-SR</example>
        [Required]
        [Display(Name="Port SFP")]
        public string PortSfp { get; set; }

        /// <summary>
        /// Status of the port
        /// </summary>
        /// <value>Member of the PortStatusTypeEnum enunmeration</value>
        [Required]
        [Display(Name="Port Status")]
        public PortStatusTypeEnum? PortStatus { get; set; }

        /// <summary>
        /// The role of the port
        /// </summary>
        /// <value>String value denoting the role of the port</value>
        [Required]
        [Display(Name="portRole")]
        public string PortRole { get; set; }

        /// <summary>
        /// The connector type of the port
        /// </summary>
        /// <value>String value denoting the required port connector</value>
        /// <example>RJ45</example>
        [Required]
        [Display(Name = "Port Connector")]
        public string PortConnector { get; set; }

        /// <summary>
        /// The physical bandwidth of the port in Gbps
        /// </summary>
        /// <value>Integer value denoting the physical bandwidth of the port</value>
        /// <example>10</example>
        [Required]
        [Display(Name = "Port Bandwidth (Gbps)")]
        public int? PortBandwidthGbps { get; set; }

        /// <summary>
        /// Pool to which the port is assigned
        /// </summary>
        /// <value>String value denoting the pool to which the port should be assigned</value>
        [Required]
        [Display(Name="Port Pool")]
        public string PortPool { get; set; }
    }
}

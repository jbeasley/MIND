
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
    /// Model for the bandwidth component of a port
    /// </summary>
    public class PortBandwidthComponentViewModel
    {
        /// <summary>
        /// The physical bandwidth of the port in Gbps
        /// </summary>
        /// <value>Integer value denoting the physical bandwidth of the port</value>
        /// <example>10</example>
        [Required]
        [Display(Name = "Port Bandwidth (Gbps)")]
        public int? PortBandwidthGbps { get; set; } = 10;

    }
}

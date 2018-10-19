
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
    /// Model for updating an existing device port
    /// </summary>
    public class PortUpdateViewModel
    {
        /// <summary>
        /// The ID of the port
        /// </summary>
        /// <value>An integer value denoting the ID of the port</value>
        /// <example>7001</example>
        [Display(Name = "Port ID")]
        public int? PortId { get; set; }

        /// <summary>
        /// Small Form-Factor Pluggable optic for the port
        /// </summary>
        /// <value>String denoting the small form-factor pluggable optic for the port</value>
        /// <example>SFP-10G-SR</example>
        [Display(Name = "Port SFP")]
        public string PortSfp { get; set; }

        /// <summary>
        /// Status of the port
        /// </summary>
        /// <value>Member of the PortStatusTypeEnum enunmeration</value>
        [Display(Name = "Port Status")]
        public PortStatusTypeEnum? PortStatus { get; set; }

        /// <summary>
        /// The connector type of the port
        /// </summary>
        /// <value>String value denoting the required port connector</value>
        /// <example>RJ45</example>
        [Display(Name = "Port Connector")]
        public string PortConnector { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for the SFP and connector components of a port.
    /// </summary>
    public class PortConnectorAndSfpComponentViewModel
    {
        /// <summary>
        /// Small Form-Factor Pluggable optic for the port
        /// </summary>
        /// <value>String denoting the small form-factor pluggable optic for the port</value>
        /// <example>SFP-10G-SR</example>
        [Required]
        [Display(Name = "Port SFP")]
        public string PortSfp { get; set; }

        /// <summary>
        /// The connector type of the port
        /// </summary>
        /// <value>String value denoting the required port connector</value>
        /// <example>RJ45</example>
        [Required]
        [Display(Name = "Port Connector")]
        public string PortConnector { get; set; }
    }
}

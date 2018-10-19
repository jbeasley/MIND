
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{ 
    /// <summary>
    /// Model of an interface
    /// </summary>
    public class InterfaceViewModel
    { 
        /// <summary>
        /// The ID of the interface
        /// </summary>
        /// <value>The ID of the interface</value>
        [Display(Name="Interface ID")]
        public int? InterfaceId { get; private set; }

        /// <summary>
        /// IPv4 address assigned to the interface
        /// </summary>
        /// <value>String value representing the IPv4 address assigned to the interface</value>
        [Display(Name = "IP Address")]
        public string IpAddress { get; private set; }

        /// <summary>
        /// IPv4 subnet mask assigned to the interface
        /// </summary>
        /// <value>String value representing the IPv4 subnet mask assigned to the interface</value>
        [Display(Name = "Subnet Mask")]
        public string SubnetMask { get; private set; }

        /// <summary>
        /// Ports which provide physical connectivity to the network for the interface
        /// </summary>
        /// <value>The ports of the interface</value>
        [Display(Name = "Ports")]
        public List<PortViewModel> Ports { get; private set; }
    }
}

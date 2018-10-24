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
    /// Model of a vlan
    /// </summary>
    public class VlanViewModel
    {
        /// <summary>
        /// The ID of the vlan
        /// </summary>
        /// <value>Integer value denoting the ID of the vlan</value>
        /// <example>88781</example>
        [Display(Name = "Vlan ID")]
        public int? VlanID { get; private set; }

        /// <summary>
        /// The vlan tag
        /// </summary>
        /// <value>Integer value denoting the vlan tag</value>
        /// <example>100</example>
        [Display(Name = "Vlan Tag")]
        public int? VlanTag { get; private set; }

        /// <summary>
        /// IPv4 address assigned to the vlan
        /// </summary>
        /// <value>String value representing the IPv4 address assigned to the vlan</value>
        /// <example>192.168.0.1</example>
        [Display(Name = "IP Address")]
        public string IpAddress { get; private set; }

        /// <summary>
        /// IPv4 subnet mask assigned to the vlan
        /// </summary>
        /// <value>String value representing the IPv4 subnet mask assigned to the vlan</value>
        /// <example>255.255.255.252</example>
        [Display(Name = "Subnet Mask")]
        public string SubnetMask { get; private set; }
    }
}

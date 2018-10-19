
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
    /// Model for updating a logical interface
    /// </summary>
    public class LogicalInterfaceUpdateViewModel
    {
        /// <summary>
        /// The ID for the logical interface.
        /// </summary>
        /// <value>An integer value denoting the ID of hte logical interface</value>
        /// <example>10010</example>
        [Display(Name = "logicalInterfaceId")]
        public int? LogicalInterfaceId { get; set; }

        /// <summary>
        /// A description of the logical interface.
        /// </summary>
        /// <value>A string value denoting the description to apply to the logical interface</value>
        /// <example>Loopback interface for multi-hop BGP peering</example>
        [Display(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// IPv4 address and mask to be assigned to the logical interface
        /// </summary>
        /// <value>An instance of Ipv4AddressAndMask</value>
        [Display(Name = "ipv4Address")]
        public Ipv4AddressAndMaskViewModel Ipv4Address { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }

    }
}

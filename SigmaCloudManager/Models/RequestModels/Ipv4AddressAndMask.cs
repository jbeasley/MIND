
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

namespace SCM.Models.RequestModels
{
    /// <summary>
    /// Model for IPv4 address and subnet mask
    /// </summary>
    public partial class Ipv4AddressAndMask
    {
        /// <summary>
        /// IPv4 address
        /// </summary>
        /// <value>String denoting an IPv4 address/value>
        public string IpAddress { get; set; }

        /// <summary>
        /// IPv4 subnet mask 
        /// </summary>
        /// <value>String denoting an IPv4 subnet mask</value>
        public string SubnetMask { get; set; }

    }
}

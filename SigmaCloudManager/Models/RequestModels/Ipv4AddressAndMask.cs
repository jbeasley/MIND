
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
    /// 
    /// </summary>
    public partial class Ipv4AddressAndMask
    {
        /// <summary>
        /// IPv4 address
        /// </summary>
        /// <value>IPv4 address assigned to the first connection in the attachment</value>
        public string IpAddress { get; set; }

        /// <summary>
        /// IPv4 subnet mask 
        /// </summary>
        /// <value>IPv4 subnet mask assigned to the first connection in the attachment</value>
        public string SubnetMask { get; set; }

    }
}

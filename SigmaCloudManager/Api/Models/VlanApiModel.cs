using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning a Vlan.
    /// </summary>
    public class VlanApiModel
    {
        public int VlanID { get; set; }
        public string IpAddress { get; set; }
        public string SubnetMask { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
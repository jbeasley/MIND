using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning an Interface.
    /// </summary>
    public class InterfaceApiModel
    {
        public int InterfaceID { get; set; }
        public string IpAddress { get; set; }
        public string SubnetMask { get; set; }
        public byte[] RowVersion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    public class MulticastVpnServiceTypeApiModel
    {
        public int MulticastVpnServiceTypeID { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
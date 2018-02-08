using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    public class VpnTenantMulticastGroupApiModel
    {
        public int VpnTenantMulticastGroupApiModelID { get; set; }
        public TenantMulticastGroupApiModel TenantMulticastGroup { get; set; }
        public byte[] RowVersion { get; set; }
        public int MulticastVpnRpID { get; set; }
    }
}
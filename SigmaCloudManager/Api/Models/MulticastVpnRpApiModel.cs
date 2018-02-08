using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    public class MulticastVpnRpApiModel
    {
        public int MulticastVpnRpID { get; set; }
        public string IpAddress { get; set; }
        public byte[] RowVersion { get; set; }
        public int AttachmentSetID { get; set; }
        public ICollection<VpnTenantMulticastGroupApiModel> VpnTenantMulticastGroups { get; set; }
    }
}
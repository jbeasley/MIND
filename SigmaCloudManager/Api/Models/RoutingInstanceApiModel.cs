using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning a VRF
    /// </summary>
    public class RoutingInstanceApiModel
    {
        public int RoutingInstanceID { get; set; }
        public string Name { get; set; }
        public int AdministratorSubField { get; set; }
        public int AssignedNumberSubField { get; set; }
        public byte[] RowVersion { get; set; }
        public DeviceApiModel Device { get; set; }
        public TenantApiModel Tenant { get; set; }
        public ICollection<BgpPeerApiModel> BgpPeers { get; set; }
        public ICollection<VifApiModel> Vifs { get; set; }
        public ICollection<AttachmentApiModel> Attachments { get; set; }
    }
}
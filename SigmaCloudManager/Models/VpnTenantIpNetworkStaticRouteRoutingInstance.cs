using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class VpnTenantIpNetworkStaticRouteRoutingInstance : IModifiableResource
    {
        public int VpnTenantIpNetworkStaticRouteRoutingInstanceID { get; private set; }
        public int TenantIpNetworkID { get; set; }
        public int AttachmentSetID { get; set; }
        public bool AddToAllRoutingInstancesInAttachmentSet { get; set; }
        public string Ipv4NextHopAddress { get; set; }
        public bool IsBfdEnabled { get; set; }
        public int? RoutingInstanceID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantIpNetworkID")]
        public virtual TenantIpNetwork TenantIpNetwork { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();
    }
}
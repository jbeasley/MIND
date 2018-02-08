using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SCM.Models
{
    public class RoutingInstance
    {
        public int RoutingInstanceID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public int? AdministratorSubField { get; set; }
        public int? AssignedNumberSubField { get; set; }
        public int DeviceID { get; set; }
        public int? TenantID { get; set; }
        public int? RouteDistinguisherRangeID { get; set; }
        public int RoutingInstanceTypeID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Device Device { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual RoutingInstanceType RoutingInstanceType { get; set; }
        public virtual RouteDistinguisherRange RouteDistinguisherRange { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Vif> Vifs { get; set; }
        public virtual ICollection<BgpPeer> BgpPeers { get; set; }
        public virtual ICollection<LogicalInterface> LogicalInterfaces { get; set; }
        public virtual ICollection<AttachmentSetRoutingInstance> AttachmentSetRoutingInstances { get; set; }
        public virtual ICollection<VpnTenantCommunityRoutingInstance> VpnTenantCommunitiesRoutingInstance { get; set; }
        public virtual ICollection<VpnTenantNetworkRoutingInstance> VpnTenantNetworksRoutingInstance { get; set; }
        public virtual ICollection<VpnTenantNetworkStaticRouteRoutingInstance> VpnTenantNetworkStaticRoutesRoutingInstance { get; set; }
    }
}
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SCM.Models
{
    public class RoutingInstance
    {
        public int RoutingInstanceID { get; private set; }
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
        public virtual ICollection<VpnTenantCommunityRoutingInstance> VpnTenantCommunityRoutingInstances { get; set; }
        public virtual ICollection<VpnTenantIpNetworkRoutingInstance> VpnTenantIpNetworkRoutingInstances { get; set; }
        public virtual ICollection<VpnTenantIpNetworkStaticRouteRoutingInstance> VpnTenantIpNetworkStaticRouteRoutingInstances { get; set; }

        public virtual void Validate()
        {
            if (this.Device == null) throw new IllegalStateException("A device must be defined for the routing instance.");
            if (this.RoutingInstanceType == null) throw new IllegalStateException("A routing instance type must be defined for the " +
                    "routing instance.");
            if (this.RoutingInstanceType.IsVrf)
            {
                if (!this.AdministratorSubField.HasValue) throw new IllegalStateException("An administrator subfield value must be defined " +
                    "for the routing instance.");
                if (!this.AssignedNumberSubField.HasValue) throw new IllegalStateException("An assigned number subfield value must be defined " +
                    "for the routing instance.");
                if (this.RouteDistinguisherRange == null) throw new IllegalStateException("A route distinguisher range must be defined " +
                    "for the routing instance.");
                if (!this.RoutingInstanceType.IsTenantFacingVrf && !this.RoutingInstanceType.IsInfrastructureVrf)
                    throw new IllegalStateException("The routing instance must be defined as either a tenant-facing vrf routing instance or an " +
                        "infrastructure vrf routing instance");
                if (this.RoutingInstanceType.IsTenantFacingVrf)
                {
                    if (this.Tenant == null)
                    {
                        throw new IllegalStateException("A tenant must be defined because the routing instance is defined a a tenant-facing vrf " +
                            "routing instance.");
                    }
                }
            }
        }
    }
}
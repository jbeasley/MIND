using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models
{

    public class Vpn : IModifiableResource
    {
        public int VpnID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public bool IsExtranet { get; set; }
        public bool IsMulticastVpn { get; set; }
        public bool IsNovaVpn { get; set; }
        public int VpnTopologyTypeID { get; set; }
        public int VpnTenancyTypeID { get; set; }
        public int? AddressFamilyID { get; set; }
        public int TenantID { get; set; }
        public bool RequiresSync { get; set; }
        public bool Created { get; set; }
        public bool ShowCreatedAlert { get; set; }
        public bool ShowRequiresSyncAlert { get; set; }
        public int? PlaneID { get; set; }
        public int? RegionID { get; set; }
        public int? MulticastVpnServiceTypeID { get; set; }
        public int? MulticastVpnDirectionTypeID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual Plane Plane { get; set; }
        public virtual Region Region { get; set; }
        public virtual AddressFamily AddressFamily { get; set; }
        public virtual VpnTopologyType VpnTopologyType { get; set; }
        public virtual VpnTenancyType VpnTenancyType { get; set; }
        public virtual MulticastVpnServiceType MulticastVpnServiceType { get; set; }
        public virtual MulticastVpnDirectionType MulticastVpnDirectionType { get; set; }
        public virtual ICollection<RouteTarget> RouteTargets { get; set; }
        public virtual ICollection<VpnAttachmentSet> VpnAttachmentSets { get; set; }
        [InverseProperty("MemberVpn")]
        public virtual ICollection<ExtranetVpnMember> ExtranetVpns { get; set; }
        [InverseProperty("ExtranetVpn")]
        public virtual ICollection<ExtranetVpnMember> ExtranetVpnMembers { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();
    }
}
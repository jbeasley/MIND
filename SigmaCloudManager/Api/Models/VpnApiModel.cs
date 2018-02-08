using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning a VPN.
    /// </summary>
    public class VpnApiModel
    {
        public int VpnID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsExtranet { get; set; }
        public bool IsMulticastVpn { get; set; }
        public bool RequiresSync { get; set; }
        public byte[] RowVersion { get; set; }
        public TenantApiModel Tenant { get; set; }
        public PlaneApiModel Plane { get; set; }
        public RegionApiModel Region { get; set; }
        public VpnTopologyTypeApiModel VpnTopologyType { get; set; }
        public VpnTenancyTypeApiModel VpnTenancyType { get; set; }
        public MulticastVpnServiceTypeApiModel MulticastVpnServiceType { get; set; }
        public MulticastVpnDirectionTypeApiModel MulticastVpnDirectionType { get; set; }
        public ICollection<VpnAttachmentSetApiModel> VpnAttachmentSets { get; set; }
        public ICollection<MulticastVpnRpApiModel> MulticastVpnRps { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.RequestModels
{
    public class VpnRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsExtranet { get; set; }
        public bool IsMulticastVpn { get; set; }
        public bool IsNovaVpn { get; set; }
        public int VpnProtocolTypeID { get; set; }
        public int VpnTopologyTypeID { get; set; }
        public int VpnTenancyTypeID { get; set; }
        public int TenantID { get; set; }
        public int? PlaneID { get; set; }
        public int? AddressFamilyID { get; set; }
        public int? RouteTargetRangeID { get; set; }
        public int? RegionID { get; set; }
        public int? MulticastVpnServiceTypeID { get; set; }
        public int? MulticastVpnDirectionTypeID { get; set; }
    }
}
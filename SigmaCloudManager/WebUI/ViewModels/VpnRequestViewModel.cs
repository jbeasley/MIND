using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    public class VpnRequestViewModel
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Display(Name = "Extranet")]
        public bool IsExtranet { get; set; }
        [Display(Name = "Nova VPN")]
        public bool IsNovaVpn { get; set; } = true;
        [Display(Name = "Multicast VPN")]
        public bool IsMulticastVpn { get; set; }
        [Required(ErrorMessage = "A VPN Protocol Type must be selected")]
        public int VpnProtocolTypeID { get; set; }
        [Required(ErrorMessage = "A VPN Topology Type must be selected.")]
        public int VpnTopologyTypeID { get; set; }
        [Required(ErrorMessage = "A VPN Tenancy Type must be selected.")]
        public int VpnTenancyTypeID { get; set; }
        [Required(ErrorMessage = "A Tenant must be selected.")]
        public int TenantID { get; set; }
        public int? PlaneID { get; set; }
        [Required(ErrorMessage = "An Address Family must be selected.")]
        public int AddressFamilyID { get; set; }
        public int? RegionID { get; set; }
        public int? RouteTargetRangeID { get; set; }
        [Display(Name = "Requires Sync")]
        public bool RequiresSync { get; set; }
        public bool Created { get; set; }
        public int? MulticastVpnServiceTypeID { get; set; }
        public int? MulticastVpnDirectionTypeID { get; set; }
        public byte[] RowVersion { get; set; }
        public TenantViewModel Tenant { get; set; }
        public PlaneViewModel Plane { get; set; }
        public RegionViewModel Region { get; set; }
        [Display(Name = "Protocol Type")]
        public VpnProtocolTypeViewModel VpnProtocolType { get; set; }
        [Display(Name = "Address Family")]
        public AddressFamilyViewModel AddressFamily { get; set; }
        [Display(Name = "Route Target Range")]
        public RouteTargetRangeViewModel RouteTargetRange { get; set; }
        [Display(Name = "Topology Type")]
        public VpnTopologyTypeViewModel VpnTopologyType { get; set; }
        [Display(Name = "Tenancy Type")]
        public VpnTenancyTypeViewModel VpnTenancyType { get; set; }
        [Display(Name = "Multicast Service Type")]
        public MulticastVpnServiceTypeViewModel MulticastVpnServiceType { get; set; }
        [Display(Name = "Multicast Direction Type")]
        public MulticastVpnDirectionTypeViewModel MulticastVpnDirectionType { get; set; }
    }
}
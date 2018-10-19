﻿using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class VpnTenantIpNetworkOutViewModel
    {
        [Display(AutoGenerateField = false)]
        public int VpnTenantIpNetworkOutID { get; set; }
        [Required(ErrorMessage = "A Tenant must be selected.")]
        public int TenantID { get; set; }
        [Required(ErrorMessage = "A Tenant IP Network must be selected.")]
        public int TenantIpNetworkID { get; set; }
        [Required(ErrorMessage = "An Attachment Set must be selected.")]
        public int AttachmentSetID { get; set; }
        [Required(ErrorMessage = "A Routing Instance must be selected.")]
        public int RoutingInstanceID { get; set; }
        [Required(ErrorMessage = "A BGP Peer must be selected.")]
        public int BgpPeerID { get; set; }
        [Required(ErrorMessage = "An Advertised IP Routing Preference value must be specified.")]
        [Display(Name = "Advertised IP Routing Preference")]
        [Range(1, 20, ErrorMessage = "Enter a number between 1 and 20")]
        public int? AdvertisedIpRoutingPreference { get; set; } = 1;
        public byte[] RowVersion { get; set; }
        [Display(Name = "Tenant")]
        public TenantViewModel Tenant { get; set; }
        [Display(Name = "Tenant IP Network")]
        public TenantIpNetworkViewModel TenantIpNetwork { get; set; }
        public AttachmentSetViewModel AttachmentSet { get; set; }
        [Display(Name = "Routing Instance")]
        public RoutingInstanceViewModel RoutingInstance { get; set; }
        [Display(Name = "BGP Peer")]
        public BgpPeerViewModel BgpPeer { get; set; }
    }
}
using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class BgpPeerViewModel
    {
        public int BgpPeerID { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IPv4 address for the peer must be entered, e.g. 192.168.0.1")]
        [Display(Name = "Peer IPv4 Address")]
        [Required(ErrorMessage = "A peer IPv4 address must be defined.")]
        public string Ipv4PeerAddress { get; set; }
        [Required(ErrorMessage = "A peer 2-byte autonomous system number must be entered between 1 and 65535")]
        [Range(1,65535)]
        [Display(Name = "Peer 2-Byte Autonomous System")]
        public int Peer2ByteAutonomousSystem { get; set; }
        [Display(Name = "Maximum Routes")]
        [Range(1, 1000)]
        public int? MaximumRoutes { get; set; } = 500;
        [Display(Name = "Require BFD Fast Failure Detection")]
        public bool IsBfdEnabled { get; set; }
        [Display(Name = "Enable Multi-Hop Peering")]
        public bool IsMultiHop { get; set; }
        [Display(Name = "Peer Password")]
        public string PeerPassword { get; set; }
        public string Name { get; set; }
        public int RoutingInstanceID { get; set; }
        public byte[] RowVersion { get; set; }
        [Display(Name = "Routing Instance")]
        public RoutingInstanceViewModel RoutingInstance { get; set; }
    }
}
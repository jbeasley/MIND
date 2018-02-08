using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning BGP Peer data.
    /// </summary>
    public class BgpPeerApiModel
    {
        public int BgpPeerID { get; set; }
        public string IpAddress { get; set; }
        public int AutonomousSystem { get; set; }
        public int? MaximumRoutes { get; set; }
        public bool IsBfdEnabled { get; set; }
        public int RoutingInstanceID { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
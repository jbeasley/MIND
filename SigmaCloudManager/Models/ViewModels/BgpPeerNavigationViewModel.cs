using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class BgpPeerNavigationViewModel
    { 
        public int? BgpPeerID { get; set; }
        public int? AttachmentID { get; set; }
        public int? VifID { get; set; }
        public int? RoutingInstanceID { get; set; }
        public bool ShowWarningMessage { get; set; }
        public bool ConcurrencyError { get; set; }
    }
}
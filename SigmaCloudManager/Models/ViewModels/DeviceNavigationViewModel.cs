using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class DeviceNavigationViewModel
    { 
        public int? DeviceID { get; set; }
        public int? TenantID { get; set; }
        public bool ShowWarningMessage { get; set; }
        public bool ConcurrencyError { get; set; }
        public string RedirectAction { get; set; }
        public string SearchString { get; set; }
    }
}
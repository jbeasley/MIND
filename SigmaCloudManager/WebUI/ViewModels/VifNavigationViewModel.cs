using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class VifNavigationViewModel
    { 
        public int? VifID { get; set; }
        public int? LogicalInterfaceID { get; set; }
        public bool ShowWarningMessage { get; set; }
        public bool ConcurrencyError { get; set; }
        public string RedirectAction { get; set; }
    }
}
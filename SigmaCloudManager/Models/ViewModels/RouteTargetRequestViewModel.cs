using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    public class RouteTargetRequestViewModel
    {
        [Required(ErrorMessage = "A Route Target Range must be selected")]
        public int RouteTargetRangeID { get; set; }
        [Display(Name = "Request Assigned Number Sub-Field")]
        [Range(1, 4294967295)]
        public int? RequestedAssignedNumberSubField { get; set; }
        [Display(Name = "Auto-Allocate Assigned Number Sub-Field")]
        public bool AutoAllocateAssignedNumberSubField { get; set; }
        [Display(Name = "Hub Export")]
        public bool IsHubExport { get; set; }
        public int VpnID { get; set; }
        [Display(Name ="Route Target Range")]
        public RouteTargetRangeViewModel RouteTargetRange { get; set; }
    }
}
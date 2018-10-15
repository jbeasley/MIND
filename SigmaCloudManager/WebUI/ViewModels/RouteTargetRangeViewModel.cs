using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    public class RouteTargetRangeViewModel
    {
        [Display(AutoGenerateField = false)]
        public int RouteTargetRangeID { get; set; }
        [Required]
        public string Name { get; set; }
        public int AdministratorSubField { get; set; }
        public int AssignedNumberSubFieldStart { get; set; }
        public int AssignedNumberSubFieldCount { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
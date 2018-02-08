using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SCM.Models.ViewModels
{
    public class RoutingInstanceViewModel
    {
        [Display(AutoGenerateField = false)]
        public int RoutingInstanceID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "A name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        [StringLength(50)]
        [Display(Name = "VRF Name")]
        public string Name { get; set; }
        [Display(Name = "VRF Administrator Sub-Field")]
        [Range(1, 4294967295)]
        public int? AdministratorSubField { get; set; }
        [Display(Name = "VRF Assigned Number Sub-Field")]
        [Range(1, 4294967295)]
        public int? AssignedNumberSubField { get; set; }
        [Required]
        public int DeviceID { get; set; }
        public int? TenantID { get; set; }
        public byte[] RowVersion { get; set; }
        public DeviceViewModel Device { get; set; }
        public TenantViewModel Tenant { get; set; }
        public RoutingInstanceTypeViewModel RoutingInstanceType { get; set; }
    }
}
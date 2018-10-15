using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SCM.Models.ViewModels
{
    public class TenantAttachmentRoutingInstanceUpdateViewModel {
        public int RoutingInstanceID { get; set; }
        public int AttachmentID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "A name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "VRF Administrator Sub-Field")]
        [Range(1, 4294967295)]
        public int? AdministratorSubField { get; set; }
        [Display(Name = "VRF Assigned Number Sub-Field")]
        [Range(1, 4294967295)]
        public int? AssignedNumberSubField { get; set; }
        public byte[] RowVersion { get; set; }
        public RoutingInstanceTypeViewModel RoutingInstanceType { get; set; }
    }
}

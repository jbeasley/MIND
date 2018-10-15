using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SCM.Models.ViewModels
{
    public class RoutingInstanceTypeViewModel
    {
        [Display(AutoGenerateField = false)]
        public int RoutingInstanceTypeID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "A name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Display(Name = "Layer3 Type")]
        public bool IsLayer3 { get; set; }
        [Display(Name = "VRF Routing Instance Type")]
        public bool IsVrf { get; set; }
        [Display(Name = "Default Routing Instance Type")]
        public bool IsDefault { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models.ViewModels
{
    public class PortSfpViewModel
    {
        public int PortSfpID { get; set; }
        [Display(Name = "Port SFP")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A port sfp name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The port sfp name must contain letters, numbers, or dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
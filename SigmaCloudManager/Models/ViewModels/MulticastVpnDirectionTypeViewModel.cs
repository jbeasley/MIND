using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    public class MulticastVpnDirectionTypeViewModel
    {
        [Display(AutoGenerateField = false)]
        public int MulticastVpnDirectionTypeID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "A name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "The name must contain letters and numbers only and no whitespace.")]
        [MaxLength(50)]
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
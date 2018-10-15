using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models.ViewModels
{
    public class PortPoolViewModel
    {
        [Display(AutoGenerateField = false)]
        public int PortPoolID { get; set; }
        [Display(Name = "Port Pool")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A port pool name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The port pool name must contain letters, numbers, or dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public int PortRoleID { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
        public PortRoleViewModel PortRole { get; set; }
    }
}
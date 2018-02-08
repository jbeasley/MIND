using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models.ViewModels
{
    public class PortConnectorViewModel
    {
        [Display(AutoGenerateField = false)]
        public int PortConnectorID { get; set; }
        [Display(Name = "Port Connector")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A port connector name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The port connector name must contain letters, numbers, or dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
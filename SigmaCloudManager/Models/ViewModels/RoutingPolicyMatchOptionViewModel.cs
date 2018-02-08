using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class RoutingPolicyMatchOptionViewModel
    {
        [Display(AutoGenerateField = false)]
        public int RoutingPolicyMatchOptionViewModelID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
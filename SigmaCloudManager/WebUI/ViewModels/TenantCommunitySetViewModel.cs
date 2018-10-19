using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class TenantCommunitySetViewModel
    {
        [Display(AutoGenerateField = false)]
        public int TenantCommunitySetID { get; set; }
        [Required(ErrorMessage = "A Tenant must be selected.")]
        public int TenantID { get; set; }
        [Required(ErrorMessage = "A name must be specified.")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "A Routing Policy Match Option must be selected.")]
        public int RoutingPolicyMatchOptionID { get; set; }
        public byte[] RowVersion { get; set; }
        public TenantViewModel Tenant { get; set; }
        [Display(Name = "Routing Policy Match Option")]
        public RoutingPolicyMatchOptionViewModel RoutingPolicyMatchOption { get; set; }
    }
}
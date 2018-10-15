using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    public enum TenancyType
    {
        Single,
        Multi
    }

    public class VpnTenancyTypeViewModel
    {
        [Display(AutoGenerateField = false)]
        public int VpnTenancyTypeID { get; set; }
        [Required]
        [Display(Name ="Tenancy Type")]
        [StringLength(50)]
        public string Name { get; set; }
        public TenancyType TenancyType { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
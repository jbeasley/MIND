using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models.ViewModels
{
    public enum PortRoleType
    {
        TenantFacing,
        ProviderInfrastructrure,
        TenantInfrastructure
    }

    public class PortRoleViewModel
    {
        [Display(AutoGenerateField = false)]
        public int PortRoleID { get; set; }
        [Display(Name = "Port Connector")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A port role name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The port role name must contain letters, numbers, or dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public PortRoleType PortRoleType { get; set; }
        public int DeviceRoleID { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
    }
}
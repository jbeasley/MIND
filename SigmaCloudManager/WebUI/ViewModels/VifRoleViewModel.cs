using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models.ViewModels
{
    public class VifRoleViewModel
    {
        [Display(AutoGenerateField = false)]
        public int VifRoleID { get; set; }
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "The name must contain letters and numbers only and no whitespace.")]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Display(Name = "Layer3 Role")]
        public bool IsLayer3Role { get; set; }
        [Display(Name = "Require Contract Bandwidth")]
        public bool RequireContractBandwidth { get; set; }
        public bool RequireSyncToNetwork { get; set; }
        public int? RoutingInstanceTypeID { get; set; }
        public int AttachmentRoleID { get; set; }
        public byte[] RowVersion { get; set; }
        public AttachmentRoleViewModel AttachmentRole { get; set; }
        public RoutingInstanceTypeViewModel RoutingInstanceType { get; set; }
    }
}
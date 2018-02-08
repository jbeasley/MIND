using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models.ViewModels
{
    public class AttachmentRoleViewModel
    {
        [Display(AutoGenerateField = false)]
        public int AttachmentRoleID { get; set; }
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "The name must contain letters and numbers only and no whitespace.")]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public int PortPoolID { get; set; }
        [Display(Name="Tagged Role")]
        public bool IsTaggedRole { get; set; }
        [Display(Name = "Layer3 Role")]
        public bool IsLayer3Role { get; set; }
        [Display(Name = "Require Contract Bandwidth")]
        public bool RequireContractBandwidth { get; set; }
        public bool RequireSyncToNetwork { get; set; }
        public int? RoutingInstanceTypeID { get; set; }
        [Display(Name = "Supported By Bundle Attachment")]
        public bool SupportedByBundle { get; set; }
        [Display(Name = "Supported By Multi-Port Attachment")]
        public bool SupportedByMultiPort { get; set; }
        public byte[] RowVersion { get; set; }
        public PortPoolViewModel PortPool { get; set; }
        public RoutingInstanceTypeViewModel RoutingInstanceType { get; set; }
    }
}
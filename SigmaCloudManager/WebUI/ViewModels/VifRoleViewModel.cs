using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mind.WebUI.Models
{
    public class VifRoleViewModel
    {
        public int VifRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Layer3 Role")]
        public bool IsLayer3Role { get; set; }
        [Display(Name = "Require Contract Bandwidth")]
        public bool RequireContractBandwidth { get; set; }
        public int? RoutingInstanceTypeId { get; set; }
        public int AttachmentRoleId { get; set; }
        public byte[] RowVersion { get; set; }
        public AttachmentRoleViewModel AttachmentRole { get; set; }
    }
}
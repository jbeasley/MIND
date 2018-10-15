using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    public enum ProtocolType
    {
        IP,
        Ethernet
    }

    public class VpnProtocolTypeViewModel
    {
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "A VPN Protocol Type must be selected")]
        public int VpnProtocolTypeID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Protocol Type")]
        public string Name { get; set; }
        public ProtocolType ProtocolType { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
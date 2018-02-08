using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    public class MulticastVpnRpViewModel
    {
        [Display(AutoGenerateField = false)]
        public int MulticastVpnRpID { get; set; }
        [Required]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        [Display(Name = "IP Address")]
        public string IpAddress { get; set; }
        public byte[] RowVersion { get; set; }
        public int AttachmentSetID { get; set; }
        public AttachmentSetViewModel AttachmentSet { get; set; }
    }
}
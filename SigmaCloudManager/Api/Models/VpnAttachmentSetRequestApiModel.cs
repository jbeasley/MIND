using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for VPN Attachment Set requests.
    /// </summary>
    public class VpnAttachmentSetRequestApiModel
    {
        public bool? IsHub { get; set; }
        public int VpnID { get; set; }
        [Required]
        public int? AttachmentSetID { get; set; }
    }
}
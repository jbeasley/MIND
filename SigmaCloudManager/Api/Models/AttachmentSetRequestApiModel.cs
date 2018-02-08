using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for requesting an Attachment Set.
    /// </summary>
    public class AttachmentSetRequestApiModel
    {
        public bool IsLayer3 { get; set; }
        [Required]
        public int? AttachmentRedundancyID { get; set; }
        [Required]
        public int? TenantID { get; set; }
        [Required]
        public int? RegionID { get; set; }
        public int? SubRegionID { get; set; }
    }
}
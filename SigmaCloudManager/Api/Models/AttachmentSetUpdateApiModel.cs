using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for updating an Attachment Set.
    /// </summary>
    public class AttachmentSetUpdateApiModel
    {
        public int AttachmentSetID { get; set; }
        [Required]
        public int? AttachmentRedundancyID { get; set; }
        public int? SubRegionID { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
    }
}
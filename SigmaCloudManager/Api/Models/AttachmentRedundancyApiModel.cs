using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning Attachment Redundancy data.
    /// </summary>
    public class AttachmentRedundancyApiModel
    {
        public int AttachmentRedundancyID { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
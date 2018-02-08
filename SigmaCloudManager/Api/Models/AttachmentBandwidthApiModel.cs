using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning Attachment Bandwidth data.
    /// </summary>
    public class AttachmentBandwidthApiModel
    {
        public int AttachmentBandwidthID { get; set; }
        public int BandwidthGbps { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
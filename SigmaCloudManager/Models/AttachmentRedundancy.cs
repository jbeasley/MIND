using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public enum AttachmentRedundancyTypeEnum
    {
        Bronze,
        Silver,
        Gold,
        Custom
    }

    public class AttachmentRedundancy
    {
        public int AttachmentRedundancyID { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public AttachmentRedundancyTypeEnum AttachmentRedundancyType { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
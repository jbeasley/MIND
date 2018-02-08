using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SCM.Models
{
    public class Mtu
    {
        public int MtuID { get; set; }
        public int MtuValue { get; set; }
        public bool ValueIncludesLayer2Overhead { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
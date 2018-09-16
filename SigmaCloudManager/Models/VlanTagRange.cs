﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public class VlanTagRange
    {
        public int VlanTagRangeID { get; private set; }
        [Required]
        public string Name { get; set; }
        public int VlanTagRangeStart { get; set; }
        public int VlanTagRangeCount { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Vif> Vifs { get; set; }
    }
}
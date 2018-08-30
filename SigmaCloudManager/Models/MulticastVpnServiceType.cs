using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public enum MvpnServiceTypeEnum
    {
        ASM,
        SSM
    }

    public class MulticastVpnServiceType
    {
        public int MulticastVpnServiceTypeID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public MvpnServiceTypeEnum MvpnServiceType { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
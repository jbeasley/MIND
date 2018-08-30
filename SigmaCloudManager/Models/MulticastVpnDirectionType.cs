using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public enum MvpnDirectionTypeEnum
    {
        Unidirectional,
        Bidirectional
    }

    public class MulticastVpnDirectionType
    {
        public int MulticastVpnDirectionTypeID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public MvpnDirectionTypeEnum MvpnDirectionType { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
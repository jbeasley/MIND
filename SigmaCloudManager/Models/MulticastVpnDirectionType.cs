using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public enum MvpnDirectionType
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
        public MvpnDirectionType MvpnDirectionType { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
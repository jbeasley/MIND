using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public enum MvpnDomainType
    {
        SenderOnly,
        ReceiverOnly,
        SenderAndReceiver
    }

    public class MulticastVpnDomainType
    {
        public int MulticastVpnDomainTypeID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public MvpnDomainType MvpnDomainType { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
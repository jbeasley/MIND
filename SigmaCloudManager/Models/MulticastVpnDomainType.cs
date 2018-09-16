using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public enum MvpnDomainTypeEnum
    {
        SenderOnly,
        ReceiverOnly,
        SenderAndReceiver
    }

    public class MulticastVpnDomainType
    {
        public int MulticastVpnDomainTypeID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public MvpnDomainTypeEnum MvpnDomainType { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace SCM.Models
{

    public enum DeviceStatusTypeEnum
    {
        Production,
        Staging,
        Retired
    }

    public class DeviceStatus
    {
        public int DeviceStatusID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public DeviceStatusTypeEnum DeviceStatusType { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
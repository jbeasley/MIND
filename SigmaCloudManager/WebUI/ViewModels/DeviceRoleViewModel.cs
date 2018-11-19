using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Mind.WebUI.Models
{

    public class DeviceRoleViewModel
    {
        public int DeviceRoleId { get; set; }
        [Display(Name = "Device Role")]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
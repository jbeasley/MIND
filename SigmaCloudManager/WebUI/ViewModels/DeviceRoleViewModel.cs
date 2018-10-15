using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace SCM.Models.ViewModels
{

    public class DeviceRoleViewModel
    {
        public int DeviceRoleID { get; set; }
        [Display(Name = "Device Role")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A device role must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The device role name must contain letters, numbers, or dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public bool RequireSyncToNetwork { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
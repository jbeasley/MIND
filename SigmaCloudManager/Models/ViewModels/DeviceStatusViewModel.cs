using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace SCM.Models.ViewModels
{

    public class DeviceStatusViewModel
    {
        [Display(AutoGenerateField = false)]
        public int DeviceStatusID { get; set; }
        [Display(Name = "Device Status")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A device status must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The device status name must contain letters, numbers, or dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
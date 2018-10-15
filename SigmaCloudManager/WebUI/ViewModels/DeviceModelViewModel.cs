using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace SCM.Models.ViewModels
{

    public class DeviceModelViewModel
    {
        [Display(AutoGenerateField = false)]
        public int DeviceModelID { get; set; }
        [Display(Name = "Device Model")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A device model name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The device model name must contain letters, numbers, or dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
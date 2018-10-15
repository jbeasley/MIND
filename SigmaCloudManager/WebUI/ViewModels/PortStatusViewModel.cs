using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models.ViewModels
{
    public enum PortStatusType
    {
        Free,
        Assigned,
        Locked,
        Migration,
        Reserved
    }

    public class PortStatusViewModel
    {
        [Display(AutoGenerateField = false)]
        public int PortStatusID { get; set; }
        [Display(Name = "Port Status")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A port status name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The port status name must contain letters, numbers, or dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        public PortStatusType PortStatusType { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Port> Ports { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Models.ViewModels
{
    public enum LogicalInterfaceType
    {
        Loopback,
        Tunnel
    }

    public class LogicalInterfaceViewModel
    {
        [Display(AutoGenerateField = false)]
        public int LogicalInterfaceID { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public int RoutingInstanceID { get; set; }
        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "An IP address is required")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        public string IpAddress { get; set; }
        [Display(Name = "Subnet Mask")]
        [Required(ErrorMessage = "A subnet mask is required")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask must be entered, e.g. 255.255.255.252")]
        public string SubnetMask { get; set; }
        [Display(Name = "Logical Interface Type")]
        [Required(ErrorMessage = "A Logical Interface Type selection is required")]
        public LogicalInterfaceType LogicalInterfaceType { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Display(Name = "VRF")]
        public RoutingInstanceViewModel RoutingInstance { get; set; }
        public byte[] RowVersion { get; set; }
    }
}

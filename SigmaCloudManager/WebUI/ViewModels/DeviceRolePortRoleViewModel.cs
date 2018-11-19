using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Models
{
    public class DeviceRolePortRoleViewModel
    {
        public int DeviceRolePortRoleId { get; set; }
        [Display(Name ="Device Role")]
        public DeviceRoleViewModel DeviceRole { get; set; }
        [Display(Name = "Port Role")]
        public PortRoleViewModel PortRole { get; set; }
    }
}

using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Models.ViewModels
{
    public class DeviceViewModel
    {
        [Display(AutoGenerateField = false)]
        public int DeviceID { get; set; }
        public int TenantID { get; set; }
        public TenantViewModel Tenant { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name="Use Layer 2 Interface MTU")]
        public bool UseLayer2InterfaceMtu { get; set; }
        public byte[] RowVersion { get; set; }
        [Required(ErrorMessage = "A Device Role must be selected")]
        public int DeviceRoleID { get; set; }
        [Display(Name = "Role")]
        public DeviceRoleViewModel DeviceRole { get; set; }
        [Required(ErrorMessage = "A Device Model must be selected")]
        public int DeviceModelID { get; set; }
        [Display(Name = "Model")]
        public DeviceModelViewModel DeviceModel { get; set; }
        [Required(ErrorMessage = "A Device Status must be selected")]
        public int DeviceStatusID { get; set; }
        [Display(Name = "Status")]
        public DeviceStatusViewModel DeviceStatus { get; set; }
        public int? LocationID { get; set; }
        public LocationViewModel Location { get; set; }
        public int? PlaneID { get; set; }
        public PlaneViewModel Plane { get; set; }
        public bool RequiresSync { get; set; }
    }
}

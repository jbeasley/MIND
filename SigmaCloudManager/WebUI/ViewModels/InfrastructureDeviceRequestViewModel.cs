using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Models.ViewModels
{
    public class InfrastructureDeviceRequestViewModel
    {
        [Display(AutoGenerateField = false)]
        public int DeviceID { get; set; }
        [Display(Name = "Name")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A device name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The device name must contain letters, numbers, or dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Display(Name="Use Layer 2 Interface MTU")]
        public bool UseLayer2InterfaceMtu { get; set; }
        [Display(Name = "Requires Sync")]
        public bool RequiresSync { get; set; }
        public byte[] RowVersion { get; set; }
        [Required(ErrorMessage = "A plane must be selected")]
        public int PlaneID { get; set; }
        public PlaneViewModel Plane { get; set; }
        [Required(ErrorMessage = "A location must be selected")]
        public int LocationID { get; set; }
        public LocationViewModel Location { get; set; }
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
    }
}

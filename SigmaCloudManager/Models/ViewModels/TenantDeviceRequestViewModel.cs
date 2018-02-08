using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Models.ViewModels
{
    public class TenantDeviceRequestViewModel
    {
        [Display(AutoGenerateField = false)]
        public int DeviceID { get; set; }
        [Required(ErrorMessage ="A Tenant must be specified")]
        public int TenantID { get; set; }
        public TenantViewModel Tenant { get; set; }
        [Display(Name = "Name")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A device name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The device name must contain letters, numbers, or dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [StringLength(250)]
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
    }
}

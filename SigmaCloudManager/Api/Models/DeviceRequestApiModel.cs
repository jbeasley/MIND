using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for requesting a Device.
    /// </summary>
    public class DeviceRequestApiModel
    {
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A Device name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The Device name must contain letters, numbers, or dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        public int? PlaneID { get; set; }
        [Required]
        public int? LocationID { get; set; }
        public ICollection<PortRequestApiModel> Ports { get; set; }
    }
}

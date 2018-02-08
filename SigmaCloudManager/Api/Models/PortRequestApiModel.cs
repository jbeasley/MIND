using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for a Port request.
    /// </summary>
    public class PortRequestApiModel
    {
        public int ID { get; set; }
        [Required]
        public int? DeviceID { get; set; }
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A port type must be specified, e.g. GigabitEthernet")]
        public string Type { get; set; }
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A port name must be specified, e.g. 0/0/0/0")]
        public string Name { get; set; }
        [Required]
        public int? PortBandwidthID { get; set; }
    }
}
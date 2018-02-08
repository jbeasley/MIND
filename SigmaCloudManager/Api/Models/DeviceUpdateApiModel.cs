using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for updating a Device.
    /// </summary>
    public class DeviceUpdateApiModel
    {
        public int ID { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
    }
}

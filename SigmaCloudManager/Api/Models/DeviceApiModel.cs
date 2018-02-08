using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning a Device.
    /// </summary>
    public class DeviceApiModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool RequiresSync { get; set; }
        public byte[] RowVersion { get; set; }
        public PlaneApiModel Plane { get; set; }
        public LocationApiModel Location { get; set; }
        public ICollection<PortApiModel> Ports { get; set; }
    }
}

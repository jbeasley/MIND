using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Models
{
    public class Plane
    {
        public int PlaneID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Vpn> Vpns { get; set; }
    }
}

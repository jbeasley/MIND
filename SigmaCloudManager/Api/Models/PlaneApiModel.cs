using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning Plane data.
    /// </summary>
    public class PlaneApiModel
    {
        public int PlaneID { get; set; }
        public string Name { get; set; }
    }
}

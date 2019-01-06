using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Models
{
    public class PlaneComponentViewModel
    {
        /// <summary>
        /// The name of the plane
        /// </summary>
        /// <value>String value denoting the name of the plane</value>
        [Display(Name ="Plane")]
        public PlaneEnum? PlaneName { get; set; }
    }
}

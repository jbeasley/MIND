using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Models
{
    public class PlaneViewModel
    {
        /// <summary>
        /// The ID of the plane
        /// </summary>
        /// <value>Integer value denoting the ID of the plane</value>
        public int PlaneID { get; private set; }

        /// <summary>
        /// The name of the plane
        /// </summary>
        /// <value>String value denoting the name of the plane</value>
        public string Name { get; private set; }
    }
}

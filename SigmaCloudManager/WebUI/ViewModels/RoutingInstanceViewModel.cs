using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Mind.WebUI.Models
{
    public class RoutingInstanceViewModel
    {
        /// <summary>
        /// The ID of the routing instance
        /// </summary>
        /// <value>Integer value denoting the ID of the routing instance</value>
        public int RoutingInstanceID { get; private set; }

        /// <summary>
        /// The name of the routing instance
        /// </summary>
        /// <value>String value denoting the name of the routing instance</value>
        [Display(Name = "Routing Instance Name")]
        public string Name { get; set; }

        /// <summary>
        /// The administrator subfield value assigned to the routing instance
        /// </summary>
        /// <value>Integer value denoting the administrator subfield value</value>
        [Display(Name = "Administrator Sub-Field")]
        public int? AdministratorSubField { get; set; }

        /// <summary>
        /// The assigned number subfield value assigned to the routing instance
        /// </summary>
        /// <value>Integer value denoting the assigned-number subfield value</value>
        [Display(Name = "Assigned Number Sub-Field")]
        public int? AssignedNumberSubField { get; set; }
    }
}
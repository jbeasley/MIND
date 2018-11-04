using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for a vpn route target
    /// </summary>
    public class RouteTargetViewModel
    {
        /// <summary>
        /// The route target range name
        /// </summary>
        /// <value>String value denoting the name of the route target range</value>
        /// <example>default</example>
        [Display(Name = "Range Name")]
        public string RangeName { get; private set; }

        /// <summary>
        /// The administrator subfield value.
        /// </summary>
        /// <value>Integer value for the administrator subfield</value>
        /// <example></example>
        [Display(Name = "Administrator SubField")]
        public int? AdministratorSubField { get; private set; }

        /// <summary>
        /// The assigned-number subfield value.
        /// </summary>
        /// <value>Integer value for the assigned number subfield</value>
        /// <example></example>
        [Display(Name = "Assigned Number SubField")]
        public int? AssignedNumberSubField { get; private set; }

        /// <summary>
        /// The name of the route target formatted according to type 0 as defined in RFC 4364
        /// </summary>
        /// <value>A string value denoting the name of the route target</value>
        /// <example>8718:1001</example>
        [Display(Name = "Name")]
        public string Name { get; private set; }

    }
}

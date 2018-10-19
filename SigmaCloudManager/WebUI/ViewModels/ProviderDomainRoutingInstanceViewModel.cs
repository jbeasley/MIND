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
    /// Model of a routing instance
    /// </summary>
    public class ProviderDomainRoutingInstanceViewModel
    {
        /// <summary>
        /// The ID of the routing instance
        /// </summary>
        /// <value>An integer value denoting the ID of the routing instance</value>
        /// <example>4001</example>
        [Display(Name = "Routing Instance ID")]
        public int? RoutingInstanceId { get; private set; }

        /// <summary>
        /// The name of the routing instance
        /// </summary>
        /// <value>A string value denoting the name of the routing instance</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        [Display(Name = "Name")]
        public string Name { get; private set; }

        /// <summary>
        /// The name of the provider domain location within which the routing instance exists
        /// </summary>
        /// <value>A string value denoting the name of the provider domain location</value>
        /// <example>Uk2</example>
        [Display(Name = "Provider Domain Location Name")]
        public string ProviderDomainLocationName { get; private set; }

        /// <summary>
        /// The administrator sub-field of the routing instance
        /// </summary>
        /// <value>An integer value denoting the assigned-number sub-field of the routing instance</value>
        /// <example>8718</example>
        [Display(Name = "Administrator SubField")]
        public int? AdministratorSubField { get; private set; }

        /// <summary>
        /// The assigned-number sub-field of the routing instance
        /// </summary>
        /// <value>An integer value denoting the assigned-number sub-field of the routing instance</value>
        /// <example>10000</example>
        [Display(Name = "Assigned Number SubField")]
        public int? AssignedNumberSubField { get; private set; }

    }
}

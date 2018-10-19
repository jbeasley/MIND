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
    /// 
    /// </summary>
    public class InfrastructureDeviceViewModel
    { 
        /// <summary>
        /// The ID of the device
        /// </summary>
        /// <value>Integer denoting the ID of the device</value>
        /// <example>90991</example>
        [Display(Name="Device ID")]
        public int? DeviceId { get; private set; }

        /// <summary>
        /// The name of the device
        /// </summary>
        /// <value>String denoting the name of the device</value>
        /// <example>UK2-PE1</example>
        [Display(Name="Name")]
        public string Name { get; private set; }

        /// <summary>
        /// A description of the device
        /// </summary>
        /// <value>A description of the device</value>
        [Display(Name="Description")]
        public string Description { get; private set; }

        /// <summary>
        /// Denotes whether layer 2 overhead is included in the device MTU calculation
        /// </summary>
        /// <value>Boolean denoting whether  layer 2 overhead is included in the device MTU calculation</value>
        /// <example>true</example>
        [Display(Name="Use Layer2 Interface MTU")]
        public bool? UseLayer2InterfaceMtu { get; private set; }

        /// <summary>
        /// The model of the device
        /// </summary>
        /// <value>String value denoting the model of the device</value>
        /// <example>ASR-9001</example>
        [Display(Name="Device Model")]
        public string DeviceModel { get; private set; }

        /// <summary>
        /// The provider network plane to which the device belongs
        /// </summary>
        /// <value>String value denoting provider network plane to which the device belongs</value>
        /// <example>Red</example>
        [Display(Name="Plane Name")]
        public string PlaneName { get; private set; }

        /// <summary>
        /// The location of the device
        /// </summary>
        /// <value>String value denoting the location of the device</value>
        /// <example>UK2</example>
        [Display(Name="locationName")]
        public string LocationName { get; private set; }

        /// <summary>
        /// The status of the device
        /// </summary>
        /// <value>String value denoting the status of the device</value>
        /// <example>Production</example>
        [Display(Name="Device Status")]
        public string DeviceStatus { get; private set; }
    }
}

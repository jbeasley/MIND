
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
    /// Model for the MTU and status components of a device
    /// </summary>
    public class DeviceMtuAndStatusComponentViewModel
    { 
        /// <summary>
        /// The status of the device
        /// </summary>
        /// <value>A member of the DeviceStatusTypeEnum enumeration</value>
        /// <example>Production</example>
        [Required]
        [Display(Name="Device Status")]
        public DeviceStatusTypeEnum? DeviceStatus { get; set; }

        /// <summary>
        /// Determines if layer 2 overhead should be included in the device MTU calculation
        /// </summary>
        /// <value>Boolean value denoting if layer 2 overhead is included in the device MTU calculation</value>
        /// <example>true</example>
        [Display(Name="Use Layer 2 Interface MTU")]
        public bool UseLayer2InterfaceMtu { get; set; }
    }
}

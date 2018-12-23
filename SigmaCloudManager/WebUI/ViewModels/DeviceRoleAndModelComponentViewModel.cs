
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
    /// Model for the role and model components of a device
    /// </summary>
    public class DeviceRoleAndModelComponentViewModel
    {  
        /// <summary>
        /// The model of the device
        /// </summary>
        /// <value>String denoting the model of the device</value>
        /// <example>ASR-1001</example>
        [Required]
        [Display(Name="Device Model")]
        public string DeviceModel { get; set; }

        /// <summary>
        /// The role of the device
        /// </summary>
        /// <value>A string denoting the role of the device</value>
        /// <example>CPE</example>
        [Display(Name = "Device Role")]
        [Required]
        public string DeviceRole { get; set; }
    }
}

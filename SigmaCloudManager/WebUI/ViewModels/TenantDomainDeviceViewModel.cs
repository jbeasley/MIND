
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
    /// Model for a tenant domain device, i.e. a device which exists within the tenant domain
    /// </summary>
    public class TenantDomainDeviceViewModel
    { 
        /// <summary>
        /// The ID of the device
        /// </summary>
        /// <value>Integer denoting the ID of the device</value>
        /// <example>90991</example>
        public int? DeviceId { get; private set; }

        /// <summary>
        /// The name of the device
        /// </summary>
        /// <value>String denoting the name of the device</value>
        /// <example>DTC-CPE-1</example>
        public string Name { get; private set; }

        /// <summary>
        /// Description of the device
        /// </summary>
        /// <value>String denoting the description</value>
        /// <example>CE gateway router for access to Sigma</example>
        public string Description { get; private set; }

        /// <summary>
        /// Denotes whether layer 2 overhead is included in the device MTU calculation
        /// </summary>
        /// <value>Boolean denoting whether  layer 2 overhead is included in the device MTU calculation</value>
        /// <example>true</example>
        [Display(Name="Use Layer 2 Interface MTU")]
        public bool? UseLayer2InterfaceMtu { get; private set; }

        /// <summary>
        /// The model of the device
        /// </summary>
        /// <value>String value denoting the model of the device</value>
        /// <example>ASR-1001</example>
        [Display(Name="Device Model")]
        public string DeviceModel { get; private set; }

        /// <summary>
        /// The ID of the tenant to which the device is assigned
        /// </summary>
        /// <value>Integer denoting the ID of the tenant</value>
        /// <example>90991</example>
        public int? TenantId { get; private set; }

        /// <summary>
        /// The tenant name to which the device is assigned
        /// </summary>
        /// <value>String value denoting tenant name to which the device is assigned</value>
        /// <example>Red</example>
        [Display(Name="Tenant Name")]
        public string TenantName { get; private set; }

        /// <summary>
        /// The location of the device
        /// </summary>
        /// <value>String value denoting the location of the device</value>
        /// <example>DTC</example>
        [Display(Name="Location Name")]
        public string LocationName { get; private set; }

        /// <summary>
        /// The status of the device
        /// </summary>
        /// <value>String value denoting the status of the device</value>
        /// <example>Production</example>
        [Display(Name="Device Status")]
        public string DeviceStatus { get; private set; }

        /// <summary>
        /// The device role of the device
        /// </summary>
        /// <value>String value denoting device role</value>
        /// <example>Red</example>
        [Display(Name = "Device Role")]
        public string DeviceRole { get; private set; }

        /// <summary>
        /// List of ports for the device
        /// </summary>
        /// <value>List of PortViewModel objects</value>
        [Display(Name = "Ports")]
        public List<PortViewModel> Ports { get; set; }

    }
}

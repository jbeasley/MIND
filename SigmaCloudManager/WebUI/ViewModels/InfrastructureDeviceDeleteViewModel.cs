
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
    /// Model for an infrastructure device to be deleted
    /// </summary>
    public class InfrastructureDeviceDeleteViewModel : IModifiableResource
    { 
        /// <summary>
        /// The ID of the device
        /// </summary>
        /// <value>Integer denoting the ID of the device</value>
        /// <example>90991</example>
        public int? DeviceId { get; set; }

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
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

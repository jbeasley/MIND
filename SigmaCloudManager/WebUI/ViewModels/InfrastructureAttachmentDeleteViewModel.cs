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
    /// Model of an infrastructure attachment for deletion
    /// </summary>
    public class InfrastructureAttachmentDeleteViewModel : IModifiableResource
    {
        /// <summary>
        /// ID of the attachment
        /// </summary>
        /// <value>Integer value denoting the ID of the attachment</value>
        /// <example>6001</example>
        public int? AttachmentId { get; set; }

        /// <summary>
        /// The ID of the device
        /// </summary>
        /// <value>Integer denoting the ID of the device</value>
        /// <example>90991</example>
        public int? DeviceId { get; set; }

        /// <summary>
        /// The name of the attachment
        /// </summary>
        /// <value>string value denoting the name of the attachment</value>
        /// <example>TenGigabitEthernet0/0</example>
        public string Name { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

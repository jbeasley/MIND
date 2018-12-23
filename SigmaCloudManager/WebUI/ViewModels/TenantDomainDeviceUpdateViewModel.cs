using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for updates to an existing tenant domain device
    /// </summary>
    public class TenantDomainDeviceUpdateViewModel : IModifiableResource
    {
        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>Integer value denoting the device identifier.</value>
        public int? DeviceId { get; set; }

        /// <summary>
        /// The name of the device
        /// </summary>
        /// <value>String denoting the name of the device</value>
        /// <example>DTC-CPE-1</example>
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The device name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        public string Name { get; set; }

        /// <summary>
        /// A description of the device
        /// </summary>
        /// <value>String denoting the description of the device</value>
        /// <example>Customer edge device located in DTC</example>
        public string Description { get; set; }

        /// <summary>
        /// The status of the device
        /// </summary>
        /// <value>A member of the DeviceStatusTypeEnum enumeration</value>
        /// <example>Production</example>
        [Display(Name = "Device Status")]
        public DeviceStatusTypeEnum? DeviceStatus { get; set; }

        /// <summary>
        /// Determines if layer 2 overhead should be included in the device MTU calculation
        /// </summary>
        /// <value>Boolean value denoting if layer 2 overhead is included in the device MTU calculation</value>
        /// <example>true</example>
        [Display(Name = "Use Layer 2 Interface MTU")]
        public bool UseLayer2InterfaceMtu { get; set; }

        /// <summary>
        /// List of ports requests for the device
        /// This list includes requests for new ports and updates for existing ports
        /// </summary>
        /// <value>List of PortRequestViewModel objects</value>
        [Display(Name = "Ports")]
        public List<PortRequestOrUpdateViewModel> Ports { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }

    }
}

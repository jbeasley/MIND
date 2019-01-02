using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for requesting a new infrastructure device
    /// </summary>
    public class InfrastructureDeviceRequestViewModel
    {
        /// <summary>
        /// The name of the device
        /// </summary>
        /// <value>String denoting the name of the device</value>
        /// <example>DTC-CPE-1</example>
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The device name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        public string Name { get; set; }

        /// <summary>
        /// A description of the device
        /// </summary>
        /// <value>String denoting the description of the device</value>
        /// <example>Customer Edge device located in DTC</example>
        public string Description { get; set; }

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

        /// <summary>
        /// The ID of the grand-parent region of the location
        /// </summary>
        /// <value>Integer value denoting the ID of the grand-parent region</value>
        public int? RegionId { get; set; }

        /// <summary>
        /// The ID of the parent subregion of the location
        /// </summary>
        /// <value>Integer value denoting the ID of the parent subregion</value>
        public int? SubRegionId { get; set; }

        /// <summary>
        /// The location of the device
        /// </summary>
        /// <value>A string denoting the location of the device</value>
        /// <example>DTC</example>
        [Required]
        [Display(Name="Location")]
        public string LocationName { get; set; }

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

        /// <summary>
        /// List of port requests for the device
        /// </summary>
        /// <value>List of PortRequestViewModel objects</value>
        [Display(Name="Ports")]
        public List<PortRequestOrUpdateViewModel> Ports { get; set; }
    }
}

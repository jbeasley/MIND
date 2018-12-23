
using System.Collections.Generic;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for updates to an existing infrastructure device
    /// </summary>
    public class InfrastructureDeviceUpdate
    {
        /// <summary>
        /// The ID of the device
        /// </summary>
        /// <value>Integer value denoting the ID of the device</value>
        public int? DeviceId { get; set; }

        /// <summary>
        /// The name of the device
        /// </summary>
        /// <value>String denoting the name of the device</value>
        public string Name { get; set; }

        /// <summary>
        /// A description of the device
        /// </summary>
        /// <value>String denoting the description of the device</value>
        public string Description { get; set; }

        /// <summary>
        /// The status of the device
        /// </summary>
        /// <value>A member of the DeviceStatusTypeEnum enumeration</value>
        public DeviceStatusTypeEnum? DeviceStatus { get; set; }

        /// <summary>
        /// Determines if layer 2 overhead should be included in the device MTU calculation
        /// </summary>
        /// <value>Boolean value denoting if layer 2 overhead is included in the device MTU calculation</value>
        public bool? UseLayer2InterfaceMtu { get; set; }

        /// <summary>
        /// List of new port requests for the device
        /// </summary>
        public List<PortRequest> PortRequests { get; set; }

        /// <summary>
        /// List of port updates for the device
        /// </summary>
        public List<PortUpdate> PortUpdates { get; set; }
    }
}

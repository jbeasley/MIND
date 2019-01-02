
using System.Collections.Generic;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting a new infrastructure device
    /// </summary>
    public class InfrastructureDeviceRequest
    {
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
        /// The model of the device
        /// </summary>
        /// <value>String denoting the model of the device</value>
        public string DeviceModel { get; set; }

        /// <summary>
        /// The role of the device
        /// </summary>
        /// <value>String denoting the role of the device</value>
        public string DeviceRole{ get; set; }

        /// <summary>
        /// The provider network plane to which the device belongs
        /// </summary>
        /// <value>A member of the PlaneEnum enumeration</value>
        public PlaneEnum? PlaneName { get; set; }

        /// <summary>
        /// The location of the device
        /// </summary>
        public string LocationName { get; set; }

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
        /// List of port requests for the device
        /// </summary>
        public List<PortRequestOrUpdate> Ports { get; set; }

    }
}

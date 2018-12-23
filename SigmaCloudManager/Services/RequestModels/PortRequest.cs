
namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting the creation of a device port
    /// </summary>
    public class PortRequest
    {
        /// <summary>
        /// The type of the port, e.g. TenGigabitEtheret
        /// </summary>
        /// <value>String denoting the type of the port</value>
        public string PortType { get; set; }

        /// <summary>
        /// The name of the port, e.g. 1/0
        /// </summary>
        /// <value>String denoting the name of the port</value>
        public string PortName { get; set; }

        /// <summary>
        /// Small Form-Factor Pluggable optic for the port
        /// </summary>
        /// <value>String denoting the small form-factor pluggable optic for the port</value>
        public string PortSfp { get; set; }

        /// <summary>
        /// Status of the port
        /// </summary>
        /// <value>Member of the PortStatusTypeEnum enunmeration</value>
        public PortStatusTypeEnum? PortStatus { get; set; }

        /// <summary>
        /// The role of the port
        /// </summary>
        /// <value>String value denoting the role of the port</value>
        public string PortRole { get; set; }

        /// <summary>
        /// Pool to which the port is assigned
        /// </summary>
        /// <value>String value denoting the pool to which the port should be assigned</value>
        public string PortPool { get; set; }

        /// <summary>
        /// The connector type of the port
        /// </summary>
        /// <value>String value denoting the required port connector</value>
        public string PortConnector { get; set; }

        /// <summary>
        /// The physical bandwidth of the port in Gbps
        /// </summary>
        /// <value>Integer value denoting the physical bandwidth of the port</value>
        public int? PortBandwidthGbps { get; set; }

        /// <summary>
        /// The ID of the tenant to which the port should be assigned.
        /// </summary>
        /// <value>Integer denoting the ID of the tenant</value>
        public int? TenantId { get; set; }

    }
}


namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for updating an existing device port
    /// </summary>
    public class PortUpdate
    {
        /// <summary>
        /// The ID of the port
        /// </summary>
        /// <value>Integer value denoting the ID of the port</value>
        public int? PortId { get; set; }

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
        /// The connector type of the port
        /// </summary>
        /// <value>String value denoting the required port connector</value>
        public string PortConnector { get; set; }

        /// <summary>
        /// The ID of the tenant to which the port should be assigned.
        /// </summary>
        /// <value>Integer denoting the ID of the tenant</value>
        public int? TenantId { get; set; }
    }
}

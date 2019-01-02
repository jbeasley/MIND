
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of a port
    /// </summary>
    public class PortViewModel
    {
        /// <summary>
        /// The ID of the port
        /// </summary>
        /// <value>An integer value denoting the ID of the port</value>
        /// <example>7001</example>
        [Display(Name = "Port ID")]
        public int? PortId { get; private set; }

        /// <summary>
        /// The type of the port, e.g. TenGigabitEtheret
        /// </summary>
        /// <value>String denoting the type of the port</value>
        /// <example>TenGigabitEthernet</example>
        [Display(Name = "Type")]
        public string PortType { get; private set; }

        /// <summary>
        /// The name of the port
        /// </summary>
        /// <value>String denoting the name of the port</value>
        /// <example>1/1/1</example>
        [Display(Name = "Name")]
        public string PortName { get; private set; }

        /// <summary>
        /// The name of the small form-factor pluggable optic for the port
        /// </summary>
        /// <value>A string denoting the name of the small form-factor pluggable optic for the port</value>
        /// <example>SFP-10G-SR</example>
        [Display(Name = "SFP")]
        public string PortSfp { get; private set; }

        /// <summary>
        /// Status of the port
        /// </summary>
        /// <value>String denoting the status of the port</value>
        /// <example>Assigned</example>
        [Display(Name = "Status")]
        public string PortStatus { get; private set; }

        /// <summary>
        /// The role of the port
        /// </summary>
        /// <value>String value denoting the role of the port</value>
        /// <example>My-Port-Role</example>
        [Display(Name = "Port Role")]
        public string PortRole { get; private set; }

        /// <summary>
        /// Pool to which the port is assigned
        /// </summary>
        /// <value>String value denoting pool to which the port is assigned</value>
        /// <example>My-Port-Pool</example>
        [Display(Name = "Port Pool")]
        public string PortPool { get; private set; }

        /// <summary>
        /// Port connector type
        /// </summary>
        /// <value>String denoting the port connector type</value>
        /// <example>SC</example>
        [Display(Name = "Port Connector")]
        public string PortConnector { get; private set; }

        /// <summary>
        /// Port Bandwidth in Gbps
        /// </summary>
        /// <value>Integer value denoting the port bandwidth in Gbps</value>
        /// <example>10</example>
        [Display(Name = "Port Bandwidth (Gbps)")]
        public int? PortBandwidthGbps { get; private set; }

        /// <summary>
        /// The ID of the tenant to which the port is assigned.
        /// </summary>
        /// <value>Integer value denoting the ID of the tenant</value>
        /// <example>9009</example>
        [Display(Name = "Tenant ID")]
        public int? TenantId { get; private set; }

        /// <summary>
        /// The name of the tenant to which the port is assigned.
        /// </summary>
        /// <value>String value denoting the name of the tenant</value>
        /// <example>product-group-tenant</example>
        [Display(Name = "Tenant Name")]
        public string TenantName { get; set; }

        /// <summary>
        /// The ID of the device to which the port belongs.
        /// </summary>
        /// <value>Integer value denoting the ID of the device</value>
        /// <example>9009</example>
        [Display(Name = "Device ID")]
        public int? DeviceId { get; private set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for requesting the creation of a device port or for update to an existing port
    /// </summary>
    public class PortRequestOrUpdateViewModel
    {
        /// <summary>
        /// The ID of an existing port for update
        /// </summary>
        /// <value>An integer value denoting the ID of the port</value>
        /// <example>7001</example>
        public int? PortId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Mind.WebUI.Models.PortRequestOrUpdateViewModel"/>
        /// is for a device with the provider domain role.
        /// </summary>
        /// <value><c>true</c> if is provider domain role; otherwise, <c>false</c>.</value>
        public bool IsProviderDomainRole { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Mind.WebUI.Models.PortRequestOrUpdateViewModel"/>
        /// is for a device with the tenant domain role.
        /// </summary>
        /// <value><c>true</c> if is tenant domain role; otherwise, <c>false</c>.</value>
        public bool IsTenantDomainRole { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Mind.WebUI.Models.PortRequestOrUpdateViewModel"/>
        /// is new.
        /// </summary>
        /// <value><c>true</c> if is new; otherwise, <c>false</c>.</value>
        public bool IsNew { get; set; }

        /// <summary>
        /// The type of the port, e.g. TenGigabitEtheret
        /// </summary>
        /// <value>String denoting the type of the port</value>
        /// <example>TenGigabitEtheret</example>
        [Required]
        [Display(Name="Type")]
        public string PortType { get; set; }

        /// <summary>
        /// The name of the port, e.g. 1/0
        /// </summary>
        /// <value>String denoting the name of the port</value>
        /// <example>1/0</example>
        [Required]
        [Display(Name="Name")]
        public string PortName { get; set; }

        /// <summary>
        /// Small Form-Factor Pluggable optic for the port
        /// </summary>
        /// <value>String denoting the small form-factor pluggable optic for the port</value>
        /// <example>SFP-10G-SR</example>
        [Required]
        [Display(Name="Port SFP")]
        public string PortSfp { get; set; }

        /// <summary>
        /// Status of the port
        /// </summary>
        /// <value>Member of the PortStatusTypeEnum enunmeration</value>
        [Required]
        [Display(Name = "Port Status")]
        public PortStatusTypeEnum? PortStatus { get; set; }

        /// <summary>
        /// The role of the port
        /// </summary>
        /// <value>String value denoting the role of the port</value>
        [Required]
        [Display(Name="Port Role")]
        public string PortRole { get; set; }

        /// <summary>
        /// The connector type of the port
        /// </summary>
        /// <value>String value denoting the required port connector</value>
        /// <example>RJ45</example>
        [Required]
        [Display(Name = "Port Connector")]
        public string PortConnector { get; set; }

        /// <summary>
        /// The physical bandwidth of the port in Gbps
        /// </summary>
        /// <value>Integer value denoting the physical bandwidth of the port</value>
        /// <example>10</example>
        [Required]
        [Display(Name = "Port Bandwidth (Gbps)")]
        public int? PortBandwidthGbps { get; set; }

        /// <summary>
        /// Pool to which the port is assigned
        /// </summary>
        /// <value>String value denoting the pool to which the port should be assigned</value>
        [Required]
        [Display(Name="Port Pool")]
        public string PortPool { get; set; }

        /// <summary>
        /// The name of the device to which the port belongs.
        /// </summary>
        /// <value>String valie denoting the name of the devicet</value>
        /// <example>Device-1</example>
        public string DeviceName { get; private set; }

        /// <summary>
        /// The name of the attachment to which an assigned port belongs.
        /// </summary>
        /// <value>String valie denoting the name of the attachment</value>
        /// <example>TenGigabitEthernet 1/0</example>
        public string AttachmentName { get; set; }

        /// <summary>
        /// The ID of a tenant to which an assigned port belongs.
        /// </summary>
        /// <value>Integer denoting the ID of the tenant</value>
        /// <example>111001</example>
        public string TenantId { get; set; }

        /// <summary>
        /// The name of a tenant to which an assigned port belongs.
        /// </summary>
        /// <value>String denoting the name of the tenant</value>
        /// <example>Elekton-Business-Unit</example>
        [Display(Name = "TenantName")]
        public string TenantName { get; set; }
    }
}

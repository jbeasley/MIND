
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{ 
    /// <summary>
    /// Model for a logical interface
    /// </summary>
    public partial class LogicalInterfaceViewModel
    {
        /// <summary>
        /// The ID of the logical interface
        /// </summary>
        /// <value>Integer value denoting the ID of the logical interface</value>
        /// <example>91009</example>
        [Display(Name = "Logical Interface ID")]
        public int? LogicalInterfaceId { get; private set; }

        /// <summary>
        /// The name of the routing instance
        /// </summary>
        /// <value>String value denoting the name of the routing instance</value>
        /// <example>713faafc85ff43db8472b6b9c38033a1</example>
        [Display(Name = "Routing Instance Name")]
        public string RoutingInstanceName { get; private set; }

        /// <summary>
        /// The ID of the routing instance
        /// </summary>
        /// <value>Integer value denoting the ID of the routing instance</value>
        /// <example>44001</example>
        [Display(Name = "Routing Instance ID")]
        public string RoutingInstanceId { get; private set; }

        /// <summary>
        /// The name of the logical interface.
        /// </summary>
        /// <value>A string value denoting the name of the logical interface</value>
        /// <example>Loopback1</example>
        [Display(Name = "Name")]
        public string Name { get; private set; }

        /// <summary>
        /// A description of the logical interface.
        /// </summary>
        /// <value>A string value denoting the description of the logical interface</value>
        /// <example>Loopback interface for multi-hop BGP peering</example>
        [Display(Name = "Description")]
        public string Description { get; private set; }

        /// <summary>
        /// The type of logical interface required.
        /// </summary>
        /// <value>Enum value denoting the type of logical interface</value>
        /// <example>Loopback</example>
        [Required]
        [Display(Name = "Logical Interface Type")]
        public string LogicalInterfaceType { get; private set; }

        /// <summary>
        /// IPv4 address assigned to the logical interface
        /// </summary>
        /// <value>String value representing the IPv4 address assigned to the logical interface</value>
        /// <example>192.168.0.1</example>
        [Display(Name = "IP Address")]
        public string IpAddress { get; private set; }

        /// <summary>
        /// IPv4 subnet mask assigned to the logical interface
        /// </summary>
        /// <value>String value representing the IPv4 subnet mask assigned to the logical interface</value>
        /// <example>255.255.255.252</example>
        [Display(Name = "Subnet Mask")]
        public string SubnetMask { get; private set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

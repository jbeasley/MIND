using SCM.Models.RequestModels;

namespace Mind.Models.RequestModels
{ 
    /// <summary>
    /// Model for requesting a logical interface in the provider domain
    /// </summary>
    public class ProviderDomainLogicalInterfaceRequest 
    {
        /// <summary>
        /// A description of the logical interface.
        /// </summary>
        /// <value>A string value denoting the description to apply to the logical interface</value>
        public string Description { get; set; }

        /// <summary>
        /// The type of logical interface required.
        /// </summary>
        /// <value>Enum value denoting the type of logical interface</value>
        public LogicalInterfaceTypeEnum LogicalInterfaceType { get; set; }

        /// <summary>
        /// IPv4 address and mask to be assigned to the logical interface
        /// </summary>
        /// <value>An instance of Ipv4AddressAndMask</value>
        public Ipv4AddressAndMask Ipv4Address { get; set; }
    }
}

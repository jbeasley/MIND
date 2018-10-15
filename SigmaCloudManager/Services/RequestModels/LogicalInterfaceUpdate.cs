
using SCM.Models.RequestModels;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for updating a logical interface
    /// </summary>
    public partial class LogicalInterfaceUpdate
    {
        /// <summary>
        /// The ID for the logical interface.
        /// </summary>
        /// <value>An integer value denoting the ID of hte logical interface</value>
        public int? LogicalInterfaceId { get; set; }

        /// <summary>
        /// A description of the logical interface.
        /// </summary>
        /// <value>A string value denoting the description to apply to the logical interface</value>
        public string Description { get; set; }

        /// <summary>
        /// IPv4 address and mask to be assigned to the logical interface
        /// </summary>
        /// <value>An instance of Ipv4AddressAndMask</value>
        public Ipv4AddressAndMask Ipv4Address { get; set; }

    }
}
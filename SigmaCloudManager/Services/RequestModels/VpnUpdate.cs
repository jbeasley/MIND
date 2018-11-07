using System.Collections.Generic;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for updating a vpn
    /// </summary>
    public class VpnUpdate
    {
        /// <summary>
        /// The name of the vpn
        /// </summary>
        /// <value>String value denoting the name of the vpn</value>
        public string Name { get; set; }

        /// <summary>
        /// A description of the VPN
        /// </summary>
        /// <value>String value denoting the vpn description</value>
        public string Description { get; set; }

        /// <summary>
        /// The geographical region which the vpn operates within. If no region is chosen then the vpn should be made available in all regions
        /// </summary>
        /// <value>Enum value denoting the region</value>
        public RegionEnum? Region { get; set; }

        /// <summary>
        /// The tenancy type of the vpn. If the tenancy type is single then only the owner of the vpn can participate in the vpn. 
        /// If the tenancy type is multi then any tenant can participate in the vpn.
        /// </summary>
        /// <value>Enum value denoting the tenancy type of the vpn</value>
        public TenancyTypeEnum? TenancyType { get; set; }

        /// <summary>
        /// Determines if the vpn supports extranet connectivity
        /// </summary>
        /// <value>Boolean denoting whether the vpn supports extranet</value>
        public bool? IsExtranet { get; set; }

        /// <summary>
        /// The multicast direction type of the VPN. 
        /// </summary>
        /// <value>Enum value denoting the multicast direction type of the vpn.</value>
        public MulticastVpnDirectionTypeEnum? MulticastVpnDirectionType { get; set; }

        /// <summary>
        /// A list of attachment sets to be associated with the vpn
        /// </summary>
        /// <value>A list of VpnAttachmentSetRequest objects</value>
        public List<VpnAttachmentSetRequest> VpnAttachmentSets { get; set; }
    }
}
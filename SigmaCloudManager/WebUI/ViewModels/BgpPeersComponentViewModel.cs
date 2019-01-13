using System.Collections.Generic;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for the BGP Peers view component
    /// </summary>
    public class BgpPeersComponentViewModel
    {
        /// <summary>
        /// Gets or sets the routing instance identifier.
        /// </summary>
        /// <value>Integer value denoting the routing instance identifier.</value>
        public int? RoutingInstanceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the port pool.
        /// </summary>
        /// <value>String denoting the name of the port pool.</value>
        public string PortPoolName { get; set; }

        /// <summary>
        /// Gets or sets the name of the attachment role.
        /// </summary>
        /// <value>String denoting the name of the attachment role.</value>
        public string AttachmentRoleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the vif role.
        /// </summary>
        /// <value>String denoting the name of the vif role.</value>
        public string VifRoleName { get; set; }

        /// <summary>
        /// Gets or sets the attachment identifier.
        /// </summary>
        /// <value>Integer value denoting the attachment identifier.</value>
        public int? AttachmentId { get; set; }

        /// <summary>
        /// Gets or sets the vif identifier.
        /// </summary>
        /// <value>Integer denoting the vif identifier.</value>
        public int? VifId { get; set; }

        /// <summary>
        /// The list of BGP peer requests denoting requests to create or update
        /// a collection of BGP peers
        /// </summary>
        /// <value>A list of BgpPeerRequestViewmodel</value>
        public List<BgpPeerRequestViewModel> BgpPeers { get; set; }
    }
}
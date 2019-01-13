using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of a routing instance request
    /// </summary>
    public class RoutingInstanceBgpPeersRequestViewModel : IModifiableResource
    {
        public RoutingInstanceBgpPeersRequestViewModel()
        {
            // Instantiate new list of BGP peer requests. In the case where all BGP peers have been removed 
            // using the web UI this will ensure that the corresponding BGP peer records are removed from the database.
            // An empty list will be passed to the application layer which will be processed as a request to remove all 
            // existing BGP peers for the routing instance.
            BgpPeers = new List<BgpPeerRequestViewModel>();
        }

        /// <summary>
        /// Gets or sets the routing instance identifier.
        /// </summary>
        /// <value>Integer denoting the routing instance identifier.</value>
        public int? RoutingInstanceId { get; set; }

        /// <summary>
        /// Gets or sets the  device identifier.
        /// </summary>
        /// <value>Integer denoting the device identifier.</value>
        public int? DeviceId { get; set; }
             
        /// <summary>
        /// A list of BGP peers to be created within the routing instance.
        /// </summary>
        /// <value>A list of BgpPeerRequestViewModel objects</value>
        public List<BgpPeerRequestViewModel> BgpPeers { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }

    }
}

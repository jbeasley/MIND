using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting a routing instance to be added to an attachment set
    /// </summary>
    public class RoutingInstanceForAttachmentSetRequest
    {
        /// <summary>
        /// Name of a routing instance
        /// </summary>
        public string RoutingInstanceName { get; set; }

        /// <summary>
        /// The local routing prefernce to apply to routes within the routing instance
        /// </summary>
        public int? LocalIpRoutingPreference { get; set; }

        /// <summary>
        /// The routing preference to advertise with routes from the routing instance
        /// </summary>
        public int? AdvertisedIpRoutingPreference { get; set; }

        /// <summary>
        /// The designated router preference for multicast routing
        /// </summary>
        public int? MulticastDesignatedRouterPreference { get; set; }
    }
}

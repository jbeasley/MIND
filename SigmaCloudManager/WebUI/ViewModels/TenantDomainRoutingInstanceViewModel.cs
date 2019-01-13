using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// View model of a routing instance in the tenant domain
    /// </summary>
    public class TenantDomainRoutingInstanceViewModel
    {
        /// <summary>
        /// The ID of the routing instance
        /// </summary>
        /// <value>An integer value denoting the ID of the routing instance</value>
        /// <example>4001</example>
        public int? RoutingInstanceId { get; private set; }

        /// <summary>
        /// The MIND system-generated name of the routing instance
        /// </summary>
        /// <value>A string value denoting the name of the routing instance</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        [Display(Name = "Name")]
        public string Name { get; private set; }

        /// <summary>
        /// A list of BGP peers which are configured for the routing instance
        /// </summary>
        /// <value>A list of BgpPeerViewModel objects</value>
        [Display(Name = "BGP Peers")]
        public List<TenantDomainBgpPeerViewModel> BgpPeers { get; private set; }
    }
}

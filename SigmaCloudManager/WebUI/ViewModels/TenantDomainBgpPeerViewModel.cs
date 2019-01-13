using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// View model of a tenant domain BGP peer
    /// </summary>
    public class TenantDomainBgpPeerViewModel
    { 
        /// <summary>
        /// The ID of the BGP peer
        /// </summary>
        /// <value>An integer denoting the BGP peer ID</value>
        /// <example>3001</example>
        public int? BgpPeerId { get; private set; }

        /// <summary>
        /// The name of the routing instance to which the bgp peer is associated.
        /// </summary>
        /// <value>A string value denoting the name of the routing instance</value>
        /// <example>713faafc85ff43db8472b6b9c38033a1</example>
        [Display(Name = "Routing Instance Name")]
        public string RoutingInstanceName { get; private set; }

        /// <summary>
        /// IPv4 address of the BGP peer
        /// </summary>
        /// <value>An IPv4 address</value>
        /// <example>12.1.1.1</example>
        [Display(Name = "IPv4 Peer Address")]
        public string Ipv4PeerAddress { get; private set; }

        /// <summary>
        /// The 2 Byte Autonomous system number of the BGP peer
        /// </summary>
        /// <value>An integer value between 1 and 65535</value>
        /// <example>65001</example>
        [Display(Name = "Peer 2 Byte Autonomous System")]
        public int? Peer2ByteAutonomousSystem { get; private set; }

        /// <summary>
        /// Authentication password for the BGP peer
        /// </summary>
        /// <value>A string representing the authentication password for the BGP peer</value>
        /// <example>pAs5w0rd!</example>
        [Display(Name = "Peer Password")]
        public string PeerPassword { get; private set; }

        /// <summary>
        /// Determines if multi-hop peering is enabled
        /// </summary>
        /// <value>Boolean value which determines if multi-hop peering is enabled</value>
        /// <example>true</example>
        [Display(Name = "MultiHop")]
        public bool IsMultiHop { get; private set; }

        /// <summary>
        /// Determines if the peer should be enabled with bidirectional forwarding detection.
        /// </summary>
        /// <value>Boolean value which determines if the peer should be enabled with bidirectional forwarding detection</value>
        /// <example>true</example>
        [Display(Name = "BFD Enabled")]
        public bool IsBfdEnabled { get; private set; }

        /// <summary>
        /// Determines the maximum number of routes the peer should accept
        /// </summary>
        /// <value>A positive integer which determines the meximum number of routes accepted from the BGP peer</value>
        /// <example>200</example>
        [Display(Name = "Maximum Routes")]
        public int? MaximumRoutes { get; private set; }
    }
}

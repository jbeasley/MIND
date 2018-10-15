namespace Mind.Models.RequestModels
{ 
    /// <summary>
    /// Model for BGP peer requests
    /// </summary>
    public class BgpPeerRequest
    {
        /// <summary>
        /// IPv4 address of the BGP peer
        /// </summary>
        /// <value>An IPv4 address</value>
        public string Ipv4PeerAddress { get; set; }

        /// <summary>
        /// The 2 Byte Autonomous system number of the BGP peer
        /// </summary>
        /// <value>An integer value between 1 and 65535</value>
        public int? Peer2ByteAutonomousSystem { get; set; }

        /// <summary>
        /// Authentication password for the BGP peer
        /// </summary>
        /// <value>A string representing the authentication password for the BGP peer</value>
        public string PeerPassword { get; set; }

        /// <summary>
        /// Determines if multi-hop peering is enabled. Defaults to false, meaning multihop is disabled.
        /// </summary>
        /// <value>Boolean value which determines if multi-hop peering is enabled</value>
        public bool? IsMultiHop { get; set; } = false;

        /// <summary>
        /// Determines if the peer should be enabled with bidirectional forwarding detection. Defaults to true, meaning BFD is enabled.
        /// </summary>
        /// <value>Boolean value which determines if the peer should be enabled with bidirectional forwarding detection</value>
        public bool? IsBfdEnabled { get; set; } = true;

        /// <summary>
        /// Determines the maximum number of routes the peer should accept. The default is 500
        /// </summary>
        /// <value>A positive integer which determines the meximum number of routes accepted from the BGP peer</value>
        public int? MaximumRoutes { get; set; } = 500;

    }
}

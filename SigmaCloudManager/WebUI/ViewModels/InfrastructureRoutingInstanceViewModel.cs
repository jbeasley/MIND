using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// View model of an infrastructure routing instance 
    /// </summary>
    public class InfrastructureRoutingInstanceViewModel
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
        /// The administrator sub-field of the routing instance
        /// </summary>
        /// <value>An integer value denoting the assigned-number sub-field of the routing instance</value>
        /// <example>8718</example>
        [Display(Name = "Administrator SubField")]
        public int? AdministratorSubField { get; private set; }

        /// <summary>
        /// The assigned-number sub-field of the routing instance
        /// </summary>
        /// <value>An integer value denoting the assigned-number sub-field of the routing instance</value>
        /// <example>10000</example>
        [Display(Name = "Assigned Number SubField")]
        public int? AssignedNumberSubField { get; private set; }

        /// <summary>
        /// A list of BGP peers which are configured for the routing instance
        /// </summary>
        /// <value>A list of BgpPeer objects</value>
        [Display(Name = "BGP Peers")]
        public List<InfrastructureBgpPeerViewModel> BgpPeers { get; private set; }

        /// <summary>
        /// A list of logical interfaces which are configured for the routing instance
        /// </summary>
        /// <value>A list of BgpPeer objects</value>
        [Display(Name = "Logical Interfaces")]
        public List<LogicalInterfaceViewModel> LogicalInterfaces { get; private set; }
    } 
}

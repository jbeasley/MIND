using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of a topolgoy type for a VPN
    /// </summary>
    public class VpnTopologyTypeViewModel
    {
        /// <summary>
        /// The ID of the VPN topology type
        /// </summary>
        /// <value>Integer value denoting the ID of the vpn topology type</value>
        public int VpnTopologyTypeID { get; private set; }

        /// <summary>
        /// The name of the VPN topology type
        /// </summary>
        /// <value>String value denoting the name of the vpn topology type</value>
        public string Name { get; private set; }
    }
}

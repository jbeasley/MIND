using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Port bandwidth view model.
    /// </summary>
    public class PortBandwidthViewModel
    {   
        /// <summary>
        /// Gets the port bandwidth identifier.
        /// </summary>
        /// <value>Integer value denoting the port bandwidth identifier.</value>
        public int PortBandwidthID { get; private set; }

        /// <summary>
        /// Gets the bandwidth value which is expressed in Gigabits per Second
        /// </summary>
        /// <value>Integer value denoting the bandwidth value</value>
        public int BandwidthGbps { get; private set; }
    }
}
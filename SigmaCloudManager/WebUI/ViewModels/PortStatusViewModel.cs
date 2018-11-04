using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of a port status option
    /// </summary>
    public class PortStatusViewModel
    {
        /// <summary>
        /// The ID of the port status
        /// </summary>
        /// <value>Integer value denoting the ID of the port status</value>
        public int PlaneID { get; private set; }

        /// <summary>
        /// The name of the port status
        /// </summary>
        /// <value>String value denoting the name of the port status</value>
        public string Name { get; private set; }
    }
}

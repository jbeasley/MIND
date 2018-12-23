using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// View model for a port connector
    /// </summary>
    public class PortConnectorViewModel
    {
        /// <summary>
        /// Gets the port connector identifier.
        /// </summary>
        /// <value>Integer value denoting the port connector identifier.</value>
        public int PortConnectorID { get; private set; }

        /// <summary>
        /// The name of the port connetor
        /// </summary>
        /// <value>String value denoting the name</value>
        public string Name { get; private set; }

        /// <summary>
        /// The description of the port connector
        /// </summary>
        /// <value>String value denoting the description</value>
        public string Description { get; private set; }
    }
}
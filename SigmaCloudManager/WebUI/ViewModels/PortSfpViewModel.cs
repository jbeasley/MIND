using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Port SFP view model.
    /// </summary>
    public class PortSfpViewModel
    {
        /// <summary>
        /// Gets the port sfp identifier.
        /// </summary>
        /// <value>Integer value denoting the port sfp identifier.</value>
        public int PortSfpID { get; private set; }

        /// <summary>
        /// The name of the port SFP
        /// </summary>
        /// <value>String value denoting the name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// The description of the port SFP
        /// </summary>
        /// <value>String value denoting the description.</value>
        public string Description { get; private set; }
    }
}
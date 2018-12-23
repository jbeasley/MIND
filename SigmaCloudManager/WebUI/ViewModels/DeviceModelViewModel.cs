using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Mind.WebUI.Models
{

    public class DeviceModelViewModel
    {
        /// <summary>
        /// Gets the device model identifier.
        /// </summary>
        /// <value>Integer value denoting the device model identifier.</value>
        public int DeviceModelID { get; private set; }

        /// <summary>
        /// The name of the device model
        /// </summary>
        /// <value>String value denoting the name</value>
        public string Name { get; private set; }

        /// <summary>
        /// Description of the device model
        /// </summary>
        /// <value>String value denotong the description.</value>
        public string Description { get; private set; }
    }
}
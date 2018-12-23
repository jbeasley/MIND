using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Mind.WebUI.Models
{

    public class DeviceStatusViewModel
    {
        /// <summary>
        /// Gets the ID of the device status.
        /// </summary>
        /// <value>Integer value denoting the ID of the device status</value>
        public int DeviceStatusID { get; private set; }

        /// <summary>
        /// The name of the device status.
        /// </summary>
        /// <value>String value denoting the name of the device status</value>
        public string Name { get; private set; }

        /// <summary>
        /// The description of the device status
        /// </summary>
        /// <value>String value denoting the description.</value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets or sets the type of the device status.
        /// </summary>
        /// <value>The type of the device status.</value>
        public DeviceStatusTypeEnum DeviceStatusType { get; private set; }

    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for requesting data to populate a grid of ports
    /// </summary>
    public class PortsGridDataViewModel
    {
        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>Integer value denoting the device identifier.</value>
        public int? DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the list of port request or updates.
        /// </summary>
        /// <value><list type="PortRequestOrUpdateViewModel"></list></value>
        public List<PortRequestOrUpdateViewModel> Ports { get; set; }
    }
}

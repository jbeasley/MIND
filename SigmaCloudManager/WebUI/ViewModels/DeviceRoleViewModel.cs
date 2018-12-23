using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Device role view model.
    /// </summary>
    public class DeviceRoleViewModel
    {
        /// <summary>
        /// Gets the device role identifier.
        /// </summary>
        /// <value>Integer value denoting the device role identifier.</value>
        public int DeviceRoleId { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>String value denoting the name.</value>
        [Display(Name = "Device Role")]
        public string Name { get; private set; }

        /// <summary>
        /// Description of the device role
        /// </summary>
        /// <value>String value denoting the description.</value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Mind.WebUI.Models.DeviceRoleViewModel"/> is a 
        /// tenant domain role.
        /// </summary>
        /// <value><c>true</c> if is tenant domain role; otherwise, <c>false</c>.</value>
        public bool IsTenantDomainRole { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Mind.WebUI.Models.DeviceRoleViewModel"/> is a 
        /// provider domain role.
        /// </summary>
        /// <value><c>true</c> if is provider domain role; otherwise, <c>false</c>.</value>
        public bool IsProviderDomainRole { get; private set; }
    }
}
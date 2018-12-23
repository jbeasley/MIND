using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Port role view model.
    /// </summary>
    public class PortRoleViewModel
    {
        /// <summary>
        /// Gets or sets the port role identifier.
        /// </summary>
        /// <value>Integer value denoting the port role identifier.</value>
        public int PortRoleId { get; private set; }

        /// <summary>
        /// The name of the port role
        /// </summary>
        /// <value>String value denoting the name.</value>
        [Display(Name = "Port Connector")]
        public string Name { get; private set; }

        /// <summary>
        /// Description of the port role
        /// </summary>
        /// <value>String value denoting the description.</value>
        public string Description { get; private set; }

        /// <summary>
        /// The type of the port role
        /// </summary>
        /// <value>Member of the PortRoleTypeEnum enumeration denoting the type of the port role.</value>
        public PortRoleTypeEnum PortRoleType { get; private set; }
    }
}
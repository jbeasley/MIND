using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// View model for attachment redundancy
    /// </summary>
    public class AttachmentRedundancyViewModel
    {
        /// <summary>
        /// Gets the attachment redundancy identifier.
        /// </summary>
        /// <value>Integer value denoting the attachment redundancy identifier.</value>
        public int AttachmentRedundancyID { get; private set; }

        /// <summary>
        /// Gets the name of of the attachment redundancy.
        /// </summary>
        /// <value>String value denoting the name.</value>
        public string Name { get; private set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace Mind.WebUI.Models
{
    public class VifRoleComponentViewModel
    {
        /// <summary>
        /// Gets or sets the attachment role identifier.
        /// </summary>
        /// <value>Integer value denoting the attachment role identifier.</value>
        public int? AttachmentRoleId { get; set; }

        /// <summary>
        /// Gets or sets the attachment redundancy.
        /// </summary>
        /// <value>Memnber of the AttachmentRedundancyEnum enumeration</value>
        [Display(Name = "VIF Role")]
        [Required(ErrorMessage = "A vif role option must be selected")]
        public string VifRoleName { get; set; }
    }
}
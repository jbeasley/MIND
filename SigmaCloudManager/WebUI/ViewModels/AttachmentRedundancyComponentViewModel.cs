using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace Mind.WebUI.Models
{
    public class AttachmentRedundancyComponentViewModel
    {
        /// <summary>
        /// Gets or sets the attachment redundancy.
        /// </summary>
        /// <value>Memnber of the AttachmentRedundancyEnum enumeration</value>
        [Display(Name = "Attachment Redundancy")]
        [Required(ErrorMessage = "An attachment redundancy option must be selected")]
        public AttachmentRedundancyEnum? AttachmentRedundancy { get; set; }
    }
}
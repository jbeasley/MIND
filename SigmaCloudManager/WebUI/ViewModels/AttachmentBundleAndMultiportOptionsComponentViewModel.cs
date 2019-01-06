using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for the bundle and multiport options component of an attachment request.
    /// </summary>
    public class AttachmentBundleAndMultiportOptionsComponentViewModel
    {
        /// <summary>
        /// Determines if a bundle style of attachment is required
        /// </summary>
        /// <value>Boolean value which denotes if a bundle style of attachment is required</value>
        /// <example>true</example>
        [Display(Name ="Bundle Required")]
        public bool BundleRequired { get; set; }
               
        /// <summary>
        /// Determines if a multi port style of attachment is required
        /// </summary>
        /// <value>Boolean value which denotes if a multi port style of attachment is required</value>
        [Display(Name = "Multiport Required")]
        public bool MultiportRequired { get; set; }

    }
}

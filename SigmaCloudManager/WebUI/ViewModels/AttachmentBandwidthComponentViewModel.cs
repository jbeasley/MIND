using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for the attachment bandwidth component
    /// </summary>
    public class AttachmentBandwidthComponentViewModel
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

        /// <summary>
        /// The required bandwidth of the attachment in Gbps
        /// </summary>
        /// <value>Integer value denoting the required attachment bandwidth in Gbps</value>
        /// <example>10</example>
        [Display(Name = "Attachment Bandwidth (Gbps)")]
        [Required]
        public int? AttachmentBandwidthGbps { get; set; }
    }
}

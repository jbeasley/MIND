using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for the attachment port pool and role component
    /// </summary>
    public class AttachmentPortPoolAndRoleComponentViewModel
    {
        /// <summary>
        /// The name of a port pool from which ports for the new attachment will be allocated from
        /// </summary>
        /// <value>String value denoting the name of a port pool</value>
        /// <example>Core</example>
        [Required]
        [Display(Name = "Port Pool Name")]
        public string PortPoolName { get; set; }

        /// <summary>
        /// The name of an attachment role. This argument sets certain constraints on how the attachment must be configured such
        /// as whether the attachment requires contract bandwidth to be defined.
        /// </summary>
        /// <value>String value denoting the name of an attachment role</value>
        /// <example>PE-P</example>
        [Required]
        [Display(Name = "Attachment Role Name")]
        public string AttachmentRoleName { get; set; }

        /// <summary>
        /// Gets or sets the name of a member of the PortRoleType enumeration.
        /// </summary>
        /// <value>String denoting the name of the PortRoleType enumeration member</value>
        public string PortRoleTypeEnumName { get; set; }

        /// <summary>
        /// Gets or sets the device role identifier.
        /// </summary>
        /// <value>Integer value denoting the device role identifier.</value>
        public int? DeviceRoleId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of an attachment role
    /// </summary>
    public class AttachmentRoleViewModel
    {
        /// <summary>
        /// The ID of the attachment role
        /// </summary>
        /// <value>Integer value denoting the ID of the attachment role</value>
        public int AttachmentRoleId { get; private set; }

        /// <summary>
        /// The name of the attachment role
        /// </summary>
        /// <value>String value denoting the name of the attachment role</value>
        public string Name { get; private set; }

        /// <summary>
        /// Description of the attachmenr role
        /// </summary>
        /// <value>String value denoting the description</value>
        public string Description { get; private set; }

        /// <summary>
        /// The ID of the port pool to which the attachment role belongs
        /// </summary>
        /// <value>String value denoting the ID of the port pool</value>
        public int PortPoolId { get; private set; }

        /// <summary>
        /// Denotes whether the attachment role is a tagged role. If so, any attachment associated with the
        /// role will be configured for tagging.
        /// </summary>
        /// <value>Boolean value denoting whether the role is a tagged role</value>
        [Display(Name="Tagged Role")]
        public bool IsTaggedRole { get; private set; }

        /// <summary>
        /// Denotes whether the attachment role is a layer 3 role. If so, any attachment associated with the
        /// role will be configured for layer 3.
        /// </summary>
        /// <value>Boolean value denoting whether the role is a layer 3 role</value>
        [Display(Name = "Layer3 Role")]
        public bool IsLayer3Role { get; private set; }

        /// <summary>
        /// Denotes whether attachments associated with this attachment role required a contract bandwidth allocation.
        /// </summary>
        /// <value>Boolean value denoting whether contract bandwidth is requried for any attachment associated with this role</value>
        [Display(Name = "Require Contract Bandwidth")]
        public bool RequireContractBandwidth { get; private set; }

        /// <summary>
        /// The type of routing instance which must be associated with any attachments which belong to this attachment role.
        /// </summary>
        /// <value>Integer value denoting the ID of the routing instance type</value>
        public int? RoutingInstanceTypeID { get; private set; }

        /// <summary>
        /// Denotes whether the attachment role supports bundle-style attachments. If so, any attachment associated with the
        /// role may be configured as a bundle.
        /// </summary>
        /// <value>Boolean value denoting whether the role supports bundle attachments</value>
        [Display(Name = "Supported By Bundle Attachment")]
        public bool SupportedByBundle { get; private set; }

        /// <summary>
        /// Denotes whether the attachment role supports multiport-style attachments. If so, any attachment associated with the
        /// role may be configured as a multiport
        /// </summary>
        /// <value>Boolean value denoting whether the role supports multiport attachments</value>

        [Display(Name = "Supported By Multi-Port Attachment")]
        public bool SupportedByMultiPort { get; private set; }
    }
}
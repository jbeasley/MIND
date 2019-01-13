using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for the contract bandwidth component
    /// </summary>
    public class ContractBandwidthComponentViewModel
    { 
        /// <summary>
        /// Gets or sets the name of the port pool.
        /// </summary>
        /// <value>String denoting the name of the port pool.</value>
        public string PortPoolName { get; set; }

        /// <summary>
        /// Gets or sets the name of the attachment role.
        /// </summary>
        /// <value>String denoting the name of the attachment role.</value>
        public string AttachmentRoleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the vif role.
        /// </summary>
        /// <value>String denoting the name of the vif role.</value>
        public string VifRoleName { get; set; }

        /// <summary>
        /// Gets or sets the attachment identifier.
        /// </summary>
        /// <value>Integer value denoting the attachment identifier.</value>
        public int? AttachmentId { get; set; }

        /// <summary>
        /// Gets or sets the vif identifier.
        /// </summary>
        /// <value>Integer denoting the vif identifier.</value>
        public int? VifId { get; set; }

        /// <summary>
        /// Gets or sets the contract bandwidth pool
        /// </summary>
        /// <value>An instance of the ContractBandwidthPoolViewModel</value>
        public ContractBandwidthPoolViewModel ContractBandwidthPool { get; set; }
    }
}

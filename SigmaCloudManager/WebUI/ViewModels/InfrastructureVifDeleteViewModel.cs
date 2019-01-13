using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of an infrastructure vif for deletion
    /// </summary>
    public class InfrastructureVifDeleteViewModel : IModifiableResource
    {
        /// <summary>
        /// ID of the vif
        /// </summary>
        /// <value>Integer value denoting the ID of the vif</value>
        /// <example>6001</example>
        public int? VifId { get; set; }

        /// <summary>
        /// The name of the vif
        /// </summary>
        /// <value>string value denoting the name of the vif</value>
        /// <example>TenGigabitEthernet0/0</example>
        public string Name { get; set; }

        /// <summary>
        /// ID of the attachment
        /// </summary>
        /// <value>Integer valude denoting the ID of the tenant</value>
        /// <example>6001</example>
        public int? AttachmentId { get; set; }

        /// <summary>
        /// The name of the attachment
        /// </summary>
        /// <value>String value for the name of the attachment</value>
        /// <example>product-group-tenant</example>
        [Display(Name = "Attachment Name")]
        public string AttachmentName { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

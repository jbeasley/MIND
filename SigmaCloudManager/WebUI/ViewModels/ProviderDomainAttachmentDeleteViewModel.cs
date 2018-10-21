using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of a provider domain attachment for deletion
    /// </summary>
    public class ProviderDomainAttachmentDeleteViewModel
    {
        /// <summary>
        /// ID of the attachment
        /// </summary>
        /// <value>Integer value denoting the ID of the attachment</value>
        /// <example>6001</example>
        public int? AttachmentId { get; set; }

        /// <summary>
        /// The name of the attachment
        /// </summary>
        /// <value>string value denoting the name of the attachment</value>
        /// <example>TenGigabitEthernet0/0</example>
        public string Name { get; set; }

        /// <summary>
        /// ID of the tenant
        /// </summary>
        /// <value>Integer valude denoting the ID of the tenant</value>
        /// <example>6001</example>
        public int? TenantId { get; set; }

        /// <summary>
        /// The name of the tenant owner of the attachment
        /// </summary>
        /// <value>String value for the name of the tenant</value>
        /// <example>product-group-tenant</example>
        [Display(Name = "Tenant Name")]
        public string TenantName { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

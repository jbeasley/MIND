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
    /// Model for updating a tenant
    /// </summary>
    public class TenantUpdateViewModel : IModifiableResource
    {
        /// <summary>
        /// Gets or Sets TenantId
        /// </summary>
        /// <value>An integer denoting the ID of the tenant</value>
        /// <example>1001</example>
        public int? TenantId { get; set; }

        /// <summary>
        /// The name of the tenant
        /// </summary>
        /// <value>The name of the tenant</value>
        /// <example>product-group-tenant</example>
        [Required(AllowEmptyStrings = false, ErrorMessage = "A tenant name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The tenant name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        [StringLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }

    }
}

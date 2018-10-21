
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
    /// Model of a tenant
    /// </summary>
    public class TenantViewModel : IModifiableResource
    {
        /// <summary>
        /// Gets or Sets TenantId
        /// </summary>
        /// <value>An integer denoting the ID of the tenant</value>
        /// <example>1001</example>
        [Display(Name = "Tenant ID")]
        public int? TenantId { get; set; }

        /// <summary>
        /// The name of the tenant
        /// </summary>
        /// <value>The name of the tenant</value>
        /// <example>product-group-tenant</example>
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }

    }
}

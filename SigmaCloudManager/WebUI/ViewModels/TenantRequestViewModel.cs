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
    /// Model for requesting a new tenant
    /// </summary>
    public class TenantRequestViewModel
    {
        /// <summary>
        /// The name of the tenant
        /// </summary>
        /// <value>The name of the tenant</value>
        /// <example>product-group-tenant</example>
        [Required(AllowEmptyStrings = false, ErrorMessage = "A tenant name must be specified")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The tenant name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        [StringLength(30)]
        public string Name { get; set; }

    }
}

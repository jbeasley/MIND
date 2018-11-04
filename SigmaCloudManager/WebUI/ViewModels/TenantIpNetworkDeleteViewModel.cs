
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
    /// Model of a tenant IP network for deletion
    /// </summary>
    public class TenantIpNetworkDeleteViewModel : IModifiableResource
    {
        /// <summary>
        /// The ID of the tenant IP network
        /// </summary>
        /// <value>The ID of the tenant IP network</value>
        /// <example>2001</example>
        [Display(Name = "Tenant IP Network ID")]
        public int? TenantIpNetworkId { get; set; }

        /// <summary>
        /// The CIDR representation of the tenant IP network. This also includes the less-than-or-equal-to
        /// parameter if it is defined for this tenant IP network.
        /// </summary>
        /// <value>String value denoting the CIDR name</value>
        /// <example>10.1.0.0/16 le 24</example>
        [Display(Name = "Full CIDR Name")]
        public string CidrNameIncludingIpv4LessThanOrEqualToLength { get; private set; }

        /// <summary>
        /// The ID of the tenant to which the tenant network belongs
        /// </summary>
        /// <value>Integer value for the ID of the tenant</value>
        /// <example>1001</example>
        public int? TenantId { get; set; }

        /// <summary>
        /// The name of the tenant owner of the IP Network
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

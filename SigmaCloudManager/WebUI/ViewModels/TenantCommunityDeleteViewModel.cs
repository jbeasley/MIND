
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
    /// Model for deleting a tenant community
    /// </summary>
    public class TenantCommunityDeleteViewModel : IModifiableResource
    {
        /// <summary>
        /// The ID of the tenant community
        /// </summary>
        /// <value>Integer value denoting the ID of the tenant community</value>
        /// <example>2001</example>
        public int? TenantCommunityId { get; set; }
        
        /// <summary>
        /// The name of the tenant community
        /// </summary>
        /// <value>String value denoting the name of the tenant community</value>
        /// <example>8718:10001</example>
        public string Name { get; private set; } 

        /// <summary>
        /// The ID of the tenant to which the tenant community belongs
        /// </summary>
        /// <value>Integer value for the ID of the tenant</value>
        /// <example>1001</example>
        public int? TenantId { get; private set; }

        /// <summary>
        /// The name of the tenant owner of the IP network
        /// </summary>
        /// <value>String value denoting the name of the tenant</value>
        /// <example>DCIS</example>
        [Display(Name = "Tenant Name")]
        public string TenantName { get; private set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }

    }
}

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
    /// Model of a VPN for deletion
    /// </summary>
    public class VpnDeleteViewModel : IModifiableResource
    {
        /// <summary>
        /// The ID of the VPN
        /// </summary>
        /// <value>Integer value denoting the ID of the vpn</value>
        /// <example>12001</example>
        public int? VpnId { get; set; }

        /// <summary>
        /// The ID of the tenant owner of the VPN
        /// </summary>
        /// <value>Integer value for the ID of the tenant</value>
        /// <example>1001</example>
        public int? TenantId { get; set; }

        /// <summary>
        /// The name of the tenant owner of the vpn
        /// </summary>
        /// <value>String value denoting the name of the tenant</value>
        /// <example>DCIS</example>
        [Display(Name = "Tenant Name")]
        public string TenantName { get; private set; }

        /// <summary>
        /// The name of the VPN
        /// </summary>
        /// <value>String value denoting the name of the vpn</value>
        /// <example>cloud-connectivity-vpn</example>
        [Display(Name="Name")]
        public string Name { get; private set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

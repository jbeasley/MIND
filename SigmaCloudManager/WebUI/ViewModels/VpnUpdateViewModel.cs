
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
    /// Model for updating a vpn
    /// </summary>
    public class VpnUpdateViewModel : IModifiableResource
    {
        /// <summary>
        /// The name of the vpn
        /// </summary>
        /// <value>String value denoting the name of the vpn</value>
        /// <example>cloud-connectivity-vpn</example>
        [Display(Name="Name")]
        public string Name { get; set; }

        /// <summary>
        /// A description of the VPN
        /// </summary>
        /// <value>String value denoting the vpn description</value>
        /// <example>vpn for providing IP connectivity between hosts running in public and private clouds</example>
        [Display(Name="Description")]
        public string Description { get; set; }

        /// <summary>
        /// The geographical region which the vpn operates within. If no region is chosen then the vpn should be made available in all regions
        /// </summary>
        /// <value>Enum value denoting the region</value>
        /// <example>EMEA</example>
        [Display(Name="Region")]
        public RegionEnum? Region { get; set; }

        /// <summary>
        /// The tenancy type of the vpn. If the tenancy type is single then only the owner of the vpn can participate in the vpn. 
        /// If the tenancy type is multi then any tenant can participate in the vpn.
        /// </summary>
        /// <value>Enum value denoting the tenancy type of the vpn</value>
        /// <example>single</example>
        [Display(Name="Tenancy Type")]
        public TenancyTypeEnum? TenancyType { get; set; }

        /// <summary>
        /// Determines if the vpn supports extranet connectivity
        /// </summary>
        /// <value>Boolean denoting whether the vpn supports extranet</value>
        /// <example>true</example>
        [Display(Name = "Extranet")]
        public bool IsExtranet { get; set; }

        /// <summary>
        /// The multicast direction type of the VPN. 
        /// </summary>
        /// <value>Enum value denoting the multicast direction type of the vpn.</value>
        /// <example>unidirectional</example>
        [Display(Name = "Multicast VPN Direction Type")]
        public MulticastVpnDirectionTypeEnum? MulticastVpnDirectionType { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

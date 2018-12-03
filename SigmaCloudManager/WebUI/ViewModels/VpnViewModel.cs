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
    /// Model of a VPN
    /// </summary>
    public class VpnViewModel
    {
        /// <summary>
        /// The ID of the VPN
        /// </summary>
        /// <value>Integer value denoting the ID of the vpn</value>
        /// <example>12001</example>
        public int? VpnId { get; private set; }

        /// <summary>
        /// The ID of the tenant to which the VPN belongs
        /// </summary>
        /// <value>Integer value for the ID of the tenant</value>
        /// <example>1001</example>
        [Display(Name = "Tenant ID")]
        public int? TenantId { get; private set; }

        /// <summary>
        /// The name of the tenant owner of the VPN
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
        /// A description of the vpn
        /// </summary>
        /// <value>String value denoting the description of the vpn</value>
        /// <example>vpn for providing IP connectivity between hosts running in public and private clouds</example>
        [Display(Name="Description")]
        public string Description { get; private set; }

        /// <summary>
        /// The tenant owner of the vpn
        /// </summary>
        /// <value>String value denoting the name of the tenant owner</value>
        /// <example>product-group-tenant</example>
        [Display(Name = "Tenant Owner Name")]
        public string TenantOwnerName { get; private set; }

        /// <summary>
        /// The geographical region which the vpn operates within.
        /// </summary>
        /// <value>String value denoting the geographical region which the vpn operates within.</value>
        /// <example>EMEA</example>
        [Display(Name="Region")]
        public string Region { get; private set; }

        /// <summary>
        /// The provider plane which the vpn operates within.
        /// </summary>
        /// <value>String value denoting the provider plane which the vpn operates within.</value>
        /// <example>red</example>
        [Display(Name="Plane")]
        public string Plane { get; private set; }

        /// <summary>
        /// The tenancy type of the vpn. If the tenancy type is 'single' then only the owner of the VPN can participate in the vpn. 
        /// If the tenancy type is 'multi' then any tenant can participate in the vpn.
        /// </summary>
        /// <value>String value denoting the tenancy type of the vpn.</value>
        /// <example>single</example>
        [Display(Name="Tenancy Type")]
        public string TenancyType { get; private set; }

        /// <summary>
        /// The topology type of the vpn. A meshed vpn allows any endpoint to communicate with any other endpoint. 
        /// A hub-and-spoke vpn allows spoke endpoints to communicate with hub endpoints but not with other spoke endpoints. 
        /// </summary>
        /// <value>String value denoting the topology type of the vpn.</value>
        /// <example>meshed</example>
        [Display(Name="Topology Type")]
        public string TopologyType { get; private set; }

        /// <summary>
        /// The address family of the vpn.
        /// </summary>
        /// <value>String valude dneoting the address family of the vpn.</value>
        /// <example>ipv4</example>
        [Display(Name="Address-Family")]
        public string AddressFamily { get; private set; }

        /// <summary>
        /// Denotes whether the vpn conforms to the Nova standard. If this attribute is set to disabled then the VPN does not follow a standard 
        /// Nova implementation and may be customised.
        /// </summary>
        /// <value>Boolean value denoting the vpn as Nova standard compliant.</value>
        /// <example>true</example>
        [Display(Name="Nova VPN")]
        public bool IsNovaVpn { get; private set; }

        /// <summary>
        /// Denotes if the vpn supports extranet connectivity
        /// </summary>
        /// <value>Boolean denoting whether the vpn supports extranet</value>
        /// <example>true</example>
        [Display(Name = "Extranet")]
        public bool IsExtranet { get; private set; }

        /// <summary>
        /// Denotes if the VPN supports IP multicast
        /// </summary>
        /// <value>Boolean denoting whether the vpn supports IP multicast.</value>
        /// <example>true</example>
        [Display(Name = "Multicast VPN")]
        public bool IsMulticastVpn { get; private set; }

        /// <summary>
        /// The multicast service type of the VPN. 
        /// </summary>
        /// <value>Enum value denoting the multicast service type of the vpn.</value>
        /// <example>ssm</example>
        [Display(Name = "Multicast VPN Service Type")]
        public string MulticastVpnServiceType { get; private set; }

        /// <summary>
        /// The multicast direction type of the VPN. 
        /// </summary>
        /// <value>Enum value denoting the multicast direction type of the vpn.</value>
        /// <example>unidirectional</example>
        [Display(Name = "Multicast VPN Direction Type")]
        public string MulticastVpnDirectionType { get; private set; }

        /// <summary>
        /// Route targets assigned to the vpn
        /// </summary>
        /// <value>A list of RouteTargetViewModel objects</value>
        [Display(Name = "Route Targets")]
        public List<RouteTargetViewModel> RouteTargets { get; private set; }

        /// <summary>
        /// Attachment Sets which are bound to the vpn
        /// </summary>
        /// <value>A list of VpnAttachmentSetViewModel objects</value>
        [Display(Name = "Attachment Sets")]
        public List<VpnAttachmentSetViewModel> VpnAttachmentSets { get; private set; }

        /// <summary>
        /// Network status of the vpn.
        /// </summary>
        /// <value>String value denoting the network status</value>
        /// <example>Staged</example>
        [Display(Name = "Network Status")]
        public string NetworkStatus { get; private set; }

    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of an infrastructure vif - a virtual attachment which is configured under a
    /// tagged infrastructure attachment
    /// </summary>
    public partial class InfrastructureVifViewModel
    { 
        /// <summary>
        /// The ID of the vif
        /// </summary>
        /// <value>Interface value denoting the ID of the vif</value>
        /// <example>99099</example>
        [Display(Name="Vif ID")]
        public int? VifId { get; private set; }

        /// <summary>
        /// The name of the vif. The name is a concatenation of the name of the attachment
        /// and the vlan tag assigned to the vif.
        /// </summary>
        /// <value>String value denoting the name of the vif</value>
        /// <example>TenGigabitEthernet0/1.100</example>
        [Display(Name="Name")]
        public string Name { get; private set; }

        /// <summary>
        /// Determines if the vif is enabled for layer 3
        /// </summary>
        /// <value>Boolean value denoting whether the vif is enabled for layer 3</value>
        /// <example>true</example>
        [Display(Name="Layer 3")]
        public bool IsLayer3 { get; private set; }

        /// <summary>
        /// The vlan tag number assigned to the vif
        /// </summary>
        /// <value>Integer denoting the vlan tag number assigned to the vif</value>
        /// <example>100</example>
        [Display(Name="Vlan Tag")]
        public int? VlanTag { get; private set; }

        /// <summary>
        /// The ID of the parent attachment
        /// </summary>
        /// <value>Integer value denoting the ID of the parent attachment</value>
        /// <example>1001</example>
        [Display(Name="Attachment ID")]
        public int? AttachmentId { get; private set; }

        /// <summary>
        /// The name of the vif role
        /// </summary>
        /// <value>String value denoting the name of a vif role</value>
        /// <example>PE-CE-SERVICE</example>
        [Required]
        [Display(Name = "Vif Role Name")]
        public string VifRoleName { get; private set; }

        /// <summary>
        /// The ID of the owning tenant
        /// </summary>
        /// <value>Integer value denoting the ID of the owning tenant</value>
        /// <example>991011</example>
        [Display(Name="Tenant ID")]
        public int? TenantId { get; private set; }

        /// <summary>
        /// THe routing instance to which the vif belongs
        /// </summary>
        /// <value>An instance of InfrastructureRoutingInstance</value>
        [Display(Name="Routing Instance")]
        public InfrastructureRoutingInstanceViewModel RoutingInstance { get; private set; }

        /// <summary>
        /// A list of vlan objects which comprise the vif
        /// </summary>
        /// <value>A list of vlan objects</value>
        [Display(Name="Vlans")]
        public List<VlanViewModel> Vlans { get; private set; }

        /// <summary>
        /// The contract bandwidth pool assigned to the vif.
        /// </summary>
        /// <value>A ContractBandwidthPool object</value>
        [Display(Name="Contract Bandwidth Pool")]
        public ContractBandwidthPoolViewModel ContractBandwidthPool { get; private set; }

        /// <summary>
        /// The maximum transmission unit supported by the vif
        /// </summary>
        /// <value>Am integer value denoting the MTU in bytes</value>
        /// <example>1500</example>
        [Display(Name = "MTU")]
        public int? Mtu { get; private set; }

    }
}

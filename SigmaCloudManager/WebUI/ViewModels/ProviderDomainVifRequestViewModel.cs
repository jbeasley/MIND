

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
    /// Model for requesting a vif which belongs to a tenant attachment to the provider domain
    /// </summary>
    public class ProviderDomainVifRequestViewModel
    {
        public ProviderDomainVifRequestViewModel()
        {
            // Instantiate a new instance of the routing instance request model. This is necessary in order to 
            // instantiate properties of the routing instance such as the list of BGP peers and handle user actions
            // in the web UI such as the removal of all BGP peers which belong to the routing instance.
            RoutingInstance = new RoutingInstanceRequestViewModel();
        }

        /// <summary>
        /// The ID of the tenant to which the vif belongs. Usually this is the same tenant as the owner of the attachment
        /// to which the vif belongs. However, it is also possible to assign a vif to a different tenant.
        /// </summary>
        /// <value>An integer denoting the ID of the tenant</value>
        /// <example>1001</example>
        [Required]
        public int? TenantId { get; set; }

        /// <summary>
        /// The ID of the parent attachment for which the new vif will be created.
        /// </summary>
        /// <value>Integer value denoting the ID of the attachment</value>
        /// <example>9001</example>
        [Required]   
        public int? AttachmentId { get; set; }

        /// <summary>
        /// The name of the attachment role related to the attachment for which the new vif will be created.
        /// </summary>
        /// <value>String value denoting the name of a attachment role</value>
        /// <example>PE-CE-UNTAGGED</example>
        [Required]
        public string AttachmentRoleName { get; set; }

        /// <summary>
        /// The name of an vif role which sets certain constrains on how the vif must be configured
        /// </summary>
        /// <value>String value denoting the name of a vif role</value>
        /// <example>PE-CE-SERVICE</example>
        [Required]
        [Display(Name = "VIF Role Name")]
        public string VifRoleName { get; set; }

        /// <summary>
        /// The requested vlan tag to be assigned to the vif. This property is optional. If a requested vlan tag is not specified
        /// then MIND will automatically allocate one.
        /// </summary>
        /// <value>An integer denoting the requested vlan tag</value>
        /// <example>100</example>
        [Display(Name = "Requested Vlan Tag")]
        [Range(2,4094)]
        public int? RequestedVlanTag { get; set; }

        /// <summary>
        /// If specified, the vif should be associated with an existing routing instance
        /// of the given name. The routing instance must belong to the same tenant as the owner of the new vif.
        /// If an existing routing instance is not specified then MIND will automatically create a new routing
        /// instance for the vif.
        /// </summary>
        /// <value>A string value of the name of an existing routing instance</value>
        /// <exanple>db7c48eaa9864cd0b3aa6af08c8370d6</exanple>
        [Display(Name = "Existing Routing Instance Name")]
        public string ExistingRoutingInstanceName { get; set; }

        /// <summary>
        /// Optional parameters for creating a routing instances to be associated with the new attachment.
        /// </summary>
        /// <value>An object of type RoutingInstanceRequest</value>
        public RoutingInstanceRequestViewModel RoutingInstance { get; set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        /// <example>100</example>
        [Display(Name="Contract Bandwidth (Mbps)")]
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// If specified, the vif should be associated with an existing contract bandwidth pool
        /// of the given name which is associated with another vif under the same attachment.
        /// </summary>
        /// <value>A string value of the name of an existing contract bandwidth pool</value>
        /// <exanple>db7c48eaa9864cd0b3aa6af08c8370d6</exanple>
        [Display(Name = "Existing Contract Bandwidth Pool Name")]
        public string ExistingContractBandwidthPoolName { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets received from the tenant domain should be trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the required trust state</value>
        [Display(Name="Trust Received Cos/Dscp")]
        public bool? TrustReceivedCosAndDscp { get; set; }

        /// <summary>
        /// A list of IPv4 addresses to be assigned to the vlans of the vif
        /// </summary>
        /// <value>A list of Ipv4AddressAndMask objects</value>
        public List<Ipv4AddressAndMaskViewModel> Ipv4Addresses { get; set; }

        /// <summary>
        /// Stage the vif ready for synchronisation with the network
        /// </summary>
        /// <value>Booelan denoting whether the vif should be staged.</value>
        /// <example>true</example>
        public bool? Stage { get; set; }

        /// <summary>
        /// Synchronise the vif with the network
        /// </summary>
        /// <value>Boolean denoting whether the vif should be synchronised with the network</value>
        /// <example>true</example>
        public bool? SyncToNetwork { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ContractBandwidthMbps != null)
            {
                if (!string.IsNullOrEmpty(ExistingContractBandwidthPoolName))
                {
                    yield return new ValidationResult(
                        "A contract bandwidth option cannot be selected concurrently with an existing contract bandwidth pool option.");
                }
            }
        }
    }
}

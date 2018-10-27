
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
using SCM.Models.RequestModels;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting a vif which belongs to a tenant attachment to the provider domain
    /// </summary>
    public class ProviderDomainVifRequest
    {
        /// <summary>
        /// The ID of the tenant to which the vif belongs. Usually this is the same tenant as the owner of the attachment
        /// to which the vif belongs. However, it is also possible to assign a vif to a different tenant.
        /// </summary>
        /// <value>An integer denoting the ID of the tenant</value>
        public Int32? TenantId { get; set; }

        /// <summary>
        /// The name of an vif role which sets certain constraints on how the vif must be configured
        /// </summary>
        /// <value>String value denoting the name of a vif role</value>
        public string VifRoleName { get; set; }

        /// <summary>
        /// The requested vlan tag to be assigned to the vif. This property is optional. If a requested vlan tag is not specified
        /// then MIND will automatically allocate one.
        /// </summary>
        /// <value>An integer denoting the requested vlan tag</value>
        public int? RequestedVlanTag { get; set; }

        /// <summary>
        /// If specified, the vif should be associated with an existing routing instance
        /// of the given name. The routing instance must belong to the same tenant as the owner of the new vif.
        /// If an existing routing instance is not specified then MIND will automatically create a new routing
        /// instance for the vif.
        /// </summary>
        /// <value>A string value of the name of an existing routing instance</value>
        public string ExistingRoutingInstanceName { get; set; }

        /// <summary>
        /// Optional parameters for creating a routing instances to be associated with the new vif
        /// </summary>
        /// <value>An object of type RoutingInstanceRequest</value>
        public RoutingInstanceRequest RoutingInstance { get; set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets received from the tenant domain should be trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the required trust state</value>
        public bool? TrustReceivedCosAndDscp { get; set; }

        /// <summary>
        /// If specified, the vif should be associated with an existing contract bandwidth pool
        /// of the given name which is associated with another vif under the same attachment.
        /// </summary>
        /// <value>A string value of the name of an existing contract bandwidth pool</value>
        public string ExistingContractBandwidthPoolName { get; set; }

        /// <summary>
        /// A list of IPv4 addresses to be assigned to the vlans of the vif
        /// </summary>
        /// <value>A list of Ipv4AddressAndMask objcets</value>
        public List<Ipv4AddressAndMask> Ipv4Addresses { get; set; }

    }
}

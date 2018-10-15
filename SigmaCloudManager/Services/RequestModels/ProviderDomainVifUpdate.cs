
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
    /// Model for updating an existing vif which belongs to a tenant attachment to the provider domain
    /// </summary>
    public class ProviderDomainVifUpdate
    {
        /// <summary>
        /// Determines if the updated vif should be associated with a new routing instance.
        /// </summary>
        /// <value>A boolean which when set to true indicates a new routing instance is required</value>
        public bool? CreateNewRoutingInstance { get; set; }

        /// <summary>
        /// If specified, the vif should be associated with an existing routing instance
        /// of the given name. The routing instance must belong to the same tenant as the owner of the vif.
        /// </summary>
        /// <value>A string value of the name of an existing routing instance</value>
        public string ExistingRoutingInstanceName { get; set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// If specified, the vif should be associated with an existing contract bandwidth pool
        /// of the given name which is associated with another vif under the same attachment.
        /// </summary>
        /// <value>A string value of the name of an existing contract bandwidth pool</value>
        public string ExistingContractBandwidthPoolName { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets received from the tenant domain should be trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the required trust state</value>
        public bool? TrustReceivedCosAndDscp { get; set; }

        /// <summary>
        /// A list of IPv4 addresses to be assigned to the vlans of the vif
        /// </summary>
        /// <value>A list of Ipv4AddressAndMask objcets</value>
        public List<Ipv4AddressAndMask> Ipv4Addresses { get; set; }

        /// <summary>
        /// Determines if the updated vif should use jumbo MTU
        /// </summary>
        /// <value>Boolean value denoting whether jumbo MTU should be enabled</value>
        public bool? UseJumboMtu { get; set; }
    }
}

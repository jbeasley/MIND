
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
    /// Model for updating an existing vif which belongs to a tenant domain attachment
    /// </summary>
    public class TenantDomainVifUpdate
    {
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
        /// Determines whether DSCP and COS markings of packets received from the provider domain should 
        /// be trusted by the tenant domain device.
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
        public bool? UseJumboMtu { get; set; }
    }
}

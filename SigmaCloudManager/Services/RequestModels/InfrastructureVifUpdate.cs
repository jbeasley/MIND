
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
    /// Model for updating an infrastructure vif
    /// </summary>
    public class InfrastructureVifUpdate
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
        /// If specified, the vif should be associated with an existing routing instance
        /// of the given name.
        /// If an existing routing instance is not specified then MIND will automatically create a new routing
        /// instance for the vif.
        /// </summary>
        /// <value>A string value of the name of an existing routing instance</value>
        public string ExistingRoutingInstanceName { get; set; }

        /// <summary>
        /// Determines if the updated vif should be associated with a new routing instance.
        /// </summary>
        /// <value>A boolean which when set to true indicates a new routing instance is required</value>
        public bool? CreateNewRoutingInstance { get; set; }

        /// <summary>
        /// A list of IPv4 addresses to be assigned to the interfaces of the vif
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

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
    /// Model for updating a vif which belongs to a tenant attachment to the provider domain
    /// </summary>
    public class ProviderDomainVifUpdateViewModel : IModifiableResource
    {
        /// <summary>
        /// If specified, the vif should be associated with an existing routing instance
        /// of the given name. The routing instance must belong to the same tenant as the owner of the vif.
        /// </summary>
        /// <value>A string value of the name of an existing routing instance</value>
        /// <exanple>db7c48eaa9864cd0b3aa6af08c8370d6</exanple>
        [Display(Name = "Existing Routing Instance Name")]
        public string ExistingRoutingInstanceName { get; set; }

        /// <summary>
        /// If specified, the vif should be associated with an existing contract bandwidth pool
        /// of the given name which is associated with another vif under the same attachment.
        /// </summary>
        /// <value>A string value of the name of an existing contract bandwidth pool</value>
        /// <exanple>db7c48eaa9864cd0b3aa6af08c8370d6</exanple>
        [Display(Name = "Existing Contract Bandwidth Pool Name")]
        public string ExistingContractBandwidthPoolName { get; set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        /// <example>100</example>
        [Display(Name="Contract Bandwidth Mbps")]
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets received from the tenant domain should be trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the required trust state</value>
        [Display(Name="Trust Received Cos/Dscp")]
        public bool? TrustReceivedCosAndDscp { get; set; }

        /// <summary>
        /// A list of IPv4 addresses to be assigned to the vlans of the vif
        /// </summary>
        /// <value>A list of Ipv4AddressAndMask objcets</value>
        public List<Ipv4AddressAndMaskViewModel> Ipv4Addresses { get; set; }

        /// <summary>
        /// Determines if the updated vif should use jumbo MTU
        /// </summary>
        /// <value>A boolean which when set to true indicates jumbo MTU is required</value>
        /// <example>true</example>
        [Display(Name = "Use Jumbo MTU")]
        public bool? UseJumboMtu { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Determines if the updated vif should be associated with a new routing instance. If the value of this property is true
        /// then the value of the ExistingRoutingInstanceName property must be null.
        /// </summary>
        /// <value>A boolean which when set to true indicates a new routing instance is required</value>
        /// <example>true</example>
        [Display(Name = "Create New Routing Instance")]
        public bool? CreateNewRoutingInstance { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CreateNewRoutingInstance.HasValue && CreateNewRoutingInstance.Value)
            {
                if (!string.IsNullOrEmpty(ExistingRoutingInstanceName))
                {
                    yield return new ValidationResult(
                        "The 'Create New Routing Instance' option cannot be used concurrently with the 'Existing Routing Instance Name' option.");
                }
            }
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

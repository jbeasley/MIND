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
    /// Model for updating a tenant attachment to the provider domain
    /// </summary>
    public class ProviderDomainAttachmentUpdateViewModel
    {
        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        /// <example>100</example>
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets received from the tenant domain should be trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the required trust state</value>
        /// <example>false</example>
        public bool? TrustReceivedCosAndDscp { get; set; }

        /// <summary>
        /// If specified, the updated attachment should be associated with an existing routing instance
        /// of the given name. If an existing routing instance name is specified then the 'CreateNewRoutingInstance' property must be
        /// false.
        /// </summary>
        /// <value>A string value of the name of an existing routing instance</value>
        /// <exanple>db7c48eaa9864cd0b3aa6af08c8370d6</exanple>
        public string ExistingRoutingInstanceName { get; set; }

        /// <summary>
        /// Determines if the updated attachment should be associated with a new routing instance. If the value of this property is true
        /// then the value of the ExistingRoutingInstanceName property must be null.
        /// </summary>
        /// <value>A boolean which when set to true indicates a new routing instance is required</value>
        /// <examople>true</examople>
        public bool? CreateNewRoutingInstance { get; set; }

        /// <summary>
        /// Determines if the updated attachment should use jumbo MTU
        /// </summary>
        /// <value>A boolean which when set to true indicates jumbo MTU is required</value>
        /// <example>true</example>
        public bool? UseJumboMtu { get; set; }

        /// <summary>
        /// The minimum number of active links in a bundle attachment. A value for this property may only be 
        /// specified for bundle attachments.
        /// </summary>
        /// <value>A value which specifies the minimum links in the bundle</value>
        /// <example>2</example>
        [Range(1, 8)]
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in a bundle attachment. A value for this property may only be 
        /// specified for bundle attachments.
        /// </summary>
        /// <value>A value which specifies the maximum links in the bundle</value>
        /// <example>true</example>
        [Range(1, 8)]
        public int? BundleMaxLinks { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CreateNewRoutingInstance.HasValue && CreateNewRoutingInstance.Value)
            {
                if (!string.IsNullOrEmpty(ExistingRoutingInstanceName))
                {
                    yield return new ValidationResult(
                        "The 'CreateNewRoutingInstance' option cannot be used concurrently with the 'ExistingRoutingInstanceName' option." +
                        "Either remove the 'ExistingRoutingInstanceName' property or remove the 'CreateNewRoutingInstance' property from " +
                        "the request.");
                }
            }
        }
    }
}

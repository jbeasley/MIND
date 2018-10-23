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
    public class ProviderDomainAttachmentUpdateViewModel : IModifiableResource
    {
        /// <summary>
        /// ID of the attachment
        /// </summary>
        /// <value>Integer value denoting the ID of the attachment</value>
        /// <example>6001</example>
        public int? AttachmentId { get; set; }

        /// <summary>
        /// The name of the attachment
        /// </summary>
        /// <value>string value denoting the name of the attachment</value>
        /// <example>TenGigabitEthernet0/0</example>
        public string Name { get; private set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        /// <example>100</example>
        [Display(Name = "Contract Bandwidth (Mbps)")]
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets received from the tenant domain should be trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the required trust state</value>
        /// <example>false</example>
        [Display(Name = "Trust Received CoS/DSCP")]
        public bool TrustReceivedCosAndDscp { get; set; }

        /// <summary>
        /// If specified, the updated attachment should be associated with an existing routing instance
        /// of the given name. If an existing routing instance name is specified then the 'CreateNewRoutingInstance' property must be
        /// false.
        /// </summary>
        /// <value>A string value of the name of an existing routing instance</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        [Display(Name = "Existing Routing Instance Name")]
        public string ExistingRoutingInstanceName { get; set; }

        /// <summary>
        /// Determines if the updated attachment should be associated with a new routing instance. If the value of this property is true
        /// then the value of the ExistingRoutingInstanceName property must be null.
        /// </summary>
        /// <value>A boolean which when set to true indicates a new routing instance is required</value>
        /// <example>true</example>
        [Display(Name = "Create a new Routing Instance")]
        public bool CreateNewRoutingInstance { get; set; }

        /// <summary>
        /// Determines if the updated attachment should use jumbo MTU
        /// </summary>
        /// <value>A boolean which when set to true indicates jumbo MTU is required</value>
        /// <example>true</example>
        [Display(Name = "Use Jumbo MTU")]
        public bool UseJumboMtu { get; set; }

        /// <summary>
        /// Denotes whether the attachment is configured as a bundle 
        /// </summary>
        /// <value>Boolean value denoting whether the attachment is configured as a bundle</value>
        /// <example>false</example>
        public bool IsBundle { get; set; }

        /// <summary>
        /// The minimum number of active links in a bundle attachment. A value for this property may only be 
        /// specified for bundle attachments.
        /// </summary>
        /// <value>A value which specifies the minimum links in the bundle</value>
        /// <example>2</example>
        [Range(1, 8)]
        [Display(Name = "Bundle Min Links")]
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in a bundle attachment. A value for this property may only be 
        /// specified for bundle attachments.
        /// </summary>
        /// <value>A value which specifies the maximum links in the bundle</value>
        /// <example>true</example>
        [Range(1, 8)]
        [Display(Name = "Bundle Max Links")]
        public int? BundleMaxLinks { get; set; }

        /// <summary>
        /// A list of IPv4 addresses to be assigned to the interfaces of the attachment.
        /// </summary>
        /// <value>A list of Ipv4AddressAndMask objcets</value>
        public List<Ipv4AddressAndMaskViewModel> Ipv4Addresses { get; set; }

        /// <summary>
        /// The description of the attachment
        /// </summary>
        /// <value>String value denoting the description</value>
        /// <example>A description of the attachment</example>
        public string Description { get; set; }

        /// <summary>
        /// Notes for the attachment
        /// </summary>
        /// <value>String value for the notes for the attachment</value>
        /// <example>Some notes about the attachment</example>
        public string Notes { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CreateNewRoutingInstance)
            {
                if (!string.IsNullOrEmpty(ExistingRoutingInstanceName))
                {
                    yield return new ValidationResult(
                        "The 'Create New Routing Instance' option cannot be used concurrently with the 'Existing Routing Instance Name' option.");
                }
            }
        }
    }
}

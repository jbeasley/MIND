/*
 * MIND API
 *
 * This is the Master Inventory Network Database (MIND) API. MIND provides automated allocation of technical attributes needed to create IP and Ethernet VPNs on the global Sigma network. MIND supports the 'Nova' services specfication which defines the collection of connectivity services supported by ENT. Go to https://thehub.thomsonreuters.com/docs/DOC-2193014 to learn more.
 *
 * OpenAPI spec version: 1.0.0
 * Contact: jonathan.beasley@thomsonreuters.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

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

namespace Mind.Api.Models
{ 
    /// <summary>
    /// Model for updating a tenant attachment to the provider domain
    /// </summary>
    [DataContract]
    public partial class ProviderDomainAttachmentUpdate : IEquatable<ProviderDomainAttachmentUpdate>, IValidatableObject
    {
        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        /// <example>100</example>
        [DataMember(Name = "contractBandwidthMbps")]
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets received from the tenant domain should be trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the required trust state</value>
        /// <example>false</example>
        [DataMember(Name = "trustReceivedCosAndDscp")]
        public bool? TrustReceivedCosAndDscp { get; set; }

        /// <summary>
        /// If specified, the updated attachment should be associated with an existing routing instance
        /// of the given name. If an existing routing instance name is specified then the 'CreateNewRoutingInstance' property must be
        /// false.
        /// </summary>
        /// <value>A string value of the name of an existing routing instance</value>
        /// <exanple>db7c48eaa9864cd0b3aa6af08c8370d6</exanple>
        [DataMember(Name = "existingRoutingInstanceName")]
        public string ExistingRoutingInstanceName { get; set; }

        /// <summary>
        /// Determines if the updated attachment should be associated with a new routing instance. If the value of this property is true
        /// then the value of the ExistingRoutingInstanceName property must be null.
        /// </summary>
        /// <value>A boolean which when set to true indicates a new routing instance is required</value>
        /// <examople>true</examople>
        [DataMember(Name = "createNewRoutingInstance")]
        public bool? CreateNewRoutingInstance { get; set; }

        /// <summary>
        /// Determines if the updated attachment should use jumbo MTU
        /// </summary>
        /// <value>A boolean which when set to true indicates jumbo MTU is required</value>
        /// <example>true</example>
        [DataMember(Name = "useJumboMtu")]
        public bool? UseJumboMtu { get; set; }

        /// <summary>
        /// The minimum number of active links in a bundle attachment. A value for this property may only be 
        /// specified for bundle attachments.
        /// </summary>
        /// <value>A value which specifies the minimum links in the bundle</value>
        /// <example>2</example>
        [DataMember(Name = "bundleMinLinks")]
        [Range(1, 8)]
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in a bundle attachment. A value for this property may only be 
        /// specified for bundle attachments.
        /// </summary>
        /// <value>A value which specifies the maximum links in the bundle</value>
        /// <example>true</example>
        [DataMember(Name = "bundleMaxLinks")]
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

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProviderDomainAttachmentUpdate {\n");
            sb.Append("  TrustReceivedCosAndDscp: ").Append(TrustReceivedCosAndDscp).Append("\n");
            sb.Append("  ContractBandwidthMbps: ").Append(ContractBandwidthMbps).Append("\n");
            sb.Append("  ExistingRoutingInstanceName: ").Append(ExistingRoutingInstanceName).Append("\n");
            sb.Append("  CreateNewRoutingInstance: ").Append(CreateNewRoutingInstance).Append("\n");
            sb.Append("  UseJumboMtu: ").Append(UseJumboMtu).Append("\n");
            sb.Append("  BundleMinLinks: ").Append(BundleMinLinks).Append("\n");
            sb.Append("  BundleMaxLinks: ").Append(BundleMaxLinks).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ProviderDomainAttachmentUpdate)obj);
        }

        /// <summary>
        /// Returns true if ProviderDomainAttachmentUpdate instances are equal
        /// </summary>
        /// <param name="other">Instance of ProviderDomainAttachmentUpdate to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProviderDomainAttachmentUpdate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    TrustReceivedCosAndDscp == other.TrustReceivedCosAndDscp ||
                    TrustReceivedCosAndDscp != null &&
                    TrustReceivedCosAndDscp.Equals(other.TrustReceivedCosAndDscp)
                ) &&
                (
                    ContractBandwidthMbps == other.ContractBandwidthMbps ||
                    ContractBandwidthMbps != null &&
                    ContractBandwidthMbps.Equals(other.ContractBandwidthMbps)
                ) &&
                (
                    ExistingRoutingInstanceName == other.ExistingRoutingInstanceName ||
                    ExistingRoutingInstanceName != null &&
                    ExistingRoutingInstanceName.Equals(other.ExistingRoutingInstanceName)
                ) &&
                (
                    CreateNewRoutingInstance == other.CreateNewRoutingInstance ||
                    CreateNewRoutingInstance != null &&
                    CreateNewRoutingInstance.Equals(other.CreateNewRoutingInstance)
                ) &&
                (
                    UseJumboMtu == other.UseJumboMtu ||
                    UseJumboMtu != null &&
                    UseJumboMtu.Equals(other.UseJumboMtu)
                ) &&
                (
                    BundleMinLinks == other.BundleMinLinks ||
                    BundleMinLinks != null &&
                    BundleMinLinks.Equals(other.BundleMinLinks)
                ) &&
                (
                    BundleMaxLinks == other.BundleMaxLinks ||
                    BundleMaxLinks != null &&
                    BundleMaxLinks.Equals(other.BundleMaxLinks)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (TrustReceivedCosAndDscp != null)
                    hashCode = hashCode * 59 + TrustReceivedCosAndDscp.GetHashCode();
                    if (ContractBandwidthMbps != null)
                    hashCode = hashCode * 59 + ContractBandwidthMbps.GetHashCode();
                    if (ExistingRoutingInstanceName != null)
                    hashCode = hashCode * 59 + ExistingRoutingInstanceName.GetHashCode();
                    if (CreateNewRoutingInstance != null)
                    hashCode = hashCode * 59 + CreateNewRoutingInstance.GetHashCode();
                    if (UseJumboMtu != null)
                    hashCode = hashCode * 59 + UseJumboMtu.GetHashCode();
                    if (BundleMinLinks != null)
                    hashCode = hashCode * 59 + BundleMinLinks.GetHashCode();
                    if (BundleMaxLinks != null)
                    hashCode = hashCode * 59 + BundleMaxLinks.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(ProviderDomainAttachmentUpdate left, ProviderDomainAttachmentUpdate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProviderDomainAttachmentUpdate left, ProviderDomainAttachmentUpdate right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

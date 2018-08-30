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
    /// Model for requesting tenant attachment to the provider domain
    /// </summary>
    [DataContract]
    public partial class ProviderDomainAttachmentRequest : IEquatable<ProviderDomainAttachmentRequest>
    { 
        /// <summary>
        /// Determines if a bundle style of attachment is required
        /// </summary>
        /// <value>Boolean value which denotes if a bundle style of attachment is required</value>
        /// <example>true</example>
        [DataMember(Name="bundleRequired")]
        public bool? BundleRequired { get; set; }

        /// <summary>
        /// The minimum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the minimum links in the bundle</value>
        /// <example>2</example>
        [DataMember(Name = "bundleMinLinks")]
        [Range(1,8)]
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the maximum links in the bundle</value>
        /// <example>2</example>
        [DataMember(Name = "bundleMaxLinks")]
        [Range(1, 8)]
        public int? BundleMaxLinks { get; set; }

        /// <summary>
        /// Determines if a multi port style of attachment is required
        /// </summary>
        /// <value>Boolean value which denotes if a multi port style of attachment is required</value>
        [DataMember(Name="multiportRequired")]
        public bool? MultiportRequired { get; set; }

        /// <summary>
        /// The name of a provider network location within which the new attachment will be provisioned
        /// </summary>
        /// <value>String value denoting the name of a provider network location</value>
        /// <example>UK2</example>
        [Required]
        [DataMember(Name="locationName")]
        public string LocationName { get; set; }

        /// <summary>
        /// The name of a port pool which ports for the new attachment will be allocated from
        /// </summary>
        /// <value>String value denoting the name of a port pool</value>
        /// <example>General</example>
        [Required]
        [DataMember(Name = "portPoolName")]
        public string PortPoolName { get; set; }

        /// <summary>
        /// The name of an attachment role which sets certain constrains on how the attachment must be configuted
        /// </summary>
        /// <value>String value denoting the name of an attachment role</value>
        /// <example>PE-CE-UNTAGGED</example>
        [Required]
        [DataMember(Name = "attachmentRoleName")]
        public string AttachmentRoleName { get; set; }

        /// <summary>
        /// Enumeration of provider network plane options
        /// </summary>
        public enum PlaneNameEnum
        { 
            /// <summary>
            /// Enum member for the Red plane
            /// </summary>
            [EnumMember(Value = "Red")]
            Red = 1,
            
            /// <summary>
            /// Enum member for the Blue plane
            /// </summary>
            [EnumMember(Value = "Blue")]
            Blue = 2
        }

        /// <summary>
        /// Optional name of the provider network plane within which the attachment will be provisioned
        /// </summary>
        /// <value>A member of the PlaneNameEnum enumeration</value>
        /// <example>Red</example>
        [DataMember(Name="planeName")]
        public PlaneNameEnum? PlaneName { get; set; }

        /// <summary>
        /// The required bandwidth of the attachment in Gbps
        /// </summary>
        /// <value>Integer value denoting the required attachment bandwidth in Gbps</value>
        /// <example>10</example>
        [DataMember(Name="attachmentBandwidthGbps")]
        public int? AttachmentBandwidthGbps { get; set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        /// <example>100</example>
        [DataMember(Name="contractBandwidthMbps")]
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets received from the tenant domain should be trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the required trust state</value>
        [DataMember(Name="trustReceivedCosAndDscp")]
        public bool? TrustReceivedCosAndDscp { get; set; }

        /// <summary>
        /// A list of IPv4 addresses to be assigned to the interfaces of the attachment.
        /// </summary>
        /// <value>A list of Ipv4AddressAndMask objcets</value>
        [DataMember(Name="ipv4Addresses")]
        public List<Ipv4AddressAndMask> Ipv4Addresses { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProviderDomainAtachmentRequest {\n");
            sb.Append("  BundleRequired: ").Append(BundleRequired).Append("\n");
            sb.Append("  BundleMinLinks: ").Append(BundleMinLinks).Append("\n");
            sb.Append("  BundleMaxLinks: ").Append(BundleMaxLinks).Append("\n");
            sb.Append("  MultiportRequired: ").Append(MultiportRequired).Append("\n");
            sb.Append("  LocationName: ").Append(LocationName).Append("\n");
            sb.Append("  PortPoolName: ").Append(PortPoolName).Append("\n");
            sb.Append("  AttachmentRoleName: ").Append(AttachmentRoleName).Append("\n");
            sb.Append("  PlaneName: ").Append(PlaneName).Append("\n");
            sb.Append("  AttachmentBandwidthGbps: ").Append(AttachmentBandwidthGbps).Append("\n");
            sb.Append("  ContractBandwidthMbps: ").Append(ContractBandwidthMbps).Append("\n");
            sb.Append("  TrustReceivedCosAndDscp: ").Append(TrustReceivedCosAndDscp).Append("\n");
            sb.Append("  Ipv4Addresses: ").Append(Ipv4Addresses).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ProviderDomainAttachmentRequest)obj);
        }

        /// <summary>
        /// Returns true if ProviderDomainAttachmentRequest instances are equal
        /// </summary>
        /// <param name="other">Instance of ProviderDomainAttachmentRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProviderDomainAttachmentRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    BundleRequired == other.BundleRequired ||
                    BundleRequired != null &&
                    BundleRequired.Equals(other.BundleRequired)
                ) &&
                (   BundleMinLinks == other.BundleMinLinks ||
                    BundleMinLinks != null &&
                    BundleMinLinks.Equals(other.BundleMinLinks)
                ) &&
                (   BundleMaxLinks == other.BundleMaxLinks ||
                    BundleMaxLinks != null &&
                    BundleMaxLinks.Equals(other.BundleMaxLinks)
                ) &&
                (
                    MultiportRequired == other.MultiportRequired ||
                    MultiportRequired != null &&
                    MultiportRequired.Equals(other.MultiportRequired)
                ) &&
                (
                    LocationName == other.LocationName ||
                    LocationName != null &&
                    LocationName.Equals(other.LocationName)
                ) &&
                (
                    PortPoolName == other.PortPoolName ||
                    PortPoolName != null &&
                    PortPoolName.Equals(other.PortPoolName)
                ) &&
                (
                    AttachmentRoleName == other.AttachmentRoleName ||
                    AttachmentRoleName != null &&
                    AttachmentRoleName.Equals(other.AttachmentRoleName)
                ) &&
                (
                    PlaneName == other.PlaneName ||
                    PlaneName != null &&
                    PlaneName.Equals(other.PlaneName)
                ) &&
                (
                    AttachmentBandwidthGbps == other.AttachmentBandwidthGbps ||
                    AttachmentBandwidthGbps != null &&
                    AttachmentBandwidthGbps.Equals(other.AttachmentBandwidthGbps)
                ) &&
                (
                    ContractBandwidthMbps == other.ContractBandwidthMbps ||
                    ContractBandwidthMbps != null &&
                    ContractBandwidthMbps.Equals(other.ContractBandwidthMbps)
                ) &&
                (
                    TrustReceivedCosAndDscp == other.TrustReceivedCosAndDscp ||
                    TrustReceivedCosAndDscp != null &&
                    TrustReceivedCosAndDscp.Equals(other.TrustReceivedCosAndDscp)
                ) &&
                (
                    Ipv4Addresses == other.Ipv4Addresses ||
                    Ipv4Addresses != null &&
                    Ipv4Addresses.Equals(other.Ipv4Addresses)
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
                    if (BundleRequired != null)
                    hashCode = hashCode * 59 + BundleRequired.GetHashCode();
                    if (BundleMinLinks != null)
                    hashCode = hashCode * 59 + BundleMinLinks.GetHashCode();
                    if (BundleMaxLinks != null)
                    hashCode = hashCode * 59 + BundleMaxLinks.GetHashCode();
                    if (MultiportRequired != null)
                    hashCode = hashCode * 59 + MultiportRequired.GetHashCode();
                    if (LocationName != null)
                    hashCode = hashCode * 59 + LocationName.GetHashCode();
                    if (PortPoolName != null)
                    hashCode = hashCode * 59 + PortPoolName.GetHashCode();
                    if (AttachmentRoleName != null)
                    hashCode = hashCode * 59 + AttachmentRoleName.GetHashCode();
                    if (PlaneName != null)
                    hashCode = hashCode * 59 + PlaneName.GetHashCode();
                    if (AttachmentBandwidthGbps != null)
                    hashCode = hashCode * 59 + AttachmentBandwidthGbps.GetHashCode();
                    if (ContractBandwidthMbps != null)
                    hashCode = hashCode * 59 + ContractBandwidthMbps.GetHashCode();
                    if (TrustReceivedCosAndDscp != null)
                    hashCode = hashCode * 59 + TrustReceivedCosAndDscp.GetHashCode();
                    if (Ipv4Addresses != null)
                    hashCode = hashCode * 59 + Ipv4Addresses.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ProviderDomainAttachmentRequest left, ProviderDomainAttachmentRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProviderDomainAttachmentRequest left, ProviderDomainAttachmentRequest right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

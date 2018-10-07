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
    /// Model for requesting an infrastructure attachment
    /// </summary>
    [DataContract]
    public partial class InfrastructureAttachmentRequest : IEquatable<InfrastructureAttachmentRequest>
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
        /// The name of a port pool from which ports for the new attachment will be allocated from
        /// </summary>
        /// <value>String value denoting the name of a port pool</value>
        /// <example>Core</example>
        [Required]
        [DataMember(Name = "portPoolName")]
        public string PortPoolName { get; set; }

        /// <summary>
        /// The name of an attachment role which sets certain constraints on how the attachment must be configuted
        /// </summary>
        /// <value>String value denoting the name of an attachment role</value>
        /// <example>PE-P</example>
        [Required]
        [DataMember(Name = "attachmentRoleName")]
        public string AttachmentRoleName { get; set; }

        /// <summary>
        /// The required bandwidth of the attachment in Gbps
        /// </summary>
        /// <value>Integer value denoting the required attachment bandwidth in Gbps</value>
        /// <example>10</example>
        [DataMember(Name="attachmentBandwidthGbps")]
        [Required]
        public int? AttachmentBandwidthGbps { get; set; }

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
            sb.Append("class InfrastructureAtachmentRequest {\n");
            sb.Append("  BundleRequired: ").Append(BundleRequired).Append("\n");
            sb.Append("  BundleMinLinks: ").Append(BundleMinLinks).Append("\n");
            sb.Append("  BundleMaxLinks: ").Append(BundleMaxLinks).Append("\n");
            sb.Append("  PortPoolName: ").Append(PortPoolName).Append("\n");
            sb.Append("  AttachmentRoleName: ").Append(AttachmentRoleName).Append("\n");
            sb.Append("  AttachmentBandwidthGbps: ").Append(AttachmentBandwidthGbps).Append("\n");
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
            return obj.GetType() == GetType() && Equals((InfrastructureAttachmentRequest)obj);
        }

        /// <summary>
        /// Returns true if InfrastructureAttachmentRequest instances are equal
        /// </summary>
        /// <param name="other">Instance of InfrastructureAttachmentRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InfrastructureAttachmentRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    BundleRequired == other.BundleRequired ||
                    BundleRequired != null &&
                    BundleRequired.Equals(other.BundleRequired)
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
                    AttachmentBandwidthGbps == other.AttachmentBandwidthGbps ||
                    AttachmentBandwidthGbps != null &&
                    AttachmentBandwidthGbps.Equals(other.AttachmentBandwidthGbps)
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
                    if (PortPoolName != null)
                    hashCode = hashCode * 59 + PortPoolName.GetHashCode();
                    if (AttachmentRoleName != null)
                    hashCode = hashCode * 59 + AttachmentRoleName.GetHashCode();
                    if (AttachmentBandwidthGbps != null)
                    hashCode = hashCode * 59 + AttachmentBandwidthGbps.GetHashCode();
                    if (Ipv4Addresses != null)
                    hashCode = hashCode * 59 + Ipv4Addresses.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(InfrastructureAttachmentRequest left, InfrastructureAttachmentRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InfrastructureAttachmentRequest left, InfrastructureAttachmentRequest right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

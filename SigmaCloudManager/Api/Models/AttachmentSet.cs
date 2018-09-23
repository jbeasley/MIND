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
    /// Model of an attachment set
    /// </summary>
    [DataContract]
    public partial class AttachmentSet : IEquatable<AttachmentSet>
    { 
        /// <summary>
        /// ID of the attachment set
        /// </summary>
        /// <value>Integer value for the ID of the attachment set</value>
        /// <example>11001</example>
        [DataMember(Name="attachmentSetId")]
        public int AttachmentSetID { get; private set; }

        /// <summary>
        /// MIND System-generated name of the attachment set
        /// </summary>
        /// <value>String value for the name of the attachment set</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        [DataMember(Name="name")]
        public string Name { get; private set; }

        /// <summary>
        /// The geographic region within which the attachment set operates.
        /// </summary>
        /// <value>String value denoting the geographic region within which the attachment set operates</value>
        /// <example>EMEA</example>
        [DataMember(Name="region")]
        public string Region { get; private set; }

        /// <summary>
        /// The geographic sub-region within which the attachment set operates
        /// </summary>
        /// <value>String value denoting the geographic sub-region within which the attachment set operates</value>
        /// <example>UK</example>
        [DataMember(Name="subRegion")]
        public string SubRegion { get; private set; }

        /// <summary>
        /// Denotes the attachment redundancy level supported by the attachment set
        /// </summary>
        /// <value>String value denoting the attachment redundancy level</value>
        /// <example>Silver</example>
        [DataMember(Name="attachmentRedundancy")]
        public string AttachmentRedundancy { get; private set; }

        /// <summary>
        /// Denotes the multicast vpn domain type of the attachment set
        /// </summary>
        /// <value>String value denoting the multicast vpn domain type</value>
        /// <example>Sender-Only</example>
        [DataMember(Name = "multicastVpnDomainType")]
        public string MulticastVpnDomainType { get; private set; }

        /// <summary>
        /// Denotees whether the attachment set is enabled for layer 3
        /// </summary>
        /// <value>Boolean value denoting if the attachment set is enabled for layer 3</value>
        /// <example>true</example>
        [DataMember(Name="isLayer3")]
        public bool? IsLayer3 { get; private set; }

        /// <summary>
        /// The list of routing instances which belong to the attachment Set
        /// </summary>
        /// <value>A list of AttachmentSetRoutingInstance objects</value>
        [DataMember(Name="attachmentSetRoutingInstances")]
        public List<AttachmentSetRoutingInstance> AttachmentSetRoutingInstances { get; private set; }

        /// <summary>
        /// A list of tenant IP network associations with the bgp inbound policy of the attachment set
        /// </summary>
        /// <value>A list of vpn tenant IP network in objects</value>
        [DataMember(Name = "bgpIpNetworkInboundPolicy")]
        public List<VpnTenantIpNetworkIn> BgpIpNetworkInboundPolicy { get; private set; }

        /// <summary>
        /// A list of tenant IP network static route associations with the attachment set
        /// </summary>
        /// <value>A list of vpn tenant IP network routing instance static route objects</value>
        [DataMember(Name = "staticRoutes")]
        public List<VpnTenantIpNetworkRoutingInstanceStaticRoute> StaticRoutes { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AttachmentSet {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Region: ").Append(Region).Append("\n");
            sb.Append("  SubRegion: ").Append(SubRegion).Append("\n");
            sb.Append("  AttachmentRedundancy: ").Append(AttachmentRedundancy).Append("\n");
            sb.Append("  IsLayer3: ").Append(IsLayer3).Append("\n");
            sb.Append("  BgpIpNetworkInboundPolicy: ").Append(BgpIpNetworkInboundPolicy).Append("\n");
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
            return obj.GetType() == GetType() && Equals((AttachmentSet)obj);
        }

        /// <summary>
        /// Returns true if AttachmentSet instances are equal
        /// </summary>
        /// <param name="other">Instance of AttachmentSet to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AttachmentSet other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) &&
                (
                    Region == other.Region ||
                    Region != null &&
                    Region.Equals(other.Region)
                ) &&
                (
                    SubRegion == other.SubRegion ||
                    SubRegion != null &&
                    SubRegion.Equals(other.SubRegion)
                ) &&
                (
                    AttachmentRedundancy == other.AttachmentRedundancy ||
                    AttachmentRedundancy != null &&
                    AttachmentRedundancy.Equals(other.AttachmentRedundancy)
                ) &&
                (
                    IsLayer3 == other.IsLayer3 ||
                    IsLayer3 != null &&
                    IsLayer3.Equals(other.IsLayer3)
                ) &&
                (
                    BgpIpNetworkInboundPolicy == other.BgpIpNetworkInboundPolicy ||
                    BgpIpNetworkInboundPolicy != null &&
                    BgpIpNetworkInboundPolicy.Equals(other.BgpIpNetworkInboundPolicy)
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
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (Region != null)
                    hashCode = hashCode * 59 + Region.GetHashCode();
                    if (SubRegion != null)
                    hashCode = hashCode * 59 + SubRegion.GetHashCode();
                    if (AttachmentRedundancy != null)
                    hashCode = hashCode * 59 + AttachmentRedundancy.GetHashCode();
                    if (IsLayer3 != null)
                    hashCode = hashCode * 59 + IsLayer3.GetHashCode();
                    if (BgpIpNetworkInboundPolicy != null)
                    hashCode = hashCode * 59 + BgpIpNetworkInboundPolicy.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(AttachmentSet left, AttachmentSet right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AttachmentSet left, AttachmentSet right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

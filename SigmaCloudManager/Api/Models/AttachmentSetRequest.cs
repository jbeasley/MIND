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
    /// 
    /// </summary>
    [DataContract]
    public partial class AttachmentSetRequest : IEquatable<AttachmentSetRequest>
    {
        /// <summary>
        /// The geographic region within which the attachment set operates
        /// </summary>
        /// <value>The geographic region within which the attachment set operates</value>
        public enum RegionEnum
        {
            /// <summary>
            /// Enum for EMEA
            /// </summary>
            [EnumMember(Value = "EMEA")]
            EMEA = 1,

            /// <summary>
            /// Enum for ASIAPAC
            /// </summary>
            [EnumMember(Value = "ASIAPAC")]
            ASIAPAC = 2,

            /// <summary>
            /// Enum for AMERS
            /// </summary>
            [EnumMember(Value = "AMERS")]
            AMERS = 3
        }

        /// <summary>
        /// The geographic region within which the attachment set operates
        /// </summary>
        /// <value>An enum member denoting the region within which the attachment set operates</value>
        [DataMember(Name = "region")]
        public RegionEnum? Region { get; set; }

        /// <summary>
        /// The sub-region within which the attachment set operates
        /// </summary>
        /// <value>A string value for the subregion within which the attachment set operates</value>
        [DataMember(Name="subRegion")]
        public string SubRegion { get; set; }

        /// <summary>
        /// Determines the type of attachment redundancy supported by the attachment set
        /// </summary>
        /// <value>An enumerated list of attachment redundancy options</value>
        public enum AttachmentRedundancyEnum
        { 
            /// <summary>
            /// Enum for Bronze
            /// </summary>
            [EnumMember(Value = "Bronze")]
            Bronze = 1,
            
            /// <summary>
            /// Enum for Silver
            /// </summary>
            [EnumMember(Value = "Silver")]
            Silver = 2,
            
            /// <summary>
            /// Enum for Gold
            /// </summary>
            [EnumMember(Value = "Gold")]
            Gold = 3,
            
            /// <summary>
            /// Enum for Custom
            /// </summary>
            [EnumMember(Value = "Custom")]
            Custom = 4
        }

        /// <summary>
        /// Determines the type of attachment redundancy supported by the attachment set
        /// </summary>
        /// <value>An enum member for the attachment redundancy supported by the attachment set</value>
        [DataMember(Name="attachmentRedundancy")]
        public AttachmentRedundancyEnum? AttachmentRedundancy { get; set; }

        /// <summary>
        /// Determines if the attachment set should be enabled for layer 3
        /// </summary>
        /// <value>Boolean denoting the layer 3 enablement state</value>
        [DataMember(Name="isLayer3")]
        public bool? IsLayer3 { get; set; }

        /// <summary>
        /// Requests for routing instances to be added to the attachment set
        /// </summary>
        /// <value>A list of RoutingInstanceForAttachmentSetRequest objects</value>
        [DataMember(Name="routingInstances")]
        public List<RoutingInstanceForAttachmentSetRequest> RoutingInstances { get; set; }

        /// <summary>
        /// Determines the type of multicast domain supported by the attachment set
        /// </summary>
        /// <value>An enumerated list of multicast domain options</value>
        public enum MulticastVpnDomainTypeEnum
        {
            /// <summary>
            /// Enum for Sender-Only
            /// </summary>
            [EnumMember(Value = "Sender-Only")]
            SenderOnly = 1,

            /// <summary>
            /// Enum for Receiver-Only
            /// </summary>
            [EnumMember(Value = "Receiver-Only")]
            ReceiverOnly = 2,

            /// <summary>
            /// Enum for Sender-and-Receiver
            /// </summary>
            [EnumMember(Value = "Sender-and-Receiver")]
            SenderAndReceiver = 3
        }

        /// <summary>
        /// Determines the type of multicast domain supported by the attachment set
        /// </summary>
        /// <value>An enum member for the multicast domain supported by the attachment set</value>
        [DataMember(Name = "multicastVpnDomainType")]
        public MulticastVpnDomainTypeEnum? MulticastVpnDomainType { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AttachmentSetRequest {\n");
            sb.Append("  Region: ").Append(Region).Append("\n");
            sb.Append("  SubRegion: ").Append(SubRegion).Append("\n");
            sb.Append("  AttachmentRedundancy: ").Append(AttachmentRedundancy).Append("\n");
            sb.Append("  IsLayer3: ").Append(IsLayer3).Append("\n");
            sb.Append("  RoutingInstances: ").Append(RoutingInstances).Append("\n");
            sb.Append("  MulticastVpnDomainType: ").Append(MulticastVpnDomainType).Append("\n");
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
            return obj.GetType() == GetType() && Equals((AttachmentSetRequest)obj);
        }

        /// <summary>
        /// Returns true if AttachmentSetRequest instances are equal
        /// </summary>
        /// <param name="other">Instance of AttachmentSetRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AttachmentSetRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
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
                    RoutingInstances == other.RoutingInstances ||
                    RoutingInstances != null &&
                    RoutingInstances.SequenceEqual(other.RoutingInstances)
                ) &&
                (
                    MulticastVpnDomainType == other.MulticastVpnDomainType ||
                    MulticastVpnDomainType != null &&
                    MulticastVpnDomainType.Equals(other.MulticastVpnDomainType)
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
                    if (Region != null)
                    hashCode = hashCode * 59 + Region.GetHashCode();
                    if (SubRegion != null)
                    hashCode = hashCode * 59 + SubRegion.GetHashCode();
                    if (AttachmentRedundancy != null)
                    hashCode = hashCode * 59 + AttachmentRedundancy.GetHashCode();
                    if (IsLayer3 != null)
                    hashCode = hashCode * 59 + IsLayer3.GetHashCode();
                    if (RoutingInstances != null)
                    hashCode = hashCode * 59 + RoutingInstances.GetHashCode();
                    if (MulticastVpnDomainType != null)
                    hashCode = hashCode * 59 + MulticastVpnDomainType.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(AttachmentSetRequest left, AttachmentSetRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AttachmentSetRequest left, AttachmentSetRequest right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

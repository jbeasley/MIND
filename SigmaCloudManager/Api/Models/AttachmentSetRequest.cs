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
    /// Model for requesting a new attachment set
    /// </summary>
    [DataContract]
    public partial class AttachmentSetRequest : IEquatable<AttachmentSetRequest>
    {
        /// <summary>
        /// The major geographic region within which the attachment set operates
        /// </summary>
        /// <value>An enum member denoting the region within which the attachment set operates</value>
        /// <example>EMEA</example>
        [DataMember(Name = "region")]
        public RegionEnum? Region { get; set; }

        /// <summary>
        /// The geographic sub-region within which the attachment set operates
        /// </summary>
        /// <value>A string value denoting the subregion within which the attachment set operates</value>
        /// <example>UK</example>
        [DataMember(Name="subRegion")]
        public string SubRegion { get; set; }

        /// <summary>
        /// Determines the attachment redundancy level supported by the attachment set
        /// </summary>
        /// <value>An enum member for the attachment redundancy supported by the attachment set</value>
        /// <example>Silver</example>
        [DataMember(Name="attachmentRedundancy")]
        public AttachmentRedundancyEnum? AttachmentRedundancy { get; set; }

        /// <summary>
        /// Determines if the attachment set should be enabled for layer 3
        /// </summary>
        /// <value>Boolean denoting the layer 3 enablement state</value>
        /// <example>true</example>
        [DataMember(Name="isLayer3")]
        public bool? IsLayer3 { get; set; }

        /// <summary>
        /// A list of routing instance request objects, Each request will result in the corresponding routing instance
        /// being added to the attachment set upon crestion. The referenced routing instance for each request must exist and belong
        /// to an attachment which is owned by the tenant owner requesting the attachment set.
        /// </summary>
        /// <value>A list of RoutingInstanceForAttachmentSetRequest objects</value>
        [DataMember(Name="attachmentSetRoutingInstances")]
        public List<RoutingInstanceForAttachmentSetRequest> AttachmentSetRoutingInstances { get; set; }

        /// <summary>
        /// The bgp IP network inbound policy of the attachment set
        /// </summary>
        /// <value>An instance of BgpIpNetworkInboundPolicyRequest</value>
        [DataMember(Name = "bgpIpNetworkInboundPolicy")]
        public BgpIpNetworkInboundPolicyRequest BgpIpNetworkInboundPolicy { get; set; }

        /// <summary>
        /// The bgp IP network Outbound policy of the attachment set
        /// </summary>
        /// <value>An instance of BgpIpNetworkOutboundPolicyRequestViewModel</value>
        [DataMember(Name = "bgpIpNetworkOutboundPolicy")]
        public BgpIpNetworkOutboundPolicyRequest BgpIpNetworkOutboundPolicy { get; set; }

        /// <summary>
        /// Determines the multicast domain type supported by the attachment set
        /// </summary>
        /// <value>An enum member for the multicast domain supported by the attachment set</value>
        /// <example>Sender-and-Receiver</example>
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
            sb.Append("  AttachmentSetRoutingInstances: ").Append(AttachmentSetRoutingInstances).Append("\n");
            sb.Append("  MulticastVpnDomainType: ").Append(MulticastVpnDomainType).Append("\n");
            sb.Append("  BgpIpNetworkInboundPolicy: ").Append(BgpIpNetworkInboundPolicy).Append("\n");
            sb.Append("  BgpIpNetworkOutboundPolicy: ").Append(BgpIpNetworkOutboundPolicy).Append("\n");
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
                    AttachmentSetRoutingInstances == other.AttachmentSetRoutingInstances ||
                    AttachmentSetRoutingInstances != null &&
                    AttachmentSetRoutingInstances.SequenceEqual(other.AttachmentSetRoutingInstances)
                ) &&
                (
                    MulticastVpnDomainType == other.MulticastVpnDomainType ||
                    MulticastVpnDomainType != null &&
                    MulticastVpnDomainType.Equals(other.MulticastVpnDomainType)
                ) &&
                (
                    BgpIpNetworkInboundPolicy == other.BgpIpNetworkInboundPolicy ||
                    BgpIpNetworkInboundPolicy != null &&
                    BgpIpNetworkInboundPolicy.Equals(other.BgpIpNetworkInboundPolicy)
                ) &&
                (
                    BgpIpNetworkOutboundPolicy == other.BgpIpNetworkOutboundPolicy ||
                    BgpIpNetworkOutboundPolicy != null &&
                    BgpIpNetworkOutboundPolicy.Equals(other.BgpIpNetworkOutboundPolicy)
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
                    if (AttachmentSetRoutingInstances != null)
                    hashCode = hashCode * 59 + AttachmentSetRoutingInstances.GetHashCode();
                    if (MulticastVpnDomainType != null)
                    hashCode = hashCode * 59 + MulticastVpnDomainType.GetHashCode();
                    if (BgpIpNetworkInboundPolicy != null)
                    hashCode = hashCode * 59 + BgpIpNetworkInboundPolicy.GetHashCode();
                    if (BgpIpNetworkOutboundPolicy != null)
                    hashCode = hashCode * 59 + BgpIpNetworkOutboundPolicy.GetHashCode();
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

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
    /// Model for requesting a tenant IP network association with the inbound policy of an attachment set
    /// </summary>
    [DataContract]
    public partial class VpnTenantIpNetworkInRequest : IEquatable<VpnTenantIpNetworkInRequest>, IValidatableObject
    {
        /// <summary>
        /// The ID of the tenant owner of the tenant IP network to be added to the BGP peers of the attachment set
        /// </summary>
        /// <value>An integer denoting the ID of the tenant owner</value>
        /// <example>1001</example>
        [DataMember(Name = "tenantId")]
        [Required(ErrorMessage="The ID of the tenant owner of the tenant IP network must be specified")]
        public int? TenantId { get; private set; }

        /// <summary>
        /// CIDR block name of the tenant IP network
        /// </summary>
        /// <value>String value for the CIDR representation of the tenant IP network</value>
        /// <example>10.1.1.0/24 le 32</example>
        [DataMember(Name = "tenantIpNetworkCidrName")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A CIDR block range must be specified, e.g. 10.1.1.0/24. You can also include the " +
            "'less than or equal to' parameter, e.g. 10.1.1.0/24 le 32")]
        public string TenantIpNetworkCidrName { get; set; }

        /// <summary>
        /// Denotes whether the tenant IP network should be learned from all BGP peers that are configured within the attachment set. This property 
        /// cannot be used concurrently with the 'Ipv4PeerAddress' property.
        /// </summary>
        /// <value>Boolean denoting whether the tenant IP network should be learned from all BGP peers that exist within the attachment set</value>
        /// <example>true</example>
        [DataMember(Name = "addToAllBgpPeersInAttachmentSet")]
        public bool? AddToAllBgpPeersInAttachmentSet { get; set; } = true;

        /// <summary>
        /// An IPv4 BGP peer address from which the tenant IP network should be learned. THe specified BGP peer must be configured and exist
        /// within the attachment set. This property cannot be used concurrently with the 'AddToAllBgpPeersInAttachmentSet' property.
        /// </summary>
        /// <value>string representing the address of an existing configured IPv4 BGP peer</value>
        /// <example>192.168.0.1</example>
        [DataMember(Name="ipv4PeerAddress")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", 
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        public string Ipv4PeerAddress { get; set; }

        /// <summary>
        /// The local IP routing preference to be applied to the route towards the tenant IP network
        /// </summary>
        /// <value>Integer representing the local IP routing preference</value>
        /// <example>200</example>
        [DataMember(Name = "localIpRoutingPreference")]
        [Range(1, 500, ErrorMessage = "Local IP routing preference must be a number between 1 and 500")]
        public int? LocalIpRoutingPreference { get; set; } = 100;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AddToAllBgpPeersInAttachmentSet.HasValue && AddToAllBgpPeersInAttachmentSet.Value)
                if (!string.IsNullOrEmpty(Ipv4PeerAddress))
                {
                    yield return new ValidationResult(
                        "A BGP peer address canot be specified when the 'AddToAllBgpPeersInAttachmentSet' " +
                        "argument is not specified or is set to 'true'. Include the 'AddToAllBgpPeersInAttachmentSet' argument with a value of " +
                        "'false' in the request if you wish to associate the IP network with a specific BGP peer.");
                }

            if (AddToAllBgpPeersInAttachmentSet.HasValue && !AddToAllBgpPeersInAttachmentSet.Value)
                if (string.IsNullOrEmpty(Ipv4PeerAddress))
                {
                    yield return new ValidationResult(
                        "You must specify either a BGP peer address with the 'Ipv4PeerAddress' argument, or specify that " +
                        "the IP network should be associated with all BGP peers in the attachment set with the " +
                        "'AddToAllBgpPeersInAttachmentSet' argument.");
                }
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class VpnTenantIpNetworkInRequest {\n");
            sb.Append("  TenantId: ").Append(TenantId).Append("\n");
            sb.Append("  TenantIpNetworkCidrName: ").Append(TenantIpNetworkCidrName).Append("\n");
            sb.Append("  AddToAllBgpPeersInAttachmentSet: ").Append(AddToAllBgpPeersInAttachmentSet).Append("\n");
            sb.Append("  Ipv4PeerAddress: ").Append(Ipv4PeerAddress).Append("\n");
            sb.Append("  LocalIpRoutingPreference: ").Append(LocalIpRoutingPreference).Append("\n");
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
            return obj.GetType() == GetType() && Equals((VpnTenantIpNetworkInRequest)obj);
        }

        /// <summary>
        /// Returns true if VpnTenantIpNetworkInRequest instances are equal
        /// </summary>
        /// <param name="other">Instance of VpnTenantIpNetworkInRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VpnTenantIpNetworkInRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    TenantId == other.TenantId||
                    TenantId != null &&
                    TenantId.Equals(other.TenantId)
                ) &&
                (
                    TenantIpNetworkCidrName == other.TenantIpNetworkCidrName ||
                    TenantIpNetworkCidrName != null &&
                    TenantIpNetworkCidrName.Equals(other.TenantIpNetworkCidrName)
                ) &&
                (
                    AddToAllBgpPeersInAttachmentSet == other.AddToAllBgpPeersInAttachmentSet ||
                    AddToAllBgpPeersInAttachmentSet != null &&
                    AddToAllBgpPeersInAttachmentSet.Equals(other.AddToAllBgpPeersInAttachmentSet)
                ) && 
                (
                    Ipv4PeerAddress == other.Ipv4PeerAddress ||
                    Ipv4PeerAddress != null &&
                    Ipv4PeerAddress.Equals(other.Ipv4PeerAddress)
                ) && 
                (
                    LocalIpRoutingPreference == other.LocalIpRoutingPreference ||
                    LocalIpRoutingPreference != null &&
                    LocalIpRoutingPreference.Equals(other.LocalIpRoutingPreference)
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
                    if (TenantId != null)
                    hashCode = hashCode * 59 + TenantId.GetHashCode();
                    if (TenantIpNetworkCidrName != null)
                    hashCode = hashCode * 59 + TenantIpNetworkCidrName.GetHashCode();
                    if (AddToAllBgpPeersInAttachmentSet != null)
                    hashCode = hashCode * 59 + AddToAllBgpPeersInAttachmentSet.GetHashCode();
                    if (Ipv4PeerAddress != null)
                    hashCode = hashCode * 59 + Ipv4PeerAddress.GetHashCode();
                    if (LocalIpRoutingPreference != null)
                    hashCode = hashCode * 59 + LocalIpRoutingPreference.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(VpnTenantIpNetworkInRequest left, VpnTenantIpNetworkInRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VpnTenantIpNetworkInRequest left, VpnTenantIpNetworkInRequest right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

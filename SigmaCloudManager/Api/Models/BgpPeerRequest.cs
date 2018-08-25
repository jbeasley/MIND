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
    public partial class BgpPeerRequest: IEquatable<BgpPeerRequest>
    {
        /// <summary>
        /// IPv4 address of the BGP peer
        /// </summary>
        /// <value>An IPv4 address</value>
        /// <example>12.1.1.1</example>
        [DataMember(Name="ipv4PeerAddress")]
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IPv4 address must be specified, e.g. 192.168.0.1")]
        public string Ipv4PeerAddress { get; set; }

        /// <summary>
        /// The 2 Byte Autonomous system number of the BGP peer
        /// </summary>
        /// <value>An integer value between 1 and 65535</value>
        /// <example>65001</example>
        [DataMember(Name="Peer2ByteAutonomousSystem")]
        [Required]
        [Range(1,65535)]
        public int? Peer2ByteAutonomousSystem { get; set; }

        /// <summary>
        /// Authentication password for the BGP peer
        /// </summary>
        /// <value>A string representing the authentication password for the BGP peer</value>
        /// <example>pAs5w0rd!</example>
        [DataMember(Name="PeerPassword")]
        public string PeerPassword { get; set; }

        /// <summary>
        /// Determines if multi-hop peering is enabled. Defaults to false, meaning multihop is disabled.
        /// </summary>
        /// <value>Boolean value which determines if multi-hop peering is enabled</value>
        /// <example>true</example>
        [DataMember(Name = "IsMultiHop")]
        public bool? IsMultiHop { get; set; } = false;

        /// <summary>
        /// Determines if the peer should be enabled with bidirectional forwarding detection. Defaults to true, meaning BFD is enabled.
        /// </summary>
        /// <value>Boolean value which determines if the peer should be enabled with bidirectional forwarding detection</value>
        /// <example>true</example>
        [DataMember(Name = "IsBfdEnabled")]
        public bool? IsBfdEnabled { get; set; } = true;

        /// <summary>
        /// Determines the maximum number of routes the peer should accept. The default is 500
        /// </summary>
        /// <value>A positive integer which determines the meximum number of routes accepted from the BGP peer/value>
        /// <example>200</example>
        [DataMember(Name = "maximumRoutes")]
        [Range(1, 1000)]
        public int? MaximumRoutes { get; set; } = 500;

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class BgpPeer {\n");
            sb.Append("  Ipv4PeerAddress: ").Append(Ipv4PeerAddress).Append("\n");
            sb.Append("  Peer2ByteAutonomousSystem: ").Append(Peer2ByteAutonomousSystem).Append("\n");
            sb.Append("  PeerPassword: ").Append(PeerPassword).Append("\n");
            sb.Append("  IsMultiHop: ").Append(IsMultiHop).Append("\n");
            sb.Append("  IsBfdEnabled: ").Append(IsBfdEnabled).Append("\n");
            sb.Append("  MaximumRoutes: ").Append(MaximumRoutes).Append("\n");
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
            return obj.GetType() == GetType() && Equals((BgpPeerRequest)obj);
        }

        /// <summary>
        /// Returns true if BgpPeer instances are equal
        /// </summary>
        /// <param name="other">Instance of BgpPeer to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BgpPeerRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Ipv4PeerAddress == other.Ipv4PeerAddress ||
                    Ipv4PeerAddress != null &&
                    Ipv4PeerAddress.Equals(other.Ipv4PeerAddress)
                ) && 
                (
                    Peer2ByteAutonomousSystem == other.Peer2ByteAutonomousSystem ||
                    Peer2ByteAutonomousSystem != null &&
                    Peer2ByteAutonomousSystem.Equals(other.Peer2ByteAutonomousSystem)
                ) && 
                (
                    PeerPassword == other.PeerPassword ||
                    PeerPassword != null &&
                    PeerPassword.Equals(other.PeerPassword)
                ) && 
                (
                    IsMultiHop == other.IsMultiHop ||
                    IsMultiHop != null &&
                    IsMultiHop.Equals(other.IsMultiHop)
                ) && 
                (
                    IsBfdEnabled == other.IsBfdEnabled ||
                    IsBfdEnabled != null &&
                    IsBfdEnabled.Equals(other.IsBfdEnabled)
                ) &&
                (
                    MaximumRoutes == other.MaximumRoutes ||
                    MaximumRoutes != null &&
                    MaximumRoutes.Equals(other.MaximumRoutes)
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
                    if (Ipv4PeerAddress != null)
                    hashCode = hashCode * 59 + Ipv4PeerAddress.GetHashCode();
                    if (Peer2ByteAutonomousSystem != null)
                    hashCode = hashCode * 59 + Peer2ByteAutonomousSystem.GetHashCode();
                    if (PeerPassword != null)
                    hashCode = hashCode * 59 + PeerPassword.GetHashCode();
                    if (IsMultiHop != null)
                    hashCode = hashCode * 59 + IsMultiHop.GetHashCode();
                    if (IsBfdEnabled != null)
                    hashCode = hashCode * 59 + IsBfdEnabled.GetHashCode();
                    if (MaximumRoutes != null)
                    hashCode = hashCode * 59 + MaximumRoutes.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(BgpPeerRequest left, BgpPeerRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BgpPeerRequest left, BgpPeerRequest right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

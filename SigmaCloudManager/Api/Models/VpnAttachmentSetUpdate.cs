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
    /// Model for updating an existing vpn attachment set (i.e. an attachment set association with a vpn)
    /// </summary>
    [DataContract]
    public partial class VpnAttachmentSetUpdate : IEquatable<VpnAttachmentSetUpdate>
    {
        /// <summary>
        /// Determines if the attachment set should be configured as a hub for the association with the vpn.
        /// The vpn topology must be 'hub-and-spoke' for the attachment set to be defined as a hub.
        /// </summary>
        /// <value>Boolean value denoting the hub state of the attachment set</value>
        /// <example>true</example>
        [DataMember(Name="isHub")]
        public bool? IsHub { get; set; }

        /// <summary>
        /// Determines if the attachment set should be directly integrated with the tenant multicast domain.
        /// The vpn must be enabled for multicast for the attachment set to be integrated with the tenant multicast domain.
        /// </summary>
        /// <value>Boolean value denoting whether the attachment set should be directly integrated with the tenant multicast domain</value>
        /// <example>true</example>
        [DataMember(Name = "isMulticastDirectlyIntegrated")]
        public bool? IsMulticastDirectlyIntegrated { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class VpnAttachmentSetUpdate {\n");
            sb.Append("  IsHub: ").Append(IsHub).Append("\n");
            sb.Append("  IsMulticastDirectlyIntegrated: ").Append(IsMulticastDirectlyIntegrated).Append("\n");
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
            return obj.GetType() == GetType() && Equals((VpnAttachmentSetUpdate)obj);
        }

        /// <summary>
        /// Returns true if VpnAttachmentSetUpdate instances are equal
        /// </summary>
        /// <param name="other">Instance of VpnAttachmentSetUpdate to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VpnAttachmentSetUpdate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    IsHub == other.IsHub ||
                    IsHub != null &&
                    IsHub.Equals(other.IsHub)
                ) && 
                (
                    IsMulticastDirectlyIntegrated == other.IsMulticastDirectlyIntegrated ||
                    IsMulticastDirectlyIntegrated != null &&
                    IsMulticastDirectlyIntegrated.Equals(other.IsMulticastDirectlyIntegrated)
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
                    if (IsHub != null)
                    hashCode = hashCode * 59 + IsHub.GetHashCode();
                    if (IsMulticastDirectlyIntegrated != null)
                    hashCode = hashCode * 59 + IsMulticastDirectlyIntegrated.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(VpnAttachmentSetUpdate left, VpnAttachmentSetUpdate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VpnAttachmentSetUpdate left, VpnAttachmentSetUpdate right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

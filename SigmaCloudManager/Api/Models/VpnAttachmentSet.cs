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
    /// Model for a vpn attachment set (i.e. an attachment set association with a vpn)
    /// </summary>
    [DataContract]
    public partial class VpnAttachmentSet : IEquatable<VpnAttachmentSet>
    {
        /// <summary>
        /// The ID of the vpn attachment set
        /// </summary>
        /// <value>Integer denoting the ID of the vpn attachment set</value>
        /// <example>13001</example>
        [DataMember(Name = "vpnAttachmentSetId")]
        public int? VpnAttachmentSetId { get; private set; }

        /// <summary>
        /// The name of the attachment set
        /// </summary>
        /// <value>A string denoting the name of the attachment set</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        [DataMember(Name = "attachmentSetName")]
        public string AttachmentSetName { get; private set; }

        /// <summary>
        /// The ID of the attachment set
        /// </summary>
        /// <value>An integer value denoting the ID of the attachment set</value>
        /// <example>9001</example>
        [DataMember(Name = "attachmentSetId")]
        public string AttachmentSetId { get; private set; }

        /// <summary>
        /// The attachment set which is bound to the vpn.
        /// </summary>
        /// <value>An AttchmentSet object</value>
        [DataMember(Name = "attachmentSet")]
        public AttachmentSet AttachmentSet { get; private set; }

        /// <summary>
        /// The name of the vpn
        /// </summary>
        /// <value>A string denoting the name of the vpn</value>
        /// <example>cloud-connectivity-vpn</example>
        [DataMember(Name = "vpnName")]
        public string VpnName { get; private set; }

        /// <summary>
        /// The ID of the vpn
        /// </summary>
        /// <value>An integer denoting the ID of the vpn</value>
        /// <example>8001</example>
        [DataMember(Name = "vpnId")]
        public string VpnId { get; private set; }

        /// <summary>
        /// The vpn to which the attachment set is bound.
        /// </summary>
        /// <value>A Vpn object</value>
        [DataMember(Name = "vpn")]
        public Vpn Vpn { get; private set; }

        /// <summary>
        /// Determines if the attachment set is configured as a hub for the association with the vpn.
        /// </summary>
        /// <value>Boolean value denoting the hub state of the attachment set</value>
        /// <example>true</example>
        [DataMember(Name="isHub")]
        public bool? IsHub { get; private set; }

        /// <summary>
        /// Determines if the attachment set is directly integrated with the tenant multicast domain.
        /// </summary>
        /// <value>Boolean value denoting whether the attachment set is directly integrated with the tenant multicast domain</value>
        /// <example>true</example>
        [DataMember(Name = "isMulticastDirectlyIntegrated")]
        public bool? IsMulticastDirectlyIntegrated { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class VpnAttachmentSet {\n");
            sb.Append("  VpnAttachmentSetId: ").Append(VpnAttachmentSetId).Append("\n");
            sb.Append("  AttachmentSetName: ").Append(AttachmentSetName).Append("\n");
            sb.Append("  AttachmentSetId: ").Append(AttachmentSetId).Append("\n");
            sb.Append("  AttachmentSet: ").Append(AttachmentSet).Append("\n");
            sb.Append("  VpnName: ").Append(VpnName).Append("\n");
            sb.Append("  VpnId: ").Append(VpnId).Append("\n");
            sb.Append("  Vpn: ").Append(Vpn).Append("\n");
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
            return obj.GetType() == GetType() && Equals((VpnAttachmentSet)obj);
        }

        /// <summary>
        /// Returns true if VpnAttachmentSet instances are equal
        /// </summary>
        /// <param name="other">Instance of VpnAttachmentSetRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VpnAttachmentSet other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                 (
                    VpnAttachmentSetId == other.VpnAttachmentSetId ||
                    VpnAttachmentSetId != null &&
                    VpnAttachmentSetId.Equals(other.VpnAttachmentSetId)
                ) &&
                (
                    AttachmentSetName == other.AttachmentSetName ||
                    AttachmentSetName != null &&
                    AttachmentSetName.Equals(other.AttachmentSetName)
                ) &&
                (
                    AttachmentSetId == other.AttachmentSetId ||
                    AttachmentSetId != null &&
                    AttachmentSetId.Equals(other.AttachmentSetId)
                ) &&
                (
                    AttachmentSet == other.AttachmentSet ||
                    AttachmentSet != null &&
                    AttachmentSet.Equals(other.AttachmentSet)
                ) &&
                (
                    VpnName == other.VpnName ||
                    VpnName != null &&
                    VpnName.Equals(other.VpnName)
                ) &&
                (
                    VpnId == other.VpnId ||
                    VpnId != null &&
                    VpnId.Equals(other.VpnId)
                ) &&
                (
                    Vpn == other.Vpn ||
                    Vpn != null &&
                    Vpn.Equals(other.Vpn)
                ) &&
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
                    if (VpnAttachmentSetId != null)
                    hashCode = hashCode * 59 + VpnAttachmentSetId.GetHashCode();
                    if (AttachmentSetName != null)
                    hashCode = hashCode * 59 + AttachmentSetName.GetHashCode();
                    if (AttachmentSetId != null)
                    hashCode = hashCode * 59 + AttachmentSetId.GetHashCode();
                    if (AttachmentSet != null)
                    hashCode = hashCode * 59 + AttachmentSet.GetHashCode();
                    if (VpnName != null)
                    hashCode = hashCode * 59 + VpnName.GetHashCode();
                    if (VpnId != null)
                    hashCode = hashCode * 59 + VpnId.GetHashCode();
                    if (Vpn != null)
                    hashCode = hashCode * 59 + Vpn.GetHashCode();
                    if (IsHub != null)
                    hashCode = hashCode * 59 + IsHub.GetHashCode();
                    if (IsMulticastDirectlyIntegrated != null)
                    hashCode = hashCode * 59 + IsMulticastDirectlyIntegrated.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(VpnAttachmentSet left, VpnAttachmentSet right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VpnAttachmentSet left, VpnAttachmentSet right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

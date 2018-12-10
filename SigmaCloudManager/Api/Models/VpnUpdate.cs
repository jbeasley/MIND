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
    /// Model for updating a vpn
    /// </summary>
    [DataContract]
    public partial class VpnUpdate : IEquatable<VpnUpdate>
    {
        /// <summary>
        /// A description of the VPN
        /// </summary>
        /// <value>String value denoting the vpn description</value>
        /// <example>vpn for providing IP connectivity between hosts running in public and private clouds</example>
        [DataMember(Name="description")]
        public string Description { get; set; }

        /// <summary>
        /// The geographical region which the vpn operates within. If no region is chosen then the vpn should be made available in all regions
        /// </summary>
        /// <value>Enum value denoting the region</value>
        /// <example>EMEA</example>
        [DataMember(Name="region")]
        public RegionEnum? Region { get; set; }

        /// <summary>
        /// The tenancy type of the vpn. If the tenancy type is single then only the owner of the vpn can participate in the vpn. 
        /// If the tenancy type is multi then any tenant can participate in the vpn.
        /// </summary>
        /// <value>Enum value denoting the tenancy type of the vpn</value>
        /// <example>single</example>
        [DataMember(Name="tenancyType")]
        public TenancyTypeEnum? TenancyType { get; set; }

        /// <summary>
        /// Determines if the vpn supports extranet connectivity
        /// </summary>
        /// <value>Boolean denoting whether the vpn supports extranet</value>
        /// <example>true</example>
        [DataMember(Name = "isExtranet")]
        public bool? IsExtranet { get; set; }

        /// <summary>
        /// The multicast direction type of the VPN. 
        /// </summary>
        /// <value>Enum value denoting the multicast direction type of the vpn.</value>
        /// <example>unidirectional</example>
        [DataMember(Name = "multicastVpnDirectionType")]
        public MulticastVpnDirectionTypeEnum? MulticastVpnDirectionType { get; set; }

        /// <summary>
        /// A list of vpn attachment set request objects denoting attachment sets to be associated with the vpn.
        /// </summary>
        /// <value>List of VpnAttachmentSetRequest objects</value>
        [DataMember(Name = "vpnAttachmentSets")]
        public List<VpnAttachmentSetRequest> VpnAttachmentSets { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class IpVpnRequest {\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Region: ").Append(Region).Append("\n");
            sb.Append("  TenancyType: ").Append(TenancyType).Append("\n");
            sb.Append("  IsExtranet: ").Append(IsExtranet).Append("\n");
            sb.Append("  MulticastVpnDirectionType: ").Append(MulticastVpnDirectionType).Append("\n");
            sb.Append("  VpnAttachmentSets: ").Append(VpnAttachmentSets).Append("\n");
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
            return obj.GetType() == GetType() && Equals((VpnRequest)obj);
        }

        /// <summary>
        /// Returns true if VpnUpdate instances are equal
        /// </summary>
        /// <param name="other">Instance of VpnUpdate to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VpnUpdate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.Equals(other.Description)
                ) &&
                (
                    Region == other.Region ||
                    Region != null &&
                    Region.Equals(other.Region)
                ) &&
                (
                    TenancyType == other.TenancyType ||
                    TenancyType != null &&
                    TenancyType.Equals(other.TenancyType)
                ) &&
                (
                    IsExtranet == other.IsExtranet ||
                    IsExtranet != null &&
                    IsExtranet.Equals(other.IsExtranet)
                ) &&
                (
                    MulticastVpnDirectionType == other.MulticastVpnDirectionType ||
                    MulticastVpnDirectionType != null &&
                    MulticastVpnDirectionType.Equals(other.MulticastVpnDirectionType)
                ) &&
                (
                    VpnAttachmentSets == other.VpnAttachmentSets ||
                    VpnAttachmentSets != null &&
                    VpnAttachmentSets.Equals(other.VpnAttachmentSets)
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
                    if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                    if (Region != null)
                    hashCode = hashCode * 59 + Region.GetHashCode();
                    if (TenancyType != null)
                    hashCode = hashCode * 59 + TenancyType.GetHashCode();
                    if (IsExtranet != null)
                    hashCode = hashCode * 59 + IsExtranet.GetHashCode();
                    if (MulticastVpnDirectionType != null)
                    hashCode = hashCode * 59 + MulticastVpnDirectionType.GetHashCode();
                    if (VpnAttachmentSets != null)
                    hashCode = hashCode * 59 + VpnAttachmentSets.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(VpnUpdate left, VpnUpdate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VpnUpdate left, VpnUpdate right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

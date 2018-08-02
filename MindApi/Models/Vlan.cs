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
    public partial class Vlan : IEquatable<Vlan>
    { 
        /// <summary>
        /// The ID of the vlan
        /// </summary>
        /// <value>The ID of the vlan</value>
        [DataMember(Name="vlanID")]
        public int? VlanID { get; set; }

        /// <summary>
        /// The vlan tag
        /// </summary>
        /// <value>The vlan tag</value>
        [DataMember(Name="vlanTag")]
        public int? VlanTag { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Vlan {\n");
            sb.Append("  VlanID: ").Append(VlanID).Append("\n");
            sb.Append("  VlanTag: ").Append(VlanTag).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Vlan)obj);
        }

        /// <summary>
        /// Returns true if Vlan instances are equal
        /// </summary>
        /// <param name="other">Instance of Vlan to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Vlan other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    VlanID == other.VlanID ||
                    VlanID != null &&
                    VlanID.Equals(other.VlanID)
                ) && 
                (
                    VlanTag == other.VlanTag ||
                    VlanTag != null &&
                    VlanTag.Equals(other.VlanTag)
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
                    if (VlanID != null)
                    hashCode = hashCode * 59 + VlanID.GetHashCode();
                    if (VlanTag != null)
                    hashCode = hashCode * 59 + VlanTag.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Vlan left, Vlan right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Vlan left, Vlan right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

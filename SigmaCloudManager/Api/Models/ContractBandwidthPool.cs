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
    /// Model of a contract bandwidth pool
    /// </summary>
    [DataContract]
    public partial class ContractBandwidthPool : IEquatable<ContractBandwidthPool>
    {
        /// <summary>
        /// The MIND system-generated name of the contract bandwidth pool 
        /// </summary>
        /// <value>A string denoting the name of the contract bandwidth pool</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        [DataMember(Name="name")]
        public string Name { get; private set; }

        /// <summary>
        /// The contract bandwidth of the pool in Mbps
        /// </summary>
        /// <value>An integer denoting the contract bandwidth of the pool in Mbps</value>
        /// <example>1000</example>
        [DataMember(Name="contractBandwidthMbps")]
        public int? ContractBandwidthMbps { get; private set; }

        /// <summary>
        /// Denotes whether DSCP and COS markings of packets are trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the trust stater</value>
        /// <example>true</example>
        [DataMember(Name = "trustReceivedCosDscp")]
        public bool? TrustReceivedCosDscp { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ContractBandwidthPool {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  ContractBandwidthMbps: ").Append(ContractBandwidthMbps).Append("\n");
            sb.Append("  TrustReceivedCosDscp: ").Append(TrustReceivedCosDscp).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ContractBandwidthPool)obj);
        }

        /// <summary>
        /// Returns true if ContractBandwidthPool instances are equal
        /// </summary>
        /// <param name="other">Instance of ContractBandwidthPool to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ContractBandwidthPool other)
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
                    ContractBandwidthMbps == other.ContractBandwidthMbps ||
                    ContractBandwidthMbps != null &&
                    ContractBandwidthMbps.Equals(other.ContractBandwidthMbps)
                ) &&
                (
                    TrustReceivedCosDscp == other.TrustReceivedCosDscp ||
                    TrustReceivedCosDscp != null &&
                    TrustReceivedCosDscp.Equals(other.TrustReceivedCosDscp)
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
                    if (ContractBandwidthMbps != null)
                    hashCode = hashCode * 59 + ContractBandwidthMbps.GetHashCode();
                    if (TrustReceivedCosDscp != null)
                    hashCode = hashCode * 59 + TrustReceivedCosDscp.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ContractBandwidthPool left, ContractBandwidthPool right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ContractBandwidthPool left, ContractBandwidthPool right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

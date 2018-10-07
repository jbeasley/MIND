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
    /// Model of a tenant IP network - an IP network which is owned (belongs to) a tenant
    /// </summary>
    [DataContract]
    public partial class TenantIpNetwork : IEquatable<TenantIpNetwork>
    { 
        /// <summary>
        /// The ID of the tenant IP network
        /// </summary>
        /// <value>The ID of the tenant IP network</value>
        /// <example>2001</example>
        [DataMember(Name="tenantIpNetworkId")]
        public int? TenantIpNetworkId { get; private set; }

        /// <summary>
        /// The CIDR IPv4 prefix
        /// </summary>
        /// <value>The CIDR IPv4 prefix</value>
        /// <example>10.1.1.0</example>
        [DataMember(Name="ipv4Prefix")]
        public string Ipv4Prefix { get; private set; }

        /// <summary>
        /// The CIDR length of the IPv4 prefix
        /// </summary>
        /// <value>An integer between 1 and 32 which denotes the CIDR length of the IPv4 prefix</value>
        /// <example>24</example>
        [DataMember(Name="ipv4length")]
        public int? Ipv4Length { get; private set; }

        /// <summary>
        /// The maximum length of IPv4 prefixes which are contained within the CUDR range
        /// </summary>
        /// <value>An intger between 1 and 32 which denotes the maximum length of IPv4 prefixes within the CIDR range</value>
        /// <example>32</example>
        [DataMember(Name = "ipv4LessThanOrEqualTolength")]
        public int? Ipv4LessThanOrEqualToLength { get; private set; }

        /// <summary>
        /// Determines whether the tenant network is allowed into any IP Extranet VPNs
        /// </summary>
        /// <value>Boolean value which when true indicates that the tenant network is enabled for extranet</value>
        /// <example>true</example>
        [DataMember(Name="allowExtranet")]
        public bool? AllowExtranet { get; private set; }

        /// <summary>
        /// The ID of the tenant to which the tenant network belongs
        /// </summary>
        /// <value>Integer value for the ID of the tenant</value>
        /// <example>1001</example>
        [DataMember(Name="tenantId")]
        public int? TenantId { get; private set; }

        /// <summary>
        /// The IP routing behavior for traffic forwarding towards the tenant IP network
        /// </summary>
        /// <value>Enum member value denoting the tenant ip routing behavior</value>
        /// <example>BluePlane</example>
        [DataMember(Name = "tenantIpRoutingBehaviour")]
        public TenantIpRoutingBehaviourEnum? IpRoutingBehaviour { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TenantIpNetwork {\n");
            sb.Append("  TenantIpNetworkId: ").Append(TenantIpNetworkId).Append("\n");
            sb.Append("  Ipv4Prefix: ").Append(Ipv4Prefix).Append("\n");
            sb.Append("  Ipv4Length: ").Append(Ipv4Length).Append("\n");
            sb.Append("  Ipv4LessThanOrEqualToLength: ").Append(Ipv4LessThanOrEqualToLength).Append("\n");
            sb.Append("  AllowExtranet: ").Append(AllowExtranet).Append("\n");
            sb.Append("  TenantId: ").Append(TenantId).Append("\n");
            sb.Append("  IpRoutingBehaviour: ").Append(IpRoutingBehaviour).Append("\n");
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
            return obj.GetType() == GetType() && Equals((TenantIpNetwork)obj);
        }

        /// <summary>
        /// Returns true if TenantIpNetwork instances are equal
        /// </summary>
        /// <param name="other">Instance of TenantIpNetwork to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TenantIpNetwork other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    TenantIpNetworkId == other.TenantIpNetworkId ||
                    TenantIpNetworkId != null &&
                    TenantIpNetworkId.Equals(other.TenantIpNetworkId)
                ) &&
                (
                    Ipv4Prefix == other.Ipv4Prefix ||
                    Ipv4Prefix != null &&
                    Ipv4Prefix.Equals(other.Ipv4Prefix)
                ) &&
                (
                    Ipv4Length == other.Ipv4Length ||
                    Ipv4Length != null &&
                    Ipv4Length.Equals(other.Ipv4Length)
                ) &&
                (
                    Ipv4LessThanOrEqualToLength == other.Ipv4LessThanOrEqualToLength ||
                    Ipv4LessThanOrEqualToLength != null &&
                    Ipv4LessThanOrEqualToLength.Equals(other.Ipv4LessThanOrEqualToLength)
                ) &&
                (
                    AllowExtranet == other.AllowExtranet ||
                    AllowExtranet != null &&
                    AllowExtranet.Equals(other.AllowExtranet)
                ) &&
                (
                    TenantId == other.TenantId ||
                    TenantId != null &&
                    TenantId.Equals(other.TenantId)
                ) &&
                (
                    IpRoutingBehaviour == other.IpRoutingBehaviour ||
                    IpRoutingBehaviour != null &&
                    IpRoutingBehaviour.Equals(other.IpRoutingBehaviour)
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
                    if (TenantIpNetworkId != null)
                    hashCode = hashCode * 59 + TenantIpNetworkId.GetHashCode();
                    if (Ipv4Prefix != null)
                    hashCode = hashCode * 59 + Ipv4Prefix.GetHashCode();
                    if (Ipv4Length != null)
                    hashCode = hashCode * 59 + Ipv4Length.GetHashCode();
                    if (Ipv4LessThanOrEqualToLength != null)
                    hashCode = hashCode * 59 + Ipv4LessThanOrEqualToLength.GetHashCode();
                    if (AllowExtranet != null)
                    hashCode = hashCode * 59 + AllowExtranet.GetHashCode();
                    if (TenantId != null)
                    hashCode = hashCode * 59 + TenantId.GetHashCode();
                    if (IpRoutingBehaviour != null)
                    hashCode = hashCode * 59 + IpRoutingBehaviour.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(TenantIpNetwork left, TenantIpNetwork right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TenantIpNetwork left, TenantIpNetwork right)
        { 
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

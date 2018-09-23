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
    /// Model of a port
    /// </summary>
    [DataContract]
    public partial class Port : IEquatable<Port>
    { 
        /// <summary>
        /// The ID of the port
        /// </summary>
        /// <value>An integer value denoting the ID of the port</value>
        /// <example>7001</example>
        [DataMember(Name="portId")]
        public int? PortId { get; private set; }

        /// <summary>
        /// The type of the port, e.g. TenGigabitEtheret
        /// </summary>
        /// <value>String denoting the type of the port</value>
        /// <example>TenGigabitEthernet</example>
        [DataMember(Name="type")]
        public string Type { get; private set; }

        /// <summary>
        /// The name of the port
        /// </summary>
        /// <value>String denoting the name of the port</value>
        /// <example>1/1/1</example>
        [DataMember(Name="name")]
        public string Name { get; private set; }

        /// <summary>
        /// The name of the small form-factor pluggable optic for the port
        /// </summary>
        /// <value>A string denoting the name of the small form-factor pluggable optic for the port</value>
        /// <example>SFP-10G-SR</example>
        [DataMember(Name="portSfp")]
        public string PortSfp { get; private set; }

        /// <summary>
        /// Status of the port
        /// </summary>
        /// <value>String denoting the status of the port</value>
        /// <example>Assigned</example>
        [DataMember(Name="portStatus")]
        public string PortStatus { get; private set; }

        /// <summary>
        /// The role of the port
        /// </summary>
        /// <value>String value denoting the role of the port</value>
        /// <example>Tenant-Facing</example>
        [DataMember(Name="portRole")]
        public string PortRole { get; private set; }

        /// <summary>
        /// Pool to which the port is assigned
        /// </summary>
        /// <value>String value denoting pool to which the port is assigned</value>
        /// <example>General</example>
        [DataMember(Name="portPool")]
        public string PortPool { get; private set; }

        /// <summary>
        /// Port connector type
        /// </summary>
        /// <value>String denoting the port connector type</value>
        /// <example>SC</example>
        [DataMember(Name = "portConnector")]
        public string PortConnector { get; private set; }

        /// <summary>
        /// Port Bandwidth in Gbps
        /// </summary>
        /// <value>Integer value denoting the port bandwidth in Gbps</value>
        /// <example>10</example>
        [DataMember(Name = "portBandwidthGbps")]
        public int? PortBandwidthGbps { get; private set; }

        /// <summary>
        /// The ID of the tenant to which the port should be assigned.
        /// </summary>
        /// <value>Integer value denoting the ID of the tenant</value>
        /// <example>9009</example>
        [DataMember(Name = "tenantId")]
        public int? TenantId { get; private set; }

        /// <summary>
        /// The name of the tenant to which the port should be assigned.
        /// </summary>
        /// <value>String value denoting the name of the tenant</value>
        /// <example>9009</example>
        [DataMember(Name = "tenantName")]
        public string TenantName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Port {\n");
            sb.Append("  PortId: ").Append(PortId).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  PortSfp: ").Append(PortSfp).Append("\n");
            sb.Append("  PortStatus: ").Append(PortStatus).Append("\n");
            sb.Append("  PortRole: ").Append(PortRole).Append("\n");
            sb.Append("  PortPool: ").Append(PortPool).Append("\n");
            sb.Append("  PortConnector: ").Append(PortConnector).Append("\n");
            sb.Append("  PortBandwidthGbps: ").Append(PortBandwidthGbps).Append("\n");
            sb.Append("  TenantId: ").Append(TenantId).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Port)obj);
        }

        /// <summary>
        /// Returns true if Port instances are equal
        /// </summary>
        /// <param name="other">Instance of Port to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Port other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    PortId == other.PortId ||
                    PortId != null &&
                    PortId.Equals(other.PortId)
                ) &&
                (
                    Type == other.Type ||
                    Type != null &&
                    Type.Equals(other.Type)
                ) &&
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) &&
                (
                    PortSfp == other.PortSfp ||
                    PortSfp != null &&
                    PortSfp.Equals(other.PortSfp)
                ) &&
                (
                    PortStatus == other.PortStatus ||
                    PortStatus != null &&
                    PortStatus.Equals(other.PortStatus)
                ) &&
                (
                    PortRole == other.PortRole ||
                    PortRole != null &&
                    PortRole.Equals(other.PortRole)
                ) &&
                (
                    PortPool == other.PortPool ||
                    PortPool != null &&
                    PortPool.Equals(other.PortPool)
                ) &&
                (
                    PortConnector == other.PortConnector ||
                    PortConnector != null &&
                    PortConnector.Equals(other.PortConnector)
                ) &&
                (
                    PortBandwidthGbps == other.PortBandwidthGbps ||
                    PortBandwidthGbps != null &&
                    PortBandwidthGbps.Equals(other.PortBandwidthGbps)
                ) &&
                (
                    TenantId == other.TenantId ||
                    TenantId != null &&
                    TenantId.Equals(other.TenantId)
                ) &&
                (
                    TenantName == other.TenantName ||
                    TenantName != null &&
                    TenantName.Equals(other.TenantName)
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
                    if (PortId != null)
                    hashCode = hashCode * 59 + PortId.GetHashCode();
                    if (Type != null)
                    hashCode = hashCode * 59 + Type.GetHashCode();
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (PortSfp != null)
                    hashCode = hashCode * 59 + PortSfp.GetHashCode();
                    if (PortStatus != null)
                    hashCode = hashCode * 59 + PortStatus.GetHashCode();
                    if (PortRole != null)
                    hashCode = hashCode * 59 + PortRole.GetHashCode();
                    if (PortPool != null)
                    hashCode = hashCode * 59 + PortPool.GetHashCode();
                    if (PortConnector != null)
                    hashCode = hashCode * 59 + PortConnector.GetHashCode();
                    if (PortBandwidthGbps != null)
                    hashCode = hashCode * 59 + PortBandwidthGbps.GetHashCode();
                    if (TenantId != null)
                    hashCode = hashCode * 59 + TenantId.GetHashCode();
                    if (TenantName != null)
                    hashCode = hashCode * 59 + TenantName.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Port left, Port right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Port left, Port right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

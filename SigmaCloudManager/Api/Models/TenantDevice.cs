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
    public partial class TenantDevice : IEquatable<TenantDevice>
    { 
        /// <summary>
        /// The ID of the device
        /// </summary>
        /// <value>Integer denoting the ID of the device</value>
        /// <example>90991</example>
        [DataMember(Name="deviceId")]
        public int? DeviceId { get; private set; }

        /// <summary>
        /// The name of the device
        /// </summary>
        /// <value>String denoting the name of the device</value>
        /// <example>DTC-CPE-1</example>
        [DataMember(Name="name")]
        public string Name { get; private set; }

        /// <summary>
        /// A description of the device
        /// </summary>
        /// <value>A description of the device</value>
        /// <example>Customer Edge device located in DTC</example>
        [DataMember(Name="description")]
        public string Description { get; private set; }

        /// <summary>
        /// Denotes whether layer 2 overhead is included in the device MTU calculation
        /// </summary>
        /// <value>Boolean denoting whether  layer 2 overhead is included in the device MTU calculation</value>
        /// <example>true</example>
        [DataMember(Name="useLayer2InterfaceMtu")]
        public bool? UseLayer2InterfaceMtu { get; private set; }

        /// <summary>
        /// The model of the device
        /// </summary>
        /// <value>String value denoting the model of the device</value>
        /// <example>ASR-1001</example>
        [DataMember(Name="deviceModel")]
        public string DeviceModel { get; private set; }

        /// <summary>
        /// The ID of the tenant to which the device is assigned
        /// </summary>
        /// <value>Integer denoting the ID of the tenant</value>
        /// <example>90991</example>
        [DataMember(Name = "tenantId")]
        public int? TenantId { get; private set; }

        /// <summary>
        /// The tenant name to which the device is assigned
        /// </summary>
        /// <value>String value denoting tenant name to which the device is assigned</value>
        /// <example>Red</example>
        [DataMember(Name="tenantName")]
        public string TenantName { get; private set; }

        /// <summary>
        /// The location of the device
        /// </summary>
        /// <value>String value denoting the location of the device</value>
        /// <example>DTC</example>
        [DataMember(Name="locationName")]
        public string LocationName { get; private set; }

        /// <summary>
        /// The status of the device
        /// </summary>
        /// <value>String value denoting the status of the device</value>
        /// <example>Production</example>
        [DataMember(Name="deviceStatus")]
        public string DeviceStatus { get; private set; }

        /// <summary>
        /// The device role of the device
        /// </summary>
        /// <value>String value denoting device role</value>
        /// <example>Red</example>
        [DataMember(Name = "deviceRole")]
        public string DeviceRole { get; private set; }

        /// <summary>
        /// A lit of ports which belong to the device
        /// </summary>
        /// <value>A list of Port objects</value>
        [DataMember(Name="ports")]
        public List<Port> Ports { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TenantDevice {\n");
            sb.Append("  DeviceId: ").Append(DeviceId).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  UseLayer2InterfaceMtu: ").Append(UseLayer2InterfaceMtu).Append("\n");
            sb.Append("  DeviceModel: ").Append(DeviceModel).Append("\n");
            sb.Append("  DeviceRole: ").Append(DeviceRole).Append("\n");
            sb.Append("  TenantId: ").Append(TenantId).Append("\n");
            sb.Append("  TenantName: ").Append(TenantName).Append("\n");
            sb.Append("  LocationName: ").Append(LocationName).Append("\n");
            sb.Append("  DeviceStatus: ").Append(DeviceStatus).Append("\n");
            sb.Append("  Ports: ").Append(Ports).Append("\n");
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
            return obj.GetType() == GetType() && Equals((TenantDevice)obj);
        }

        /// <summary>
        /// Returns true if TenantDevice instances are equal
        /// </summary>
        /// <param name="other">Instance of TenantDevice to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TenantDevice other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    DeviceId == other.DeviceId ||
                    DeviceId != null &&
                    DeviceId.Equals(other.DeviceId)
                ) && 
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) && 
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.Equals(other.Description)
                ) && 
                (
                    UseLayer2InterfaceMtu == other.UseLayer2InterfaceMtu ||
                    UseLayer2InterfaceMtu != null &&
                    UseLayer2InterfaceMtu.Equals(other.UseLayer2InterfaceMtu)
                ) && 
                (
                    DeviceModel == other.DeviceModel ||
                    DeviceModel != null &&
                    DeviceModel.Equals(other.DeviceModel)
                ) &&
                (
                    DeviceRole == other.DeviceRole ||
                    DeviceRole != null &&
                    DeviceRole.Equals(other.DeviceRole)
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
                ) && 
                (
                    LocationName == other.LocationName ||
                    LocationName != null &&
                    LocationName.Equals(other.LocationName)
                ) && 
                (
                    DeviceStatus == other.DeviceStatus ||
                    DeviceStatus != null &&
                    DeviceStatus.Equals(other.DeviceStatus)
                ) && 
                (
                    Ports == other.Ports ||
                    Ports != null &&
                    Ports.SequenceEqual(other.Ports)
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
                    if (DeviceId != null)
                    hashCode = hashCode * 59 + DeviceId.GetHashCode();
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                    if (UseLayer2InterfaceMtu != null)
                    hashCode = hashCode * 59 + UseLayer2InterfaceMtu.GetHashCode();
                    if (DeviceModel != null)
                    hashCode = hashCode * 59 + DeviceModel.GetHashCode();
                    if (DeviceRole != null)
                    hashCode = hashCode * 59 + DeviceRole.GetHashCode();
                    if (TenantId != null)
                    hashCode = hashCode * 59 + TenantId.GetHashCode();
                    if (TenantName != null)
                    hashCode = hashCode * 59 + TenantName.GetHashCode();
                    if (LocationName != null)
                    hashCode = hashCode * 59 + LocationName.GetHashCode();
                    if (DeviceStatus != null)
                    hashCode = hashCode * 59 + DeviceStatus.GetHashCode();
                    if (Ports != null)
                    hashCode = hashCode * 59 + Ports.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(TenantDevice left, TenantDevice right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TenantDevice left, TenantDevice right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

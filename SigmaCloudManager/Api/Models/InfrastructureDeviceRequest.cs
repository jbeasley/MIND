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
    /// Model for requesting a new infrastructure device
    /// </summary>
    [DataContract]
    public partial class InfrastructureDeviceRequest : IEquatable<InfrastructureDeviceRequest>
    { 
        /// <summary>
        /// The name of the device
        /// </summary>
        /// <value>String denoting the name of the device</value>
        /// <example>UK2-PE1</example>
        [Required]
        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// A description of the device
        /// </summary>
        /// <value>String denoting the description of the device</value>
        /// <example>Provider Edge device located in UK2</example>
        [DataMember(Name="description")]
        public string Description { get; set; }

        /// <summary>
        /// The model of the device
        /// </summary>
        /// <value>String denoting the model of the device</value>
        /// <example>ASR-9001</example>
        [Required]
        [DataMember(Name="deviceModel")]
        public string DeviceModel { get; set; }

        /// <summary>
        /// The role of the device
        /// </summary>
        /// <value>A string denoting the role of the device</value>
        /// <example>PE</example>
        [DataMember(Name = "deviceRole")]
        [Required]
        public string DeviceRole { get; set; }

        /// <summary>
        /// The provider network plane to which the device belongs
        /// </summary>
        /// <value>A member of the PlaneEnum enumeration</value>
        /// <example>Red</example>
        [Required]
        [DataMember(Name="planeName")]
        public PlaneEnum? PlaneName { get; set; }

        /// <summary>
        /// The location of the device
        /// </summary>
        /// <value>A string denoting the location of the device</value>
        /// <example>UK2</example>
        [Required]
        [DataMember(Name="locationName")]
        public string LocationName { get; set; }

        /// <summary>
        /// The status of the device
        /// </summary>
        /// <value>A member of the DeviceStatusTypeEnum enumeration</value>
        /// <example>Production</example>
        [Required]
        [DataMember(Name="deviceStatus")]
        public DeviceStatusTypeEnum? DeviceStatus { get; set; }

        /// <summary>
        /// Determines if layer 2 overhead should be included in the device MTU calculation
        /// </summary>
        /// <value>Boolean value denoting if layer 2 overhead is included in the device MTU calculation</value>
        /// <example>true</example>
        [DataMember(Name="useLayer2InterfaceMtu")]
        public bool? UseLayer2InterfaceMtu { get; set; }

        /// <summary>
        /// List of port requests for the device
        /// </summary>
        /// <value>List of PortRequest objects</value>
        [DataMember(Name="ports")]
        public List<PortRequest> Ports { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InfrastructureDeviceRequest {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  DeviceModel: ").Append(DeviceModel).Append("\n");
            sb.Append("  DeviceRole: ").Append(DeviceRole).Append("\n");
            sb.Append("  PlaneName: ").Append(PlaneName).Append("\n");
            sb.Append("  LocationName: ").Append(LocationName).Append("\n");
            sb.Append("  DeviceStatus: ").Append(DeviceStatus).Append("\n");
            sb.Append("  UseLayer2InterfaceMtu: ").Append(UseLayer2InterfaceMtu).Append("\n");
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
            return obj.GetType() == GetType() && Equals((InfrastructureDeviceRequest)obj);
        }

        /// <summary>
        /// Returns true if InfrastructureDeviceRequest instances are equal
        /// </summary>
        /// <param name="other">Instance of InfrastructureDeviceRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InfrastructureDeviceRequest other)
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
                    Description == other.Description ||
                    Description != null &&
                    Description.Equals(other.Description)
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
                    PlaneName == other.PlaneName ||
                    PlaneName != null &&
                    PlaneName.Equals(other.PlaneName)
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
                    UseLayer2InterfaceMtu == other.UseLayer2InterfaceMtu ||
                    UseLayer2InterfaceMtu != null &&
                    UseLayer2InterfaceMtu.Equals(other.UseLayer2InterfaceMtu)
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
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                    if (DeviceModel != null)
                    hashCode = hashCode * 59 + DeviceModel.GetHashCode();
                    if (DeviceRole != null)
                    hashCode = hashCode * 59 + DeviceRole.GetHashCode();
                    if (PlaneName != null)
                    hashCode = hashCode * 59 + PlaneName.GetHashCode();
                    if (LocationName != null)
                    hashCode = hashCode * 59 + LocationName.GetHashCode();
                    if (DeviceStatus != null)
                    hashCode = hashCode * 59 + DeviceStatus.GetHashCode();
                    if (UseLayer2InterfaceMtu != null)
                    hashCode = hashCode * 59 + UseLayer2InterfaceMtu.GetHashCode();
                    if (Ports != null)
                    hashCode = hashCode * 59 + Ports.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(InfrastructureDeviceRequest left, InfrastructureDeviceRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InfrastructureDeviceRequest left, InfrastructureDeviceRequest right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

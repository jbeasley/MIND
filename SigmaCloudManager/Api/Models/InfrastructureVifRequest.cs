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
    /// Model for requesting an infrastructure vif
    /// </summary>
    [DataContract]
    public partial class InfrastructureVifRequest : IEquatable<InfrastructureVifRequest>, IValidatableObject
    {
        public InfrastructureVifRequest()
        {
            RoutingInstance = new RoutingInstanceRequest();
        }

        /// <summary>
        /// The name of an vif role which sets certain constrains on how the vif must be configuted
        /// </summary>
        /// <value>String value denoting the name of a vif role</value>
        /// <example>PE-LBA-SERVICE</example>
        [Required]
        [DataMember(Name = "vifRoleName")]
        public string VifRoleName { get; set; }

        /// <summary>
        /// The requested vlan tag to be assigned to the vif. This property is optional. If a requested vlan tag is not specified
        /// then MIND will automatically allocate one.
        /// </summary>
        /// <value>An integer denoting the requested vlan tag</value>
        /// <example>100</example>
        [DataMember(Name = "requestedVlanTag")]
        [Range(2,4094)]
        public int? RequestedVlanTag { get; set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        /// <example>100</example>
        [DataMember(Name="contractBandwidthMbps")]
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// If specified, the vif should be associated with an existing contract bandwidth pool
        /// of the given name which is associated with another vif under the same attachment.
        /// </summary>
        /// <value>A string value of the name of an existing contract bandwidth pool</value>
        /// <exanple>db7c48eaa9864cd0b3aa6af08c8370d6</exanple>
        [DataMember(Name = "existingContractBandwidthPoolName")]
        public string ExistingContractBandwidthPoolName { get; set; }

        /// <summary>
        /// If specified, the vif should be associated with an existing routing instance
        /// of the given name.
        /// If an existing routing instance is not specified then MIND will automatically create a new routing
        /// instance for the vif.
        /// </summary>
        /// <value>A string value of the name of an existing routing instance</value>
        /// <exanple>db7c48eaa9864cd0b3aa6af08c8370d6</exanple>
        [DataMember(Name = "existingRoutingInstanceName")]
        public string ExistingRoutingInstanceName { get; set; }

        /// <summary>
        /// Optional parameters for creating a routing instances to be associated with the new vif
        /// </summary>
        /// <value>An object of type routingInstanceRequest</value>
        [DataMember(Name = "routingInstance")]
        public RoutingInstanceRequest RoutingInstance { get; set; }

        /// <summary>
        /// A list of IPv4 addresses to be assigned to the vlans of the vif
        /// </summary>
        /// <value>A list of Ipv4AddressAndMask objcets</value>
        [DataMember(Name="ipv4Addresses")]
        public List<Ipv4AddressAndMask> Ipv4Addresses { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ContractBandwidthMbps != null)
            {
                if (!string.IsNullOrEmpty(ExistingContractBandwidthPoolName))
                {
                    yield return new ValidationResult(
                        "The 'ContractBandwidthMbps' option cannot be used concurrently with the 'ExistingContractBandwidthPoolName' option. " +
                        "Either remove the 'ExistingContractBandwidthPoolName' property or remove the 'ContractBandwidthMbps' property from " +
                        "the request.");
                }
            }
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InfrastructureVifRequest {\n");
            sb.Append("  RequestedVlanTag: ").Append(RequestedVlanTag).Append("\n");
            sb.Append("  VifRoleName: ").Append(VifRoleName).Append("\n");
            sb.Append("  ContractBandwidthMbps: ").Append(ContractBandwidthMbps).Append("\n");
            sb.Append("  Ipv4Addresses: ").Append(Ipv4Addresses).Append("\n");
            sb.Append("  ExistingContractBandwidthPoolName: ").Append(ExistingContractBandwidthPoolName).Append("\n");
            sb.Append("  RoutingInstance: ").Append(RoutingInstance).Append("\n");
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
            return obj.GetType() == GetType() && Equals((InfrastructureVifRequest)obj);
        }

        /// <summary>
        /// Returns true if InfrastructureVifRequest instances are equal
        /// </summary>
        /// <param name="other">Instance of InfrastructureVifRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InfrastructureVifRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    ExistingContractBandwidthPoolName == other.ExistingContractBandwidthPoolName ||
                    ExistingContractBandwidthPoolName != null &&
                    ExistingContractBandwidthPoolName.Equals(other.ExistingContractBandwidthPoolName)
                ) &&
                (
                    VifRoleName == other.VifRoleName ||
                    VifRoleName != null &&
                    VifRoleName.Equals(other.VifRoleName)
                ) &&
                (   RequestedVlanTag == other.RequestedVlanTag ||
                    RequestedVlanTag != null &&
                    RequestedVlanTag.Equals(other.RequestedVlanTag)
                ) &&
                (
                    ContractBandwidthMbps == other.ContractBandwidthMbps ||
                    ContractBandwidthMbps != null &&
                    ContractBandwidthMbps.Equals(other.ContractBandwidthMbps)
                ) &&
                (
                    Ipv4Addresses == other.Ipv4Addresses ||
                    Ipv4Addresses != null &&
                    Ipv4Addresses.Equals(other.Ipv4Addresses)
                ) &&
                (
                    RoutingInstance == other.RoutingInstance ||
                    RoutingInstance != null &&
                    RoutingInstance.Equals(other.RoutingInstance)
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
                    if (RequestedVlanTag != null)
                    hashCode = hashCode * 59 + RequestedVlanTag.GetHashCode();
                    if (VifRoleName != null)
                    hashCode = hashCode * 59 + VifRoleName.GetHashCode();
                    if (ContractBandwidthMbps != null)
                    hashCode = hashCode * 59 + ContractBandwidthMbps.GetHashCode();
                    if (Ipv4Addresses != null)
                    hashCode = hashCode * 59 + Ipv4Addresses.GetHashCode();
                    if (ExistingContractBandwidthPoolName != null)
                    hashCode = hashCode * 59 + ExistingContractBandwidthPoolName.GetHashCode();
                    if (RoutingInstance != null)
                    hashCode = hashCode * 59 + RoutingInstance.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(InfrastructureVifRequest left, InfrastructureVifRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InfrastructureVifRequest left, InfrastructureVifRequest right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

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
    public partial class VpnTenantIpNetworkRoutingInstanceStaticRouteRequest : IEquatable<VpnTenantIpNetworkRoutingInstanceStaticRouteRequest>, IValidatableObject
    {

        /// <summary>
        /// CIDR block name of the tenant IP network
        /// </summary>
        /// <value>String value for the CIDR representation of the tenant IP network</value>
        /// <example>10.1.1.0/24 le 32</example>
        [DataMember(Name = "tenantIpNetworkCidrName")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A CIDR block range must be specified, e.g. 10.1.1.0/24. You can also include the " +
            "'less than or equal to' parameter, e.g. 10.1.1.0/24 le 32")]
        public string TenantIpNetworkCidrName { get; set; }

        /// <summary>
        /// Denotes whether the static route should be applied to all routing instances that are configured within the attachment set. This property 
        /// caanot be used concurrently with the 'RoutingInstanceName' property.
        /// </summary>
        /// <value>Boolean denoting whether the static route should be applied to all routing instances that exist within the attachment set</value>
        /// <example>false</example>
        [DataMember(Name = "addToAllRoutingInstancesInAttachmentSet")]
        public bool? AddToAllRoutingInstancesInAttachmentSet { get; set; } = false;

        /// <summary>
        /// The MIND system-generated name of a routing instance which is associated with the attachment set
        /// to which the static route is to be associated
        /// </summary>
        /// <value>String denoting the name of the routing instance</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        [DataMember(Name = "routingInstanceName")]
        public string RoutingInstanceName { get; set; }

        /// <summary>
        /// An IPv4 next-hop address towards which traffic for the tenant IP network should be forwarded. The specified next-hop must be
        /// reachable from all routing instances for which the static route is to be applied.
        /// </summary>
        /// <value>string representing the next-hop address</value>
        /// <example>192.168.0.1</example>
        [DataMember(Name="ipv4NextHopAddress")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", 
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        [Required]
        public string Ipv4NextHopAddress { get; set; }

        /// <summary>
        /// Determines whether the static route is enabled with BFD fast-failure detection.
        /// </summary>
        /// <value>Boolean value denoting whether BFD is enabled for the static route</value>
        /// <example>200</example>
        [DataMember(Name = "isBfdEnabled")]
        public bool? IsBfdEnabled { get; set; } = true;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AddToAllRoutingInstancesInAttachmentSet.HasValue && AddToAllRoutingInstancesInAttachmentSet.Value)
                if (!string.IsNullOrEmpty(RoutingInstanceName))
                {
                    yield return new ValidationResult(
                        "The name of a routing instance canot be specified when the 'AddToAllRoutingInstancesInAttachmentSet' " +
                        "argument is set to 'true'.");
                }

            if (AddToAllRoutingInstancesInAttachmentSet.HasValue && !AddToAllRoutingInstancesInAttachmentSet.Value)
                if (string.IsNullOrEmpty(RoutingInstanceName))
                {
                    yield return new ValidationResult(
                        "You must specify either the name of a routing instance with the 'RoutingInstanceName' argument, or specify that " +
                        "the static route should be associated with all routing instances in the attachment set with the " +
                        "'AddToAllRoutingInstancesInAttachmentSet' argument.");
                }
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class VpnTenantIpNetworkRoutingInstanceStaticRouteRequest {\n");
            sb.Append("  TenantIpNetworkCidrName: ").Append(TenantIpNetworkCidrName).Append("\n");
            sb.Append("  AddToAllRoutingInstancesInAttachmentSet: ").Append(AddToAllRoutingInstancesInAttachmentSet).Append("\n");
            sb.Append("  RoutingInstanceName: ").Append(RoutingInstanceName).Append("\n");
            sb.Append("  Ipv4NextHopAddress: ").Append(Ipv4NextHopAddress).Append("\n");
            sb.Append("  IsBfdEnabled: ").Append(IsBfdEnabled).Append("\n");
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
            return obj.GetType() == GetType() && Equals((VpnTenantIpNetworkRoutingInstanceStaticRouteRequest)obj);
        }

        /// <summary>
        /// Returns true if VpnTenantIpNetworkRoutingInstanceStaticRouteRequest instances are equal
        /// </summary>
        /// <param name="other">Instance of VpnTenantIpNetworkRoutingInstanceStaticRouteRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VpnTenantIpNetworkRoutingInstanceStaticRouteRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    TenantIpNetworkCidrName == other.TenantIpNetworkCidrName ||
                    TenantIpNetworkCidrName != null &&
                    TenantIpNetworkCidrName.Equals(other.TenantIpNetworkCidrName)
                ) &&
                (
                    AddToAllRoutingInstancesInAttachmentSet == other.AddToAllRoutingInstancesInAttachmentSet ||
                    AddToAllRoutingInstancesInAttachmentSet != null &&
                    AddToAllRoutingInstancesInAttachmentSet.Equals(other.AddToAllRoutingInstancesInAttachmentSet)
                ) &&
                (
                    RoutingInstanceName == other.RoutingInstanceName ||
                    RoutingInstanceName != null &&
                    RoutingInstanceName.Equals(other.RoutingInstanceName)
                ) && 
                (
                    Ipv4NextHopAddress == other.Ipv4NextHopAddress ||
                    Ipv4NextHopAddress != null &&
                    Ipv4NextHopAddress.Equals(other.Ipv4NextHopAddress)
                ) && 
                (
                    IsBfdEnabled == other.IsBfdEnabled ||
                    IsBfdEnabled != null &&
                    IsBfdEnabled.Equals(other.IsBfdEnabled)
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
                    if (TenantIpNetworkCidrName != null)
                    hashCode = hashCode * 59 + TenantIpNetworkCidrName.GetHashCode();
                    if (AddToAllRoutingInstancesInAttachmentSet != null)
                    hashCode = hashCode * 59 + AddToAllRoutingInstancesInAttachmentSet.GetHashCode();
                    if (RoutingInstanceName != null)
                    hashCode = hashCode * 59 + RoutingInstanceName.GetHashCode();
                    if (Ipv4NextHopAddress != null)
                    hashCode = hashCode * 59 + Ipv4NextHopAddress.GetHashCode();
                    if (IsBfdEnabled != null)
                    hashCode = hashCode * 59 + IsBfdEnabled.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(VpnTenantIpNetworkRoutingInstanceStaticRouteRequest left, VpnTenantIpNetworkRoutingInstanceStaticRouteRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VpnTenantIpNetworkRoutingInstanceStaticRouteRequest left, VpnTenantIpNetworkRoutingInstanceStaticRouteRequest right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

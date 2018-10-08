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
    public partial class ProviderDomainAttachment : IEquatable<ProviderDomainAttachment>
    { 
        /// <summary>
        /// ID of the attachment
        /// </summary>
        /// <value>Integer valude denoting the ID of the attachment</value>
        /// <exmple>6001</exmple>
        [DataMember(Name="attachmentId")]
        public int? AttachmentId { get; private set; }

        /// <summary>
        /// The attachment is enabled for layer 3
        /// </summary>
        /// <value>Boolean which denotes whether the attachment is enabled for layer 3</value>
        /// <example>true</example>
        [DataMember(Name="isLayer3")]
        public bool? IsLayer3 { get; private set; }

        /// <summary>
        /// The attachment is delivered as a bundle
        /// </summary>
        /// <value>Boolean value which denotes if the attachment is delivered as a bundle</value>
        /// <example>true</example>
        [DataMember(Name="isBundle")]
        public bool? IsBundle { get; private set; }

        /// <summary>
        /// For bundle attachments, the minimum number of active links in the bundle
        /// </summary>
        /// <value>Integer value denoting the minimum links in the bundle</value>
        /// <example>2</example>
        [DataMember(Name = "bundleMinLinks")]
        public int? BundleMinLinks { get; private set; }

        /// <summary>
        /// For bundle attachments, the maximum number of active links in the bundle
        /// </summary>
        /// <value>Integer value denoting the maximum links in the bundle</value>
        /// <example>2</example>
        [DataMember(Name = "bundleMaxLinks")]
        public int? BundleMaxLinks { get; private set; }

        /// <summary>
        /// The attachment is delivered as a multiport
        /// </summary>
        /// <value>Boolean denoting if the attachment is delivered as a multiport</value>
        /// <example>true</example>
        [DataMember(Name="isMultiport")]
        public bool? IsMultiport { get; private set; }

        /// <summary>
        /// The attachment is enabled with tagging
        /// </summary>
        /// <value>Boolean value denoting if the attachment is enabled with tagging</value>
        /// <example>true</example>
        [DataMember(Name="isTagged")]
        public bool? IsTagged { get; private set; }

        /// <summary>
        /// The name of the tenant owner of the attachment
        /// </summary>
        /// <value>String value for the name of the tenant</value>
        /// <example>product-group-tenant</example>
        [DataMember(Name="tenantName")]
        public string TenantName { get; private set; }

        /// <summary>
        /// The name of the provider domain infrastructure device which terminates the attachment within the provider domain
        /// </summary>
        /// <value>String value denoting the name of the infrastructure device</value>
        /// <example>UK2-PE1</example>
        [DataMember(Name="infrastructureDeviceName")]
        public string InfrastructureDeviceName { get; private set; }

        /// <summary>
        /// The name of the provider location within which the attachment is terminated
        /// </summary>
        /// <value>String value denoting the provider location</value>
        /// <example>UK2</example>
        [DataMember(Name = "LocationName")]
        public string LocationName { get; private set; }

        /// <summary>
        /// The name of the provider plane within which the attachment is terminated
        /// </summary>
        /// <value>String value denoting the provider plane</value>
        /// <example>Red</example>
        [DataMember(Name = "PlaneName")]
        public string PlaneName { get; private set; }

        /// <summary>
        /// The bandwidth of the attachment in Gbps
        /// </summary>
        /// <value>Integer value denoting the bandwidth of the attachment in Gbps</value>
        /// <example>10</example>
        [DataMember(Name="attachmentBandwidthGbps")]
        public int? AttachmentBandwidthGbps { get; private set; }

        /// <summary>
        /// The contract bandwidth pool created for the attachment
        /// </summary>
        /// <value>An object of type ContractBandwidthPool</value>
        [DataMember(Name="contractBandwidthPool")]
        public ContractBandwidthPool ContractBandwidthPool { get; private set; }

        /// <summary>
        /// The routing instance created for the attachment
        /// </summary>
        /// <value>An object of type RoutingInstance</value>
        [DataMember(Name="routingInstance")]
        public ProviderDomainRoutingInstance RoutingInstance { get; private set; }

        /// <summary>
        /// A list of interfaces created for the attachment
        /// </summary>
        /// <value>A list of Interface objects</value>
        [DataMember(Name = "interfaces")]
        public List<Interface> Interfaces { get; private set; }

        /// <summary>
        /// The maximum transmission unit supported by the attachment
        /// </summary>
        /// <value>The MTU in bytes</value>
        [DataMember(Name = "mtu")]
        public int? Mtu { get; private set; }

        /// <summary>
        /// The name of the attachment role
        /// </summary>
        /// <value>String value denoting the name of an attachment role</value>
        /// <example>PE-CE-UNTAGGED</example>
        [Required]
        [DataMember(Name = "AttachmentRoleName")]
        public string AttachmentRoleName { get; private set; }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProviderDomainAttachment {\n");
            sb.Append("  AttachmentId: ").Append(AttachmentId).Append("\n");
            sb.Append("  IsLayer3: ").Append(IsLayer3).Append("\n");
            sb.Append("  IsBundle: ").Append(IsBundle).Append("\n");
            sb.Append("  BundleMinLinks: ").Append(BundleMinLinks).Append("\n");
            sb.Append("  BundleMaxLinks: ").Append(BundleMaxLinks).Append("\n");
            sb.Append("  IsMultiport: ").Append(IsMultiport).Append("\n");
            sb.Append("  IsTagged: ").Append(IsTagged).Append("\n");
            sb.Append("  Tenant: ").Append(TenantName).Append("\n");
            sb.Append("  InfrastructureDevice: ").Append(InfrastructureDeviceName).Append("\n");
            sb.Append("  LocationName: ").Append(LocationName).Append("\n");
            sb.Append("  PlaneName: ").Append(PlaneName).Append("\n");
            sb.Append("  AttachmentBandwidthGbps: ").Append(AttachmentBandwidthGbps).Append("\n");
            sb.Append("  ContractBandwidthPool: ").Append(ContractBandwidthPool).Append("\n");
            sb.Append("  RoutingInstance: ").Append(RoutingInstance).Append("\n");
            sb.Append("  Mtu: ").Append(Mtu).Append("\n");
            sb.Append("  AttachmentRoleName: ").Append(AttachmentRoleName).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ProviderDomainAttachment)obj);
        }

        /// <summary>
        /// Returns true if ProviderDomainAttachment instances are equal
        /// </summary>
        /// <param name="other">Instance of Attachment to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProviderDomainAttachment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    AttachmentId == other.AttachmentId ||
                    AttachmentId != null &&
                    AttachmentId.Equals(other.AttachmentId)
                ) &&
                (
                    IsLayer3 == other.IsLayer3 ||
                    IsLayer3 != null &&
                    IsLayer3.Equals(other.IsLayer3)
                ) &&
                (
                    IsBundle == other.IsBundle ||
                    IsBundle != null &&
                    IsBundle.Equals(other.IsBundle)
                ) &&
                (
                    BundleMinLinks == other.BundleMinLinks ||
                    BundleMinLinks != null &&
                    BundleMinLinks.Equals(other.BundleMinLinks)
                ) &&
                (
                    BundleMaxLinks == other.BundleMaxLinks ||
                    BundleMaxLinks != null &&
                    BundleMaxLinks.Equals(other.BundleMaxLinks)
                ) &&
                (
                    IsMultiport == other.IsMultiport ||
                    IsMultiport != null &&
                    IsMultiport.Equals(other.IsMultiport)
                ) &&
                (
                    IsTagged == other.IsTagged ||
                    IsTagged != null &&
                    IsTagged.Equals(other.IsTagged)
                ) &&
                (
                    TenantName == other.TenantName ||
                    TenantName != null &&
                    TenantName.Equals(other.TenantName)
                ) &&
                (
                    InfrastructureDeviceName == other.InfrastructureDeviceName ||
                    InfrastructureDeviceName != null &&
                    InfrastructureDeviceName.Equals(other.InfrastructureDeviceName)
                ) &&
                (
                    LocationName == other.LocationName ||
                    LocationName != null &&
                    LocationName.Equals(other.LocationName)
                ) &&
                (
                    PlaneName == other.PlaneName ||
                    PlaneName != null &&
                    PlaneName.Equals(other.PlaneName)
                ) &&
                (
                    AttachmentBandwidthGbps == other.AttachmentBandwidthGbps ||
                    AttachmentBandwidthGbps != null &&
                    AttachmentBandwidthGbps.Equals(other.AttachmentBandwidthGbps)
                ) &&
                (
                    ContractBandwidthPool == other.ContractBandwidthPool ||
                    ContractBandwidthPool != null &&
                    ContractBandwidthPool.Equals(other.ContractBandwidthPool)
                ) &&
                (
                    RoutingInstance == other.RoutingInstance ||
                    RoutingInstance != null &&
                    RoutingInstance.Equals(other.RoutingInstance)
                ) &&
                (
                    Mtu == other.Mtu ||
                    Mtu != null &&
                    Mtu.Equals(other.Mtu)
                ) &&
                (
                    AttachmentRoleName == other.AttachmentRoleName ||
                    AttachmentRoleName != null &&
                    AttachmentRoleName.Equals(other.AttachmentRoleName)
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
                    if (AttachmentId != null)
                    hashCode = hashCode * 59 + AttachmentId.GetHashCode();
                    if (IsLayer3 != null)
                    hashCode = hashCode * 59 + IsLayer3.GetHashCode();
                    if (IsBundle != null)
                    hashCode = hashCode * 59 + IsBundle.GetHashCode();
                    if (BundleMinLinks != null)
                    hashCode = hashCode * 59 + BundleMinLinks.GetHashCode();
                    if (BundleMaxLinks != null)
                    hashCode = hashCode * 59 + BundleMaxLinks.GetHashCode();
                    if (IsMultiport != null)
                    hashCode = hashCode * 59 + IsMultiport.GetHashCode();
                    if (IsTagged != null)
                    hashCode = hashCode * 59 + IsTagged.GetHashCode();
                    if (TenantName != null)
                    hashCode = hashCode * 59 + TenantName.GetHashCode();
                    if (InfrastructureDeviceName != null)
                    hashCode = hashCode * 59 + InfrastructureDeviceName.GetHashCode();
                    if (LocationName != null)
                    hashCode = hashCode * 59 + LocationName.GetHashCode();
                    if (PlaneName != null)
                    hashCode = hashCode * 59 + PlaneName.GetHashCode();
                    if (AttachmentBandwidthGbps != null)
                    hashCode = hashCode * 59 + AttachmentBandwidthGbps.GetHashCode();
                    if (ContractBandwidthPool != null)
                    hashCode = hashCode * 59 + ContractBandwidthPool.GetHashCode();
                    if (RoutingInstance != null)
                    hashCode = hashCode * 59 + RoutingInstance.GetHashCode();
                    if (Mtu != null)
                    hashCode = hashCode * 59 + Mtu.GetHashCode();
                    if (AttachmentRoleName != null)
                    hashCode = hashCode * 59 + AttachmentRoleName.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ProviderDomainAttachment left, ProviderDomainAttachment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProviderDomainAttachment left, ProviderDomainAttachment right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

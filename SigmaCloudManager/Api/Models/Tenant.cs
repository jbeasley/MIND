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
    /// Model of a tenant
    /// </summary>
    [DataContract]
    public partial class Tenant : IEquatable<Tenant>
    { 
        /// <summary>
        /// Gets or Sets TenantId
        /// </summary>
        /// <value>An integer denoting the ID of the tenant</value>
        /// <example>1001</example>
        [DataMember(Name="tenantId")]
        public Int32? TenantId { get; private set; }

        /// <summary>
        /// The name of the tenant
        /// </summary>
        /// <value>The name of the tenant</value>
        /// <example>product-group-tenant</example>
        [DataMember(Name="name")]
        public string Name { get; private set; }
        
        /// <summary>
        /// List of IP networks which are registered to the tenant
        /// </summary>
        /// <value>A list of TenantIpNetwork objects</value>
        [DataMember(Name="tenantIpNetworks")]
        public List<TenantIpNetwork> TenantIpNetworks { get; private set; }

        /// <summary>
        /// A list of provider attachments which have been created for the tenant
        /// </summary>
        /// <value>A list of Attachment objects</value>
        [DataMember(Name = "attachments")]
        public List<ProviderDomainAttachment> Attachments { get; private set; }

        /// <summary>
        /// A list of provider vifs which are assigned to the tenant.
        /// </summary>
        /// <value>A list of Vif objects</value>
        [DataMember(Name = "vifs")]
        public List<ProviderVif> Vifs { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Tenant {\n");
            sb.Append("  TenantId: ").Append(TenantId).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  TenantIpNetworks: ").Append(TenantIpNetworks).Append("\n");
            sb.Append("  Vifs: ").Append(Vifs).Append("\n");
            sb.Append("  Attachments: ").Append(Attachments).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Tenant)obj);
        }

        /// <summary>
        /// Returns true if Tenant instances are equal
        /// </summary>
        /// <param name="other">Instance of Tenant to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Tenant other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    TenantId == other.TenantId ||
                    TenantId != null &&
                    TenantId.Equals(other.TenantId)
                ) &&
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) &&
                (
                    TenantIpNetworks == other.TenantIpNetworks ||
                    TenantIpNetworks != null &&
                    TenantIpNetworks.Equals(other.TenantIpNetworks)
                ) &&
                (
                    Vifs == other.Vifs ||
                    Vifs != null &&
                    Vifs.Equals(other.Vifs)
                ) &&
                (
                    Attachments == other.Attachments ||
                    Attachments != null &&
                    Attachments.Equals(other.Attachments)
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
                    if (TenantId != null)
                    hashCode = hashCode * 59 + TenantId.GetHashCode();
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (TenantIpNetworks != null)
                    hashCode = hashCode * 59 + TenantIpNetworks.GetHashCode();
                    if (Vifs != null)
                    hashCode = hashCode * 59 + Vifs.GetHashCode();
                    if (Attachments != null)
                    hashCode = hashCode * 59 + Attachments.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Tenant left, Tenant right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Tenant left, Tenant right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

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
    /// Model of a location in the provider domain
    /// </summary>
    [DataContract]
    public partial class ProviderDomainLocation : IEquatable<ProviderDomainLocation>
    { 
        /// <summary>
        /// The ID of the provider domain location
        /// </summary>
        /// <value>An integer denoting the provider domain location ID</value>
        /// <example>3001</example>
        [DataMember(Name = "locationId")]
        public int? LocationId { get; private set; }

        /// <summary>
        /// The site name of the provider domain location.
        /// </summary>
        /// <value>A string value denoting the name of the provider domain llcation</value>
        /// <example>UK2</example>
        [DataMember(Name = "siteName")]
        public string SiteName { get; private set; }
 
        /// <summary>
        /// The name of the geographic subregion to which the provider domain location belongs.
        /// </summary>
        /// <value>String value denoting the name of the geographic subregion</value>
        /// <example>UK</example>
        [DataMember(Name = "subRegionName")]
        public string SubRegionName { get; private set; }

        /// <summary>
        /// The name of the major geographic rregion to which the provider domain location belongs.
        /// </summary>
        /// <value>String value denoting the name of the major geographic region</value>
        /// <example>EMEA</example>
        [DataMember(Name = "regionName")]
        public string RegionName { get; private set; }

        /// <summary>
        /// The name of the locale community assigned to the provider domain location.
        /// </summary>
        /// <value>String value denoting the name of the locale community</value>
        /// <example>8718:10010</example>
        [DataMember(Name = "localeCommunityName")]
        public string LocaleCommunityName { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProviderDomainLocation {\n");
            sb.Append("  LocationId: ").Append(LocationId).Append("\n");
            sb.Append("  SiteName: ").Append(SiteName).Append("\n");
            sb.Append("  SubRegionName: ").Append(SubRegionName).Append("\n");
            sb.Append("  RegionName: ").Append(RegionName).Append("\n");
            sb.Append("  LocaleCommunityName: ").Append(LocaleCommunityName).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ProviderDomainLocation)obj);
        }

        /// <summary>
        /// Returns true if ProviderDomainLocation instances are equal
        /// </summary>
        /// <param name="other">Instance of ProviderDomainLocation to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProviderDomainLocation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    LocationId == other.LocationId ||
                    LocationId != null &&
                    LocationId.Equals(other.LocationId)
                ) &&
                (
                    SiteName == other.SiteName ||
                    SiteName != null &&
                    SiteName.Equals(other.SiteName)
                ) &&
                (
                    SubRegionName == other.SubRegionName ||
                    SubRegionName != null &&
                    SubRegionName.Equals(other.SubRegionName)
                ) &&
                (
                    RegionName == other.RegionName ||
                    RegionName != null &&
                    RegionName.Equals(other.RegionName)
                ) &&
                (
                    LocaleCommunityName == other.LocaleCommunityName ||
                    LocaleCommunityName != null &&
                    LocaleCommunityName.Equals(other.LocaleCommunityName)
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
                    if (LocationId != null)
                    hashCode = hashCode * 59 + LocationId.GetHashCode();
                    if (SubRegionName != null)
                    hashCode = hashCode * 59 + SubRegionName.GetHashCode();
                    if (SiteName != null)
                    hashCode = hashCode * 59 + SiteName.GetHashCode();
                    if (RegionName != null)
                    hashCode = hashCode * 59 + RegionName.GetHashCode();
                    if (LocaleCommunityName != null)
                    hashCode = hashCode * 59 + LocaleCommunityName.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ProviderDomainLocation left, ProviderDomainLocation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProviderDomainLocation left, ProviderDomainLocation right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

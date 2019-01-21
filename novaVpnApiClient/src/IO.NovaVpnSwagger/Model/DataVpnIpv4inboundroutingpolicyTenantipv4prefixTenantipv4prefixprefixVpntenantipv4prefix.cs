/* 
 * vpn
 *
 * This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
 *
 * OpenAPI spec version: 1.0.0.1
 * Contact: jonathan.beasley@refinitiv.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.NovaVpnSwagger.Client.SwaggerDateConverter;

namespace IO.NovaVpnSwagger.Model
{
    /// <summary>
    /// DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix
    /// </summary>
    [DataContract]
    public partial class DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix :  IEquatable<DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix" /> class.
        /// </summary>
        /// <param name="prefix">An IPv4 prefix and length in CIDR form, x.x.x.x/n (leaf).</param>
        /// <param name="lessThanOrEqualToLength">Include prefix lengths up to and including the specified length. (leaf).</param>
        /// <param name="localIpRoutingPreference">The IP routing preference for the prefix. (leaf).</param>
        /// <param name="tenantCommunity">List of BGP communities which are associated with the prefix. (list).</param>
        public DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix(string prefix = default(string), int? lessThanOrEqualToLength = default(int?), int? localIpRoutingPreference = default(int?), List<DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity> tenantCommunity = default(List<DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity>))
        {
            this.Prefix = prefix;
            this.LessThanOrEqualToLength = lessThanOrEqualToLength;
            this.LocalIpRoutingPreference = localIpRoutingPreference;
            this.TenantCommunity = tenantCommunity;
        }
        
        /// <summary>
        /// An IPv4 prefix and length in CIDR form, x.x.x.x/n (leaf)
        /// </summary>
        /// <value>An IPv4 prefix and length in CIDR form, x.x.x.x/n (leaf)</value>
        [DataMember(Name="prefix", EmitDefaultValue=false)]
        public string Prefix { get; set; }

        /// <summary>
        /// Include prefix lengths up to and including the specified length. (leaf)
        /// </summary>
        /// <value>Include prefix lengths up to and including the specified length. (leaf)</value>
        [DataMember(Name="less-than-or-equal-to-length", EmitDefaultValue=false)]
        public int? LessThanOrEqualToLength { get; set; }

        /// <summary>
        /// The IP routing preference for the prefix. (leaf)
        /// </summary>
        /// <value>The IP routing preference for the prefix. (leaf)</value>
        [DataMember(Name="local-ip-routing-preference", EmitDefaultValue=false)]
        public int? LocalIpRoutingPreference { get; set; }

        /// <summary>
        /// List of BGP communities which are associated with the prefix. (list)
        /// </summary>
        /// <value>List of BGP communities which are associated with the prefix. (list)</value>
        [DataMember(Name="tenant-community", EmitDefaultValue=false)]
        public List<DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity> TenantCommunity { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix {\n");
            sb.Append("  Prefix: ").Append(Prefix).Append("\n");
            sb.Append("  LessThanOrEqualToLength: ").Append(LessThanOrEqualToLength).Append("\n");
            sb.Append("  LocalIpRoutingPreference: ").Append(LocalIpRoutingPreference).Append("\n");
            sb.Append("  TenantCommunity: ").Append(TenantCommunity).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix);
        }

        /// <summary>
        /// Returns true if DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix instances are equal
        /// </summary>
        /// <param name="input">Instance of DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Prefix == input.Prefix ||
                    (this.Prefix != null &&
                    this.Prefix.Equals(input.Prefix))
                ) && 
                (
                    this.LessThanOrEqualToLength == input.LessThanOrEqualToLength ||
                    (this.LessThanOrEqualToLength != null &&
                    this.LessThanOrEqualToLength.Equals(input.LessThanOrEqualToLength))
                ) && 
                (
                    this.LocalIpRoutingPreference == input.LocalIpRoutingPreference ||
                    (this.LocalIpRoutingPreference != null &&
                    this.LocalIpRoutingPreference.Equals(input.LocalIpRoutingPreference))
                ) && 
                (
                    this.TenantCommunity == input.TenantCommunity ||
                    this.TenantCommunity != null &&
                    this.TenantCommunity.SequenceEqual(input.TenantCommunity)
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
                int hashCode = 41;
                if (this.Prefix != null)
                    hashCode = hashCode * 59 + this.Prefix.GetHashCode();
                if (this.LessThanOrEqualToLength != null)
                    hashCode = hashCode * 59 + this.LessThanOrEqualToLength.GetHashCode();
                if (this.LocalIpRoutingPreference != null)
                    hashCode = hashCode * 59 + this.LocalIpRoutingPreference.GetHashCode();
                if (this.TenantCommunity != null)
                    hashCode = hashCode * 59 + this.TenantCommunity.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}

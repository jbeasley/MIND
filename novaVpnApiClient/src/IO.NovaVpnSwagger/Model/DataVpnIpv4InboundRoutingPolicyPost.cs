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
    /// DataVpnIpv4InboundRoutingPolicyPost
    /// </summary>
    [DataContract]
    public partial class DataVpnIpv4InboundRoutingPolicyPost :  IEquatable<DataVpnIpv4InboundRoutingPolicyPost>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataVpnIpv4InboundRoutingPolicyPost" /> class.
        /// </summary>
        /// <param name="vpntenantIpv4Prefix">List of IPv4 prefixes for routes towards Tenant Networks. (list).</param>
        /// <param name="vpntenantCommunity">List of BGP communities for routes towards Tenant Networks. (list).</param>
        public DataVpnIpv4InboundRoutingPolicyPost(List<DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix> vpntenantIpv4Prefix = default(List<DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix>), List<DataVpnIpv4inboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity> vpntenantCommunity = default(List<DataVpnIpv4inboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity>))
        {
            this.VpntenantIpv4Prefix = vpntenantIpv4Prefix;
            this.VpntenantCommunity = vpntenantCommunity;
        }
        
        /// <summary>
        /// List of IPv4 prefixes for routes towards Tenant Networks. (list)
        /// </summary>
        /// <value>List of IPv4 prefixes for routes towards Tenant Networks. (list)</value>
        [DataMember(Name="vpn:tenant-ipv4-prefix", EmitDefaultValue=false)]
        public List<DataVpnIpv4inboundroutingpolicyTenantipv4prefixTenantipv4prefixprefixVpntenantipv4prefix> VpntenantIpv4Prefix { get; set; }

        /// <summary>
        /// List of BGP communities for routes towards Tenant Networks. (list)
        /// </summary>
        /// <value>List of BGP communities for routes towards Tenant Networks. (list)</value>
        [DataMember(Name="vpn:tenant-community", EmitDefaultValue=false)]
        public List<DataVpnIpv4inboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity> VpntenantCommunity { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataVpnIpv4InboundRoutingPolicyPost {\n");
            sb.Append("  VpntenantIpv4Prefix: ").Append(VpntenantIpv4Prefix).Append("\n");
            sb.Append("  VpntenantCommunity: ").Append(VpntenantCommunity).Append("\n");
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
            return this.Equals(input as DataVpnIpv4InboundRoutingPolicyPost);
        }

        /// <summary>
        /// Returns true if DataVpnIpv4InboundRoutingPolicyPost instances are equal
        /// </summary>
        /// <param name="input">Instance of DataVpnIpv4InboundRoutingPolicyPost to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DataVpnIpv4InboundRoutingPolicyPost input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.VpntenantIpv4Prefix == input.VpntenantIpv4Prefix ||
                    this.VpntenantIpv4Prefix != null &&
                    this.VpntenantIpv4Prefix.SequenceEqual(input.VpntenantIpv4Prefix)
                ) && 
                (
                    this.VpntenantCommunity == input.VpntenantCommunity ||
                    this.VpntenantCommunity != null &&
                    this.VpntenantCommunity.SequenceEqual(input.VpntenantCommunity)
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
                if (this.VpntenantIpv4Prefix != null)
                    hashCode = hashCode * 59 + this.VpntenantIpv4Prefix.GetHashCode();
                if (this.VpntenantCommunity != null)
                    hashCode = hashCode * 59 + this.VpntenantCommunity.GetHashCode();
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

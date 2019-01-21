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
    /// DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber
    /// </summary>
    [DataContract]
    public partial class DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber :  IEquatable<DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber" /> class.
        /// </summary>
        /// <param name="vpnnumber">(leaf).</param>
        public DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber(int? vpnnumber = default(int?))
        {
            this.Vpnnumber = vpnnumber;
        }
        
        /// <summary>
        /// (leaf)
        /// </summary>
        /// <value>(leaf)</value>
        [DataMember(Name="vpn:number", EmitDefaultValue=false)]
        public int? Vpnnumber { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber {\n");
            sb.Append("  Vpnnumber: ").Append(Vpnnumber).Append("\n");
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
            return this.Equals(input as DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber);
        }

        /// <summary>
        /// Returns true if DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber instances are equal
        /// </summary>
        /// <param name="input">Instance of DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DataVpnIpv4InboundRoutingPolicyTenantIpv4PrefixTenantIpv4PrefixPrefixTenantCommunityTenantCommunityAutonomousSystemNumberTenantCommunityNumberNumber input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Vpnnumber == input.Vpnnumber ||
                    (this.Vpnnumber != null &&
                    this.Vpnnumber.Equals(input.Vpnnumber))
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
                if (this.Vpnnumber != null)
                    hashCode = hashCode * 59 + this.Vpnnumber.GetHashCode();
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

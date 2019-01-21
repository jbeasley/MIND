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
    /// DataVpnIpv4outboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity
    /// </summary>
    [DataContract]
    public partial class DataVpnIpv4outboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity :  IEquatable<DataVpnIpv4outboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataVpnIpv4outboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity" /> class.
        /// </summary>
        /// <param name="autonomousSystemNumber">Autonomous System Number component of the community (leaf).</param>
        /// <param name="number">(leaf).</param>
        /// <param name="advertisedIpRoutingPreference">The advertised IP routing preference for the community. (leaf).</param>
        public DataVpnIpv4outboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity(int? autonomousSystemNumber = default(int?), int? number = default(int?), int? advertisedIpRoutingPreference = default(int?))
        {
            this.AutonomousSystemNumber = autonomousSystemNumber;
            this.Number = number;
            this.AdvertisedIpRoutingPreference = advertisedIpRoutingPreference;
        }
        
        /// <summary>
        /// Autonomous System Number component of the community (leaf)
        /// </summary>
        /// <value>Autonomous System Number component of the community (leaf)</value>
        [DataMember(Name="autonomous-system-number", EmitDefaultValue=false)]
        public int? AutonomousSystemNumber { get; set; }

        /// <summary>
        /// (leaf)
        /// </summary>
        /// <value>(leaf)</value>
        [DataMember(Name="number", EmitDefaultValue=false)]
        public int? Number { get; set; }

        /// <summary>
        /// The advertised IP routing preference for the community. (leaf)
        /// </summary>
        /// <value>The advertised IP routing preference for the community. (leaf)</value>
        [DataMember(Name="advertised-ip-routing-preference", EmitDefaultValue=false)]
        public int? AdvertisedIpRoutingPreference { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataVpnIpv4outboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity {\n");
            sb.Append("  AutonomousSystemNumber: ").Append(AutonomousSystemNumber).Append("\n");
            sb.Append("  Number: ").Append(Number).Append("\n");
            sb.Append("  AdvertisedIpRoutingPreference: ").Append(AdvertisedIpRoutingPreference).Append("\n");
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
            return this.Equals(input as DataVpnIpv4outboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity);
        }

        /// <summary>
        /// Returns true if DataVpnIpv4outboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity instances are equal
        /// </summary>
        /// <param name="input">Instance of DataVpnIpv4outboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DataVpnIpv4outboundroutingpolicyTenantcommunityTenantcommunityautonomoussystemnumberTenantcommunitynumberVpntenantcommunity input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.AutonomousSystemNumber == input.AutonomousSystemNumber ||
                    (this.AutonomousSystemNumber != null &&
                    this.AutonomousSystemNumber.Equals(input.AutonomousSystemNumber))
                ) && 
                (
                    this.Number == input.Number ||
                    (this.Number != null &&
                    this.Number.Equals(input.Number))
                ) && 
                (
                    this.AdvertisedIpRoutingPreference == input.AdvertisedIpRoutingPreference ||
                    (this.AdvertisedIpRoutingPreference != null &&
                    this.AdvertisedIpRoutingPreference.Equals(input.AdvertisedIpRoutingPreference))
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
                if (this.AutonomousSystemNumber != null)
                    hashCode = hashCode * 59 + this.AutonomousSystemNumber.GetHashCode();
                if (this.Number != null)
                    hashCode = hashCode * 59 + this.Number.GetHashCode();
                if (this.AdvertisedIpRoutingPreference != null)
                    hashCode = hashCode * 59 + this.AdvertisedIpRoutingPreference.GetHashCode();
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

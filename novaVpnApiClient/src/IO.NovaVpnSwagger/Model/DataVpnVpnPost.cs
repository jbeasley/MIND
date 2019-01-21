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
    /// DataVpnVpnPost
    /// </summary>
    [DataContract]
    public partial class DataVpnVpnPost :  IEquatable<DataVpnVpnPost>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataVpnVpnPost" /> class.
        /// </summary>
        /// <param name="vpninstance">List of VPN instances (list).</param>
        public DataVpnVpnPost(List<DataVpnVpnInstanceInstancenameVpninstance> vpninstance = default(List<DataVpnVpnInstanceInstancenameVpninstance>))
        {
            this.Vpninstance = vpninstance;
        }
        
        /// <summary>
        /// List of VPN instances (list)
        /// </summary>
        /// <value>List of VPN instances (list)</value>
        [DataMember(Name="vpn:instance", EmitDefaultValue=false)]
        public List<DataVpnVpnInstanceInstancenameVpninstance> Vpninstance { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataVpnVpnPost {\n");
            sb.Append("  Vpninstance: ").Append(Vpninstance).Append("\n");
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
            return this.Equals(input as DataVpnVpnPost);
        }

        /// <summary>
        /// Returns true if DataVpnVpnPost instances are equal
        /// </summary>
        /// <param name="input">Instance of DataVpnVpnPost to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DataVpnVpnPost input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Vpninstance == input.Vpninstance ||
                    this.Vpninstance != null &&
                    this.Vpninstance.SequenceEqual(input.Vpninstance)
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
                if (this.Vpninstance != null)
                    hashCode = hashCode * 59 + this.Vpninstance.GetHashCode();
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

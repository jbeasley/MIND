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
    /// DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName
    /// </summary>
    [DataContract]
    public partial class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName :  IEquatable<DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName" /> class.
        /// </summary>
        /// <param name="vpnvpnAttachmentSet">VRF membership of the VPN (list).</param>
        public DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName(List<DataVpnVpnInstanceInstancenameVpnattachmentsetVpnattachmentsetnameVpnvpnattachmentset> vpnvpnAttachmentSet = default(List<DataVpnVpnInstanceInstancenameVpnattachmentsetVpnattachmentsetnameVpnvpnattachmentset>))
        {
            this.VpnvpnAttachmentSet = vpnvpnAttachmentSet;
        }
        
        /// <summary>
        /// VRF membership of the VPN (list)
        /// </summary>
        /// <value>VRF membership of the VPN (list)</value>
        [DataMember(Name="vpn:vpn-attachment-set", EmitDefaultValue=false)]
        public List<DataVpnVpnInstanceInstancenameVpnattachmentsetVpnattachmentsetnameVpnvpnattachmentset> VpnvpnAttachmentSet { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName {\n");
            sb.Append("  VpnvpnAttachmentSet: ").Append(VpnvpnAttachmentSet).Append("\n");
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
            return this.Equals(input as DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName);
        }

        /// <summary>
        /// Returns true if DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName instances are equal
        /// </summary>
        /// <param name="input">Instance of DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DataVpnVpnInstanceInstanceNameVpnAttachmentSetVpnAttachmentSetName input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.VpnvpnAttachmentSet == input.VpnvpnAttachmentSet ||
                    this.VpnvpnAttachmentSet != null &&
                    this.VpnvpnAttachmentSet.SequenceEqual(input.VpnvpnAttachmentSet)
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
                if (this.VpnvpnAttachmentSet != null)
                    hashCode = hashCode * 59 + this.VpnvpnAttachmentSet.GetHashCode();
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

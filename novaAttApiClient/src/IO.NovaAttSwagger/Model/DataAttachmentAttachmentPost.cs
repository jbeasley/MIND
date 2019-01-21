/* 
 * attachment
 *
 * This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
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
using SwaggerDateConverter = IO.NovaAttSwagger.Client.SwaggerDateConverter;

namespace IO.NovaAttSwagger.Model
{
    /// <summary>
    /// DataAttachmentAttachmentPost
    /// </summary>
    [DataContract]
    public partial class DataAttachmentAttachmentPost :  IEquatable<DataAttachmentAttachmentPost>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataAttachmentAttachmentPost" /> class.
        /// </summary>
        /// <param name="attachmentpe">List of PE devices (list).</param>
        public DataAttachmentAttachmentPost(List<DataAttachmentAttachmentPePepenameAttachmentpe> attachmentpe = default(List<DataAttachmentAttachmentPePepenameAttachmentpe>))
        {
            this.Attachmentpe = attachmentpe;
        }
        
        /// <summary>
        /// List of PE devices (list)
        /// </summary>
        /// <value>List of PE devices (list)</value>
        [DataMember(Name="attachment:pe", EmitDefaultValue=false)]
        public List<DataAttachmentAttachmentPePepenameAttachmentpe> Attachmentpe { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataAttachmentAttachmentPost {\n");
            sb.Append("  Attachmentpe: ").Append(Attachmentpe).Append("\n");
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
            return this.Equals(input as DataAttachmentAttachmentPost);
        }

        /// <summary>
        /// Returns true if DataAttachmentAttachmentPost instances are equal
        /// </summary>
        /// <param name="input">Instance of DataAttachmentAttachmentPost to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DataAttachmentAttachmentPost input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Attachmentpe == input.Attachmentpe ||
                    this.Attachmentpe != null &&
                    this.Attachmentpe.SequenceEqual(input.Attachmentpe)
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
                if (this.Attachmentpe != null)
                    hashCode = hashCode * 59 + this.Attachmentpe.GetHashCode();
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

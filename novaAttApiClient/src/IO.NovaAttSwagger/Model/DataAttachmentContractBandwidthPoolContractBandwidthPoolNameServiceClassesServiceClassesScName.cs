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
    /// DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName
    /// </summary>
    [DataContract]
    public partial class DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName :  IEquatable<DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName" /> class.
        /// </summary>
        /// <param name="attachmentserviceClasses">List of Service Classes supported by this contract bandwidth pool (list).</param>
        public DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName(List<DataAttachmentContractbandwidthpoolContractbandwidthpoolnameServiceclassesServiceclassesscnameAttachmentserviceclasses> attachmentserviceClasses = default(List<DataAttachmentContractbandwidthpoolContractbandwidthpoolnameServiceclassesServiceclassesscnameAttachmentserviceclasses>))
        {
            this.AttachmentserviceClasses = attachmentserviceClasses;
        }
        
        /// <summary>
        /// List of Service Classes supported by this contract bandwidth pool (list)
        /// </summary>
        /// <value>List of Service Classes supported by this contract bandwidth pool (list)</value>
        [DataMember(Name="attachment:service-classes", EmitDefaultValue=false)]
        public List<DataAttachmentContractbandwidthpoolContractbandwidthpoolnameServiceclassesServiceclassesscnameAttachmentserviceclasses> AttachmentserviceClasses { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName {\n");
            sb.Append("  AttachmentserviceClasses: ").Append(AttachmentserviceClasses).Append("\n");
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
            return this.Equals(input as DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName);
        }

        /// <summary>
        /// Returns true if DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName instances are equal
        /// </summary>
        /// <param name="input">Instance of DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DataAttachmentContractBandwidthPoolContractBandwidthPoolNameServiceClassesServiceClassesScName input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.AttachmentserviceClasses == input.AttachmentserviceClasses ||
                    this.AttachmentserviceClasses != null &&
                    this.AttachmentserviceClasses.SequenceEqual(input.AttachmentserviceClasses)
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
                if (this.AttachmentserviceClasses != null)
                    hashCode = hashCode * 59 + this.AttachmentserviceClasses.GetHashCode();
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

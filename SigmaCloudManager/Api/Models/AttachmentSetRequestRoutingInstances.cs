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
    /// 
    /// </summary>
    [DataContract]
    public partial class AttachmentSetRequestRoutingInstances : IEquatable<AttachmentSetRequestRoutingInstances>
    { 
        /// <summary>
        /// The ID of the routing instance
        /// </summary>
        /// <value>The ID of the routing instance</value>
        [DataMember(Name="routingInstanceId")]
        public int? RoutingInstanceId { get; set; }

        /// <summary>
        /// Default preference applied to IPv4 and IPv6 routes within the routing instance
        /// </summary>
        /// <value>Default preference applied to IPv4 and IPv6 routes within the routing instance</value>
        [DataMember(Name="locaIpRoutingPreference")]
        public int? LocaIpRoutingPreference { get; set; }

        /// <summary>
        /// Default preference of IPv4 and IPv6 routes advertised from the routing instance
        /// </summary>
        /// <value>Default preference of IPv4 and IPv6 routes advertised from the routing instance</value>
        [DataMember(Name="advertisedIpRoutingPreference")]
        public int? AdvertisedIpRoutingPreference { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AttachmentSetRequestRoutingInstances {\n");
            sb.Append("  RoutingInstanceId: ").Append(RoutingInstanceId).Append("\n");
            sb.Append("  LocaIpRoutingPreference: ").Append(LocaIpRoutingPreference).Append("\n");
            sb.Append("  AdvertisedIpRoutingPreference: ").Append(AdvertisedIpRoutingPreference).Append("\n");
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
            return obj.GetType() == GetType() && Equals((AttachmentSetRequestRoutingInstances)obj);
        }

        /// <summary>
        /// Returns true if AttachmentSetRequestRoutingInstances instances are equal
        /// </summary>
        /// <param name="other">Instance of AttachmentSetRequestRoutingInstances to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AttachmentSetRequestRoutingInstances other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    RoutingInstanceId == other.RoutingInstanceId ||
                    RoutingInstanceId != null &&
                    RoutingInstanceId.Equals(other.RoutingInstanceId)
                ) && 
                (
                    LocaIpRoutingPreference == other.LocaIpRoutingPreference ||
                    LocaIpRoutingPreference != null &&
                    LocaIpRoutingPreference.Equals(other.LocaIpRoutingPreference)
                ) && 
                (
                    AdvertisedIpRoutingPreference == other.AdvertisedIpRoutingPreference ||
                    AdvertisedIpRoutingPreference != null &&
                    AdvertisedIpRoutingPreference.Equals(other.AdvertisedIpRoutingPreference)
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
                    if (RoutingInstanceId != null)
                    hashCode = hashCode * 59 + RoutingInstanceId.GetHashCode();
                    if (LocaIpRoutingPreference != null)
                    hashCode = hashCode * 59 + LocaIpRoutingPreference.GetHashCode();
                    if (AdvertisedIpRoutingPreference != null)
                    hashCode = hashCode * 59 + AdvertisedIpRoutingPreference.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(AttachmentSetRequestRoutingInstances left, AttachmentSetRequestRoutingInstances right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AttachmentSetRequestRoutingInstances left, AttachmentSetRequestRoutingInstances right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}

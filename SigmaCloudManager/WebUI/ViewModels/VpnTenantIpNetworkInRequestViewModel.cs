

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

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for requesting a tenant IP network association with the inbound policy of an attachment set
    /// </summary>
    public class VpnTenantIpNetworkInRequestViewModel: IValidatableObject
    {
        /// <summary>
        /// The ID of the tenant owner of the tenant IP network to be added to the BGP peers of the attachment set
        /// </summary>
        /// <value>An integer denoting the ID of the tenant owner</value>
        /// <example>1001</example>
        [Required(ErrorMessage="The ID of the tenant owner of the tenant IP network must be specified")]
        public int? TenantId { get; private set; }

        /// <summary>
        /// CIDR block name of the tenant IP network
        /// </summary>
        /// <value>String value for the CIDR representation of the tenant IP network</value>
        /// <example>10.1.1.0/24 le 32</example>
        [Display(Name = "IP Network CIDR Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A CIDR block range must be specified, e.g. 10.1.1.0/24. You can also include the " +
            "'less than or equal to' parameter, e.g. 10.1.1.0/24 le 32")]
        public string TenantIpNetworkCidrName { get; set; }

        /// <summary>
        /// Denotes whether the tenant IP network should be learned from all BGP peers that are configured within the attachment set. This property 
        /// cannot be used concurrently with the 'Ipv4PeerAddress' property.
        /// </summary>
        /// <value>Boolean denoting whether the tenant IP network should be learned from all BGP peers that exist within the attachment set</value>
        /// <example>true</example>
        [Display(Name = "Add to all BGP Peers in AttachmentSet")]
        public bool? AddToAllBgpPeersInAttachmentSet { get; set; } = true;

        /// <summary>
        /// An IPv4 BGP peer address from which the tenant IP network should be learned. THe specified BGP peer must be configured and exist
        /// within the attachment set. This property cannot be used concurrently with the 'AddToAllBgpPeersInAttachmentSet' property.
        /// </summary>
        /// <value>string representing the address of an existing configured IPv4 BGP peer</value>
        /// <example>192.168.0.1</example>
        [Display(Name="IPv4 Peer Address")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", 
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        public string Ipv4PeerAddress { get; set; }

        /// <summary>
        /// The local IP routing preference to be applied to the route towards the tenant IP network
        /// </summary>
        /// <value>Integer representing the local IP routing preference</value>
        /// <example>200</example>
        [Display(Name = "Local IP Routing Preference")]
        [Range(1, 500, ErrorMessage = "Local IP routing preference must be a number between 1 and 500")]
        public int? LocalIpRoutingPreference { get; set; } = 100;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AddToAllBgpPeersInAttachmentSet.HasValue && AddToAllBgpPeersInAttachmentSet.Value)
                if (!string.IsNullOrEmpty(Ipv4PeerAddress))
                {
                    yield return new ValidationResult(
                        "A BGP peer address cannot be specified when the 'AddToAllBgpPeersInAttachmentSet' " +
                        "argument is not specified or is set to 'true'. Include the 'AddToAllBgpPeersInAttachmentSet' argument with a value of " +
                        "'false' in the request if you wish to associate the IP network with a specific BGP peer.");
                }

            if (AddToAllBgpPeersInAttachmentSet.HasValue && !AddToAllBgpPeersInAttachmentSet.Value)
                if (string.IsNullOrEmpty(Ipv4PeerAddress))
                {
                    yield return new ValidationResult(
                        "You must specify either a BGP peer address with the 'Ipv4PeerAddress' argument, or specify that " +
                        "the IP network should be associated with all BGP peers in the attachment set with the " +
                        "'AddToAllBgpPeersInAttachmentSet' argument.");
                }
        }
    }
}

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
    /// Model for requesting the components needed for a layer 3 tenant attachment to the provider domain
    /// </summary>
    public class ProviderDomainAttachmentL3RequestViewModel
    {
        /// <summary>
        /// The calculated number of IP addresses required for the new attachment.
        /// This value is calculated on the server-side and depends upon the selected  attachment role.
        /// The value is then used on the client-side to display a number of text boxes which allow the user
        /// to enter the required number of IP addresses.
        /// </summary>
        public int? NumIpAddressesRequired { get; set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        /// <example>100</example>
        [Display(Name = "Contract Bandwidth")]
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets received from the tenant domain should be trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the required trust state</value>
        /// <example>false</example>
        [Display(Name = "Trust CoS and DSCP")]
        public bool TrustReceivedCosAndDscp { get; set; }

        /// <summary>
        /// A list of IPv4 addresses to be assigned to the interfaces of the attachment.
        /// </summary>
        /// <value>A list of Ipv4AddressAndMask objcets</value>
        public List<Ipv4AddressAndMaskViewModel> Ipv4Addresses { get; set; }
    }
}


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
    /// Model for updating a routing instance association with an attachment set
    /// </summary>
    public class AttachmentSetRoutingInstanceUpdateViewModel
    {
        /// <summary>
        /// Default local preference applied to IPv4 and IPv6 routes within the routing instance
        /// </summary>
        /// <value>Integer value denoting the local IP routing prefernece</value>
        /// <example>200</example>
        [Display(Name = "Local IP Routing Preference")]
        [Range(1, 500, ErrorMessage = "Local IP routing preference must be a number between 1 and 500")]
        public int? LocalIpRoutingPreference { get; set; }

        /// <summary>
        /// Default routing preference applied to IPv4 and IPv6 routes advertised from the routing instance
        /// </summary>
        /// <value>Integer denoting the advertised IP routing preference</value>
        /// <example>10</example>
        [Display(Name = "Advertised IP Routing Preference")]
        [Range(1, 20, ErrorMessage = "Advertised IP routing preference must be a number between 1 and 20")]
        public int? AdvertisedIpRoutingPreference { get; set; }

        /// <summary>
        /// Default multicast designated router preference.
        /// </summary>
        /// <value>Integer denoting the multicast designated router preference</value>
        /// <example>10</example>
        [Display(Name = "Multicast Designated Router Preference")]
        [Range(1, 500, ErrorMessage = "Multicast designated router preference must be a number between 1 and 500")]
        public int? MulticastDesignatedRouterPreference { get; set; }

    }
}

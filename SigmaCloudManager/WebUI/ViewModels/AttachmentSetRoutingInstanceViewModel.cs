
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
    /// Model of an attachment set routing instance - a routing instance which belongs (is associated with) an attachment set
    /// </summary>
    public class AttachmentSetRoutingInstanceViewModel
    {
        /// <summary>
        /// ID of the attachment set routing instance
        /// </summary>
        /// <value>An integer value</value>
        [Display(Name = "Attachment Set Routing Instance ID")]
        public int? AttachmentSetRoutingInstanceID { get; private set; }

        /// <summary>
        /// Default preference applied to IPv4 and IPv6 routes within the routing instance
        /// </summary>
        /// <value>Integer value denoting the default preference applied to IPv4 and IPv6 routes within the routing instance</value>
        [Display(Name = "Local IP Routing Preference")]
        public int? LocalIpRoutingPreference { get; private set; }

        /// <summary>
        /// Default preference of IPv4 and IPv6 routes advertised from the routing instance
        /// </summary>
        /// <value>Integer value denoting the default preference of IPv4 and IPv6 routes advertised from the routing instance</value>
        [Display(Name = "Advertised IP Routing Preference")]
        public int? AdvertisedIpRoutingPreference { get; private set; }

        /// <summary>
        /// Default multicast designated router preference
        /// </summary>
        /// <value>Integer value denoting the multicast designated router preference</value>
        [Display(Name = "Multicast Designated Router Preference")]
        public int? MulticastDesignatedRouterPreference { get; private set; }

        /// <summary>
        /// The routing instance
        /// </summary>
        /// <value>An instance of RoutingInstance</value>
        [Display(Name = "Routing Instance")]
        public ProviderDomainRoutingInstanceViewModel RoutingInstance { get; private set; }
    }
}

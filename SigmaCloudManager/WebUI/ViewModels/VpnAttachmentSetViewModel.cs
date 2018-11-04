using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace Mind.WebUI.Models
{
    public class VpnAttachmentSetViewModel
    {
        /// <summary>
        /// /The name of the tenant owner of the attachment set
        /// </summary>
        /// <value>String value denoting the name of the tenant</value>
        [Display(Name ="Tenant Name")]
        public string TenantName { get; private set; }

        /// <summary>
        /// The name of the attachment set
        /// </summary>
        /// <value>String value denoting the name of the attachment set</value>
        [Display(Name = "Attachment Set Name")]
        public string AttachmentSetName { get; private set; }

        /// <summary>
        /// Denotes whether the attachment set is designated as a hub for a hub-and-spoke vpn to which the attachment set is bound.
        /// </summary>
        /// <value>Boolean value denoting whether the attachment set is a hub</value>
        [Display(Name = "Hub")]
        public bool IsHub { get; private set; }

        /// <summary>
        /// Denotes whether the attachments of the attachment set are directly integrated with the tenant domain for 
        /// the multicast vpn to which the attachment set belongs.
        /// </summary>
        /// <value>Boolean value denoting whether the attachment set is directly integrated for multicast services with the tenant domain</value>
        [Display(Name = "Multicast Directly-Integrated")]
        public bool IsMulticastDirectlyIntegrated { get; private set; }
    }
}
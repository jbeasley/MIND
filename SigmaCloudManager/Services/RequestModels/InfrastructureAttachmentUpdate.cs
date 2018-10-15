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
using Mind.Models.RequestModels;

namespace SCM.Models.RequestModels
{
    /// <summary>
    /// Model for updating an infrastructure attachment
    /// </summary>
    public partial class InfrastructureAttachmentUpdate
    {
        /// <summary>
        /// Determines if the updated attachment should use jumbo MTU
        /// </summary>
        /// <value>Boolean value denoting whether the attachment is enabled for jumbo MTU</value>
        public bool? UseJumboMtu { get; set; }

        /// <summary>
        /// The minimum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the minimum links in the bundle</value>
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the maximum links in the bundle</value>
        public int? BundleMaxLinks { get; set; }

        /// <summary>
        /// IPv4 addresses to be assigned to the interfaces of the attachment.
        /// </summary>
        /// <value>A list of IPv4 addresses and subnet masks</value>
        public List<Ipv4AddressAndMask> Ipv4Addresses { get; set; }
    }
}

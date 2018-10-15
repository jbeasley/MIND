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

namespace SCM.Models.RequestModels
{ 
    /// <summary>
    /// Model for updating an existing tenant domain attachment
    /// </summary>
    public partial class TenantDomainAttachmentUpdate
    { 

        /// <summary>
        /// Determines if the updated attachment should use jumbo MTU
        /// </summary>
        /// <value>Boolean value denoting whether the attachment is enabled for jumbo MTU</value>
        public bool? UseJumboMtu { get; set; }

        /// <summary>
        /// The minimum number of active links in the updated bundle attachment
        /// </summary>
        /// <value>Intege value which specifies the minimum links in the bundle</value>
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in the updated bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the maximum links in the bundle</value>
        public int? BundleMaxLinks { get; set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets sent from the provider network should be trusted by the tenant
        /// </summary>
        /// <value>Boolean value which denotes whether the DSCP and COS markings should be trusted</value>
        public bool? TrustReceivedCosAndDscp { get; set; }
    }
}

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
    /// 
    /// </summary>
    public partial class ProviderDomainAttachmentViewModel
    {
        /// <summary>
        /// ID of the attachment
        /// </summary>
        /// <value>Integer valude denoting the ID of the attachment</value>
        /// <exmple>6001</exmple>
        public int? AttachmentId { get; private set; }

        /// <summary>
        /// The attachment is enabled for layer 3
        /// </summary>
        /// <value>Boolean which denotes whether the attachment is enabled for layer 3</value>
        /// <example>true</example>
        public bool? IsLayer3 { get; private set; }

        /// <summary>
        /// The attachment is delivered as a bundle
        /// </summary>
        /// <value>Boolean value which denotes if the attachment is delivered as a bundle</value>
        /// <example>true</example>
        public bool? IsBundle { get; private set; }

        /// <summary>
        /// For bundle attachments, the minimum number of active links in the bundle
        /// </summary>
        /// <value>Integer value denoting the minimum links in the bundle</value>
        /// <example>2</example>
        public int? BundleMinLinks { get; private set; }

        /// <summary>
        /// For bundle attachments, the maximum number of active links in the bundle
        /// </summary>
        /// <value>Integer value denoting the maximum links in the bundle</value>
        /// <example>2</example>
        public int? BundleMaxLinks { get; private set; }

        /// <summary>
        /// The attachment is delivered as a multiport
        /// </summary>
        /// <value>Boolean denoting if the attachment is delivered as a multiport</value>
        /// <example>true</example>
        public bool? IsMultiport { get; private set; }

        /// <summary>
        /// The attachment is enabled with tagging
        /// </summary>
        /// <value>Boolean value denoting if the attachment is enabled with tagging</value>
        /// <example>true</example>
        public bool? IsTagged { get; private set; }

        /// <summary>
        /// The name of the tenant owner of the attachment
        /// </summary>
        /// <value>String value for the name of the tenant</value>
        /// <example>product-group-tenant</example>
        public string TenantName { get; private set; }

        /// <summary>
        /// The name of the provider domain infrastructure device which terminates the attachment within the provider domain
        /// </summary>
        /// <value>String value denoting the name of the infrastructure device</value>
        /// <example>UK2-PE1</example>
        public string InfrastructureDeviceName { get; private set; }

        /// <summary>
        /// The name of the provider location within which the attachment is terminated
        /// </summary>
        /// <value>String value denoting the provider location</value>
        /// <example>UK2</example>
        public string LocationName { get; private set; }

        /// <summary>
        /// The name of the provider plane within which the attachment is terminated
        /// </summary>
        /// <value>String value denoting the provider plane</value>
        /// <example>Red</example>
        public string PlaneName { get; private set; }

        /// <summary>
        /// The bandwidth of the attachment in Gbps
        /// </summary>
        /// <value>Integer value denoting the bandwidth of the attachment in Gbps</value>
        /// <example>10</example>
        public int? AttachmentBandwidthGbps { get; private set; }

        /// <summary>
        /// The contract bandwidth pool created for the attachment
        /// </summary>
        /// <value>An object of type ContractBandwidthPool</value>
        public ContractBandwidthPool ContractBandwidthPool { get; private set; }

        /// <summary>
        /// The routing instance created for the attachment
        /// </summary>
        /// <value>An object of type RoutingInstance</value>
        public ProviderDomainRoutingInstance RoutingInstance { get; private set; }

        /// <summary>
        /// A list of interfaces created for the attachment
        /// </summary>
        /// <value>A list of Interface objects</value>
        public List<Interface> Interfaces { get; private set; }

        /// <summary>
        /// The maximum transmission unit supported by the attachment
        /// </summary>
        /// <value>The MTU in bytes</value>
        public int? Mtu { get; private set; }

        /// <summary>
        /// The name of the attachment role
        /// </summary>
        /// <value>String value denoting the name of an attachment role</value>
        /// <example>PE-CE-UNTAGGED</example>
        public string AttachmentRoleName { get; private set; }
    }
}

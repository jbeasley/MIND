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
    public class ProviderDomainAttachmentViewModel
    {
        /// <summary>
        /// ID of the attachment
        /// </summary>
        /// <value>Integer value denoting the ID of the attachment</value>
        /// <example>6001</example>
        public int? AttachmentId { get; private set; }

        /// <summary>
        /// The name of the attachment
        /// </summary>
        /// <value>string value denoting the name of the attachment</value>
        /// <example>TenGigabitEthernet0/0</example>
        public string Name { get; private set; }

        /// <summary>
        /// The attachment is enabled for layer 3
        /// </summary>
        /// <value>Boolean which denotes whether the attachment is enabled for layer 3</value>
        /// <example>true</example>
        [Display(Name="Layer 3")]
        public bool IsLayer3 { get; private set; }

        /// <summary>
        /// The attachment is delivered as a bundle
        /// </summary>
        /// <value>Boolean value which denotes if the attachment is delivered as a bundle</value>
        /// <example>true</example>
        [Display(Name="Bundle")]
        public bool IsBundle { get; private set; }

        /// <summary>
        /// For bundle attachments, the minimum number of active links in the bundle
        /// </summary>
        /// <value>Integer value denoting the minimum links in the bundle</value>
        /// <example>2</example>
        [Display(Name = "Bundle Min Links")]
        public int? BundleMinLinks { get; private set; }

        /// <summary>
        /// For bundle attachments, the maximum number of active links in the bundle
        /// </summary>
        /// <value>Integer value denoting the maximum links in the bundle</value>
        /// <example>2</example>
        [Display(Name = "Bundle Max Links")]
        public int? BundleMaxLinks { get; private set; }

        /// <summary>
        /// The attachment is delivered as a multiport
        /// </summary>
        /// <value>Boolean denoting if the attachment is delivered as a multiport</value>
        /// <example>true</example>
        [Display(Name = "Multiport")]
        public bool IsMultiport { get; private set; }

        /// <summary>
        /// The attachment is enabled with tagging
        /// </summary>
        /// <value>Boolean value denoting if the attachment is enabled with tagging</value>
        /// <example>true</example>
        [Display(Name = "Tagged")]
        public bool IsTagged { get; private set; }

        /// <summary>
        /// ID of the tenant
        /// </summary>
        /// <value>Integer valude denoting the ID of the tenant</value>
        /// <example>6001</example>
        public int? TenantId { get; private set; }

        /// <summary>
        /// The name of the tenant owner of the attachment
        /// </summary>
        /// <value>String value for the name of the tenant</value>
        /// <example>product-group-tenant</example>
        [Display(Name = "Tenant Name")]
        public string TenantName { get; private set; }

        /// <summary>
        /// The name of the provider domain infrastructure device which terminates the attachment within the provider domain
        /// </summary>
        /// <value>String value denoting the name of the infrastructure device</value>
        /// <example>UK2-PE1</example>
        [Display(Name = "Infrastructure Device Name")]
        public string InfrastructureDeviceName { get; private set; }

        /// <summary>
        /// The name of the provider location within which the attachment is terminated
        /// </summary>
        /// <value>String value denoting the provider location</value>
        /// <example>UK2</example>
        [Display(Name = "Provider Location Name")]
        public string LocationName { get; private set; }

        /// <summary>
        /// The name of the provider plane within which the attachment is terminated
        /// </summary>
        /// <value>String value denoting the provider plane</value>
        /// <example>Red</example>
        [Display(Name = "Plane Name")]
        public string PlaneName { get; private set; }

        /// <summary>
        /// The bandwidth of the attachment in Gbps
        /// </summary>
        /// <value>Integer value denoting the bandwidth of the attachment in Gbps</value>
        /// <example>10</example>
        [Display(Name = "Attachment Bandwidth (Gbps)")]
        public int? AttachmentBandwidthGbps { get; private set; }

        /// <summary>
        /// The description of the attachment
        /// </summary>
        /// <value>String value denoting the description</value>
        /// <example>A description of the attachment</example>
        public string Description { get; private set; }

        /// <summary>
        /// Notes for the attachment
        /// </summary>
        /// <value>String value for the notes for the attachment</value>
        /// <example>Some notes about the attachment</example>
        public string Notes { get; private set; }

        /// <summary>
        /// The contract bandwidth pool created for the attachment
        /// </summary>
        /// <value>An object of type ContractBandwidthPool</value>
        public ContractBandwidthPoolViewModel ContractBandwidthPool { get; private set; }

        /// <summary>
        /// The routing instance created for the attachment
        /// </summary>
        /// <value>An object of type ProviderDomainRoutingInstanceViewModel</value>
        public ProviderDomainRoutingInstanceViewModel RoutingInstance { get; private set; }

        /// <summary>
        /// A list of interfaces created for the attachment
        /// </summary>
        /// <value>A list of InterfaceViewModel objects</value>
        public List<InterfaceViewModel> Interfaces { get; private set; }

        /// <summary>
        /// The maximum transmission unit supported by the attachment
        /// </summary>
        /// <value>The MTU in bytes</value>
        [Display(Name = "MTU")]
        public int? Mtu { get; private set; }

        /// <summary>
        /// ID of the attachment role
        /// </summary>
        /// <value>Integer value denoting the ID of the attachment role</value>
        /// <example>7001</example>
        public int? AttachmentRoleId { get; private set; }

        /// <summary>
        /// The name of the attachment role
        /// </summary>
        /// <value>String value denoting the name of an attachment role</value>
        /// <example>PE-CE-UNTAGGED</example>
        [Display(Name = "Attachment Role")]
        public string AttachmentRoleName { get; private set; }

        /// <summary>
        /// Network status of the attachment.
        /// </summary>
        /// <value>String value denoting the network status</value>
        /// <example>Staged</example>
        [Display(Name = "Network Status")]
        public string NetworkStatus { get; private set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; private set; }
    }
}

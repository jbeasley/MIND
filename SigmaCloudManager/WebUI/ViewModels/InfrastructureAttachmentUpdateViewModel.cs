using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// View model for updating an infrastructure attachment
    /// </summary>
    public class InfrastructureAttachmentUpdateViewModel : IModifiableResource
    {
        public InfrastructureAttachmentUpdateViewModel()
        {
            // Instantiate a new instance of the routing instance request model. This is necessary in order to 
            // instantiate properties of the routing instance such as the list of BGP peers and handle user actions
            // in the web UI such as the removal of all BGP peers which belong to the routing instance.
            RoutingInstance = new RoutingInstanceRequestViewModel();
        }

        /// <summary>
        /// ID of the attachment
        /// </summary>
        /// <value>Integer value denoting the ID of the attachment</value>
        /// <example>6001</example>
        public int? AttachmentId { get; set; }

        /// <summary>
        /// ID of the device
        /// </summary>
        /// <value>Integer value denoting the ID of the device</value>
        /// <example>1001</example>
        public int? DeviceId { get; set; }

        /// <summary>
        /// The name of the attachment
        /// </summary>
        /// <value>string value denoting the name of the attachment</value>
        /// <example>TenGigabitEthernet0/0</example>
        public string Name { get; set; }

        /// <summary>
        /// Denotes whether the attachment is configured as a bundle 
        /// </summary>
        /// <value>Boolean value denoting whether the attachment is configured as a bundle</value>
        /// <example>false</example>
        public bool IsBundle { get; set; }

        /// <summary>
        /// The minimum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the minimum links in the bundle</value>
        /// <example>2</example>
        [Display(Name = "Bundle Min Links")]
        [Range(1, 8)]
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the maximum links in the bundle</value>
        /// <example>2</example>
        [Display(Name = "Bundle Max Links")]
        [Range(1, 8)]
        public int? BundleMaxLinks { get; set; }

        /// <summary>
        /// A list of IPv4 addresses to be assigned to the interfaces of the attachment.
        /// </summary>
        /// <value>A list of Ipv4AddressAndMaskViewModel objects</value>
        [Display(Name = "IPv4 Addresses")]
        public List<Ipv4AddressAndMaskViewModel> Ipv4Addresses { get; set; }

        /// <summary>
        /// Optional parameters for performing updates on the 
        /// existing routing instance associated with the attachment
        /// </summary>
        /// <value>An object of type RoutingInstanceRequestViewModel</value>
        public RoutingInstanceRequestViewModel RoutingInstance { get; set; }

        /// <summary>
        /// Determines if the updated attachment should use jumbo MTU
        /// </summary>
        /// <value>A boolean which when set to true indicates jumbo MTU is required</value>
        /// <example>true</example>
        [Display(Name = "Use Jumbo MTU")]
        public bool UseJumboMtu { get; set; }

        /// <summary>
        /// A description for the new attachment
        /// </summary>
        /// <value>String value for the description</value>
        /// <example>Connectivity to LAN</example>
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Notes for the new attachment
        /// </summary>
        /// <value>String value for notes</value>
        /// <example>Some user notes which help explain the purpose of the attachment</example>
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Concurrency token for the model
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}

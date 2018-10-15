
namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for updating an existing vpn attachment set (i.e. an attachment set association with a vpn)
    /// </summary>
    public class VpnAttachmentSetUpdate
    {
        /// <summary>
        /// Determines if the attachment set should be configured as a hub for the association with the vpn.
        /// The vpn topology must be 'hub-and-spoke' for the attachment set to be defined as a hub.
        /// </summary>
        /// <value>Boolean value denoting the hub state of the attachment set</value>
        public bool? IsHub { get; set; }

        /// <summary>
        /// Determines if the attachment set should be directly integrated with the tenant multicast domain.
        /// The vpn must be enabled for multicast for the attachment set to be integrated with the tenant multicast domain.
        /// </summary>
        /// <value>Boolean value denoting whether the attachment set should be directly integrated with the tenant multicast domain</value>
        public bool? IsMulticastDirectlyIntegrated { get; set; }
    }
}

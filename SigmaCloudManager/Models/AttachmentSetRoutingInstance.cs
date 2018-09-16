using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class AttachmentSetRoutingInstance : IModifiableResource
    {
        public int AttachmentSetRoutingInstanceID { get; private set; }
        public int AttachmentSetID { get; set; }
        public int RoutingInstanceID { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
        public int? AdvertisedIpRoutingPreference { get; set; }
        public int? LocalIpRoutingPreference { get; set; }
        public int? MulticastDesignatedRouterPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the attachment set routing instance
        /// </summary>
        public virtual void Validate()
        {
            if (this.AttachmentSet == null)
                throw new IllegalStateException($"An attachment set is required.");

            if (this.RoutingInstance == null) throw new IllegalStateException($"The routing instance " +
                $"'{this.RoutingInstance.Name}' was not found or is invalid.");

            if (this.AttachmentSet.IsLayer3 != this.RoutingInstance.RoutingInstanceType.IsLayer3)
                throw new IllegalStateException($"Routing instance '{this.RoutingInstance.Name}' cannot be added to attachment set " +
                    $"'{this.AttachmentSet.Name}'. The protocol layer of the attachment set and the routing instance do not match. " +
                    $"Attachment set 'IsLayer3' property is '{this.AttachmentSet.IsLayer3}'. Routing instance 'IsLayer3' " +
                    $"property is '{this.RoutingInstance.RoutingInstanceType.IsLayer3}'.");

            // The routing instance must belong to the same tenant as the attachment set
            if (this.RoutingInstance.TenantID != this.AttachmentSet.TenantID)
                throw new IllegalStateException($"Routing instance '{this.RoutingInstance.Name}' "
                   + $" does not belong to the same tenant as the attachment set. The tenant for that attachment set is " +
                   $"'{this.AttachmentSet.Tenant.Name}'.");

            // The routing instance must be associated with a device in the same region as the attachment set
            if (this.RoutingInstance.Device.Location.SubRegion.Region.RegionID != this.AttachmentSet.RegionID)
                throw new IllegalStateException($"Routing instance '{this.RoutingInstance.Name}' is not associated with "
                     + $"a device in region {this.AttachmentSet.Region.Name}.");
        }
    }
}
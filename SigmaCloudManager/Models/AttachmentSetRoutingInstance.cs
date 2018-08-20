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
        public int AttachmentSetRoutingInstanceID { get; set; }
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
    }
}
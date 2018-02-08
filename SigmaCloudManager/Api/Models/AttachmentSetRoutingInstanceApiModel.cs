using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Models
{
    public class AttachmentSetRoutingInstanceApiModel
    {
        public int AttachmentSetRoutingInstanceID { get; set; }
        public int AttachmentSetID { get; set; }
        public RoutingInstanceApiModel RoutingInstance { get; set; }
        public int? IpRoutingPreference { get; set; }
        public int? MulticastRoutingPreference { get; set; }
    }
}

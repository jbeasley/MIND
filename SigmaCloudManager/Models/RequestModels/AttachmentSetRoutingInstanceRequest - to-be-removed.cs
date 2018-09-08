using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Models.RequestModels
{
    public class AttachmentSetRoutingInstanceRequest
    {
        public int AttachmentSetID { get; set; }
        public int? LocationID { get; set; }
        public int? PlaneID { get; set; }
        public int? TenantID { get; set; }
    }
}

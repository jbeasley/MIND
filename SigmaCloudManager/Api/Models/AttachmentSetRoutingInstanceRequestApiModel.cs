﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for requesting an Attachment Set VRF.
    /// </summary>
    public class AttachmentSetRoutingInstanceRequestApiModel
    {
        public int AttachmentSetID { get; set; }
        [Required]
        public int? RoutingInstanceID { get; set; }
        [Range(1, 500, ErrorMessage = "The preference must be a number between 1 and 500")]
        public int? IpRoutingPreference { get; set; }
        public int? MulticastRoutingPreference { get; set; }
    }
}

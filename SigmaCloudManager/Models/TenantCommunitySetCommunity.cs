using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class TenantCommunitySetCommunity
    {
        public int TenantCommunitySetCommunityID { get; private set; }
        [Required]
        public int TenantCommunitySetID { get; set; }
        [Required]
        public int TenantCommunityID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual TenantCommunitySet TenantCommunitySet { get; set; }
        public virtual TenantCommunity TenantCommunity { get; set; }
    }
}
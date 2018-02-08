﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning a VPN Tenant Community.
    /// </summary>
    public class VpnTenantCommunityInApiModel
    {
        public int VpnTenantCommunityInID { get; set; }
        public byte[] RowVersion { get; set; }
        public TenantCommunityApiModel TenantCommunity { get; set; }
    }
}
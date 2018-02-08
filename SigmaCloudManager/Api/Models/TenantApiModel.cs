using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning Tenant data.
    /// </summary>
    public class TenantApiModel
    {
        public int TenantID { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
        public ICollection<TenantNetworkApiModel> TenantNetworks { get; set; }
        public ICollection<TenantCommunityApiModel> TenantCommunities { get; set; }
    }
}

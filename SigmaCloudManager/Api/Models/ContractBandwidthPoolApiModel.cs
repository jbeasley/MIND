using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning Contract Bandwidth Pool data.
    /// </summary>
    public class ContractBandwidthPoolApiModel
    {
        public int ContractBandwidthPoolID { get; set; }
        public string Name { get; set; }
        public bool TrustReceivedCosDscp { get; set; }
        public ContractBandwidthApiModel ContractBandwidth { get; set; }
        public TenantApiModel Tenant { get; set; }
        public byte[] RowVersion { get; set; }
    }
}

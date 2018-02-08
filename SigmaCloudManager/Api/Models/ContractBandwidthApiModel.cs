using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning Contract Bandwidth data.
    /// </summary>
    public class ContractBandwidthApiModel
    {
        public int ContractBandwidthID { get; set; }
        public int BandwidthMbps { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model for contract bandwidth
    /// </summary>
    public class ContractBandwidthViewModel
    {
        /// <summary>
        /// The ID of the contract bandwidth record
        /// </summary>
        /// <value>Integer value denoting the ID of the contract bandwidth record</value>
        public int ContractBandwidthId { get; set; }

        /// <summary>
        /// The value of the contract bandwidth record in Mbps
        /// </summary>
        /// <value>Integer value denoting the contract bandwidth value</value>
        [Display(Name = "Contract Bandwidth (Mbps))")]
        public int BandwidthMbps { get; set; }
    }
}
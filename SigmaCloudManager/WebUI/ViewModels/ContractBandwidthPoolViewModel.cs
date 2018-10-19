using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{ 
    /// <summary>
    /// Model of a contract bandwidth pool
    /// </summary>
    public class ContractBandwidthPoolViewModel
    {
        /// <summary>
        /// The MIND system-generated name of the contract bandwidth pool 
        /// </summary>
        /// <value>A string denoting the name of the contract bandwidth pool</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        public string Name { get; private set; }

        /// <summary>
        /// The contract bandwidth of the pool in Mbps
        /// </summary>
        /// <value>An integer denoting the contract bandwidth of the pool in Mbps</value>
        /// <example>1000</example>
        [Display(Name="Contract Bandwidth (Mbps)")]
        public int? ContractBandwidthMbps { get; private set; }

        /// <summary>
        /// Denotes whether DSCP and COS markings of packets are trusted by the provider
        /// </summary>
        /// <value>Boolean value denoting the trust stater</value>
        /// <example>true</example>
        [Display(Name = "Trust Received CoS/DSCP")]
        public bool? TrustReceivedCosDscp { get; private set; }
    }
}

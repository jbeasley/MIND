using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning a Port.
    /// </summary>
    public class PortApiModel
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
        public PortBandwidthApiModel PortBandwidth { get; set; }
    }
}
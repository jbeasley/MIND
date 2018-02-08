using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning Sub-Region data.
    /// </summary>
    public class SubRegionApiModel
    {
        public int SubRegionID { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
        public RegionApiModel Region { get; set; }
    }
}
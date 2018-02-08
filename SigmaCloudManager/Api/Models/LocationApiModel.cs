using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning Location data.
    /// </summary>
    public class LocationApiModel
    {
        public int LocationID { get; set; }
        public string SiteName { get; set; }
        public int? AlternateLocationID { get; set; }
        public LocationApiModel AlternateLocation { get; set; }
        public SubRegionApiModel SubRegion { get; set; }

    }
}

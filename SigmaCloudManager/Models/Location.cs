using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SCM.Models
{
    public static class LocationQueryableExtensions
    {
        public static IQueryable<Location> IncludeDeepProperties(this IQueryable<Location> query)
        {
            return query.Include(x => x.SubRegion.Region);
        }
    }

    public enum LocationTypeEnum
    {
        ProviderDomain = 0,
        TenantDomain = 1,
        ProviderAndTenantDomain = 2
    }

    public class Location : IModifiableResource
    {
        public int LocationID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string SiteName { get; set; }
        public int SubRegionID { get; set; }
        public int? Tier { get; set; }
        public int? AutonomousSystemNumber { get; set; }
        public int? Number { get; set; }
        public LocationTypeEnum LocationType { get; set; }
        [NotMapped]
        public string LocaleCommunityName
        {
            get
            {
                return $"{AutonomousSystemNumber}:{Number}";
            }
        }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual SubRegion SubRegion { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();
    }
}
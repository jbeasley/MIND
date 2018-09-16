using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models
{
    public class Region
    {
        public int RegionID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int AutonomousSystemNumber { get; set; }
        [Required]
        public int Number { get; set; }
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
        public virtual ICollection<SubRegion> SubRegions { get; set; }
    }
}
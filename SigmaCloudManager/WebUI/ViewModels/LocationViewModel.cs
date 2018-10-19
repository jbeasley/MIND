using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of a location
    /// </summary>
    public class LocationViewModel
    {
        /// <summary>
        /// The ID of the location
        /// </summary>
        /// <value>Integer value denoting the ID of the location</value>
        public int LocationId { get; private set; }

        /// <summary>
        /// The name of the site
        /// </summary>
        /// <value>String value denoting the name of the site</value>
        [Display(Name ="Site Name")]
        public string SiteName { get; private set; }

        /// <summary>
        /// The BGP autonomous system number portion of the locale community assigned to the subregion
        /// </summary>
        /// <value>Integer value denoting the autonomous system number</value>
        [Display(Name = "Locale Community AS Number")]
        public int AutonomousSystemNumber { get; private set; }

        /// <summary>
        /// The number port of the locale community assigned to the subregion
        /// </summary>
        /// <value>Integer value denoting the number portion of the local community</value>
        [Display(Name = "Locale Community Number")]
        public int Number { get; private set; }

        /// <summary>
        /// The name of the locale community assigned to the subregion
        /// </summary>
        /// <value>String value denoting the name of the locale community</value>
        [Display(Name = "Locale Community Name")]
        public string LocaleCommunityName { get; private set; }

        /// <summary>
        /// The ID of the parent subregion
        /// </summary>
        /// <value>Integer value denoting the ID of the parent subregion</value>
        public int SubRegionId { get; private set; }
    }
}

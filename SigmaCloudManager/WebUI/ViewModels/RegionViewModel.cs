using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mind.WebUI.Models
{
    public class RegionViewModel
    {
        /// <summary>
        /// The ID of the region
        /// </summary>
        /// <value>Integer denoting the ID of the subregion</value>
        public int RegionId { get; set; }

        /// <summary>
        /// The name of the region
        /// </summary>
        /// <value>String denoting the name of the subregion</value>
        public string Name { get; private set; }

        /// <summary>
        /// The BGP autonomous system number portion of the locale community assigned to the region
        /// </summary>
        /// <value>Integer value denoting the autonomous system number</value>
        [Display(Name = "Locale Community AS Number")]
        public int AutonomousSystemNumber { get; private set; }

        /// <summary>
        /// The number port of the locale community assigned to the region
        /// </summary>
        /// <value>Integer value denoting the number portion of the local community</value>
        [Display(Name = "Locale Community Number")]
        public int Number { get; private set; }

        /// <summary>
        /// The name of the locale community assigned to the region
        /// </summary>
        /// <value>String value denoting the name of the locale community</value>
        [Display(Name = "Locale Community Name")]
        public string LocaleCommunityName { get; private set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of a subregion
    /// </summary>
    public class SubRegionViewModel
    {
        /// <summary>
        /// The ID of the subregion
        /// </summary>
        /// <value>Integer denoting the ID of the subregion</value>
        public int SubRegionId { get; private set; }
        
        /// <summary>
        /// The name of the subregion
        /// </summary>
        /// <value>String denoting the name of the subregion</value>
        public string Name { get; private set; }

        /// <summary>
        /// The ID of the parent region
        /// </summary>
        /// <value>Integer value denoting the ID of the parent region</value>
        public int RegionId { get; private set; }

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
    }
}
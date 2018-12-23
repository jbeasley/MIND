using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of for selecting a location
    /// </summary>
    public class LocationSelectorViewModel
    { 
        /// <summary>
        /// The site name of the location
        /// </summary>
        /// <value>String value denoting the site name of the location</value>
        [Display(Name ="Location Name")]
        [Required(AllowEmptyStrings =false, ErrorMessage = "A location must be selected")]
        public string LocationName { get; set; }

        /// <summary>
        /// The ID of the grand-parent region of the location
        /// </summary>
        /// <value>Integer value denoting the ID of the grand-parent region</value>
        [Display(Name = "Region Name")]
        [Required(ErrorMessage = "A region must be selected")]
        public int? RegionId { get; set; }

        /// <summary>
        /// The ID of the parent subregion of the location
        /// </summary>
        /// <value>Integer value denoting the ID of the parent subregion</value>
        [Display(Name = "Sub-Region Name")]
        [Required(ErrorMessage = "A sub-region must be selected")]
        public int? SubRegionId { get; set; }
    }
}

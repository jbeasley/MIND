using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace Mind.WebUI.Models
{ 
    public class MtuOptionsComponentViewModel
    {
        /// <summary>
        /// Determines if an attachment or vif should use jumbo MTU
        /// </summary>
        /// <value>A boolean which when set to true indicates jumbo MTU is required</value>
        /// <example>true</example>
        [Display(Name = "Use Jumbo MTU")]
        public bool UseJumboMtu { get; set; }

    }
}
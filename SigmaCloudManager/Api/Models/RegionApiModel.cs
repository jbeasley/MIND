using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Api.Models
{
    public class RegionApiModel
    {
        /// <summary>
        /// API Model for returning Region data.
        /// </summary>
        public int RegionID { get; set; }
        public string Name { get; set; }
    }
}
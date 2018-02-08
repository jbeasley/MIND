using SCM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Models.RequestModels
{
    public class RoutingInstanceUpdate
    {
        public int RoutingInstanceID { get; set; }
        public string Name { get; set; }
        public int? AdministratorSubField { get; set; }
        public int? AssignedNumberSubField { get; set; }
    }
}

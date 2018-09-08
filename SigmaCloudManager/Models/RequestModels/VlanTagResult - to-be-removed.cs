using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Models.RequestModels
{
    public class VlanTagResult
    {
        public int AllocatedVlanTag { get; set; }
        public VlanTagRange VlanTagRange { get; set; }
    }
}

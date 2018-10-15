using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.RequestModels
{
    public class RouteTargetRequest
    {
        public int RouteTargetRangeID { get; set; }
        public int? RequestedAssignedNumberSubField { get; set; }
        public bool AutoAllocateAssignedNumberSubField { get; set; } = true;
        public int? VpnTopologyTypeID { get; set; }
        public int? VpnID { get; set; }
        public bool IsHubExport { get; set; }
    }
}
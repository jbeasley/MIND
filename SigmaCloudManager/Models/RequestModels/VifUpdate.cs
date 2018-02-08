using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Models.RequestModels
{
    public class VifUpdate
    {
        public int VifID { get; set; }
        public int? TenantID { get; set; }
        public int AttachmentID { get; set; }
        public int DeviceID { get; set; }
        public bool IsLayer3 { get; set; }
        public int? RoutingInstanceID { get; set; }
        public int? ContractBandwidthPoolID { get; set; }
        public int? ContractBandwidthID { get; set; }
        public bool TrustReceivedCosDscp { get; set; }
        public bool CreateNewRoutingInstance { get; set; }
        public int? RoutingInstanceTypeID { get; set; }
    }
}

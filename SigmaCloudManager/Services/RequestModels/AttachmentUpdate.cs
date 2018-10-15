using SCM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Models.RequestModels
{
    public class AttachmentUpdate
    {
        public int AttachmentID { get; set; }
        public int? TenantID { get; set; }
        public int? RoutingInstanceID { get; set; }
        public int DeviceID { get; set; }
        public bool IsLayer3 { get; set; }
        public int? ContractBandwidthID { get; set; }
        public int MtuID { get; set; }
        public bool TrustReceivedCosDscp { get; set; }
        public int? BundleMinLinks { get; set; }
        public int? BundleMaxLinks { get; set; }
        public bool CreateNewRoutingInstance { get; set; }
        public int? RoutingInstanceTypeID { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public Byte[] RowVersion { get; set; }
    }
}

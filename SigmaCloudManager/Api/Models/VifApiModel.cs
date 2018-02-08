using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SCM.Models.RequestModels;


namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning a Vif.
    /// </summary>
    public class VifApiModel
    {
        public int VifID { get; set; }
        public string Name { get; set; }
        public int VlanTag { get; set; }
        public bool IsLayer3 { get; set; }
        public bool RequiresSync { get; set; }
        public int AttachmentID { get; set; }
        public int TenantID { get; set; }
        public byte[] Rowversion { get; set; }
        public int? ContractBandwidthPoolID { get; set; }
        public int RoutingInstanceID { get; set; }
        public RoutingInstanceApiModel RoutingInstance { get; set; }
        public ICollection<VlanApiModel> Vlans { get; set; }
        public ContractBandwidthPoolApiModel ContractBandwidthPool { get; set; }
    }
}

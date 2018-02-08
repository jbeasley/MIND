using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning an Attachment.
    /// </summary>
    public class AttachmentApiModel
    {
        public int AttachmentID { get; set; }
        public string Name { get; set; }
        public bool IsBundle { get; set; }
        public bool IsMultiPort { get; set; }
        public bool IsTagged { get; set; }
        public bool IsLayer3 { get; set; }
        public bool RequiresSync { get; set; }
        public int? ContractBandwidthID { get; set; }
        public TenantApiModel Tenant { get; set; }
        public RoutingInstanceApiModel RoutingInstance { get; set; }
        public DeviceApiModel Device { get; set; }
        public ICollection<VifApiModel> Vifs { get; set; }
        public ICollection<InterfaceApiModel> Interfaces { get; set; }
        public AttachmentBandwidthApiModel AttachmentBandwidth { get; set; }
        public ContractBandwidthApiModel ContractBandwidth { get; set; }
    }
}

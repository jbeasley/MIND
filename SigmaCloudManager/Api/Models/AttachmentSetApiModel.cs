using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning an Attachment Set.
    /// </summary>
    public class AttachmentSetApiModel
    {
        public int AttachmentSetID { get; set; }
        public string Name { get; set; }
        public bool IsLayer3 { get; set; }
        public byte[] RowVersion { get; set; }
        public AttachmentRedundancyApiModel AttachmentRedundancy { get; set; }
        public TenantApiModel Tenant { get; set; }
        public RegionApiModel Region { get; set; }
        public SubRegionApiModel SubRegion { get; set; }
        public ICollection<AttachmentSetRoutingInstanceApiModel> AttachmentSetRoutingInstances { get; set; }
    }
}
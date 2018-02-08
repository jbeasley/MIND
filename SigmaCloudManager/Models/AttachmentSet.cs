﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class AttachmentSet
    {
        public int AttachmentSetID { get; set; }
        [MaxLength(50)]
        [NotMapped]
        public string Name
        {
            get
            {
                return $"{Tenant.Name}-{AttachmentSetID}";
            }
        }
        public bool IsLayer3 { get; set; }
        public int TenantID { get; set; }
        public int AttachmentRedundancyID { get; set; }
        public int RegionID { get; set; }
        public int? SubRegionID { get; set; }
        public int? MulticastVpnDomainTypeID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual AttachmentRedundancy AttachmentRedundancy { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual Region Region { get; set; }
        public virtual SubRegion SubRegion { get; set; }
        public virtual MulticastVpnDomainType MulticastVpnDomainType { get; set; }
        public virtual ICollection<AttachmentSetRoutingInstance> AttachmentSetRoutingInstances { get; set; }
        public virtual ICollection<VpnAttachmentSet> VpnAttachmentSets { get; set; }
        public virtual ICollection<VpnTenantNetworkIn> VpnTenantNetworksIn { get; set; }
        public virtual ICollection<VpnTenantNetworkStaticRouteRoutingInstance> VpnTenantNetworkStaticRoutesRoutingInstance { get; set; }
        public virtual ICollection<VpnTenantCommunityIn> VpnTenantCommunitiesIn { get; set; }
        public virtual ICollection<VpnTenantNetworkOut> VpnTenantNetworksOut { get; set; }
        public virtual ICollection<VpnTenantCommunityOut> VpnTenantCommunitiesOut { get; set; }
        public virtual ICollection<VpnTenantNetworkRoutingInstance> VpnTenantNetworksRoutingInstance { get; set; }
        public virtual ICollection<VpnTenantCommunityRoutingInstance> VpnTenantCommunitiesRoutingInstance { get; set; }
        public virtual ICollection<MulticastVpnRp> MulticastVpnRps { get; set; }
        public virtual ICollection<VpnTenantMulticastGroup> VpnTenantMulticastGroups { get; set; }
    }
}
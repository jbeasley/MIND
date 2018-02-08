using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SCM.Models.NetModels.VpnNetModels;

namespace SCM.Models.NetModels.IpVpnNetModels
{
    public class TenantCommunityNetModel
    {
        public int AutonomousSystemNumber { get; set; }
        public int Number { get; set; }
    }

    public class TenantCommunitySetNetModel
    {
        public string Name { get; set; }
        public string MatchOption { get; set; }
        public List<TenantCommunityNetModel> Communities { get; set; }
        public TenantCommunitySetNetModel()
        {
            Communities = new List<TenantCommunityNetModel>();
        }
    }

    public abstract class IpRoutingInstanceNetModel : RoutingInstanceBaseNetModel
    {
        public int LocalIpRoutingPreference { get; set; }
        public int AdvertisedIpRoutingPreference { get; set; }
    }

    public class IpBgpPeerNetModel
    {
        public int BgpPeerID { get; set; }
    }

    public abstract class IpVpnAttachmentSetNetModel : VpnAttachmentSetBaseNetModel
    {
        public bool? IsHub { get; set; }
    }

    public abstract class IpVpnServiceNetModel : VpnServiceBaseNetModel
    {
        public string TopologyType { get; set; }
        public RouteTargetNetModel RouteTargetA { get; set; }
        public RouteTargetNetModel RouteTargetB { get; set; }
        public bool IsExtranet { get; set; }
    }
}
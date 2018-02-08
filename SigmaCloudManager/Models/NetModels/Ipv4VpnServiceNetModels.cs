using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models.NetModels.VpnNetModels;
using SCM.Models.NetModels.IpVpnNetModels;
using AutoMapper;

namespace SCM.Models.NetModels.Ipv4VpnNetModels
{

    #region Interface Definiitions

    public interface IIpv4RoutingInstanceNetModel
    {
        int RoutingInstanceID { get; set; }
        string RoutingInstanceName { get; set; }
        int LocalIpRoutingPreference { get; set; }
        int AdvertisedIpRoutingPreference { get; set; }
        IEnumerable<IIpv4BgpPeerNetModel> Ipv4BgpPeers { get; set; }
        Ipv4RoutingInstanceRoutingPolicyNetModel Ipv4RoutingInstanceRoutingPolicy { get; set; }
        Ipv4RoutingInstanceStaticRoutingNetModel Ipv4RoutingInstanceStaticRouting { get; set; }
    }

    public interface IIpv4BgpPeerNetModel
    {
        int BgpPeerID { get; set; }
        string PeerIpv4Address { get; set; }
        Ipv4OutboundRoutingPolicyNetModel Ipv4OutboundRoutingPolicy { get; set; }
        Ipv4InboundRoutingPolicyNetModel Ipv4InboundRoutingPolicy { get; set; }
    }

    public interface IIpv4PeNetModel
    {
        IEnumerable<IIpv4RoutingInstanceNetModel> RoutingInstances { get; set; }
        string PEName { get; set; }
    }

    #endregion

    #region Class Definitions

    public class TenantInboundBgpIpv4PrefixNetModel
    {
        public string Prefix { get; set; }
        public int? LessThanOrEqualToLength { get; set; }
        public int LocalIpRoutingPreference { get; set; }
        public List<TenantCommunityNetModel> TenantCommunities { get; set; }
        public TenantInboundBgpIpv4PrefixNetModel()
        {
            TenantCommunities = new List<TenantCommunityNetModel>();
        }
    }

    public class TenantInboundBgpCommunityNetModel
    {
        public int AutonomousSystemNumber { get; set; }
        public int Number { get; set; }
        public int LocalIpRoutingPreference { get; set; }
    }

    public class Ipv4RoutingInstanceStaticRoutingNetModel
    {
        public List<TenantStaticIpv4RouteNetModel> TenantStaticIpv4Routes { get; set; }
        public Ipv4RoutingInstanceStaticRoutingNetModel()
        {
            TenantStaticIpv4Routes = new List<TenantStaticIpv4RouteNetModel>();
        }
    }

    public class TenantStaticIpv4RouteNetModel
    {
        public string Prefix { get; set; }
        public string NextHopAddress { get; set; }
        public bool IsBfdEnabled { get; set; }
    }

    public class Ipv4InboundRoutingPolicyNetModel
    {
        public List<TenantInboundBgpIpv4PrefixNetModel> TenantInboundBgpIpv4Prefixes { get; set; }
        public List<TenantInboundBgpCommunityNetModel> TenantInboundBgpCommunities { get; set; }
        public Ipv4InboundRoutingPolicyNetModel()
        {
            TenantInboundBgpIpv4Prefixes = new List<TenantInboundBgpIpv4PrefixNetModel>();
            TenantInboundBgpCommunities = new List<TenantInboundBgpCommunityNetModel>();
        }
    }

    public class TenantOutboundBgpIpv4PrefixNetModel
    {
        public string Prefix { get; set; }
        public int? LessThanOrEqualToLength { get; set; }
        public int AdvertisedIpRoutingPreference { get; set; }
    }

    public class TenantOutboundBgpCommunityNetModel
    {
        public int AutonomousSystemNumber { get; set; }
        public int Number { get; set; }
        public int AdvertisedIpRoutingPreference { get; set; }
    }

    public class Ipv4OutboundRoutingPolicyNetModel
    {
        public List<TenantOutboundBgpIpv4PrefixNetModel> TenantOutboundBgpIpv4Prefixes { get; set; }
        public List<TenantOutboundBgpCommunityNetModel> TenantOutboundBgpCommunities { get; set; }
        public Ipv4OutboundRoutingPolicyNetModel()
        {
            TenantOutboundBgpIpv4Prefixes = new List<TenantOutboundBgpIpv4PrefixNetModel>();
            TenantOutboundBgpCommunities = new List<TenantOutboundBgpCommunityNetModel>();
        }
    }

    public class TenantRoutingInstancePrefixNetModel
    {
        public string Prefix { get; set; }
        public int LocalIpRoutingPreference { get; set; }
    }

    public class TenantRoutingInstanceCommunityNetModel
    { 
        public int AutonomousSystemNumber { get; set; }
        public int Number { get; set; }
        public int LocalIpRoutingPreference { get; set; }
    }

    public class TenantRoutingInstanceCommunitySetNetModel : TenantCommunitySetNetModel
    {
        public int LocalIpRoutingPreference { get; set; }
    }

    public class Ipv4RoutingInstanceRoutingPolicyNetModel
    {
        public List<TenantRoutingInstancePrefixNetModel> TenantRoutingInstanceIpv4Prefixes { get; set; }
        public List<TenantRoutingInstanceCommunityNetModel> TenantRoutingInstanceCommunities { get; set; }
        public List<TenantRoutingInstanceCommunitySetNetModel> TenantRoutingInstanceCommunitySets { get; set; }
        public Ipv4RoutingInstanceRoutingPolicyNetModel()
        {
            TenantRoutingInstanceIpv4Prefixes = new List<TenantRoutingInstancePrefixNetModel>();
            TenantRoutingInstanceCommunities = new List<TenantRoutingInstanceCommunityNetModel>();
            TenantRoutingInstanceCommunitySets = new List<TenantRoutingInstanceCommunitySetNetModel>();
        }
    }

    public class Ipv4BgpPeerNetModel : IpBgpPeerNetModel, IIpv4BgpPeerNetModel
    {
        public string PeerIpv4Address { get; set; }
        public Ipv4OutboundRoutingPolicyNetModel Ipv4OutboundRoutingPolicy { get; set; }
        public Ipv4InboundRoutingPolicyNetModel Ipv4InboundRoutingPolicy { get; set; }
        public Ipv4BgpPeerNetModel()
        {
            Ipv4OutboundRoutingPolicy = new Ipv4OutboundRoutingPolicyNetModel();
            Ipv4InboundRoutingPolicy = new Ipv4InboundRoutingPolicyNetModel();
        }
    }

    public class Ipv4RoutingInstanceNetModel : IpRoutingInstanceNetModel, IIpv4RoutingInstanceNetModel
    {
        public IEnumerable<IIpv4BgpPeerNetModel> Ipv4BgpPeers { get; set; }
        public Ipv4RoutingInstanceRoutingPolicyNetModel Ipv4RoutingInstanceRoutingPolicy { get; set; }
        public Ipv4RoutingInstanceStaticRoutingNetModel Ipv4RoutingInstanceStaticRouting { get; set; }
        public Ipv4RoutingInstanceNetModel()
        {
            Ipv4BgpPeers = new List<Ipv4BgpPeerNetModel>();
            Ipv4RoutingInstanceRoutingPolicy = new Ipv4RoutingInstanceRoutingPolicyNetModel();
            Ipv4RoutingInstanceStaticRouting = new Ipv4RoutingInstanceStaticRoutingNetModel();
        }
    }

    public class Ipv4PeNetModel : PeBaseNetModel, IIpv4PeNetModel
    {
        public IEnumerable<IIpv4RoutingInstanceNetModel> RoutingInstances { get; set; }
        public Ipv4PeNetModel()
        {
            RoutingInstances = new List<Ipv4RoutingInstanceNetModel>();
        }
    }

    public class Ipv4VpnAttachmentSetNetModel : IpVpnAttachmentSetNetModel
    {
        public IEnumerable<IIpv4PeNetModel> PEs { get; set; }
        public Ipv4VpnAttachmentSetNetModel(VpnAttachmentSet source, ResolutionContext context)
        {
            var mapper = context.Mapper;

            var devices = source.AttachmentSet.AttachmentSetRoutingInstances.Select(s => s.RoutingInstance.Device).ToList();
            var PEs = mapper.Map<List<Ipv4PeNetModel>>(devices);

            foreach (Ipv4PeNetModel pe in PEs)
            {
                var sourceRoutingInstances = source.AttachmentSet.AttachmentSetRoutingInstances.Where(v => v.RoutingInstance.Device.Name == pe.PEName);
                var vrfs = mapper.Map<List<Ipv4RoutingInstanceNetModel>>(sourceRoutingInstances);

                PopulateRoutingInstanceNetModels(vrfs, source.AttachmentSet, mapper);

                pe.RoutingInstances = vrfs;
            }

            this.PEs = PEs;
            this.IsHub = source.Vpn.VpnTopologyType.TopologyType == TopologyType.HubandSpoke ? source.IsHub : null;
            this.Name = source.AttachmentSet.Name;
        }

        public void PopulateRoutingInstanceNetModels(IEnumerable<IIpv4RoutingInstanceNetModel> vrfs, AttachmentSet source, IMapper mapper)
        {
            // Get static routes which need to be added to all VRFs in the Attachment Set

            var staticRoutes = mapper.Map<List<TenantStaticIpv4RouteNetModel>>(source.VpnTenantNetworkStaticRoutesRoutingInstance
                .Where(x => x.AddToAllRoutingInstancesInAttachmentSet));

            foreach (var vrf in vrfs)
            {
                // Add static routes to each VRF

                vrf.Ipv4RoutingInstanceStaticRouting.TenantStaticIpv4Routes.AddRange(mapper.Map<List<TenantStaticIpv4RouteNetModel>>
                    (source.VpnTenantNetworkStaticRoutesRoutingInstance.Where(x => x.RoutingInstanceID == vrf.RoutingInstanceID)));

                vrf.Ipv4RoutingInstanceStaticRouting.TenantStaticIpv4Routes.AddRange(staticRoutes);

                // Add inbound community-based policies to the VRF

                vrf.Ipv4RoutingInstanceRoutingPolicy.TenantRoutingInstanceCommunities.AddRange(mapper.Map<List<TenantRoutingInstanceCommunityNetModel>>
                    (source.VpnTenantCommunitiesRoutingInstance.Where(x => x.RoutingInstanceID == vrf.RoutingInstanceID && x.TenantCommunity != null)));

                vrf.Ipv4RoutingInstanceRoutingPolicy.TenantRoutingInstanceCommunitySets.AddRange(mapper.Map<List<TenantRoutingInstanceCommunitySetNetModel>>
                    (source.VpnTenantCommunitiesRoutingInstance.Where(x => x.RoutingInstanceID == vrf.RoutingInstanceID && x.TenantCommunitySet != null)));

                // Add inbound IPv4 prefix policy to the VRF

                vrf.Ipv4RoutingInstanceRoutingPolicy.TenantRoutingInstanceIpv4Prefixes.AddRange(mapper.Map<List<TenantRoutingInstancePrefixNetModel>>
                (source.VpnTenantNetworksRoutingInstance
                .Where(x => x.RoutingInstanceID == vrf.RoutingInstanceID)));

                // Add prefixes and communities to each BGP peer

                PopulateBgpPeerNetModels(vrf.Ipv4BgpPeers, source, mapper);
            }
        }

        private void PopulateBgpPeerNetModels(IEnumerable<IIpv4BgpPeerNetModel> bgpPeers, AttachmentSet source, IMapper mapper)
        {

            // Get prefixes and communities which need to be added to all BGP peers in the Attachment Set

            var inboundPrefixes = mapper.Map<List<TenantInboundBgpIpv4PrefixNetModel>>(source.VpnTenantNetworksIn
                .Where(x => x.AddToAllBgpPeersInAttachmentSet));

            var inboundCommunities = mapper.Map<List<TenantInboundBgpCommunityNetModel>>(source.VpnTenantCommunitiesIn
                .Where(x => x.AddToAllBgpPeersInAttachmentSet));

            foreach (var bgpPeer in bgpPeers) { 

                // Add inbound prefixes for the current BGP peer

                bgpPeer.Ipv4InboundRoutingPolicy.TenantInboundBgpIpv4Prefixes.AddRange(mapper.Map<List<TenantInboundBgpIpv4PrefixNetModel>>
                    (source.VpnTenantNetworksIn.Where(x => x.BgpPeerID == bgpPeer.BgpPeerID)));

                // Add inbound prefixes which are for all BGP peers

                bgpPeer.Ipv4InboundRoutingPolicy.TenantInboundBgpIpv4Prefixes.AddRange(inboundPrefixes);

                // Add inbound communities for the current BGP peer

                bgpPeer.Ipv4InboundRoutingPolicy.TenantInboundBgpCommunities.AddRange(mapper.Map<List<TenantInboundBgpCommunityNetModel>>
                    (source.VpnTenantCommunitiesIn.Where(x => x.BgpPeerID == bgpPeer.BgpPeerID)));

                // Add inbound communities which are for all BGP peers

                bgpPeer.Ipv4InboundRoutingPolicy.TenantInboundBgpCommunities.AddRange(inboundCommunities);

                // Add outbound prefixes for the current BGP peer

                bgpPeer.Ipv4OutboundRoutingPolicy.TenantOutboundBgpIpv4Prefixes.AddRange(mapper.Map<List<TenantOutboundBgpIpv4PrefixNetModel>>
                    (source.VpnTenantNetworksOut.Where(x => x.BgpPeerID == bgpPeer.BgpPeerID)));

                // Add outbound communities for the current BGP peer

                bgpPeer.Ipv4OutboundRoutingPolicy.TenantOutboundBgpCommunities.AddRange(mapper.Map<List<TenantOutboundBgpCommunityNetModel>>
                    (source.VpnTenantCommunitiesOut.Where(x => x.BgpPeerID == bgpPeer.BgpPeerID)));
            }
        }
    }

    public class Ipv4VpnServiceNetModel : IpVpnServiceNetModel
    {
        public List<Ipv4VpnAttachmentSetNetModel> VpnAttachmentSets { get; set; }
        public string AddressFamilyName { get; set; }
        public Ipv4VpnServiceNetModel(Vpn source, ResolutionContext context)
        {
            var mapper = context.Mapper;

            Name = source.Name;
            ProtocolType = source.VpnTopologyType.VpnProtocolType.Name;
            TopologyType = source.VpnTopologyType.Name;
            IsExtranet = source.IsExtranet;
            AddressFamilyName = source.AddressFamily.Name;

            if (source.VpnTopologyType.TopologyType == Models.TopologyType.AnytoAny)
            {
                RouteTargetA = mapper.Map<RouteTargetNetModel>(source.RouteTargets.Single());
            }
            else
            {
                RouteTargetA = mapper.Map<RouteTargetNetModel>(source.RouteTargets.Single(r => !r.IsHubExport));
                RouteTargetB = mapper.Map<RouteTargetNetModel>(source.RouteTargets.Single(r => r.IsHubExport));
            }

            VpnAttachmentSets = new List<Ipv4VpnAttachmentSetNetModel>();
        }
    }

    #endregion
}

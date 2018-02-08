using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SCM.Models.NetModels.Ipv4VpnNetModels;
using AutoMapper;

namespace SCM.Models.NetModels.Ipv4MulticastVpnNetModels
{
    #region Interface Definitions

    public interface IIpv4MulticastRoutingInstanceNetModel : IIpv4RoutingInstanceNetModel
    {
        int MulticastDesignatedRouterPreference { get; set; }
    }

    #endregion

    #region Class Definitions

    public class Ipv4MulticastRoutingInstanceNetModel : Ipv4RoutingInstanceNetModel, IIpv4MulticastRoutingInstanceNetModel
    {
        public int MulticastDesignatedRouterPreference { get; set; }
    }

    public class Ipv4MulticastPeNetModel : Ipv4PeNetModel
    {
        public new IEnumerable<IIpv4MulticastRoutingInstanceNetModel> RoutingInstances { get; set; }
        public Ipv4MulticastPeNetModel()
        {
            RoutingInstances = new List<Ipv4MulticastRoutingInstanceNetModel>();
        }
    }

    public class Ipv4MulticastVpnAttachmentSetNetModel : Ipv4VpnAttachmentSetNetModel
    {
        public new List<Ipv4MulticastPeNetModel> PEs { get; set; }
        public MulticastAsmServiceNetModel MulticastAsmService { get; set; }
        public MulticastSsmServiceNetModel MulticastSsmService { get; set; }
        public Ipv4MulticastVpnAttachmentSetNetModel(VpnAttachmentSet source, ResolutionContext context) : base(source, context)
        {
            var mapper = context.Mapper;
            var devices = source.AttachmentSet.AttachmentSetRoutingInstances.Select(s => s.RoutingInstance.Device).ToList();
            var PEs = mapper.Map<List<Ipv4MulticastPeNetModel>>(devices);

            if (source.Vpn.MulticastVpnServiceType.MvpnServiceType == MvpnServiceType.ASM)
            {
                this.MulticastAsmService = new MulticastAsmServiceNetModel();
                this.MulticastAsmService.MulticastVpnRps = mapper.Map<List<MulticastVpnRpNetModel>>(source.AttachmentSet.MulticastVpnRps);
                this.MulticastAsmService.MulticastVpnDomainType = source.AttachmentSet.MulticastVpnDomainType.Name;
                this.MulticastAsmService.IsDirectlyIntegrated = source.IsMulticastDirectlyIntegrated.GetValueOrDefault();
                this.MulticastSsmService = null;
            }
            else if (source.Vpn.MulticastVpnServiceType.MvpnServiceType == MvpnServiceType.SSM)
            {
                this.MulticastSsmService = new MulticastSsmServiceNetModel();
                this.MulticastSsmService.MulticastVpnSsmGroups = mapper.Map<List<MulticastVpnSsmGroupNetModel>>(source.AttachmentSet.VpnTenantMulticastGroups);
                this.MulticastSsmService.MulticastVpnDomainType = source.AttachmentSet.MulticastVpnDomainType.Name;
                this.MulticastSsmService.IsDirectlyIntegrated = source.IsMulticastDirectlyIntegrated.GetValueOrDefault();
                this.MulticastAsmService = null;
            }

            foreach (Ipv4MulticastPeNetModel pe in PEs)
            {
                var sourceRoutingInstances = source.AttachmentSet.AttachmentSetRoutingInstances.Where(v => v.RoutingInstance.Device.Name == pe.PEName);
                var vrfs = mapper.Map<List<Ipv4MulticastRoutingInstanceNetModel>>(sourceRoutingInstances);

                base.PopulateRoutingInstanceNetModels(vrfs, source.AttachmentSet, mapper);

                pe.RoutingInstances = vrfs;
            }

            this.PEs = PEs;
        }
    }

    public class MulticastVpnRpNetModel
    {
        public string RendezvousPointIpAddress { get; set; }
        public List<MulticastVpnAsmGroupNetModel> MulticastGroups { get; set; }

        public MulticastVpnRpNetModel()
        {
            MulticastGroups = new List<MulticastVpnAsmGroupNetModel>();
        }
    }

    public class MulticastVpnAsmGroupNetModel
    {
        public string MulticastGroupAddress { get; set; }
        public string MulticastGroupMask { get; set; }
    }

    public class MulticastVpnSsmGroupNetModel
    {
        public string MulticastSourceAddress { get; set; }
        public string MulticastSourceMask { get; set; }
        public string MulticastGroupAddress { get; set; }
        public string MulticastGroupMask { get; set; }
    }

    public class MulticastVpnNetModel
    {
        public string MulticastVpnServiceType { get; set; }
        public string MulticastVpnDirectionType { get; set; }
    }

    public class MulticastAsmServiceNetModel
    {
        public List<MulticastVpnRpNetModel> MulticastVpnRps { get; set; }
        public MulticastAsmServiceNetModel()
        {
            MulticastVpnRps = new List<MulticastVpnRpNetModel>();
        }
        public string MulticastVpnDomainType { get; set; }
        public bool IsDirectlyIntegrated { get; set; }
    }

    public class MulticastSsmServiceNetModel
    {
        public List<MulticastVpnSsmGroupNetModel> MulticastVpnSsmGroups { get; set; }
        public MulticastSsmServiceNetModel()
        {
            MulticastVpnSsmGroups = new List<MulticastVpnSsmGroupNetModel>();
        }
        public string MulticastVpnDomainType { get; set; }
        public bool IsDirectlyIntegrated { get; set; }
    }

    public class Ipv4MulticastVpnServiceNetModel : Ipv4VpnServiceNetModel
    {
        public bool IsMulticastVpn { get; set; }
        public MulticastVpnNetModel MulticastVpn { get; set; }
        public bool ShouldSerializeMulticastVpn()
        {
            return IsMulticastVpn;
        }
        public new List<Ipv4MulticastVpnAttachmentSetNetModel> VpnAttachmentSets { get; set; }
        public Ipv4MulticastVpnServiceNetModel(Vpn vpn, ResolutionContext context) : base(vpn, context)
        {
            MulticastVpn = new MulticastVpnNetModel();
            VpnAttachmentSets = new List<Ipv4MulticastVpnAttachmentSetNetModel>();
        }
    }

    #endregion
}
using AutoMapper;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using SCM.Services;

namespace SCM.Models.NetModels.AttachmentNetModels
{
    public class AutoMapperAttachmentServiceProfileConfiguration : Profile
    {
        public AutoMapperAttachmentServiceProfileConfiguration()
        {
            CreateMap<Attachment, UntaggedAttachmentInterfaceNetModel>()
               .ForMember(dest => dest.EnableIpv4, conf => conf.MapFrom(src => src.IsLayer3))
               .ForMember(dest => dest.AttachmentBandwidth, conf => conf.MapFrom(src => src.AttachmentBandwidth.BandwidthGbps))
               .ForMember(dest => dest.ContractBandwidthPool, conf => conf.MapFrom(src => src.ContractBandwidthPool))
               .ForMember(dest => dest.PolicyBandwidth, conf => conf.MapFrom(src => src.ContractBandwidthPool))
               .ForMember(dest => dest.InterfaceName, conf => conf.MapFrom(src => src.Interfaces.Single().Ports.Single().Name))
               .ForMember(dest => dest.InterfaceType, conf => conf.MapFrom(src => src.Interfaces.Single().Ports.Single().Type))
               .ForMember(dest => dest.InterfaceMtu, conf => conf.MapFrom(src => src.Mtu.MtuValue))
               .ForMember(dest => dest.RoutingInstanceName, conf => conf.MapFrom(src => src.RoutingInstance.Name))
               .ForMember(dest => dest.Ipv4, conf => conf.MapFrom(src => src.IsLayer3 ? src.Interfaces.Single() : null))
               .Include<Attachment, UntaggedAttachmentInterfaceServiceNetModel>();

            CreateMap<Attachment, UntaggedAttachmentInterfaceServiceNetModel>();

            CreateMap<Attachment, TaggedAttachmentInterfaceNetModel>()
                .ForMember(dest => dest.AttachmentBandwidth, conf => conf.MapFrom(src => src.AttachmentBandwidth.BandwidthGbps))
                .ForMember(dest => dest.InterfaceName, conf => conf.MapFrom(src => src.Interfaces.Single().Ports.Single().Name))
                .ForMember(dest => dest.InterfaceType, conf => conf.MapFrom(src => src.Interfaces.Single().Ports.Single().Type))
                .ForMember(dest => dest.InterfaceMtu, conf => conf.MapFrom(src => src.Mtu.MtuValue))
                .ForMember(dest => dest.ContractBandwidthPools, conf => conf.MapFrom(src => src.Vifs.Select(q => q.ContractBandwidthPool)
                    .GroupBy(q => q.Name)
                    .Select(group => group.First())))
                .ForMember(dest => dest.Vifs, conf => conf.MapFrom(src => src.Vifs.SelectMany(q => q.Vlans)))
                .Include<Attachment, TaggedAttachmentInterfaceServiceNetModel>();

            CreateMap<Attachment, TaggedAttachmentInterfaceServiceNetModel>();

            CreateMap<Attachment, UntaggedAttachmentBundleInterfaceNetModel>()
                .ForMember(dest => dest.EnableIpv4, conf => conf.MapFrom(src => src.IsLayer3))
                .ForMember(dest => dest.AttachmentBandwidth, conf => conf.MapFrom(src => src.AttachmentBandwidth.BandwidthGbps))
                .ForMember(dest => dest.ContractBandwidthPool, conf => conf.MapFrom(src => src.ContractBandwidthPool))
                .ForMember(dest => dest.PolicyBandwidth, conf => conf.MapFrom(src => src.ContractBandwidthPool))
                .ForMember(dest => dest.BundleInterfaceMembers, conf => conf.MapFrom(src => src.Interfaces.SelectMany(q => q.Ports)))
                .ForMember(dest => dest.RoutingInstanceName, conf => conf.MapFrom(src => src.RoutingInstance.Name))
                .ForMember(dest => dest.Ipv4, conf => conf.MapFrom(src => src.IsLayer3 ? src.Interfaces.Single() : null))
                .Include<Attachment, UntaggedAttachmentBundleInterfaceServiceNetModel>();

            CreateMap<Attachment, UntaggedAttachmentBundleInterfaceServiceNetModel>();

            CreateMap<Attachment, TaggedAttachmentBundleInterfaceNetModel>()
              .ForMember(dest => dest.AttachmentBandwidth, conf => conf.MapFrom(src => src.AttachmentBandwidth.BandwidthGbps))
              .ForMember(dest => dest.BundleID, conf => conf.MapFrom(src => src.ID))
              .ForMember(dest => dest.BundleInterfaceMembers, conf => conf.MapFrom(src => src.Interfaces.SelectMany(q => q.Ports)))
              .ForMember(dest => dest.ContractBandwidthPools, conf => conf.MapFrom(src => src.Vifs.Select(q => q.ContractBandwidthPool)
                    .GroupBy(q => q.Name)
                    .Select(group => group.First())))
              .ForMember(dest => dest.Vifs, conf => conf.MapFrom(src => src.Vifs.SelectMany(q => q.Vlans)))
              .Include<Attachment, TaggedAttachmentBundleInterfaceServiceNetModel>();

            CreateMap<Attachment, TaggedAttachmentBundleInterfaceServiceNetModel>();

            CreateMap<Attachment, UntaggedAttachmentMultiPortNetModel>()
               .ForMember(dest => dest.AttachmentBandwidth, conf => conf.MapFrom(src => src.AttachmentBandwidth.BandwidthGbps))
               .ForMember(dest => dest.MultiPortMembers, conf => conf.MapFrom(src => src.Interfaces.SelectMany(q => q.Ports)))
               .ForMember(dest => dest.Name, conf => conf.MapFrom(src => src.ID))
               .Include<Attachment, UntaggedAttachmentMultiPortServiceNetModel>();

            CreateMap<Attachment, UntaggedAttachmentMultiPortServiceNetModel>();

            CreateMap<Attachment, TaggedAttachmentMultiPortNetModel>()
                .ForMember(dest => dest.AttachmentBandwidth, conf => conf.MapFrom(src => src.AttachmentBandwidth.BandwidthGbps))
                .ForMember(dest => dest.ContractBandwidthPools, conf => conf.MapFrom(src => src.Vifs.Select(q => q.ContractBandwidthPool)
                     .GroupBy(q => q.Name)
                     .Select(group => group.First())))
                .ForMember(dest => dest.MultiPortMembers, conf => conf.MapFrom(src => src.Interfaces.SelectMany(q => q.Ports)))
                .ForMember(dest => dest.Name, conf => conf.MapFrom(src => src.ID))
                .Include<Attachment, TaggedAttachmentMultiPortServiceNetModel>();

            CreateMap<Attachment, TaggedAttachmentMultiPortServiceNetModel>();

            CreateMap<Attachment, RoutingInstanceNetModel>()
                .ForMember(dest => dest.RoutingInstanceName, conf => conf.MapFrom(src => src.RoutingInstance.Name))
                .ForMember(dest => dest.AdministratorSubField, conf => conf.MapFrom(src => src.RoutingInstance.AdministratorSubField))
                .ForMember(dest => dest.AssignedNumberSubField, conf => conf.MapFrom(src => src.RoutingInstance.AssignedNumberSubField))
                .ForMember(dest => dest.BgpPeers, conf => conf.MapFrom(src => src.RoutingInstance.BgpPeers))
                .Include<Attachment, RoutingInstanceServiceNetModel>();

            CreateMap<Attachment, RoutingInstanceServiceNetModel>();

            CreateMap<Vlan, AttachmentVifNetModel>()
               .ForMember(dest => dest.EnableIpv4, conf => conf.MapFrom(src => src.Vif.IsLayer3))
               .ForMember(dest => dest.VlanID, conf => conf.MapFrom(src => src.Vif.VlanTag))
               .ForMember(dest => dest.ContractBandwidthPoolName, conf => conf.MapFrom(src => src.Vif.ContractBandwidthPool.Name))
               .ForMember(dest => dest.RoutingInstanceName, conf => conf.MapFrom(src => src.Vif.RoutingInstance.Name))
               .ForMember(dest => dest.Ipv4, conf => conf.MapFrom(src => src.Vif.IsLayer3 ? src : null))
               .Include<Vlan, AttachmentVifServiceNetModel>();

            CreateMap<Vlan, AttachmentVifServiceNetModel>();

            CreateMap<Vlan, MultiPortVifNetModel>()
               .ForMember(dest => dest.VlanID, conf => conf.MapFrom(src => src.Vif.VlanTag))
               .ForMember(dest => dest.EnableIpv4, conf => conf.MapFrom(src => src.Vif.IsLayer3))
               .ForMember(dest => dest.RoutingInstanceName, conf => conf.MapFrom(src => src.Vif.RoutingInstance.Name))
               .ForMember(dest => dest.Ipv4, conf => conf.MapFrom(src => src.Vif.IsLayer3 ? src : null))
               .ForMember(dest => dest.PolicyBandwidthName, conf => conf.MapFrom(src => $"{src.Vif.ContractBandwidthPool.Name}-{src.Interface.InterfaceID}"))
               .Include<Vlan, MultiPortVifServiceNetModel>();

            CreateMap<Vlan, MultiPortVifServiceNetModel>();

            CreateMap<Vif, RoutingInstanceNetModel>()
              .ForMember(dest => dest.RoutingInstanceName, conf => conf.MapFrom(src => src.RoutingInstance.Name))
              .ForMember(dest => dest.AdministratorSubField, conf => conf.MapFrom(src => src.RoutingInstance.AdministratorSubField))
              .ForMember(dest => dest.AssignedNumberSubField, conf => conf.MapFrom(src => src.RoutingInstance.AssignedNumberSubField))
              .ForMember(dest => dest.BgpPeers, conf => conf.MapFrom(src => src.RoutingInstance.BgpPeers))
              .Include<Vif, RoutingInstanceServiceNetModel>();

            CreateMap<Vif, RoutingInstanceServiceNetModel>();

            CreateMap<Attachment, AttachmentServiceNetModel>().ConvertUsing(new AttachmentTypeConverter());

            CreateMap<Vif, AttachmentServiceNetModel>().ConvertUsing(new VifTypeConverter());

            CreateMap<Port, BundleInterfaceMemberNetModel>()
                .ForMember(dest => dest.InterfaceType, conf => conf.MapFrom(src => src.Type))
                .ForMember(dest => dest.InterfaceName, conf => conf.MapFrom(src => src.Name))
                .ForMember(dest => dest.InterfaceMtu, conf => conf.MapFrom(src => src.Interface.Attachment.Mtu.MtuValue));

            CreateMap<Port, UntaggedMultiPortMemberNetModel>()
                .ForMember(dest => dest.InterfaceType, conf => conf.MapFrom(src => src.Type))
                .ForMember(dest => dest.InterfaceName, conf => conf.MapFrom(src => src.Name))
                .ForMember(dest => dest.InterfaceMtu, conf => conf.MapFrom(src => src.Interface.Attachment.Mtu.MtuValue))
                .ForMember(dest => dest.PolicyBandwidth, conf => conf.ResolveUsing(new UntaggedMultiPortMemberPolicyBandwidthResolver()))
                .ForMember(dest => dest.RoutingInstanceName, conf => conf.MapFrom(src => src.Interface.Attachment.RoutingInstance.Name))
                .ForMember(dest => dest.EnableIpv4, conf => conf.MapFrom(src => src.Interface.Attachment.IsLayer3))
                .ForMember(dest => dest.Ipv4, conf => conf.MapFrom(src => src.Interface.Attachment.IsLayer3 ? src.Interface : null));

            CreateMap<Port, TaggedMultiPortMemberNetModel>()
                .ForMember(dest => dest.InterfaceType, conf => conf.MapFrom(src => src.Type))
                .ForMember(dest => dest.InterfaceName, conf => conf.MapFrom(src => src.Name))
                .ForMember(dest => dest.InterfaceMtu, conf => conf.MapFrom(src => src.Interface.Attachment.Mtu.MtuValue))
                .ForMember(dest => dest.PolicyBandwidths, conf => conf.ResolveUsing(new TaggedMultiPortMemberPolicyBandwidthResolver()))
                .ForMember(dest => dest.Vifs, conf => conf.MapFrom(src => src.Interface.Vlans));

            CreateMap<Interface, Ipv4NetModel>()
                .ForMember(dest => dest.PrefixLength, conf => conf.MapFrom(src => IPNetwork.Parse(src.IpAddress, src.SubnetMask).Cidr));

            CreateMap<Vlan, Ipv4NetModel>()
                .ForMember(dest => dest.PrefixLength, conf => conf.MapFrom(src => IPNetwork.Parse(src.IpAddress, src.SubnetMask).Cidr));

            CreateMap<RoutingInstance, RoutingInstanceNetModel>()
                .ForMember(dest => dest.RoutingInstanceName, conf => conf.MapFrom(src => src.Name))
                .ForMember(dest => dest.AdministratorSubField, conf => conf.MapFrom(src => src.AdministratorSubField))
                .ForMember(dest => dest.AssignedNumberSubField, conf => conf.MapFrom(src => src.AssignedNumberSubField));

            CreateMap<BgpPeer, BgpPeerNetModel>()
                .ForMember(dest => dest.PeerIpv4Address, conf => conf.MapFrom(src => src.IpAddress))
                .ForMember(dest => dest.PeerAutonomousSystem, conf => conf.MapFrom(src => src.AutonomousSystem));

            CreateMap<ContractBandwidthPool, ContractBandwidthPoolNetModel>()
                .ForMember(dest => dest.ContractBandwidth, conf => conf.MapFrom(src => src.ContractBandwidth.BandwidthMbps))
                .Include<ContractBandwidthPool, ContractBandwidthPoolServiceNetModel>();

            CreateMap<ContractBandwidthPool, ContractBandwidthPoolServiceNetModel>();

            CreateMap<ContractBandwidthPool, PolicyBandwidthNetModel>()
                .ForMember(dest => dest.Name, conf => conf.MapFrom(src => src.Name))
                .ForMember(dest => dest.Bandwidth, conf => conf.MapFrom(src => src.ContractBandwidth.BandwidthMbps));

            CreateMap<Vlan, TaggedMultiPortPolicyBandwidthNetModel>()
                .ConvertUsing(new TaggedMultiPortPolicyBandwidthTypeConverter());

            CreateMap<Vlan, TaggedMultiPortPolicyBandwidthServiceNetModel>()
                .ConvertUsing(new TaggedMultiPortPolicyBandwidthServiceTypeConverter());

            CreateMap<PolicyBandwidth, TaggedMultiPortPolicyBandwidthNetModel>();

            CreateMap<PolicyBandwidth, TaggedMultiPortPolicyBandwidthServiceNetModel>();
                
            CreateMap<Device, AttachmentServiceNetModel>().ConvertUsing(new DeviceTypeConverter());

        }

        public class DeviceTypeConverter : ITypeConverter<Device, AttachmentServiceNetModel>
        {
            public AttachmentServiceNetModel Convert(Device source, AttachmentServiceNetModel destination, ResolutionContext context)
            {
                var result = new AttachmentServiceNetModel();
                var mapper = context.Mapper;

                result.PEName = source.Name;
                result.RoutingInstances = mapper.Map<List<RoutingInstanceNetModel>>(source.RoutingInstances
                    .Where(x => x.RoutingInstanceType.IsVrf));

                if (source.Attachments != null)
                {
                    // Only Tenant-Facing Attachments are supported for network sync
                    var attachments = source.Attachments.Where(x => x.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleType.TenantFacing);

                    foreach (var attachment in attachments)
                    {
                        if (attachment.IsBundle)
                        {
                            if (attachment.IsTagged)
                            {
                                result.TaggedAttachmentBundleInterfaces.Add(mapper.Map<TaggedAttachmentBundleInterfaceNetModel>(attachment));
                            }
                            else
                            {
                                result.UntaggedAttachmentBundleInterfaces.Add(mapper.Map<UntaggedAttachmentBundleInterfaceNetModel>(attachment));
                            }
                        }
                        else if (attachment.IsMultiPort)
                        {
                            if (attachment.IsTagged)
                            {
                                result.TaggedAttachmentMultiPorts.Add(mapper.Map<TaggedAttachmentMultiPortNetModel>(attachment));
                            }
                            else
                            {
                                result.UntaggedAttachmentMultiPorts.Add(mapper.Map<UntaggedAttachmentMultiPortNetModel>(attachment));
                            }
                        }
                        else
                        {
                            if (attachment.IsTagged)
                            {
                                result.TaggedAttachmentInterfaces.Add(mapper.Map<TaggedAttachmentInterfaceNetModel>(attachment));
                            }
                            else
                            {
                                result.UntaggedAttachmentInterfaces.Add(mapper.Map<UntaggedAttachmentInterfaceNetModel>(attachment));
                            }
                        }
                    }
                }

                return result;
            }
        }

        public class AttachmentTypeConverter : ITypeConverter<Attachment, AttachmentServiceNetModel>
        {
            public AttachmentServiceNetModel Convert(Attachment source, AttachmentServiceNetModel destination, ResolutionContext context)
            {
                var result = new AttachmentServiceNetModel();
                var mapper = context.Mapper;

                result.PEName = source.Device.Name;
                var vrfs = new List<RoutingInstance>();

                if (source.IsBundle)
                {
                    if (source.IsTagged)
                    {
                        result.TaggedAttachmentBundleInterfaces.Add(mapper.Map<TaggedAttachmentBundleInterfaceNetModel>(source));
                        result.RoutingInstances.AddRange(mapper.Map<List<RoutingInstanceNetModel>>(source.Vifs.Select(q => q.RoutingInstance)));
                    }
                    else
                    {
                        result.UntaggedAttachmentBundleInterfaces.Add(mapper.Map<UntaggedAttachmentBundleInterfaceNetModel>(source));
                        result.RoutingInstances.Add(mapper.Map<RoutingInstanceNetModel>(source.RoutingInstance));
                    }
                }
                else if (source.IsMultiPort)
                {
                    if (source.IsTagged)
                    {
                        result.TaggedAttachmentMultiPorts.Add(mapper.Map<TaggedAttachmentMultiPortNetModel>(source));
                        result.RoutingInstances.AddRange(mapper.Map<List<RoutingInstanceNetModel>>(source.Vifs.Select(q => q.RoutingInstance)));
                    }
                    else
                    {
                        result.UntaggedAttachmentMultiPorts.Add(mapper.Map<UntaggedAttachmentMultiPortNetModel>(source));
                        result.RoutingInstances.Add(mapper.Map<RoutingInstanceNetModel>(source.RoutingInstance));
                    }
                }
                else
                {
                    if (source.IsTagged)
                    {
                        result.TaggedAttachmentInterfaces.Add(mapper.Map<TaggedAttachmentInterfaceNetModel>(source));
                        result.RoutingInstances.AddRange(mapper.Map<List<RoutingInstanceNetModel>>(source.Vifs.Select(q => q.RoutingInstance)));
                    }
                    else
                    {
                        result.UntaggedAttachmentInterfaces.Add(mapper.Map<UntaggedAttachmentInterfaceNetModel>(source));
                        result.RoutingInstances.Add(mapper.Map<RoutingInstanceNetModel>(source.RoutingInstance));
                    }
                }
                return result;
            }
        }

        public class VifTypeConverter : ITypeConverter<Vif, AttachmentServiceNetModel>
        {
            public AttachmentServiceNetModel Convert(Vif source, AttachmentServiceNetModel destination, ResolutionContext context)
            {
                var result = new AttachmentServiceNetModel();
                var mapper = context.Mapper;

                result.PEName = source.Attachment.Device.Name;

                if (source.Attachment.IsBundle)
                {
                    if (source.Attachment.IsTagged)
                    {
                        var data = mapper.Map<TaggedAttachmentBundleInterfaceNetModel>(source.Attachment);
                        var vif = mapper.Map<AttachmentVifNetModel>(source.Vlans.Single());
                        data.Vifs.Add(vif);
                        data.ContractBandwidthPools.Add(mapper.Map<ContractBandwidthPoolNetModel>(source.ContractBandwidthPool));
                        result.TaggedAttachmentBundleInterfaces.Add(data);
                        result.RoutingInstances.Add(mapper.Map<RoutingInstanceNetModel>(source.RoutingInstance));
                    }
                }
                else if (source.Attachment.IsMultiPort)
                {
                    if (source.Attachment.IsTagged)
                    {
                        var data = mapper.Map<TaggedAttachmentMultiPortNetModel>(source.Attachment);

                        // Filter data to remove all VIFs and policy-bandwidths other than the VIF we're interested in
                        // and the policy-bandwidth for that VIF

                        foreach (var member in data.MultiPortMembers)
                        {
                            member.Vifs = member.Vifs.Where(q => q.VlanID == source.VlanTag).ToList();
                            var vif = member.Vifs.Single();
                            member.PolicyBandwidths = member.PolicyBandwidths.Where(q => q.Name == vif.PolicyBandwidthName).ToList();
                        }

                        data.ContractBandwidthPools.Add(mapper.Map<ContractBandwidthPoolNetModel>(source.ContractBandwidthPool));
                        result.TaggedAttachmentMultiPorts.Add(data);
                        result.RoutingInstances.Add(mapper.Map<RoutingInstanceNetModel>(source.RoutingInstance));
                    }
                }
                else
                {
                    if (source.Attachment.IsTagged)
                    {
                        var data = mapper.Map<TaggedAttachmentInterfaceNetModel>(source.Attachment);
                        var vif = mapper.Map<AttachmentVifNetModel>(source.Vlans.Single());
                        data.Vifs.Add(vif);
                        data.ContractBandwidthPools.Add(mapper.Map<ContractBandwidthPoolNetModel>(source.ContractBandwidthPool));
                        result.TaggedAttachmentInterfaces.Add(data);
                        result.RoutingInstances.Add(mapper.Map<RoutingInstanceNetModel>(source.RoutingInstance));
                    }
                }

                return result;
            }
        }
    }

    public class UntaggedMultiPortMemberPolicyBandwidthResolver : IValueResolver<Port, UntaggedMultiPortMemberNetModel, PolicyBandwidthNetModel>
    {
        public PolicyBandwidthNetModel Resolve(Port source, UntaggedMultiPortMemberNetModel destination, PolicyBandwidthNetModel destMember, ResolutionContext context)
        {
            var memberPortCount = source.Interface.Ports.Count();
            var portBandwidthGbps = source.PortBandwidth.BandwidthGbps;
            var contractBandwidthMbps = source.Interface.Attachment.ContractBandwidthPool.ContractBandwidth.BandwidthMbps;

            return new PolicyBandwidthNetModel()
            {
                Name = $"{source.Interface.Attachment.ContractBandwidthPool.Name}-{source.InterfaceID}",
                Bandwidth = contractBandwidthMbps / memberPortCount
            };
        }
    }

    public class TaggedMultiPortMemberPolicyBandwidthResolver : IValueResolver<Port, TaggedMultiPortMemberNetModel, List<TaggedMultiPortPolicyBandwidthNetModel>>
    {
        public List<TaggedMultiPortPolicyBandwidthNetModel> Resolve(Port source, TaggedMultiPortMemberNetModel destination, List<TaggedMultiPortPolicyBandwidthNetModel> destMember, ResolutionContext context)
        {
            var attachment = source.Interface.Attachment;
            var result = new List<TaggedMultiPortPolicyBandwidthNetModel>();

            foreach (var vif in attachment.Vifs)
            {
                var policyBandwidth = MultiPortPolicyBandwidth.VifPolicyBandwidth(source.Interface, vif);
                result.Add(context.Mapper.Map<TaggedMultiPortPolicyBandwidthNetModel>(policyBandwidth));
            }

            // Return only unique (according to the name property) Policy Bandwidths

            return result
                .GroupBy(q => q.Name)
                .Select(group => group.First())
                .ToList();
        }
    }

    public class TaggedMultiPortPolicyBandwidthTypeConverter : ITypeConverter<Vlan, TaggedMultiPortPolicyBandwidthNetModel>
    {
        public TaggedMultiPortPolicyBandwidthNetModel Convert(Vlan source, TaggedMultiPortPolicyBandwidthNetModel destination, ResolutionContext context)
        {
            var policyBandwidth = MultiPortPolicyBandwidth.VifPolicyBandwidth(source.Interface, source.Vif);
            return context.Mapper.Map<TaggedMultiPortPolicyBandwidthServiceNetModel>(policyBandwidth);
        }
    }

    public class TaggedMultiPortPolicyBandwidthServiceTypeConverter : ITypeConverter<Vlan, TaggedMultiPortPolicyBandwidthServiceNetModel>
    {
        public TaggedMultiPortPolicyBandwidthServiceNetModel Convert(Vlan source, TaggedMultiPortPolicyBandwidthServiceNetModel destination, ResolutionContext context)
        {
            var policyBandwidth = MultiPortPolicyBandwidth.VifPolicyBandwidth(source.Interface, source.Vif);
            return context.Mapper.Map<TaggedMultiPortPolicyBandwidthServiceNetModel>(policyBandwidth);
        }
    }

    /// <summary>
    /// Helper class to generate Policy Bandwidth for a VIF which is 
    /// a member of a MultiPort Attachment.
    /// </summary>
    public static class MultiPortPolicyBandwidth
    {
        public static PolicyBandwidth VifPolicyBandwidth(Interface iface, Vif vif)
        {
            var attachment = vif.Attachment;
            var membersCount = attachment.Interfaces.Count();
            var bandwidth = vif.ContractBandwidthPool.ContractBandwidth.BandwidthMbps / membersCount;

            // Policy bandwidth calculation allow for inflation of 10%, per Nova Architecture

            return new PolicyBandwidth()
            {
                Name = $"{vif.ContractBandwidthPool.Name}-{iface.InterfaceID}",
                Bandwidth = (int)Math.Round(bandwidth * 1.1, MidpointRounding.AwayFromZero),
                ContractBandwidthPoolName = vif.ContractBandwidthPool.Name
            };
        }
    }

    public class PolicyBandwidth
    {
        public string Name { get; set; }
        public int Bandwidth { get; set; }
        public string ContractBandwidthPoolName { get; set; }
    }
}
using AutoMapper;
using SCM.Models.RequestModels;
using System.Linq;
using System;
using System.Collections.Generic;
using Mind.WebUI.Models;

namespace Mind.WebUI.Models
{
    public class AutoMapperViewModelProfileConfiguration : Profile
    {
        public AutoMapperViewModelProfileConfiguration()
        {
            // Map between Entity Models and View Models to return data to the client browser

            CreateMap<SCM.Models.Attachment, Mind.WebUI.Models.ProviderDomainAttachmentViewModel>()
                .ForMember(dst => dst.InfrastructureDeviceName, conf => conf.MapFrom(src => src.Device.Name))
                .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.Tenant.Name))
                .ForMember(dst => dst.LocationName, conf => conf.MapFrom(src => src.Device.Location.SiteName))
                .ForMember(dst => dst.PlaneName, conf => conf.MapFrom(src => src.Device.Plane.Name))
                .ForMember(dst => dst.AttachmentBandwidthGbps, conf => conf.MapFrom(src => src.AttachmentBandwidth.BandwidthGbps))
                .ForMember(dst => dst.Mtu, conf => conf.MapFrom(src => src.Mtu.MtuValue))
                .ForMember(dst => dst.AttachmentRoleName, conf => conf.MapFrom(src => src.AttachmentRole.Name));

            CreateMap<SCM.Models.Attachment, Mind.WebUI.Models.ProviderDomainAttachmentUpdateViewModel>()
                .ForMember(dst => dst.ContractBandwidthMbps, conf => conf.MapFrom(src => src.ContractBandwidthPool.ContractBandwidth.BandwidthMbps))
                .ForMember(dst => dst.ExistingRoutingInstanceName, conf => conf.MapFrom(src => src.RoutingInstance.Name))
                .ForMember(dst => dst.UseJumboMtu, conf => conf.MapFrom(src => src.Mtu.IsJumbo));

            CreateMap<SCM.Models.Attachment, Mind.WebUI.Models.ProviderDomainAttachmentDeleteViewModel>()
                .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.Tenant.Name));

            CreateMap<SCM.Models.Device, Mind.WebUI.Models.InfrastructureDeviceViewModel>()
                .ForMember(dst => dst.DeviceModel, conf => conf.MapFrom(src => src.DeviceModel.Name))
                .ForMember(dst => dst.DeviceStatus, conf => conf.MapFrom(src => src.DeviceStatus.Name))
                .ForMember(dst => dst.LocationName, conf => conf.MapFrom(src => src.Location.SiteName))
                .ForMember(dst => dst.PlaneName, conf => conf.MapFrom(src => src.Plane.Name));

            CreateMap<SCM.Models.Port, Mind.WebUI.Models.PortViewModel>()
                .ForMember(dst => dst.PortId, conf => conf.MapFrom(src => src.ID))
                .ForMember(dst => dst.PortPool, conf => conf.MapFrom(src => src.PortPool.Name))
                .ForMember(dst => dst.PortRole, conf => conf.MapFrom(src => src.PortPool.PortRole.Name))
                .ForMember(dst => dst.PortSfp, conf => conf.MapFrom(src => src.PortSfp.Name))
                .ForMember(dst => dst.PortStatus, conf => conf.MapFrom(src => src.PortStatus.Name))
                .ForMember(dst => dst.PortConnector, conf => conf.MapFrom(src => src.PortConnector.Name))
                .ForMember(dst => dst.PortBandwidthGbps, conf => conf.MapFrom(src => src.PortBandwidth.BandwidthGbps));

            CreateMap<SCM.Models.LogicalInterface, Mind.WebUI.Models.LogicalInterfaceViewModel>()
                .ForMember(dst => dst.RoutingInstanceName, conf => conf.MapFrom(src => src.RoutingInstance.Name));

            CreateMap<SCM.Models.ContractBandwidthPool, Mind.WebUI.Models.ContractBandwidthPoolViewModel>()
                .ForMember(dst => dst.ContractBandwidthMbps, conf => conf.MapFrom(src => src.ContractBandwidth.BandwidthMbps));

            CreateMap<SCM.Models.RoutingInstance, Mind.WebUI.Models.ProviderDomainRoutingInstanceViewModel>()
                .ForMember(dst => dst.ProviderDomainLocationName, conf => conf.MapFrom(src => src.Device.Location.SiteName));

            // View model to entity model mappings

            CreateMap<Mind.WebUI.Models.TenantRequestViewModel, SCM.Models.Tenant>();
            CreateMap<Mind.WebUI.Models.TenantUpdateViewModel, SCM.Models.Tenant>();

        }
    }
}
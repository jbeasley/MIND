using AutoMapper;
using SCM.Models.RequestModels;
using SCM.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Mind.Api.Models
{
    /// <summary>
    /// Automapper profile for the RESTful Web API
    /// </summary>
    public class AutoMapperApiModelProfileConfiguration : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperApiModelProfileConfiguration()
        {

            // Map entity models to API models for return data to the API caller

            CreateMap<SCM.Models.Device, Mind.Api.Models.InfrastructureDevice>()
                .ForMember(dst => dst.DeviceModel, conf => conf.MapFrom(src => src.DeviceModel.Name))
                .ForMember(dst => dst.DeviceStatus, conf => conf.MapFrom(src => src.DeviceStatus.Name))
                .ForMember(dst => dst.LocationName, conf => conf.MapFrom(src => src.Location.SiteName))
                .ForMember(dst => dst.PlaneName, conf => conf.MapFrom(src => src.Plane.Name));
            CreateMap<SCM.Models.RoutingInstance, Mind.Api.Models.RoutingInstance>();
            CreateMap<SCM.Models.ContractBandwidthPool, Mind.Api.Models.ContractBandwidthPool>()
                .ForMember(dst => dst.ContractBandwidthMbps, conf => conf.MapFrom(src => src.ContractBandwidth.BandwidthMbps));
            CreateMap<SCM.Models.Attachment, Mind.Api.Models.Attachment>()
                .ForMember(dst => dst.InfrastructureDevice, conf => conf.MapFrom(src => src.Device))
                .ForMember(dst => dst.AttachmentBandwidthGbps, conf => conf.MapFrom(src => src.AttachmentBandwidth.BandwidthGbps));

            CreateMap<SCM.Models.Port, Mind.Api.Models.Port>()
                .ForMember(dst => dst.PortPool, conf => conf.MapFrom(src => src.PortPool.Name))
                .ForMember(dst => dst.PortRole, conf => conf.MapFrom(src => src.PortPool.PortRole.Name))
                .ForMember(dst => dst.PortSfp, conf => conf.MapFrom(src => src.PortSfp.Name))
                .ForMember(dst => dst.PortStatus, conf => conf.MapFrom(src => src.PortStatus.Name));

            // Map API input request models to entity models

            CreateMap<Tenant, SCM.Models.Tenant>().ReverseMap();

        }
    }
}
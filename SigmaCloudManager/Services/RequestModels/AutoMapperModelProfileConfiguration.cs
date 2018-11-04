using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace SCM.Models.RequestModels
{
    public class AutoMapperServiceModelProfileConfiguration : Profile
    {
        public AutoMapperServiceModelProfileConfiguration()
        {

            CreateMap<AttachmentRequest, Attachment>()
                .ForMember(dest => dest.IsBundle, conf => conf.MapFrom(src => src.BundleRequired))
                .ForMember(dest => dest.IsMultiPort, conf => conf.MapFrom(src => src.MultiPortRequired))
                .ForMember(dest => dest.Interfaces, conf => conf.UseValue(new List<Interface>()));
            CreateMap<AttachmentRequest, RoutingInstance>();
            CreateMap<AttachmentUpdate, Attachment>();
            CreateMap<AttachmentUpdate, ContractBandwidthPool>();
            CreateMap<AttachmentUpdate, RoutingInstance>();
            CreateMap<RoutingInstanceUpdate, RoutingInstance>();
            CreateMap<AttachmentRequest, ContractBandwidthPool>();
        }
    }
}
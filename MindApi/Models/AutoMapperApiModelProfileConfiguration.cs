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

            // Map API input request models to entity models

            CreateMap<Tenant, SCM.Models.Tenant>().ReverseMap();

        }
    }
}
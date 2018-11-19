using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using SCM.Services;
using AutoMapper;

namespace Mind.Api.Controllers
{
   
    /// <summary>
    /// Abstract base controller, inherited by all
    /// API controllers
    /// </summary>
    public abstract class BaseApiController : Controller
    {
        /// <summary>
        /// Reference to the service class supporting the business logic for the current controller
        /// </summary>
        protected internal IBaseService Service { get; set; }

        /// <summary>
        /// Automapper
        /// </summary>
        protected internal IMapper Mapper { get; set; }

        public BaseApiController(IBaseService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using SCM.Services;
using AutoMapper;
using SCM.Data;

namespace SCM.Controllers
{
   
    /// <summary>
    /// Abstract base controller, inherited by all
    /// view controllers
    /// </summary>
    public abstract class BaseViewController : Controller
    {

        /// <summary>
        /// Automapper
        /// </summary>
        protected internal readonly IMapper Mapper;
        protected internal readonly IMapper _mapper;

        /// <summary>
        /// Unit of work for access to repositories
        /// </summary>
        protected internal readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Reference to the service class supporting the business logic for the current controller
        /// </summary>
        protected internal readonly IBaseService Service;
        protected internal readonly IBaseService _service;

        public BaseViewController()
        {
        }

        public BaseViewController(IBaseService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        public BaseViewController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        protected internal string FormatAsHtmlList(IList<string> strList)
        {
            var l = string.Join("", strList.Select(x => $"<li>{x}</li>"));
            return !string.IsNullOrWhiteSpace(l) ? $"<ul>{l}</ul>" : null;
        } 
    }
}
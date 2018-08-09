using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using SCM.Validators;
using SCM.Services;
using AutoMapper;

namespace SCM.Controllers
{
   
    /// <summary>
    /// Abstract base controller, inherited by all
    /// view controllers
    /// </summary>
    public abstract class BaseViewController : Controller
    {

        /// <summary>
        /// Reference to a validator class.
        /// </summary>
        protected internal IValidator Validator { get; set; }

        /// <summary>
        /// Automapper
        /// </summary>
        protected internal readonly IMapper Mapper;

        /// <summary>
        /// Reference to the service class supporting the business logic for the current controller
        /// </summary>
        protected internal readonly IBaseService Service;

        public BaseViewController()
        {
        }

        public BaseViewController(IBaseService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        /// <summary>
        /// Executed before each action method is executed.
        /// Sets the validation dictionary for this controller's validator (if present)
        /// to the current modelstate object.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var validationDic = new ModelStateWrapper(context.ModelState);
            if (this.Validator != null)
            {
                Validator.ValidationDictionary = validationDic;
            }
            if (Service != null)
            {
                if (Service.Validator != null)
                {
                    Service.Validator.ValidationDictionary = validationDic;
                }
            }
        }

        protected internal string FormatAsHtmlList(IList<string> strList)
        {
            var l = string.Join("", strList.Select(x => $"<li>{x}</li>"));
            return !string.IsNullOrWhiteSpace(l) ? $"<ul>{l}</ul>" : null;
        } 
    }
}
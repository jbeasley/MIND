using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCM.Api.Validators;
using SCM.Validators;

namespace SCM.Api.Controllers
{

    /// <summary>
    /// Abstract base controller, inherited by all
    /// API controllers
    /// </summary>
    [ValidateModel]
    [Route("api/v1/")]
    public abstract class BaseApiController : Controller
    {
        public BaseApiController(IMapper mapper)
        {
            Mapper = mapper;
        }

        internal IMapper Mapper { get; set; }

        /// <summary>
        /// Reference to an API validator class.
        /// </summary>
        internal IApiValidator Validator { get; set; }

        /// <summary>
        /// Executed before each action method is executed.
        /// Sets the validation dictionary for this controller's validator (if present)
        /// to the current modelstate object.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (this.Validator != null)
            {
                Validator.ValidationDictionary = new ModelStateWrapper(context.ModelState);
            }
        }
    }
}
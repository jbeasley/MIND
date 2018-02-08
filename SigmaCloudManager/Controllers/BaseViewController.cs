using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using SCM.Validators;

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
        internal IValidator Validator { get; set; }

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

        internal string FormatAsHtmlList(IList<string> strList)
        {
            var l = string.Join("", strList.Select(x => $"<li>{x}</li>"));
            return !string.IsNullOrWhiteSpace(l) ? $"<ul>{l}</ul>" : null;
        } 
    }
}
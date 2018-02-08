using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace SCM.Api
{ 
    public class ValidateNetworkServiceRequestAttribute : ActionFilterAttribute
    {
        public ValidateNetworkServiceRequestAttribute(IOptions<ApplicationConfiguration> applicationConfiguration)
        {
            ApplicationConfiguration = applicationConfiguration;
        }

        private IOptions<ApplicationConfiguration> ApplicationConfiguration { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ApplicationConfiguration.Value.EnableNetworkSync)
            {
                context.Result = new NetworkServiceRequestForbiddenResult(context.ModelState);
            }
        }
    }
}
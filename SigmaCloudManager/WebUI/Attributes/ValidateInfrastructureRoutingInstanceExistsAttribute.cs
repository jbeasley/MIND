using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mind.Services;

namespace Mind.WebUI.Attributes
{
    /// <summary>
    /// Validates that an infrastructure routing instance exists in the database
    /// </summary>
    public class ValidateInfrastructureRoutingInstanceExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateInfrastructureRoutingInstanceExistsAttribute() :
            base(typeof(ValidateInfrastructureRoutingInstanceExistsActionFilter))
        {
        }

        private class ValidateInfrastructureRoutingInstanceExistsActionFilter : IAsyncActionFilter
        {
            private readonly IInfrastructureRoutingInstanceService _infrastructureRoutingInstanceService;
            public ValidateInfrastructureRoutingInstanceExistsActionFilter(IInfrastructureRoutingInstanceService infrastructureRoutingInstanceService)
            {
                _infrastructureRoutingInstanceService = infrastructureRoutingInstanceService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue("routingInstanceId", out object value) && value is int routingInstanceId)
                {

                    if ((await _infrastructureRoutingInstanceService.GetByIDAsync(routingInstanceId, asTrackable: false)) != null)
                    {
                        await next();
                    }
                }

                context.Result = new ViewResult { ViewName = "ItemNotFound" };
                return;
            }
        }
    }
}

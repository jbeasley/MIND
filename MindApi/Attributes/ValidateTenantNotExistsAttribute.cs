using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SCM.Services;

namespace Mind.Api.Attributes
{
    /// <summary>
    /// Validates that a tenant does not already exist by checking the name property
    /// </summary>
    public class ValidateTenantNotExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateTenantNotExistsAttribute() : base(typeof(ValidateTenantNotExistsActionFilter))
        {
        }

        private class ValidateTenantNotExistsActionFilter : IAsyncActionFilter
        {
            private readonly ITenantService _tenantService;
            public ValidateTenantNotExistsActionFilter(ITenantService tenantService)
            {
                _tenantService = tenantService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("name"))
                {
                    var name = context.ActionArguments["name"] as string;
                    if (!String.IsNullOrEmpty(name))
                    {
                        {
                            if ((await _tenantService.GetByNameAsync(name)) != null)
                            {
                                context.ModelState.AddModelError("Name", "The name is already in use.");
                                context.Result = new ValidationFailedResult(context.ModelState);
                                return;
                            }
                        }
                    }
                    await next();
                }
            }
        }
    }
}

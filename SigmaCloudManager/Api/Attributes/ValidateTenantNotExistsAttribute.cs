using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mind.Api.Models;
using Mind.Services;

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
            private readonly IMapper _mapper;

            public ValidateTenantNotExistsActionFilter(ITenantService tenantService, IMapper mapper)
            {
                _tenantService = tenantService;
                _mapper = mapper;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("body"))
                {
                    var tenant = context.ActionArguments["body"] as Tenant;

                    if (!String.IsNullOrEmpty(tenant.Name))
                    {
                        {
                            if ((await _tenantService.GetByNameAsync(tenant.Name)) != null)
                            {
                                context.ModelState.AddModelError("Name", "The name is already in use.");
                                context.Result = new ValidationFailedResult(context.ModelState);
                                return;
                            }
                        }
                    }
                }
                await next();
            }
        }
    }
}

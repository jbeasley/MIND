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
using SCM.Services;

namespace Mind.Api.Attributes
{
    /// <summary>
    /// Validates that a tenant does not already exist by checking the name property
    /// </summary>
    public class ValidateTenantExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateTenantExistsAttribute() : base(typeof(ValidateTenantExistsActionFilter))
        {
        }

        private class ValidateTenantExistsActionFilter : IAsyncActionFilter
        {
            private readonly ITenantService _tenantService;
            private readonly IMapper _mapper;

            public ValidateTenantExistsActionFilter(ITenantService tenantService, IMapper mapper)
            {
                _tenantService = tenantService;
                _mapper = mapper;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("tenantId"))
                {
                    var tenantId = context.ActionArguments["tenantId"] as int?;

                    if (tenantId.HasValue)
                    {
                        {
                            if ((await _tenantService.GetByIDAsync(tenantId.Value)) == null)
                            {
                                context.ModelState.AddModelError(string.Empty, "Could not find the tenant.");
                                context.Result = new NotFoundObjectResult(new ApiResponse(context.ModelState) { Message = "Not found" });
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

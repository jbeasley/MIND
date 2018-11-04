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
using Mind.Api.Controllers;

namespace Mind.WebUI.Attributes
{
    /// <summary>
    /// Validates that a tenant IP network exists in the database
    /// </summary>
    public class ValidateTenantIpNetworkExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateTenantIpNetworkExistsAttribute() : base(typeof(ValidateTenantIpNetworkExistsActionFilter))
        {
        }

        private class ValidateTenantIpNetworkExistsActionFilter : IAsyncActionFilter
        {
            private readonly ITenantIpNetworkService _tenantIpNetworkService;
            public ValidateTenantIpNetworkExistsActionFilter(ITenantIpNetworkService tenantIpNetworkService)
            {
                _tenantIpNetworkService = tenantIpNetworkService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue("tenantIpNetworkId", out object value) && value is int tenantIpNetworkId)
                {

                    if ((await _tenantIpNetworkService.GetByIDAsync(tenantIpNetworkId, asTrackable: false)) != null)
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

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
    /// Validates that a tenant domain device exists in the database
    /// </summary>
    public class ValidateTenantDomainDeviceExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateTenantDomainDeviceExistsAttribute() : base(typeof(ValidateTenantDomainDeviceExistsActionFilter))
        {
        }

        private class ValidateTenantDomainDeviceExistsActionFilter : IAsyncActionFilter
        {
            private readonly ITenantDomainDeviceService _tenantDomainDeviceService;
            public ValidateTenantDomainDeviceExistsActionFilter(ITenantDomainDeviceService tenantDomainDeviceService)
            {
                _tenantDomainDeviceService = tenantDomainDeviceService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue("deviceId", out object value) && value is int tenantDomainDeviceId)
                {

                    if ((await _tenantDomainDeviceService.GetByIDAsync(tenantDomainDeviceId, asTrackable: false)) != null)
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

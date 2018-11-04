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
    /// Validates that a tenant community exists in the database
    /// </summary>
    public class ValidateTenantCommunityExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateTenantCommunityExistsAttribute() : base(typeof(ValidateTenantCommunityExistsActionFilter))
        {
        }

        private class ValidateTenantCommunityExistsActionFilter : IAsyncActionFilter
        {
            private readonly ITenantCommunityService _tenantCommunityService;
            public ValidateTenantCommunityExistsActionFilter(ITenantCommunityService tenantCommunityService)
            {
                _tenantCommunityService = tenantCommunityService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue("tenantCommunityId", out object value) && value is int tenantCommunityId)
                {

                    if ((await _tenantCommunityService.GetByIDAsync(tenantCommunityId, asTrackable: false)) != null)
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

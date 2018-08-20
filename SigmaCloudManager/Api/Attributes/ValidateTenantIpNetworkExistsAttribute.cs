using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mind.Api.Models;
using Mind.Services;
using SCM.Data;
using Mind.Api.Controllers;

namespace Mind.Api.Attributes
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
            private readonly IUnitOfWork _unitOfWork;
            public ValidateTenantIpNetworkExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var tenantId = context.ActionArguments["tenantId"] as int?;                
                var tenantIpNetworkId = context.ActionArguments["tenantIpNetworkId"] as int?;

                if ((from result in await _unitOfWork.TenantIpNetworkRepository.GetAsync(q => 
                    q.TenantIpNetworkID == tenantIpNetworkId.Value && q.TenantID == tenantId.Value,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the tenant IP network.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

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
    /// Validates that a tenant device exists in the database
    /// </summary>
    public class ValidateTenantDeviceExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateTenantDeviceExistsAttribute() : base(typeof(ValidateTenantDeviceExistsActionFilter))
        {
        }

        private class ValidateTenantDeviceExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateTenantDeviceExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var tenantId = context.ActionArguments["tenantId"] as int?;
                var deviceId = context.ActionArguments["deviceId"] as int?;
                
                if ((from result in await _unitOfWork.DeviceRepository.GetAsync(
                    q => 
                    q.DeviceID == deviceId && 
                    q.DeviceRole.IsTenantDomainRole &&
                    q.TenantID == tenantId,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the tenant device.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

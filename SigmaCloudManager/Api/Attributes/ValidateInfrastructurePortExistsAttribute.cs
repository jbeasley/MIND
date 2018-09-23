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
    /// Validates that an infrastructure port exists in the database
    /// </summary>
    public class ValidateInfrastructurePortExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateInfrastructurePortExistsAttribute() : base(typeof(ValidateInfrastructurePortExistsActionFilter))
        {
        }

        private class ValidateInfrastructurePortExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateInfrastructurePortExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {             
                var deviceId = context.ActionArguments["deviceId"] as int?;
                var portId = context.ActionArguments["portId"] as int?;

                if ((from result in await _unitOfWork.PortRepository.GetAsync(
                    q => 
                    q.DeviceID == deviceId && 
                    q.Device.DeviceRole.IsProviderDomainRole &&
                    q.ID == portId,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the infrastructure port.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

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
    /// Validates that an infrastructure logical interface exists in the database
    /// </summary>
    public class ValidateInfrastructureLogicalInterfaceExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateInfrastructureLogicalInterfaceExistsAttribute() : base(typeof(ValidateInfrastructureLogicalInterfaceExistsActionFilter))
        {
        }

        private class ValidateInfrastructureLogicalInterfaceExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateInfrastructureLogicalInterfaceExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {             
                var deviceId = context.ActionArguments["deviceId"] as int?;
                var logicalInterfaceId = context.ActionArguments["logicalInterfaceId"] as int?;

               if ((from result in await _unitOfWork.LogicalInterfaceRepository.GetAsync(
                    q =>
                    q.RoutingInstance.DeviceID == deviceId &&
                    q.RoutingInstance.Device.DeviceRole.IsProviderDomainRole &&
                    q.RoutingInstance.RoutingInstanceType.IsInfrastructureVrf || q.RoutingInstance.RoutingInstanceType.IsDefault,
                    AsTrackable: false)
                         select result)
                    .Where(x => x.LogicalInterfaceID == logicalInterfaceId)
                    .SingleOrDefault() == null)
                {
                    
                    context.ModelState.AddModelError(string.Empty, "Could not find the infrastructure logical interface.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

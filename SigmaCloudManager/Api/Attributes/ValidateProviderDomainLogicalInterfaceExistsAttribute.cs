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
using Mind.Api.Controllers;
using Mind.Api.Models;
using Mind.Services;
using SCM.Data;

namespace Mind.Api.Attributes
{
    /// <summary>
    /// Validates that a provider domain logical interface exists for a given routing instance in the database
    /// </summary>
    public class ValidateProviderDomainLogicalInterfaceExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateProviderDomainLogicalInterfaceExistsAttribute() : base(typeof(ValidateProviderDomainLogicalInterfaceExistsActionFilter))
        {
        }

        private class ValidateProviderDomainLogicalInterfaceExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateProviderDomainLogicalInterfaceExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                
                var logicalInterfaceId = context.ActionArguments["logicalInterfaceId"] as int?;              
                var routingInstanceId = context.ActionArguments["routingInstanceId"] as int?;
               
                if ((from result in await _unitOfWork.LogicalInterfaceRepository.GetAsync(q => 
                    q.LogicalInterfaceID == logicalInterfaceId &&
                    q.RoutingInstanceID == routingInstanceId.Value &&
                    q.RoutingInstance.RoutingInstanceType.IsTenantFacingVrf,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the logical interface in the provider domain with the specified arguments.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

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
using SCM.Models;

namespace Mind.WebUI.Attributes
{
    /// <summary>
    /// Validates that a provider domain logical interface exists in the database
    /// </summary>
    public class ValidateProviderDomainLogicalInterfaceExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateProviderDomainLogicalInterfaceExistsAttribute() : 
            base(typeof(ValidateProviderDomainLogicalInterfaceExistsActionFilter))
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
               
                if ((from result in await _unitOfWork.LogicalInterfaceRepository.GetAsync(q =>
                        q.LogicalInterfaceID == logicalInterfaceId &&
                        q.RoutingInstance.RoutingInstanceType.IsTenantFacingVrf,
                        AsTrackable: false)
                        select result)
                       .SingleOrDefault() == null)
                {

                    context.Result = new ViewResult { ViewName = "ItemNotFound" };
                    return;
                }

                await next();
            }
        }
    }
}

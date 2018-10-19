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
    /// Validates that a provider domain routing instance exists in the database
    /// </summary>
    public class ValidateProviderDomainRoutingInstanceExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateProviderDomainRoutingInstanceExistsAttribute() : 
            base(typeof(ValidateProviderDomainRoutingInstanceExistsActionFilter))
        {
        }

        private class ValidateProviderDomainRoutingInstanceExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateProviderDomainRoutingInstanceExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {          
                var routingInstanceId = context.ActionArguments["routingInstanceId"] as int?;
               
                if ((from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(q =>
                        q.RoutingInstanceID == routingInstanceId &&
                        q.RoutingInstanceType.IsTenantFacingVrf,
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

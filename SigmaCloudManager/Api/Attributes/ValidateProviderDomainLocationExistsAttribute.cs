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
    /// Validates that a provider domain location exists in the database
    /// </summary>
    public class ValidateProviderDomainLocationExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateProviderDomainLocationExistsAttribute() : base(typeof(ValidateProviderDomainLocationExistsActionFilter))
        {
        }

        private class ValidateProviderDomainLocationExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateProviderDomainLocationExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                
                var locationId = context.ActionArguments["locationId"] as int?;              
               
                if ((from result in await _unitOfWork.LocationRepository.GetAsync(q => 
                    q.LocationID == locationId &&
                    q.LocationType == SCM.Models.LocationTypeEnum.ProviderAndTenantDomain ||
                    q.LocationType == SCM.Models.LocationTypeEnum.ProviderDomain,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the location in the provider domain with the specified arguments.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

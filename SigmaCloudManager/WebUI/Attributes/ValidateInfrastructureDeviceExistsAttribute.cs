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
    /// Validates that an infrastructure device exists in the database
    /// </summary>
    public class ValidateInfrastructureDeviceExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateInfrastructureDeviceExistsAttribute() : 
            base(typeof(ValidateInfrastructureDeviceExistsActionFilter))
        {
        }

        private class ValidateInfrastructureDeviceExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateInfrastructureDeviceExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {          
                var deviceId = context.ActionArguments["deviceId"] as int?;
               
                if ((from result in await _unitOfWork.DeviceRepository.GetAsync(q =>
                        q.DeviceID == deviceId 
                        && q.DeviceRole.IsProviderDomainRole,
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

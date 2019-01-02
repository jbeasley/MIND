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
            private readonly IInfrastructureDeviceService _infrastructureDeviceService;
            public ValidateInfrastructureDeviceExistsActionFilter(IInfrastructureDeviceService infrastructureDeviceService)
            {
                _infrastructureDeviceService = infrastructureDeviceService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue("deviceId", out object value) && value is int infrastructureDeviceId)
                {

                    if ((await _infrastructureDeviceService.GetByIDAsync(infrastructureDeviceId, asTrackable: false)) != null)
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

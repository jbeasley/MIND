using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mind.Api.Models;
using Mind.Services;
using Mind.Api.Controllers;

namespace Mind.WebUI.Attributes
{
    /// <summary>
    /// Validates that a VPN exists in the database
    /// </summary>
    public class ValidateVpnExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateVpnExistsAttribute() : base(typeof(ValidateVpnExistsActionFilter))
        {
        }

        private class ValidateVpnExistsActionFilter : IAsyncActionFilter
        {
            private readonly IVpnService _vpnService;
            public ValidateVpnExistsActionFilter(IVpnService vpnService)
            {
                _vpnService = vpnService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue("vpnId", out object value) && value is int vpnId)
                {

                    if ((await _vpnService.GetByIDAsync(vpnId, asTrackable: false)) != null)
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

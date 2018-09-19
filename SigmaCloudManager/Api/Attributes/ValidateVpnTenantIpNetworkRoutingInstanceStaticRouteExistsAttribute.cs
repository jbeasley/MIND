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
    /// Validates that a static route for a tenant IP network association with a routing instance of a given attachment set exists in the database
    /// </summary>
    public class ValidateVpnTenantIpNetworkRoutingInstanceStaticRouteExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateVpnTenantIpNetworkRoutingInstanceStaticRouteExistsAttribute() : base(typeof(ValidateVpnTenantIpNetworkRoutingInstanceStaticRouteExistsActionFilter))
        {
        }

        private class ValidateVpnTenantIpNetworkRoutingInstanceStaticRouteExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateVpnTenantIpNetworkRoutingInstanceStaticRouteExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var attachmentSetId = context.ActionArguments["attachmentSetId"] as int?;                
                var vpnTenantIpNetworkRoutingInstanceStaticRouteId = context.ActionArguments["vpnTenantIpNetworkRoutingInstanceStaticRouteId"] as int?;

                if ((from result in await _unitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.GetAsync(q => 
                    q.AttachmentSetID == attachmentSetId.Value
                    && q.VpnTenantIpNetworkRoutingInstanceStaticRouteID == vpnTenantIpNetworkRoutingInstanceStaticRouteId.Value,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the static route for tenant IP network association with the attachment set routing instance.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

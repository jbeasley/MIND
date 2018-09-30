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
    /// Validates that a tenant domain IP network inbound policy exists in the database
    /// </summary>
    public class ValidateTenantDomainIpNetworkInboundPolicyExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateTenantDomainIpNetworkInboundPolicyExistsAttribute() : base(typeof(ValidateTenantDomainIpNetworkInboundPolicyExistsActionFilter))
        {
        }

        private class ValidateTenantDomainIpNetworkInboundPolicyExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateTenantDomainIpNetworkInboundPolicyExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var deviceId = context.ActionArguments["deviceId"] as int?;                
                var vpnTenantIpNetworkInId = context.ActionArguments["vpnTenantIpNetworkInId"] as int?;

                if ((from result in await _unitOfWork.VpnTenantIpNetworkInRepository.GetAsync(q => 
                    q.BgpPeer.RoutingInstance.DeviceID == deviceId.Value &&
                    q.BgpPeer.RoutingInstance.Device.DeviceRole.IsTenantDomainRole &&
                    q.VpnTenantIpNetworkInID == vpnTenantIpNetworkInId.Value,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the tenant domain IP network inbound policy.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

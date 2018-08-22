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
    /// Validates that a 'VPN tenant IP network in' exists in the database
    /// </summary>
    public class ValidateVpnTenantIpNetworkInExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateVpnTenantIpNetworkInExistsAttribute() : base(typeof(ValidateVpnTenantIpNetworkInExistsActionFilter))
        {
        }

        private class ValidateVpnTenantIpNetworkInExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateVpnTenantIpNetworkInExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var attachmentSetId = context.ActionArguments["attachmentSetId"] as int?;                
                var tenantIpNetworkId = context.ActionArguments["tenantIpNetworkId"] as int?;

                if ((from result in await _unitOfWork.VpnTenantIpNetworkInRepository.GetAsync(q => 
                    q.AttachmentSetID == attachmentSetId.Value
                    && q.TenantIpNetworkID == tenantIpNetworkId.Value,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the tenant IP network association with the attachment set inbound policy.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

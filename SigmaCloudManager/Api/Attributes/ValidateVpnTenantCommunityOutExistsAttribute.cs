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
    /// Validates that a tenant community association with the outbound policy of a given attachment set exists in the database
    /// </summary>
    public class ValidateVpnTenantCommunityOutExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateVpnTenantCommunityOutExistsAttribute() : base(typeof(ValidateVpnTenantCommunityOutExistsActionFilter))
        {
        }

        private class ValidateVpnTenantCommunityOutExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateVpnTenantCommunityOutExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var attachmentSetId = context.ActionArguments["attachmentSetId"] as int?;                
                var vpnCommunityOutId = context.ActionArguments["vpnTenantCommunityOutId"] as int?;

                if ((from result in await _unitOfWork.VpnTenantCommunityOutRepository.GetAsync(q => 
                    q.AttachmentSetID == attachmentSetId.Value
                    && q.VpnTenantCommunityOutID == vpnCommunityOutId.Value,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the tenant community association with the attachment set outbound policy.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

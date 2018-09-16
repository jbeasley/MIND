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
    /// Validates that a vpn attachment set (i.e. an attachment set association with a vpn) exists in the database
    /// </summary>
    public class ValidateVpnAttachmentSetExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateVpnAttachmentSetExistsAttribute() : base(typeof(ValidateVpnAttachmentSetExistsActionFilter))
        {
        }

        private class ValidateVpnAttachmentSetExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateVpnAttachmentSetExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var vpnId = context.ActionArguments["vpnId"] as int?;
                var attachmentSetId = context.ActionArguments["attachmentSetId"] as int?;
                
                if ((from result in await _unitOfWork.VpnAttachmentSetRepository.GetAsync(
                    q => 
                    q.VpnID == vpnId && q.AttachmentSetID == attachmentSetId,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the vpn attachment set.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

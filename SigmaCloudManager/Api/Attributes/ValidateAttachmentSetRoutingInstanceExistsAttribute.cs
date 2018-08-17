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

namespace Mind.Api.Attributes
{
    /// <summary>
    /// Validates that an attachment set routing instance exists in the database
    /// </summary>
    public class ValidateAttachmentSetRoutingInstanceExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateAttachmentSetRoutingInstanceExistsAttribute() : base(typeof(ValidateAttachmentSetRoutingInstanceExistsActionFilter))
        {
        }

        private class ValidateAttachmentSetRoutingInstanceExistsActionFilter : IAsyncActionFilter
        {
            private readonly IAttachmentSetRoutingInstanceService _attachmentSetRoutingInstanceService;
            private readonly IMapper _mapper;

            public ValidateAttachmentSetRoutingInstanceExistsActionFilter(IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService, IMapper mapper)
            {
                _attachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
                _mapper = mapper;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.ActionArguments.ContainsKey("attachmentSetRoutingInstanceId"))
                {
                    context.ModelState.AddModelError(string.Empty, "An ID for the attachment set routing instance was not found.");
                    context.Result = new NotFoundObjectResult(new ApiResponse(context.ModelState) { Message = "Not found error" });
                    return;
                }

                var attachmentSetRoutingInstanceId = context.ActionArguments["attachmentSetRoutingInstanceId"] as int?;
                if (!attachmentSetRoutingInstanceId.HasValue)
                {
                    context.ModelState.AddModelError(string.Empty, "An ID for the attachment set routing instance was not found.");
                    context.Result = new NotFoundObjectResult(new ApiResponse(context.ModelState) { Message = "Not found error" });
                }

                if ((await _attachmentSetRoutingInstanceService.GetByIDAsync(attachmentSetRoutingInstanceId.Value)) == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the attachment set routing instance.");
                    context.Result = new NotFoundObjectResult(new ApiResponse(context.ModelState) { Message = "Not found error" });
                    return;
                }

                await next();
            }
        }
    }
}

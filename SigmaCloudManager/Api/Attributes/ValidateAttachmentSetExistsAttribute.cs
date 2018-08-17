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
    /// Validates that an attachment set exists in the database
    /// </summary>
    public class ValidateAttachmentSetExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateAttachmentSetExistsAttribute() : base(typeof(ValidateAttachmentSetExistsActionFilter))
        {
        }

        private class ValidateAttachmentSetExistsActionFilter : IAsyncActionFilter
        {
            private readonly IAttachmentSetService _attachmentSetService;
            private readonly IMapper _mapper;

            public ValidateAttachmentSetExistsActionFilter(IAttachmentSetService attachmentSetService, IMapper mapper)
            {
                _attachmentSetService = attachmentSetService;
                _mapper = mapper;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.ActionArguments.ContainsKey("attachmentSetId"))
                {
                    context.ModelState.AddModelError(string.Empty, "An ID for the attachment set was not found.");
                    context.Result = new NotFoundObjectResult(new ApiResponse(context.ModelState) { Message = "Not found error" });
                    return;
                }

                var attachmentSetId = context.ActionArguments["attachmentSetId"] as int?;
                if (!attachmentSetId.HasValue)
                {
                    context.ModelState.AddModelError(string.Empty, "An ID for the attachment set was not found.");
                    context.Result = new NotFoundObjectResult(new ApiResponse(context.ModelState) { Message = "Not found error" });
                }

                if ((await _attachmentSetService.GetByIDAsync(attachmentSetId.Value)) == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the attachment set.");
                    context.Result = new NotFoundObjectResult(new ApiResponse(context.ModelState) { Message = "Not found error" });
                    return;
                }

                await next();
            }
        }
    }
}

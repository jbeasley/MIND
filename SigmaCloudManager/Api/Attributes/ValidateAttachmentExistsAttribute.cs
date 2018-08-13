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
    /// Validates that a provider domain attachment exists in the database
    /// </summary>
    public class ValidateProviderDomainAttachmentExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateProviderDomainAttachmentExistsAttribute() : base(typeof(ValidateProviderDomainAttachmentExistsActionFilter))
        {
        }

        private class ValidateProviderDomainAttachmentExistsActionFilter : IAsyncActionFilter
        {
            private readonly IProviderDomainAttachmentService _attachmentService;
            private readonly IMapper _mapper;

            public ValidateProviderDomainAttachmentExistsActionFilter(IProviderDomainAttachmentService attachmentService, IMapper mapper)
            {
                _attachmentService = attachmentService;
                _mapper = mapper;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("attachmentId"))
                {
                    var attachmentId = context.ActionArguments["attachmentId"] as int?;

                    if (attachmentId.HasValue)
                    {
                        if ((await _attachmentService.GetByIDAsync(attachmentId.Value)) == null)
                        {
                            context.ModelState.AddModelError(string.Empty, "Could not find the attachment.");
                            context.Result = new NotFoundObjectResult(new ApiResponse(context.ModelState) { Message = "Not found error" });
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}

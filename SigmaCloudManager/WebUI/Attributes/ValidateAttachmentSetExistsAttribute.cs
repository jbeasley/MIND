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
            public ValidateAttachmentSetExistsActionFilter(IAttachmentSetService attachmentSetService)
            {
                _attachmentSetService = attachmentSetService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue("attachmentSetId", out object value) && value is int attachmentSetId)
                {

                    if ((await _attachmentSetService.GetByIDAsync(attachmentSetId, asTrackable: false)) != null)
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

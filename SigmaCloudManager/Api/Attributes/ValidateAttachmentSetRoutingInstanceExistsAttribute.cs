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
using Mind.Api.Controllers;
using Mind.Api.Models;
using Mind.Services;
using SCM.Data;

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
            private readonly IUnitOfWork _unitOfWork;
            public ValidateAttachmentSetRoutingInstanceExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                
                var attachmentSetId = context.ActionArguments["attachmentSetId"] as int?;              
                var routingInstanceId = context.ActionArguments["routingInstanceId"] as int?;
               
                if ((from result in await _unitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(q => 
                    q.AttachmentSetID == attachmentSetId
                    && q.RoutingInstanceID == routingInstanceId.Value,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the attachment set routing instance.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

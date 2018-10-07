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
using SCM.Models;

namespace Mind.Api.Attributes
{
    /// <summary>
    /// Validates that a infrastructure attachment exists in the database
    /// </summary>
    public class ValidateInfrastructureAttachmentExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateInfrastructureAttachmentExistsAttribute() : base(typeof(ValidateInfrastructureAttachmentExistsActionFilter))
        {
        }

        private class ValidateInfrastructureAttachmentExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateInfrastructureAttachmentExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {          
                var attachmentId = context.ActionArguments["attachmentId"] as int?;
               
                if ((from result in await _unitOfWork.AttachmentRepository.GetAsync(q =>
                        q.AttachmentID == attachmentId 
                        && q.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.ProviderInfrastructure,
                        AsTrackable: false)
                        select result)
                       .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the infrastructure attachment.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

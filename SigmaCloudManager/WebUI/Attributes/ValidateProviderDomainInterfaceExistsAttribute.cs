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

namespace Mind.WebUI.Attributes
{
    /// <summary>
    /// Validates that a provider domain interface exists in the database
    /// </summary>
    public class ValidateProviderDomainInterfaceExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateProviderDomainInterfaceExistsAttribute() : 
            base(typeof(ValidateProviderDomainInterfaceExistsActionFilter))
        {
        }

        private class ValidateProviderDomainInterfaceExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateProviderDomainInterfaceExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {          
                var logicalInterfaceId = context.ActionArguments["interfaceId"] as int?;
               
                if ((from result in await _unitOfWork.InterfaceRepository.GetAsync(q =>
                        q.InterfaceID == logicalInterfaceId &&
                        q.Attachment.AttachmentRole.PortPool.PortRole.PortRoleType == Mind.Models.PortRoleTypeEnum.TenantFacing,
                        AsTrackable: false)
                        select result)
                       .SingleOrDefault() == null)
                {

                    context.Result = new ViewResult { ViewName = "ItemNotFound" };
                    return;
                }

                await next();
            }
        }
    }
}

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
    /// Validates that a provider domain vif exists in the database
    /// </summary>
    public class ValidateProviderDomainVifExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateProviderDomainVifExistsAttribute() : 
            base(typeof(ValidateProviderDomainVifExistsActionFilter))
        {
        }

        private class ValidateProviderDomainVifExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateProviderDomainVifExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue("vifId", out object value) && value is int vifId)
                {

                    if ((from result in await _unitOfWork.VifRepository.GetAsync(q =>
                        q.VifID == vifId
                        && q.VifRole.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.TenantFacing,
                        AsTrackable: false)
                         select result)
                       .SingleOrDefault() != null)
                    {
                        await next();
                    }

                    context.Result = new ViewResult { ViewName = "ItemNotFound" };
                    return;
                }
            }
        }
    }
}

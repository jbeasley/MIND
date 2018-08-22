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
    /// Validates that a tenant IP network exists in the database
    /// </summary>
    public class ValidateTenantIpNetworkExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateTenantIpNetworkExistsAttribute() : base(typeof(ValidateTenantIpNetworkExistsActionFilter))
        {
        }

        private class ValidateTenantIpNetworkExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateTenantIpNetworkExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var tenantId = context.ActionArguments.ContainsKey("tenantId") ? context.ActionArguments["tenantId"] as int? : null;
                var attachmentSetId = context.ActionArguments.ContainsKey("attachmentSetId") ? context.ActionArguments["attachmentSetId"] as int? : null;
                var tenantIpNetworkId = context.ActionArguments["tenantIpNetworkId"] as int?;

                var query = (from result in await _unitOfWork.TenantIpNetworkRepository.GetAsync(q =>
                    q.TenantIpNetworkID == tenantIpNetworkId.Value, includeProperties:"Tenant.AttachmentSets",
                    AsTrackable: false)
                             select result);

                if (attachmentSetId != null)
                {
                    query = from result in query
                            from r in result.Tenant.AttachmentSets
                            where (r.AttachmentSetID == attachmentSetId)
                            select result;
                }
                else if (tenantId != null)
                {
                    query = query.Where(x => x.TenantID == tenantId);
                }

                var tenantIpNetwork = query.SingleOrDefault();
                if (tenantIpNetwork == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the tenant IP network.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }
           
                await next();
            }
        }
    }
}

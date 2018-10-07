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
using Microsoft.EntityFrameworkCore;

namespace Mind.Api.Attributes
{
    /// <summary>
    /// Validates that a tenant community exists in the database
    /// </summary>
    public class ValidateTenantCommunityExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateTenantCommunityExistsAttribute() : base(typeof(ValidateTenantCommunityExistsActionFilter))
        {
        }

        private class ValidateTenantCommunityExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateTenantCommunityExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var tenantId = context.ActionArguments.ContainsKey("tenantId") ? context.ActionArguments["tenantId"] as int? : null;
                var attachmentSetId = context.ActionArguments.ContainsKey("attachmentSetId") ? context.ActionArguments["attachmentSetId"] as int? : null;
                var tenantCommunityId = context.ActionArguments["tenantCommunityId"] as int?;

                var query = (from result in await _unitOfWork.TenantCommunityRepository.GetAsync(
                          q =>
                             q.TenantCommunityID == tenantCommunityId.Value, 
                             query: q => q.Include(x => x.Tenant.AttachmentSets),
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

                var tenantCommunity = query.SingleOrDefault();
                if (tenantCommunity == null)
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

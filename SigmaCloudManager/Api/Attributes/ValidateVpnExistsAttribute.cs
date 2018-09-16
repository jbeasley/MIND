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
using SCM.Data;
using System.Linq;

namespace Mind.Api.Attributes
{
    /// <summary>
    /// Validates that a vpn exists in the database
    /// </summary>
    public class ValidateVpnExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateVpnExistsAttribute() : base(typeof(ValidateVpnExistsActionFilter))
        {
        }

        private class ValidateVpnExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateVpnExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {             
                var vpnId = context.ActionArguments["vpnId"] as int?;
                var tenantId = context.ActionArguments.ContainsKey("tenantId") ? (int?)context.ActionArguments["tenantId"] : null;
                var query = (from result in await _unitOfWork.VpnRepository.GetAsync(
                    q =>
                        q.VpnID == vpnId,
                        AsTrackable: false)
                             select result);

                if (tenantId.HasValue) query = query.Where(x => x.TenantID == tenantId);
                var vpn = query.SingleOrDefault();
                if (vpn == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the vpn.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

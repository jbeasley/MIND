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
    /// Validates that a provider domain BGP peer exists for a given routing instance in the database
    /// </summary>
    public class ValidateProviderDomainBgpPeerExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateProviderDomainBgpPeerExistsAttribute() : base(typeof(ValidateProviderDomainBgpPeerExistsActionFilter))
        {
        }

        private class ValidateProviderDomainBgpPeerExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateProviderDomainBgpPeerExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                
                var bgpPeerId = context.ActionArguments["bgpPeerId"] as int?;              
                var routingInstanceId = context.ActionArguments["routingInstanceId"] as int?;
               
                if ((from result in await _unitOfWork.BgpPeerRepository.GetAsync(q => 
                    q.BgpPeerID == bgpPeerId &&
                    q.RoutingInstanceID == routingInstanceId.Value &&
                    q.RoutingInstance.RoutingInstanceType.IsTenantFacingVrf,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the BGP peer in the provider domain with the specified arguments.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

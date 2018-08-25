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
    /// Validates that a BGP peer exists in the database
    /// </summary>
    public class ValidateBgpPeerExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateBgpPeerExistsAttribute() : base(typeof(ValidateBgpPeerExistsActionFilter))
        {
        }

        private class ValidateBgpPeerExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;
            public ValidateBgpPeerExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                
                var bgpPeerId = context.ActionArguments["bgpPeerId"] as int?;              
                var routingInstanceId = context.ActionArguments["routingInstanceId"] as int?;
               
                if ((from result in await _unitOfWork.BgpPeerRepository.GetAsync(q => 
                    q.BgpPeerID == bgpPeerId
                    && q.RoutingInstanceID == routingInstanceId.Value,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault() == null)
                {
                    context.ModelState.AddModelError(string.Empty, "Could not find the BGP peer with the specified arguments.");
                    context.Result = new ResourceNotFoundResult(context.ModelState);
                    return;
                }

                await next();
            }
        }
    }
}

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
    /// Set a cookie to persist the provider domain attachment context. This cookie may be used client-side
    /// to set the current provider domain attachment context.
    /// </summary>
    public class SetProviderDomainAttachmentCookieStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called before the action method is invoked
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("attachmentId", out object value) && value is int attachmentId)
            {
                context.HttpContext.Response.Cookies.Append("provider-domain-attachment-id", attachmentId.ToString(),
                    new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddDays(1) });
            }
        }
    }
}

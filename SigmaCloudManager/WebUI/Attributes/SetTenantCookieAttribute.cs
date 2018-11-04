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
    /// Set a tenantID cookie to persist the tenant context. This cookie may be used client-side
    /// to set the current tenant context.
    /// </summary>
    public class SetTenantCookieStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called before the action method is invoked
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("tenantId", out object value) && value is int tenantId)
            {
                context.HttpContext.Response.Cookies.Append("tenantId", tenantId.ToString(),
                    new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddDays(1) });
            }
        }
    }
}

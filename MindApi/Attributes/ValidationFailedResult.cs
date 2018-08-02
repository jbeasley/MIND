using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Api.Models;

namespace Mind.Api.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationFailedResult : ObjectResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ApiResponse(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Api.Models;
using Microsoft.AspNetCore.Http;

namespace SCM.Api
{
    public class NetworkServiceRequestForbiddenResult : ObjectResult
    {
        public NetworkServiceRequestForbiddenResult(ModelStateDictionary modelState)
            : base(new NetworkServiceForbiddenApiModel())
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }
    }
}

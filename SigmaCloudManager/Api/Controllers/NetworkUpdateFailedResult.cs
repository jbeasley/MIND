using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Api.Models;

namespace Mind.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class NetworkUpdateFailedResult : ObjectResult
    {
        /// <summary>
        /// 
        /// </summary>
        public NetworkUpdateFailedResult()
            : base(new ApiResponse()
            {
                Code = "NetworkUpdateError",
                Message = "An error occurred while updating the network. No changes have been applied. " +
                "Try again, and if the problem persists report it to your system administrator."
            })
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}

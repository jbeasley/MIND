using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Models
{
    /// <summary>
    /// Network Error class.
    /// </summary>
    public class NetworkServiceForbiddenApiModel
    {
        public string Message { get; } = "Unable to process the request. " +
                    "Access to the network service is forbidden due to server configuration." +
                    "Contact your system administrator if you believe access should be enabled.";
    }
}

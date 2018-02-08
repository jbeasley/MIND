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
    public class NetworkServiceFailureApiModel
    {
        public string Message { get; } = "Unable to complete the request. " +
                    "Perhaps the network is unavailable. " +
                    "Check logs for more details.";
    }
}

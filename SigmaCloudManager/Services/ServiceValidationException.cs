using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Services
{
    /// <summary>
    /// Exceptions which is raised by a service upon failure of a validation check.
    /// </summary>
    public class ServiceValidationException : Exception
    {
        public ServiceValidationException()
        {
        }

        public ServiceValidationException(string message) : base(message)
        {
        }

        public ServiceValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

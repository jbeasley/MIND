using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Services
{
    public class ServiceBadArgumentsException : Exception
    {
        public ServiceBadArgumentsException()
        {
        }

        public ServiceBadArgumentsException(string message) : base(message)
        {
        }

        public ServiceBadArgumentsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

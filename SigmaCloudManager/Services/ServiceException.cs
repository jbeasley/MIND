using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class ServiceException : Exception
    {
        public ServiceResult ServiceResult { get; set; }
        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message,
                            Exception innerException) : base(message, innerException)
        {
        }

        public ServiceException(string message, ServiceResult result) : base(message)
        {
            ServiceResult = result;
        }

        public ServiceException(string message, ServiceResult result,
                            Exception innerException) : base(message, innerException)
        {
            ServiceResult = result;
        }
    }

    public class UnstartableServiceException : ServiceException
    {
        public UnstartableServiceException(string message) : base(message)
        {
        }
        public UnstartableServiceException(string message,
                    Exception innerException) : base(message, innerException)
        {
        }

        public UnstartableServiceException(string message, ServiceResult result) : base(message)
        {
            ServiceResult = result;
        }

        public UnstartableServiceException(string message, ServiceResult result,
                            Exception innerException) : base(message, innerException)
        {
            ServiceResult = result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class NetworkServiceFailureException : Exception
    {
        public NetworkSyncServiceResult NetworkServiceResult { get; }
        public NetworkServiceFailureException(string message) : base(message)
        {
        }

        public NetworkServiceFailureException(string message,
                            Exception innerException) : base(message, innerException)
        {
        }

        public NetworkServiceFailureException(string message, NetworkSyncServiceResult result) : base(message)
        {
            NetworkServiceResult = result;
        }

        public NetworkServiceFailureException(string message, NetworkSyncServiceResult result,
                            Exception innerException) : base(message, innerException)
        {
            NetworkServiceResult = result;
        }
    }
}

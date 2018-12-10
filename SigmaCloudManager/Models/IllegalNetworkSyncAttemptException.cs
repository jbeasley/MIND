using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Models
{
    /// <summary>
    /// Illegal network sync attempt exception.
    /// </summary>
    public class IllegalNetworkSyncAttemptException : Exception
    {
        public IllegalNetworkSyncAttemptException(string message) : base(message)
        {
        }

        public IllegalNetworkSyncAttemptException(string message,
                            Exception innerException) : base(message, innerException)
        {
        }
    }
}

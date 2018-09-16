using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Models
{
    public class IllegalDeleteAttemptException : Exception
    {
        public IllegalDeleteAttemptException(string message) : base(message)
        {
        }

        public IllegalDeleteAttemptException(string message,
                            Exception innerException) : base(message, innerException)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public class ValidationFailureException : Exception
    {
        public ValidationFailureException(string message) : base(message)
        {
        }

        public ValidationFailureException(string message,
                            Exception innerException) : base(message, innerException)
        {
        }
    }
}

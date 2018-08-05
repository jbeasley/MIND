using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class BuilderUnableToCompleteException : Exception
    {
        public BuilderUnableToCompleteException(string message) : base(message)
        {
        }

        public BuilderUnableToCompleteException(string message,
                            Exception innerException) : base(message, innerException)
        {
        }
    }
}

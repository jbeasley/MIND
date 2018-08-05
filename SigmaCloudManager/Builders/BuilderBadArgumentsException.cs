using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class BuilderBadArgumentsException : Exception
    {
        public BuilderBadArgumentsException(string message) : base(message)
        {
        }

        public BuilderBadArgumentsException(string message,
                            Exception innerException) : base(message, innerException)
        {
        }
    }
}

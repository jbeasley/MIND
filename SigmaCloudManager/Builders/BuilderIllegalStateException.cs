using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class BuilderIllegalStateException : Exception
    {
        public BuilderIllegalStateException(string message) : base(message)
        {
        }

        public BuilderIllegalStateException(string message,
                            Exception innerException) : base(message, innerException)
        {
        }
    }
}

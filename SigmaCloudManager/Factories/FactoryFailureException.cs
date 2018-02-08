using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Factories
{
    public class FactoryFailureException : Exception
    {
        public FactoryResult FactoryResult { get; }
        public FactoryFailureException(string message) : base(message)
        {
        }

        public FactoryFailureException(string message,
                            Exception innerException) : base(message, innerException)
        {
        }

        public FactoryFailureException(string message, FactoryResult result) : base(message)
        {
            FactoryResult = result;
        }

        public FactoryFailureException(string message, FactoryResult result,
                            Exception innerException) : base(message, innerException)
        {
            FactoryResult = result;
        }
    }
}

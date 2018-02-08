using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Factories
{
    /// <summary>
    /// Simple class for returning the results from Factory classes
    /// </summary>
    public class FactoryResult
    {
        public bool IsSuccess { get; set; }

        public List<string> Messages { get; private set; } = new List<string>();

        public object Item { get; set; }
    }
}

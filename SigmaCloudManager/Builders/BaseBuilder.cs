using SCM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Abstract base class for builders
    /// </summary>
    public abstract class BaseBuilder
    {
        protected internal readonly IUnitOfWork _unitOfWork;
        protected internal readonly IDictionary<string, object> _args;

        public BaseBuilder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _args = new Dictionary<string, object>();
        }
    }
}

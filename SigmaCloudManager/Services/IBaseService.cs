using SCM.Data;
using SCM.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Services
{
    public interface IBaseService
    {
        IValidator Validator { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}

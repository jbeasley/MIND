using SCM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Services
{
    public interface IBaseService
    {
        IUnitOfWork UnitOfWork { get; }
    }
}

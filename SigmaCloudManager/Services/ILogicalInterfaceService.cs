using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface ILogicalInterfaceService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<LogicalInterface> GetByIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<LogicalInterface>> GetAllByDeviceIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<LogicalInterface>> GetAllByRoutingInstanceIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(LogicalInterface logicalInterface);
        Task<int> UpdateAsync(LogicalInterface logicalInterface);
        Task<int> DeleteAsync(LogicalInterface logicalInterface);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IPortPoolService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<PortPool>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<PortPool>> GetAllByPortRoleIDAsync(int id, bool includeProperties = true);
        Task<PortPool> GetByIDAsync(int id, bool includeProperties = true);
        //Task<PortPool> GetByNameAsync(string name, bool includeProperties = true);
        Task<int> AddAsync(PortPool portPool);
        Task<int> UpdateAsync(PortPool portPool);
        Task<int> DeleteAsync(PortPool portPool);
    }
}

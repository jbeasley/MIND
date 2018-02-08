using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVlanService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<Vlan>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<Vlan>> GetAllByVifIDAsync(int id, bool includeProperties = true);
        Task<Vlan> GetByIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(Vlan vlan);
        Task<int> UpdateAsync(Vlan vlan);
        Task<int> DeleteAsync(Vlan vlan);
    }
}

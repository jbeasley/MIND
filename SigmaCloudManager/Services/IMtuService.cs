using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IMtuService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<Mtu>> GetAllAsync();
        Task<Mtu> GetByIDAsync(int id);
        Task<Mtu> GetByValueAsync(int mtuValue);
        Task<int> AddAsync(Mtu mtu);
        Task<int> UpdateAsync(Mtu mtu);
        Task<int> DeleteAsync(Mtu mtu);
    }
}

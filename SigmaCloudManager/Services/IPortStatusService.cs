using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IPortStatusService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<PortStatus>> GetAllAsync();
        Task<PortStatus> GetByIDAsync(int id);
        //Task<PortStatus> GetByNameAsync(string name);
        Task<int> AddAsync(PortStatus portStatus);
        Task<int> UpdateAsync(PortStatus portStatus);
        Task<int> DeleteAsync(PortStatus portStatus);
    }
}

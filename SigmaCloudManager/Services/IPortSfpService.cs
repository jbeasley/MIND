using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IPortSfpService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<PortSfp>> GetAllAsync();
        Task<PortSfp> GetByIDAsync(int id);
        //Task<PortSfp> GetByNameAsync(string name);
        Task<int> AddAsync(PortSfp portSfp);
        Task<int> UpdateAsync(PortSfp portSfp);
        Task<int> DeleteAsync(PortSfp portSfp);
    }
}

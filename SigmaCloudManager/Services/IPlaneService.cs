using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IPlaneService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<Plane>> GetAllAsync();
        Task<Plane> GetByIDAsync(int id);
        Task<Plane> GetByNameAsync(string name);
        Task<int> AddAsync(Plane plane);
        Task<int> UpdateAsync(Plane plane);
        Task<int> DeleteAsync(Plane plane);
    }
}

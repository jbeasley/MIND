using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IAddressFamilyService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<AddressFamily>> GetAllAsync();
        Task<AddressFamily> GetByIDAsync(int id);
        //Task<AddressFamily> GetByNameAsync(string name);
        Task<int> AddAsync(AddressFamily addressFamily);
        Task<int> UpdateAsync(AddressFamily addressFamily);
        Task<int> DeleteAsync(AddressFamily addressFamily);
    }
}

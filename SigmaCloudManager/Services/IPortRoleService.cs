using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IPortRoleService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<PortRole>> GetAllAsync();
        Task<IEnumerable<PortRole>> GetAllByDeviceRoleIDAsync(int deviceRoleID);
        Task<PortRole> GetByIDAsync(int id);
        Task<PortRole> GetByNameAsync(string name);
        Task<PortRole> GetByPortRoleTypeAsync(PortRoleType portRoleType);
        Task<int> AddAsync(PortRole portRole);
        Task<int> UpdateAsync(PortRole portRole);
        Task<int> DeleteAsync(PortRole portRole);
    }
}

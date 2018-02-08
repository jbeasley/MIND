using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IMulticastVpnDirectionTypeService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<MulticastVpnDirectionType>> GetAllAsync();
        Task<MulticastVpnDirectionType> GetByIDAsync(int id);
        //Task<MulticastVpnDirectionType> GetByNameAsync(string name);
        Task<int> AddAsync(MulticastVpnDirectionType multicastVpnDirectionType);
        Task<int> UpdateAsync(MulticastVpnDirectionType multicastVpnDirectionType);
        Task<int> DeleteAsync(MulticastVpnDirectionType multicastVpnDirectionType);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IMulticastVpnDomainTypeService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<MulticastVpnDomainType>> GetAllAsync();
        Task<MulticastVpnDomainType> GetByIDAsync(int id);
        //Task<MulticastVpnDomainType> GetByNameAsync(string name);
        Task<int> AddAsync(MulticastVpnDomainType multicastVpnDomainTypeService);
        Task<int> UpdateAsync(MulticastVpnDomainType multicastVpnDomainTypeService);
        Task<int> DeleteAsync(MulticastVpnDomainType multicastVpnDomainTypeService);
    }
}

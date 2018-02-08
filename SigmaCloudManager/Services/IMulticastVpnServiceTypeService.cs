using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IMulticastVpnServiceTypeService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<MulticastVpnServiceType>> GetAllAsync();
        Task<MulticastVpnServiceType> GetByIDAsync(int id);
        //Task<MulticastVpnServiceType> GetByNameAsync(string name);
        Task<int> AddAsync(MulticastVpnServiceType multicastVpnServiceType);
        Task<int> UpdateAsync(MulticastVpnServiceType multicastVpnServiceType);
        Task<int> DeleteAsync(MulticastVpnServiceType multicastVpnServiceType);
    }
}

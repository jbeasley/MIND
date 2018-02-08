using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IInterfaceService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<Interface> GetByIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<Interface>> GetAllByDeviceIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<Interface>> GetAllByAttachmentIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(Interface iface);
        Task<int> UpdateAsync(Interface iface);
        Task<int> DeleteAsync(Interface iface);
    }
}

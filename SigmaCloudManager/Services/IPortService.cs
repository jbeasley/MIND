using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IPortService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<Port> GetByIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<Port>> GetAllByDeviceIDAsync(int id, int? portPoolID = null, bool includeProperties = true);
        Task<IEnumerable<Port>> GetAllByInterfaceIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<Port>> GetAllByAttachmentIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(Port port);
        Task<int> UpdateAsync(Port port);
        Task<int> DeleteAsync(Port port);
    }
}

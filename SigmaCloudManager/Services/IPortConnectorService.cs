using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IPortConnectorService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<PortConnector>> GetAllAsync();
        Task<PortConnector> GetByIDAsync(int id);
        //Task<PortConnector> GetByNameAsync(string name);
        Task<int> AddAsync(PortConnector portConnector);
        Task<int> UpdateAsync(PortConnector portConnector);
        Task<int> DeleteAsync(PortConnector portConnector);
    }
}

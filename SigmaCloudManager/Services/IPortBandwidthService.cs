using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IPortBandwidthService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<PortBandwidth>> GetAllAsync();
        Task<PortBandwidth> GetByIDAsync(int id);
        Task<PortBandwidth> GetAsync(int bandwidth);
        Task<int> AddAsync(PortBandwidth portBandwidth);
        Task<int> UpdateAsync(PortBandwidth portBandwidth);
        Task<int> DeleteAsync(PortBandwidth portBandwidth);
    }
}

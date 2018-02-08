using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.NetModels;
using SCM.Data;

namespace SCM.Services
{
    public interface IDeviceService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<Device>> GetAllAsync(bool? isProviderDomainRole = null, bool? isTenantDomainRole = null,
            bool? requiresSync = null, bool? created = null, bool? showRequiresSyncAlert = null, bool? showCreatedAlert = null,
            string searchString = "", bool includeProperties = true);
        Task<IEnumerable<Device>> GetAllByLocationIDAsync(int locationID, int? planeID = null, bool includeProperties = true);
        Task<IEnumerable<Device>> GetAllByTenantIDAsync(int tenantID, string searchString = "", bool includeProperties = true);
        Task<Device> GetByIDAsync(int id, bool includeProperties = true);
        Task<Device> GetByNameAsync(string name, bool includeProperties = true);
        Task<ServiceResult> AddAsync(Device device);
        Task<int> UpdateAsync(Device device);
        Task<int> UpdateAsync(IEnumerable<Device> devices);
        Task<ServiceResult> DeleteAsync(Device device);
        Task<IEnumerable<ServiceResult>> CheckNetworkSyncAsync(IEnumerable<Device> devices, IProgress<ServiceResult> progress);
        Task<ServiceResult> CheckNetworkSyncAsync(Device device);
        Task<IEnumerable<ServiceResult>> SyncToNetworkAsync(IEnumerable<Device> devices, IProgress<ServiceResult> progress);
        Task<ServiceResult> SyncToNetworkAsync(Device device);
        Task<ServiceResult> DeleteFromNetworkAsync(Device device);
    }
}

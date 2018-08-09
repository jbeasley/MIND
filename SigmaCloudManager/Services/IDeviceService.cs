using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IDeviceService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<Device>> GetAllAsync(bool? isProviderDomainRole = null, bool? isTenantDomainRole = null,
            bool? requiresSync = null, bool? created = null, bool? showRequiresSyncAlert = null, bool? showCreatedAlert = null,
            string searchString = "", bool deep = false, bool asTrackable = false);
        Task<IEnumerable<Device>> GetAllByLocationIDAsync(int locationID, int? planeID = null, bool deep = false, bool asTrackable = false);
        Task<IEnumerable<Device>> GetAllByTenantIDAsync(int tenantID, string searchString = "", bool deep = false, bool asTrackable = false);
        Task<Device> GetByIDAsync(int id, bool deep = false, bool asTrackable = false);
        Task<Device> GetByNameAsync(string name, bool deep = false, bool asTrackable = false);
        Task<ServiceResult> AddAsync(Device device);
        Task<int> UpdateAsync(Device device);
        Task<int> UpdateAsync(IEnumerable<Device> devices);
        Task<ServiceResult> DeleteAsync(Device device);
    }
}

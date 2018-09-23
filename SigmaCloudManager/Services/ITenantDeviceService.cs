using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Mind.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public interface ITenantDeviceService : IBaseService
    {
        Task<IEnumerable<Device>> GetAllByTenantIDAsync(int tenantId, bool? created = null, bool? showCreatedAlert = null,
            string searchString = "", bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<Device>> GetAllByLocationIDAsync(int locationID, bool? deep = false, bool asTrackable = false);
        Task<Device> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<Device> GetByNameAsync(string name, bool? deep = false, bool asTrackable = false);
        Task<Device> AddAsync(int tenantId, TenantDeviceRequest request);
        Task<Device> UpdateAsync(int deviceId, TenantDeviceUpdate update);
        Task DeleteAsync(int deviceId);
    }
}

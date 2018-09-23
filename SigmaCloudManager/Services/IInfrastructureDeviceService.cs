using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Mind.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public interface IInfrastructureDeviceService : IBaseService
    {
        Task<IEnumerable<Device>> GetAllAsync(bool? created = null, bool? showCreatedAlert = null,
            string searchString = "", bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<Device>> GetAllByLocationIDAsync(int locationID, int? planeID = null, bool? deep = false, bool asTrackable = false);
        Task<Device> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<Device> GetByNameAsync(string name, bool? deep = false, bool asTrackable = false);
        Task<Device> AddAsync(InfrastructureDeviceRequest request);
        Task<Device> UpdateAsync(int deviceId, InfrastructureDeviceUpdate update);
        Task DeleteAsync(int deviceId);
    }
}

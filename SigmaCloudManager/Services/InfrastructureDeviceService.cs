using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;
using SCM.Services;
using Mind.Builders;

namespace Mind.Services
{
    public class InfrastructureDeviceService : BaseDeviceService, IInfrastructureDeviceService
    {
        private readonly IInfrastructureDeviceDirector _director;
        private readonly IInfrastructureDeviceUpdateDirector _updateDirector;

        public InfrastructureDeviceService(IUnitOfWork unitOfWork, IMapper mapper, IInfrastructureDeviceDirector director, 
            IInfrastructureDeviceUpdateDirector updateDirector) : base (unitOfWork, mapper)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        public Task<IEnumerable<Device>> GetAllAsync(bool? created = null, bool? showCreatedAlert = null, string searchString = "", bool? deep = false, bool asTrackable = false)
        {
            return base.GetAllAsync(isProviderDomainRole: true,
                created: created, showCreatedAlert: showCreatedAlert, searchString: searchString,
                deep: deep, asTrackable: asTrackable);
        }

        public Task<IEnumerable<Device>> GetAllByLocationIDAsync(int locationID, int? planeID = null, bool? deep = false, bool asTrackable = false)
        {
            return base.GetAllByLocationIDAsync(locationID: locationID, planeID: planeID, 
                isProviderDomainRole: true, deep: deep, asTrackable: asTrackable);
        }

        public Task<Device> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return base.GetByIDAsync(id: id, isProviderDomainRole: true, deep: deep, asTrackable: asTrackable);
        }

        public Task<Device> GetByNameAsync(string name, bool? deep = false, bool asTrackable = false)
        {
            return base.GetByNameAsync(name: name, isProviderDomainRole: true, deep: deep, asTrackable: asTrackable);
        }

        public async Task<Device> AddAsync(InfrastructureDeviceRequest request)
        {
            var device = await _director.BuildAsync(request);
            UnitOfWork.DeviceRepository.Insert(device);
            await UnitOfWork.SaveAsync();

            return await GetByIDAsync(device.DeviceID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int deviceId)
        {
            var device = (from devices in await UnitOfWork.DeviceRepository.GetAsync(
                        q =>
                          q.DeviceID == deviceId,
                          query: q => q.IncludeDeleteValidationProperties(),
                          AsTrackable: true)
                          select devices)
                          .Single();

            device.ValidateDelete();
            UnitOfWork.DeviceRepository.Delete(device);
            await UnitOfWork.SaveAsync();
        }

        public async Task<Device> UpdateAsync(int deviceId, InfrastructureDeviceUpdate update)
        {
            await _updateDirector.UpdateAsync(deviceId, update);
            await UnitOfWork.SaveAsync();
            return await GetByIDAsync(deviceId, deep: true, asTrackable: false);
        }
    }
}

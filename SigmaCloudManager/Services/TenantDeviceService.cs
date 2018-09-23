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
using Microsoft.EntityFrameworkCore;

namespace Mind.Services
{
    public class TenantDeviceService : BaseDeviceService, ITenantDeviceService
    {
        private readonly ITenantDeviceDirector _director;
        private readonly ITenantDeviceUpdateDirector _updateDirector;

        public TenantDeviceService(IUnitOfWork unitOfWork, IMapper mapper, ITenantDeviceDirector director, 
            ITenantDeviceUpdateDirector updateDirector) : base (unitOfWork, mapper)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        public async Task<IEnumerable<Device>> GetAllByTenantIDAsync(int tenantId, bool? created = null, 
            bool? showCreatedAlert = null, string searchString = "", bool? deep = false, bool asTrackable = false)
        {
            var query = (from devices in await this.UnitOfWork.DeviceRepository.GetAsync(
                    q =>
                        q.TenantID == tenantId && q.DeviceRole.IsTenantDomainRole,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.DeviceRole),
                        AsTrackable: asTrackable)
                         select devices);

            if (created != null) query = query.Where(x => x.Created);
            if (showCreatedAlert != null) query = query.Where(x => x.ShowCreatedAlert);
            if (!string.IsNullOrEmpty(searchString)) query = query.Where(x => x.Name.Contains(searchString));

            return query.ToList();
        }

        public Task<IEnumerable<Device>> GetAllByLocationIDAsync(int locationID, bool? deep = false, bool asTrackable = false)
        {
            return base.GetAllByLocationIDAsync(locationID: locationID, 
                isTenantDomainRole: true, deep: deep, asTrackable: asTrackable);
        }

        public Task<Device> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return base.GetByIDAsync(id: id, isTenantDomainRole: true, deep: deep, asTrackable: asTrackable);
        }

        public Task<Device> GetByNameAsync(string name, bool? deep = false, bool asTrackable = false)
        {
            return base.GetByNameAsync(name: name, isTenantDomainRole: true, deep: deep, asTrackable: asTrackable);
        }

        public async Task<Device> AddAsync(int tenantId, TenantDeviceRequest request)
        {
            var device = await _director.BuildAsync(tenantId, request);
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

        public async Task<Device> UpdateAsync(int deviceId, TenantDeviceUpdate update)
        {
            await _updateDirector.UpdateAsync(deviceId, update);
            await UnitOfWork.SaveAsync();
            return await GetByIDAsync(deviceId, deep: true, asTrackable: false);
        }
    }
}

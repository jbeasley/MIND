using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;
using SCM.Models;
using SCM.Data;
using SCM.Factories;

namespace SCM.Services
{
    public class BaseDeviceService : BaseService
    {
        public BaseDeviceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
       
        public async Task<IEnumerable<Device>> GetAllAsync(bool? isProviderDomainRole = null, bool? isTenantDomainRole = null, 
            bool? created = null, bool? showCreatedAlert = null,
            string searchString = "", bool? deep = false, bool asTrackable = false)
        {
            var query = from devices in await this.UnitOfWork.DeviceRepository.GetAsync(
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.DeviceRole),
                        AsTrackable: asTrackable)
                        select devices;

            if (isProviderDomainRole != null) query = query.Where(x => x.DeviceRole.IsProviderDomainRole == isProviderDomainRole);
            if (isTenantDomainRole != null) query = query.Where(x => x.DeviceRole.IsTenantDomainRole == isTenantDomainRole);
            if (created != null ) query = query.Where(x => x.Created);
            if (showCreatedAlert != null) query = query.Where(x => x.ShowCreatedAlert);
            if (!string.IsNullOrEmpty(searchString)) query = query.Where(x => x.Name.Contains(searchString));
            
            return query.ToList();
        }
      
        public async Task<IEnumerable<Device>> GetAllByLocationIDAsync(int locationID, int? planeID = null, 
            bool? isProviderDomainRole = null, bool? isTenantDomainRole = null, bool? deep = false, 
            bool asTrackable = false)
        {
            var query = from devices in await this.UnitOfWork.DeviceRepository.GetAsync(
                    q => 
                        q.LocationID == locationID,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.DeviceRole),
                        AsTrackable: asTrackable)
                        select devices;

            if (isProviderDomainRole != null) query = query.Where(x => x.DeviceRole.IsProviderDomainRole == isProviderDomainRole);
            if (isTenantDomainRole != null) query = query.Where(x => x.DeviceRole.IsTenantDomainRole == isTenantDomainRole);
            if (planeID != null) query = query.Where(x => x.PlaneID == planeID);

            return query.ToList();
        }

        public async Task<Device> GetByIDAsync(int id, bool? isProviderDomainRole = null, bool? isTenantDomainRole = null, 
            bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await this.UnitOfWork.DeviceRepository.GetAsync(
                   q =>
                     q.DeviceID == id,
                     query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.DeviceRole),
                     AsTrackable: asTrackable)
                         select result);

            if (isProviderDomainRole != null) query = query.Where(x => x.DeviceRole.IsProviderDomainRole == isProviderDomainRole);
            if (isTenantDomainRole != null) query = query.Where(x => x.DeviceRole.IsTenantDomainRole == isTenantDomainRole);

            return query.SingleOrDefault();
        }

        public async Task<Device> GetByNameAsync(string name, bool? isProviderDomainRole = null, bool? isTenantDomainRole = null,
            bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await this.UnitOfWork.DeviceRepository.GetAsync(
                  q =>
                    q.Name == name,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.DeviceRole),
                    AsTrackable: asTrackable)
                         select result);

            if (isProviderDomainRole != null) query = query.Where(x => x.DeviceRole.IsProviderDomainRole == isProviderDomainRole);
            if (isTenantDomainRole != null) query = query.Where(x => x.DeviceRole.IsTenantDomainRole == isTenantDomainRole);

            return query.SingleOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SCM.Services;

namespace Mind.Services
{
    public class BasePortService : BaseService
    {
        public BasePortService(IUnitOfWork unitOfWork, IMapper mapper) : base (unitOfWork, mapper)
        {
        }

        public async Task<Port> GetByIDAsync(int id, bool isProviderDomainRole = false, bool isTenantDomainRole = false, bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.PortRepository.GetAsync(
                    q =>
                         q.ID == id,
                         query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.Device.DeviceRole),
                         AsTrackable: asTrackable)
                         select result);

            if (isProviderDomainRole) query = query.Where(x => x.Device.DeviceRole.IsProviderDomainRole);
            if (isTenantDomainRole) query = query.Where(x => x.Device.DeviceRole.IsTenantDomainRole);

            return query.SingleOrDefault();
        }

        public async Task<IEnumerable<Port>> GetAllByDeviceIDAsync(int deviceId, bool isProviderDomainRole = false, bool isTenantDomainRole = false, 
            bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.PortRepository.GetAsync(
                    q =>
                        q.DeviceID == deviceId,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.Device.DeviceRole),
                        AsTrackable: asTrackable)
                        select result);

            if (isProviderDomainRole) query = query.Where(x => x.Device.DeviceRole.IsProviderDomainRole);
            if (isTenantDomainRole) query = query.Where(x => x.Device.DeviceRole.IsTenantDomainRole);

            return query.ToList();
        }

        public async Task<IEnumerable<Port>> GetAllByInterfaceIDAsync(int interfaceId, bool isProviderDomainRole = false, bool isTenantDomainRole = false,
            bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.PortRepository.GetAsync(
                    q =>
                         q.InterfaceID == interfaceId,
                         query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.Device.DeviceRole),
                         AsTrackable: asTrackable)
                         select result);

            if (isProviderDomainRole) query = query.Where(x => x.Device.DeviceRole.IsProviderDomainRole);
            if (isTenantDomainRole) query = query.Where(x => x.Device.DeviceRole.IsTenantDomainRole);

            return query.ToList();
        }

        public async Task<IEnumerable<Port>> GetAllByAttachmentIDAsync(int attachmentId, bool isProviderDomainRole = false, bool isTenantDomainRole = false,
            bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.PortRepository.GetAsync(
                    q =>
                         q.Interface.AttachmentID == attachmentId,
                         query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.Device.DeviceRole),
                         AsTrackable: asTrackable)
                         select result);

            if (isProviderDomainRole) query = query.Where(x => x.Device.DeviceRole.IsProviderDomainRole);
            if (isTenantDomainRole) query = query.Where(x => x.Device.DeviceRole.IsTenantDomainRole);

            return query.ToList();
        }
    }
}
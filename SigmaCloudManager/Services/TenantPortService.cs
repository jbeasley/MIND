using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mind.Builders;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;
using SCM.Validators;

namespace Mind.Services
{
    public class TenantPortService : BasePortService, ITenantPortService
    {
        private readonly IPortDirector _director;
        private readonly IPortUpdateDirector _updateDirector;

        public TenantPortService(IUnitOfWork unitOfWork, IMapper mapper, 
            IPortDirector director, IPortUpdateDirector updateDirector) : base (unitOfWork, mapper)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        public Task<IEnumerable<Port>> GetAllByAttachmentIDAsync(int attachmentId, bool? deep = false, bool asTrackable = false)
        {
            return base.GetAllByAttachmentIDAsync(attachmentId, isTenantDomainRole: true, deep: deep, asTrackable: asTrackable);
        }

        public Task<IEnumerable<Port>> GetAllByDeviceIDAsync(int deviceId, bool? deep = false, bool asTrackable = false)
        {
            return base.GetAllByDeviceIDAsync(deviceId, isTenantDomainRole: true, deep: deep, asTrackable: asTrackable);
        }

        public Task<IEnumerable<Port>> GetAllByInterfaceIDAsync(int interfaceId, bool? deep = false, bool asTrackable = false)
        {
            return base.GetAllByInterfaceIDAsync(interfaceId, isTenantDomainRole: true, deep: deep, asTrackable: asTrackable);
        }

        public Task<Port> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return base.GetByIDAsync(id, isTenantDomainRole: true, deep: deep, asTrackable: asTrackable);
        }

        public async Task<Port> AddAsync(int deviceId, PortRequest request)
        {
            var port = await _director.BuildAsync(deviceId, request);
            UnitOfWork.PortRepository.Insert(port);
            await UnitOfWork.SaveAsync();

            return await GetByIDAsync(port.ID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int portId)
        {
            var port = (from result in await UnitOfWork.PortRepository.GetAsync(
                    q =>
                        q.ID == portId,
                        query: q => q.IncludeDeleteValidationProperties(),
                        AsTrackable: true)
                        select result)
                        .Single();

            port.ValidateDelete();
            UnitOfWork.PortRepository.Delete(port);
            await UnitOfWork.SaveAsync();
        }

        public async Task<Port> UpdateAsync(int portId, PortUpdate update)
        {
            await _updateDirector.UpdateAsync(portId, update);
            await UnitOfWork.SaveAsync();

            return await GetByIDAsync(portId, deep: true, asTrackable: false);
        }
    }
}

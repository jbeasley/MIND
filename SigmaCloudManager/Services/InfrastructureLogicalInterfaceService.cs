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
    public class InfrastructureLogicalInterfaceService : BaseLogicalInterfaceService, IInfrastructureLogicalInterfaceService
    {
        private readonly IInfrastructureLogicalInterfaceDirector _director;

        public InfrastructureLogicalInterfaceService(IUnitOfWork unitOfWork, IMapper mapper, 
            IInfrastructureLogicalInterfaceDirector director) : base (unitOfWork, mapper)
        {
            _director = director;
        }

        public Task<IEnumerable<LogicalInterface>> GetAllByDeviceIDAsync(int deviceId, bool? deep = false, bool asTrackable = false)
        {
            return base.GetAllByDeviceIDAsync(deviceId, isInfrastructure: true, deep: deep, asTrackable: asTrackable);
        }

        public Task<LogicalInterface> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return base.GetByIDAsync(id, isInfrastructure: true, deep: deep, asTrackable: asTrackable);
        }

        public async Task<LogicalInterface> AddAsync(int deviceId, InfrastructureLogicalInterfaceRequest request)
        {
            var logicalInterface = await _director.BuildAsync(deviceId, request);
            UnitOfWork.LogicalInterfaceRepository.Insert(logicalInterface);
            await UnitOfWork.SaveAsync();

            return await GetByIDAsync(logicalInterface.LogicalInterfaceID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int logicalInterfaceId)
        {
            var logicalInterface = (from result in await UnitOfWork.LogicalInterfaceRepository.GetAsync(
                                q =>
                                    q.LogicalInterfaceID == logicalInterfaceId,
                                    AsTrackable: true)
                                    select result)
                                   .Single();

            UnitOfWork.LogicalInterfaceRepository.Delete(logicalInterface);
            await UnitOfWork.SaveAsync();
        }

        public async Task<LogicalInterface> UpdateAsync(int logicalInterfaceId, LogicalInterfaceUpdate update)
        {
            await _director.UpdateAsync(logicalInterfaceId, update);
            await UnitOfWork.SaveAsync();

            return await GetByIDAsync(logicalInterfaceId, deep: true, asTrackable: false);
        }
    }
}

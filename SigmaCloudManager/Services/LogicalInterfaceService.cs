using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using AutoMapper;
using SCM.Factories;

namespace SCM.Services
{
    public class LogicalInterfaceService : BaseService, ILogicalInterfaceService
    {
        public LogicalInterfaceService(ILogicalInterfaceFactory logicalInterfaceFactory, 
            IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            LogicalInterfaceFactory = logicalInterfaceFactory;
        }

        private ILogicalInterfaceFactory LogicalInterfaceFactory { get; }
        private string Properties { get; } = "RoutingInstance";

        public async Task<LogicalInterface> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.LogicalInterfaceRepository.GetAsync(q => q.LogicalInterfaceID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<IEnumerable<LogicalInterface>> GetAllByDeviceIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.LogicalInterfaceRepository.GetAsync(q => q.RoutingInstance.DeviceID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<IEnumerable<LogicalInterface>> GetAllByRoutingInstanceIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.LogicalInterfaceRepository.GetAsync(q => q.RoutingInstanceID == id, 
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<int> AddAsync(LogicalInterface logicalInterface)
        {
            // Call the factory to populate the logical interface object

            var factoryResult = await LogicalInterfaceFactory.NewAsync(logicalInterface);
            var item = (LogicalInterface)factoryResult.Item;
            this.UnitOfWork.LogicalInterfaceRepository.Insert(item);

            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(LogicalInterface logicalInterface)
        {
            this.UnitOfWork.LogicalInterfaceRepository.Update(logicalInterface);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(LogicalInterface logicalInterface)
        {
            this.UnitOfWork.LogicalInterfaceRepository.Delete(logicalInterface);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
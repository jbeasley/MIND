using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;

namespace SCM.Factories
{
    /// <summary>
    /// Factory for creating Logical Interfaces
    /// </summary>
    public class LogicalInterfaceFactory : BaseFactory, ILogicalInterfaceFactory
    {
        public LogicalInterfaceFactory(IRoutingInstanceService routingInstanceService, 
            IUnitOfWork unitOfWork, IMapper mapper) : base(mapper)
        {
            RoutingInstanceService = routingInstanceService;
            UnitOfWork = unitOfWork;
        }

        private IRoutingInstanceService RoutingInstanceService { get; }
        private IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Create a new Logical Interface
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FactoryResult> NewAsync(LogicalInterface logicalInterface)
        {
            var result = new FactoryResult
            {
                IsSuccess = true,
                Item = logicalInterface
            };

            // Find the first un-used identifier in the range 1 - 65535
            // The range is scoped to the device to which the logical interface belongs
            // and the logical interface type

            var routingInstance = await RoutingInstanceService.GetByIDAsync(logicalInterface.RoutingInstanceID);
            var usedIds = from x in await UnitOfWork.LogicalInterfaceRepository.GetAsync(q => 
            q.RoutingInstance.DeviceID == routingInstance.DeviceID 
                && q.LogicalInterfaceType == logicalInterface.LogicalInterfaceType)
                          select x.ID;

            int? id = Enumerable.Range(1, 65535).Except(usedIds).FirstOrDefault();

            logicalInterface.ID = id ?? throw new FactoryFailureException("Unable to assign a free ID value to the Logical Interface. "
                    + $"Have all IDs in the range 1 - 65535 for device '{routingInstance.Device.Name}' been used?");

            return result;
        }
    }
}

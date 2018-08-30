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
    /// Factory for creating Devices
    /// </summary>
    public class DeviceFactory : BaseFactory, IDeviceFactory
    {
        public DeviceFactory(IMapper mapper, 
            IRoutingInstanceTypeService routingInstanceTypeService, 
            IDeviceRoleService deviceRoleService,
            IRoutingInstanceFactory routingInstanceFactory) : base(mapper)
        {
            RoutingInstanceTypeService = routingInstanceTypeService;
            DeviceRoleService = deviceRoleService;
            RoutingInstanceFactory = routingInstanceFactory;
        }

        private IRoutingInstanceTypeService RoutingInstanceTypeService { get; }
        private IDeviceRoleService DeviceRoleService { get; }
        private IRoutingInstanceFactory RoutingInstanceFactory { get; }

        /// <summary>
        /// Create a new Device
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FactoryResult> NewAsync(Device device)
        {
            var result = new FactoryResult
            {
                IsSuccess = true,
                Item = device
            };

            var deviceRole = await DeviceRoleService.GetByIDAsync(device.DeviceRoleID);
     
            device.RequiresSync = deviceRole.RequireSyncToNetwork;
            device.Created = true;
            device.ShowRequiresSyncAlert = deviceRole.RequireSyncToNetwork;
            device.ShowCreatedAlert = true;

            // Create a global routing instance for the new device

            var routingInstanceType = await RoutingInstanceTypeService.GetByTypeAsync(RoutingInstanceTypeEnum.Default);
            if (routingInstanceType == null)
            {
                throw new FactoryFailureException("Could not create device. The default layer3 routing instance type was not found.");
            }

            var routingInstanceResult = await RoutingInstanceFactory.NewAsync(new RoutingInstance()
            {
                RoutingInstanceTypeID = routingInstanceType.RoutingInstanceTypeID
            });

            var routingInstance = (RoutingInstance)routingInstanceResult.Item;
            device.RoutingInstances.Add(routingInstance);

            return result;
        }
    }
}

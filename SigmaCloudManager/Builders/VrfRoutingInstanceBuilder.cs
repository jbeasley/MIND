using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class VrfRoutingInstanceBuilder : IRoutingInstanceBuilder
    {
        private readonly IRoutingInstanceTypeService _routingInstanceTypeService;
        private readonly IRouteDistinguisherRangeService _routeDistinguisherRangeService;
        private readonly IRoutingInstanceService _routingInstanceService;
        private RoutingInstance _routingInstance;

        public VrfRoutingInstanceBuilder(IRoutingInstanceTypeService routingInstanceTypeService, IRouteDistinguisherRangeService routeDistinguisherRangeService,
            IRoutingInstanceService routingInstanceService)
        {
            _routingInstanceTypeService = routingInstanceTypeService;
            _routeDistinguisherRangeService = routeDistinguisherRangeService;
            _routingInstanceService = routingInstanceService;
        }

        public async Task Create()
        { 
            _routingInstance.Name = Guid.NewGuid().ToString("N");

            var rdRange = await _routeDistinguisherRangeService.GetByNameAsync("Default");
            if (rdRange == null)
            {
                throw new BuilderUnableToCompleteException("The default route distinguisher range was not found. " +
                    "Please contact your system administrator to resolve this issue.");
            }

            var usedRDs = await _routingInstanceService.GetAllByRouteDistinguisherRangeNameAsync("Default");

            // Allocate a new unused RD from the RD range

            int? newRdAssignedNumberSubField = Enumerable.Range(rdRange.AssignedNumberSubFieldStart, rdRange.AssignedNumberSubFieldCount)
                .Except(usedRDs.Select(q => q.AssignedNumberSubField.Value)).FirstOrDefault();

            if (newRdAssignedNumberSubField == null)
            {
                throw new BuilderUnableToCompleteException("Failed to allocate a free route distinguisher. "
                    + "Please contact your system administrator, or try another range.");
            }

            _routingInstance.AdministratorSubField = rdRange.AdministratorSubField;
            _routingInstance.AssignedNumberSubField = newRdAssignedNumberSubField.Value;
            _routingInstance.RouteDistinguisherRangeID = rdRange.RouteDistinguisherRangeID;
        }

        public RoutingInstance GetResult()
        {
            return _routingInstance;
        }
    }
}

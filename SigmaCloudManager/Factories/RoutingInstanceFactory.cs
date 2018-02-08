using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Factories
{
    /// <summary>
    /// Factory for creating Routing Instances
    /// </summary>
    public class RoutingInstanceFactory : BaseFactory, IRoutingInstanceFactory
    {
        public RoutingInstanceFactory(IRoutingInstanceTypeService routingInstanceTypeService, 
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(mapper)
        {
            RoutingInstanceTypeService = routingInstanceTypeService;
            UnitOfWork = unitOfWork;
        }
        
        private IRoutingInstanceTypeService RoutingInstanceTypeService { get; }
        private IUnitOfWork UnitOfWork { get; }

        public async Task<FactoryResult> NewAsync(RoutingInstance routingInstance)
        {
            return await NewRoutingInstanceAsync(routingInstance);
        }

        public async Task<FactoryResult> NewAsync(AttachmentRequest request)
        {
            var routingInstance = Mapper.Map<RoutingInstance>(request);
            return await NewRoutingInstanceAsync(routingInstance);
        }

        public async Task<FactoryResult> NewAsync(AttachmentUpdate update)
        {
            var routingInstance = Mapper.Map<RoutingInstance>(update);
            return await NewRoutingInstanceAsync(routingInstance);
        }

        public async Task<FactoryResult> NewAsync(VifRequest request)
        {
            var routingInstance = Mapper.Map<RoutingInstance>(request);
            return await NewRoutingInstanceAsync(routingInstance);
        }

        public async Task<FactoryResult> NewAsync(VifUpdate update)
        {
            var routingInstance = Mapper.Map<RoutingInstance>(update);
            return await NewRoutingInstanceAsync(routingInstance);
        }

        private async Task<FactoryResult> NewRoutingInstanceAsync(RoutingInstance routingInstance)
        {
            var result = new FactoryResult
            {
                IsSuccess = true,
                Item = routingInstance
            };

            var routingInstanceType = await RoutingInstanceTypeService.GetByIDAsync(routingInstance.RoutingInstanceTypeID);
            if (routingInstanceType == null)
            {
                throw new FactoryFailureException("Could not find the requested Routing Instance Type.");
            }

            if (routingInstanceType.IsVrf)
            {
                routingInstance.Name = Guid.NewGuid().ToString("N");

                var dbResult = await UnitOfWork.RouteDistinguisherRangeRepository.GetAsync(q => q.Name == "Default");
                var rdRange = dbResult.SingleOrDefault();

                if (rdRange == null)
                {
                    throw new FactoryFailureException("The default route distinguisher range was not found.");
                }

                var usedRDs = await UnitOfWork.RoutingInstanceRepository.GetAsync(q =>
                    q.RouteDistinguisherRangeID == rdRange.RouteDistinguisherRangeID);

                // Allocate a new unused RD from the RD range

                int? newRdAssignedNumberSubField = Enumerable.Range(rdRange.AssignedNumberSubFieldStart, rdRange.AssignedNumberSubFieldCount)
                    .Except(usedRDs.Select(q => q.AssignedNumberSubField.Value)).FirstOrDefault();

                if (newRdAssignedNumberSubField == null)
                {
                    throw new FactoryFailureException("Failed to allocate a free route distinguisher. "
                        + "Please contact your administrator, or try another range.");
                }

                routingInstance.AdministratorSubField = rdRange.AdministratorSubField;
                routingInstance.AssignedNumberSubField = newRdAssignedNumberSubField.Value;
                routingInstance.RouteDistinguisherRangeID = rdRange.RouteDistinguisherRangeID;

            }
            else if (routingInstanceType.IsDefault)
            {
                routingInstance.Name = "Default";
            }
            else
            {
                throw new FactoryFailureException("Unable to create routing instance. Invalid Routing Instance Type.");
            }

            return result;
        }
    }
}

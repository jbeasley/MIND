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
    public class BaseLogicalInterfaceService : BaseService
    {
        public BaseLogicalInterfaceService(IUnitOfWork unitOfWork, IMapper mapper) : base (unitOfWork, mapper)
        {
        }

        public async Task<LogicalInterface> GetByIDAsync(int id, bool isTenantFacing = false, bool isInfrastructure = false, 
            bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.LogicalInterfaceRepository.GetAsync(
                    q =>
                         q.LogicalInterfaceID == id,
                         query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.RoutingInstance.RoutingInstanceType)
                                                                                                .Include(x => x.RoutingInstance.Device.DeviceRole),
                         AsTrackable: asTrackable)
                         select result);

            if (isTenantFacing) query = query.Where(x => x.RoutingInstance.RoutingInstanceType.IsTenantFacingVrf);
            if (isInfrastructure) query = query.Where(x => x.RoutingInstance.Device.DeviceRole.IsProviderDomainRole && 
            x.RoutingInstance.RoutingInstanceType.IsDefault || x.RoutingInstance.RoutingInstanceType.IsInfrastructureVrf);

            return query.SingleOrDefault();
        }

        public async Task<IEnumerable<LogicalInterface>> GetAllByRoutingInstanceIDAsync(int routingInstanceId, bool isTenantFacing = false, 
            bool isInfrastructure = false, bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.LogicalInterfaceRepository.GetAsync(
                    q =>
                        q.RoutingInstanceID == routingInstanceId,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.RoutingInstance.RoutingInstanceType)
                                                                                               .Include(x => x.RoutingInstance.Device.DeviceRole),
                        AsTrackable: asTrackable)
                        select result);

            if (isTenantFacing) query = query.Where(x => x.RoutingInstance.RoutingInstanceType.IsTenantFacingVrf);
            if (isInfrastructure) query = query.Where(x => x.RoutingInstance.Device.DeviceRole.IsProviderDomainRole &&
            x.RoutingInstance.RoutingInstanceType.IsDefault || x.RoutingInstance.RoutingInstanceType.IsInfrastructureVrf);

            return query.ToList();
        }
    
        public async Task<IEnumerable<LogicalInterface>> GetAllByDeviceIDAsync(int deviceId, bool isTenantFacing = false,
            bool isInfrastructure = false, bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.LogicalInterfaceRepository.GetAsync(
                    q =>

                         q.RoutingInstance.DeviceID == deviceId,
                         query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.RoutingInstance.RoutingInstanceType)
                                                                                                .Include(x => x.RoutingInstance.Device.DeviceRole),
                         AsTrackable: asTrackable)
                         select result);

            if (isTenantFacing) query = query.Where(x => x.RoutingInstance.RoutingInstanceType.IsTenantFacingVrf);
            if (isInfrastructure) query = query.Where(x => x.RoutingInstance.Device.DeviceRole.IsProviderDomainRole &&
            x.RoutingInstance.RoutingInstanceType.IsDefault || x.RoutingInstance.RoutingInstanceType.IsInfrastructureVrf);

            return query.ToList();
        }
    }
}
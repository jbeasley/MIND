using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using AutoMapper;
using SCM.Services;
using Mind.Builders;
using Mind.Models.RequestModels;
using System;
using Microsoft.EntityFrameworkCore;

namespace Mind.Services
{
    public class InfrastructureRoutingInstanceService : BaseService, IInfrastructureRoutingInstanceService
    {
        private readonly Func<RoutingInstanceType, IRoutingInstanceDirector> _directorFactory;

        public InfrastructureRoutingInstanceService(IUnitOfWork unitOfWork, IMapper mapper, 
            Func<RoutingInstanceType, IRoutingInstanceDirector> directorFactory) : base(unitOfWork, mapper)
        {
            _directorFactory = directorFactory;
        }

        public async Task<RoutingInstance> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.RoutingInstanceRepository.GetAsync(
                q =>
                    q.RoutingInstanceID == id &&
                    (q.RoutingInstanceType.IsDefault || q.RoutingInstanceType.IsInfrastructureVrf),
                    query: q => deep.GetValueOrDefault() ? q.IncludeDeepProperties() : q,
                    AsTrackable: false)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<IEnumerable<RoutingInstance>> GetAllByDeviceIDAsync(int deviceId, bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.RoutingInstanceRepository.GetAsync(
                   q =>
                       q.DeviceID == deviceId &&
                       (q.RoutingInstanceType.IsInfrastructureVrf || q.RoutingInstanceType.IsDefault),
                       query: q => deep.GetValueOrDefault() ? q.IncludeDeepProperties() : q,
                       AsTrackable: false)
                         select result);

            return query.ToList();
        }

        public async Task<RoutingInstance> UpdateAsync(int routingInstanceId, RoutingInstanceRequest update)
        {
            var routingInstance = (from result in await UnitOfWork.RoutingInstanceRepository.GetAsync(
                    q =>
                     q.RoutingInstanceID == routingInstanceId &&
                    (q.RoutingInstanceType.IsDefault || q.RoutingInstanceType.IsInfrastructureVrf),
                    query: q => q.Include(x => x.RoutingInstanceType),
                    AsTrackable: false)
                    select result)
                    .Single();

            var director = _directorFactory(routingInstance.RoutingInstanceType);
            await director.UpdateAsync(routingInstanceId, update);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(routingInstanceId, deep: true, asTrackable: false);
        }
    }
}
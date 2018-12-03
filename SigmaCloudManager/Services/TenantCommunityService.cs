using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Mind.Models.RequestModels;
using SCM.Services;
using Mind.Builders;

namespace Mind.Services
{
    public class TenantCommunityService : BaseService, ITenantCommunityService
    {
        private readonly ITenantCommunityDirector _director;
        private readonly ITenantCommunityUpdateDirector _updateDirector;

        public TenantCommunityService(IUnitOfWork unitOfWork, ITenantCommunityDirector director,
            ITenantCommunityUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        public async Task<IEnumerable<TenantCommunity>> GetAllByTenantIDAsync(int id, string searchString = "", bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await this.UnitOfWork.TenantCommunityRepository.GetAsync(
                      q =>
                         q.TenantID == id,
                         query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                         AsTrackable: asTrackable)
                         select result);

            if (!string.IsNullOrEmpty(searchString)) query = query.Where(q => q.Name == searchString);
            return query.ToList();
        }

        public async Task<IEnumerable<TenantCommunity>> GetAllByTenantCommunitySetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.TenantCommunityRepository.GetAsync(
                  q =>
                    q.TenantCommunitySets
                     .Any(x =>
                    x.TenantCommunitySetID == id),
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select result)
                    .ToList();
        }

        public async Task<TenantCommunity> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.TenantCommunityRepository.GetAsync(
                q =>
                   q.TenantCommunityID == id, 
                   query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                   AsTrackable: asTrackable)
                   select result)
                   .SingleOrDefault();
        }

        /// <summary>
        /// Get a Tenant Community from the Autonomous System and Number constituent components
        /// of the Tenant Community.
        /// </summary>
        /// <param name="asNumber"></param>
        /// <param name="number"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<TenantCommunity> GetByCommunityAsync(int asNumber, int number, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.TenantCommunityRepository.GetAsync(
                q =>
                   q.AutonomousSystemNumber == asNumber && q.Number == number,
                   query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                   AsTrackable: asTrackable)
                    select result)
                   .SingleOrDefault();
        }

        public async Task<TenantCommunity> AddAsync(TenantCommunity tenantCommunity)
        {
            this.UnitOfWork.TenantCommunityRepository.Insert(tenantCommunity);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(tenantCommunity.TenantCommunityID, deep: true, asTrackable: false);
        }

        public async Task<TenantCommunity> AddAsync(int tenantId, TenantCommunityRequest request)
        {
            var tenantCommunity = await _director.BuildAsync(tenantId, request);
            this.UnitOfWork.TenantCommunityRepository.Insert(tenantCommunity);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(tenantCommunity.TenantCommunityID, deep: true, asTrackable: false);
        }

        public async Task<TenantCommunity> UpdateAsync(TenantCommunity tenantCommunity)
        {
            this.UnitOfWork.TenantCommunityRepository.Update(tenantCommunity);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(tenantCommunity.TenantCommunityID, deep: true, asTrackable: false);
        }

        public async Task<TenantCommunity> UpdateAsync(int tenantCommunityId, TenantCommunityRequest update)
        {
            var tenantCommunity = await _updateDirector.UpdateAsync(tenantCommunityId, update);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(tenantCommunityId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int tenantCommunityId)
        {
            var tenantCommunity = (from result in await UnitOfWork.TenantCommunityRepository.GetAsync(
                                q =>
                                   q.TenantCommunityID == tenantCommunityId,
                                   query: q => q.IncludeDeleteValidationProperties(),
                                   AsTrackable: true)
                                   select result)
                                   .Single();

            this.UnitOfWork.TenantCommunityRepository.Delete(tenantCommunity);
            await this.UnitOfWork.SaveAsync();
        }
    }
}

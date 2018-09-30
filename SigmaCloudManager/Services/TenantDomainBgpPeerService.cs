using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Mind.Builders;
using Mind.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public class TenantDomainBgpPeerService : BaseService, ITenantDomainBgpPeerService
    {
        private readonly ITenantDomainBgpPeerDirector _director;
        private readonly IBgpPeerUpdateDirector _updateDirector;

        public TenantDomainBgpPeerService(IUnitOfWork unitOfWork, ITenantDomainBgpPeerDirector director, 
            IBgpPeerUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        public async Task<IEnumerable<BgpPeer>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.BgpPeerRepository.GetAsync(
                q => 
                q.RoutingInstance.DeviceID == id,
                query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                AsTrackable: asTrackable);

        }

        public async Task<BgpPeer> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.BgpPeerRepository.GetAsync(
                q => 
                q.BgpPeerID == id,
                query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<BgpPeer> AddAsync(int deviceId, BgpPeerRequest request)
        {
            var bgpPeer = await _director.BuildAsync(deviceId, request);
            this.UnitOfWork.BgpPeerRepository.Insert(bgpPeer);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(bgpPeer.BgpPeerID, deep: true, asTrackable: false);
        }

        public async Task<BgpPeer> UpdateAsync(int bgpPeerId, BgpPeerRequest update)
        {
            await _updateDirector.UpdateAsync(bgpPeerId, update);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(bgpPeerId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int bgpPeerId)
        {
            var bgpPeer = (from result in await UnitOfWork.BgpPeerRepository.GetAsync(
                        q => q.BgpPeerID == bgpPeerId,
                           query: q => q.IncludeDeleteValidationProperties(),
                           AsTrackable: true)
                           select result)
                           .Single();

            bgpPeer.ValidateDelete();

            await this.UnitOfWork.BgpPeerRepository.DeleteAsync(bgpPeerId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}

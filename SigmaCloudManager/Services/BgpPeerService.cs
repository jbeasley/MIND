using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Validators;
using Mind.Services;
using Mind.Builders;
using Mind.Models.RequestModels;

namespace SCM.Services
{
    public class BgpPeerService : BaseService, IBgpPeerService
    {
        private readonly string _properties = "RoutingInstance,"
                  + "VpnTenantCommunitiesIn.TenantCommunity,"
                  + "VpnTenantIpNetworksIn.TenantIpNetwork,"
                  + "VpnTenantCommunitiesOut.TenantCommunity,"
                  + "VpnTenantIpNetworksOut.TenantIpNetwork";
        private readonly IBgpPeerValidator _validator;
        private readonly IBgpPeerDirector _director;
        private readonly IBgpPeerUpdateDirector _updateDirector;

        public BgpPeerService(IUnitOfWork unitOfWork, IBgpPeerDirector director, 
            IBgpPeerUpdateDirector updateDirector, IBgpPeerValidator validator) : base(unitOfWork, validator)
        {
            _validator = validator;
            _director = director;
            _updateDirector = updateDirector;
        }

        public async Task<IEnumerable<BgpPeer>> GetAllByRoutingInstanceIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return  await this.UnitOfWork.BgpPeerRepository.GetAsync(q => q.RoutingInstanceID == id, 
                includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                AsTrackable: asTrackable);

        }

        public async Task<BgpPeer> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.BgpPeerRepository.GetAsync(q => q.BgpPeerID == id,
                includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="bgpPeer"></param>
        /// <returns></returns>
        public async Task<BgpPeer> AddAsync(BgpPeer bgpPeer)
        {
            this.UnitOfWork.BgpPeerRepository.Insert(bgpPeer);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(bgpPeer.BgpPeerID, deep: true, asTrackable: false);
        }

        public async Task<BgpPeer> AddAsync(int routingInstanceId, BgpPeerRequest request)
        {
            var bgpPeer = await _director.BuildAsync(routingInstanceId, request);
            this.UnitOfWork.BgpPeerRepository.Insert(bgpPeer);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(bgpPeer.BgpPeerID, deep: true, asTrackable: false);
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="bgpPeer"></param>
        /// <returns></returns>
        public async Task<BgpPeer> UpdateAsync(BgpPeer bgpPeer)
        {
            this.UnitOfWork.BgpPeerRepository.Update(bgpPeer);
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
            await _validator.ValidateDeleteAsync(bgpPeerId);
            if (!_validator.IsValid) throw new ServiceValidationException();
            await this.UnitOfWork.BgpPeerRepository.DeleteAsync(bgpPeerId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}

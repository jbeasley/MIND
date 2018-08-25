using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Validators;
using Mind.Services;

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

        public BgpPeerService(IUnitOfWork unitOfWork, IBgpPeerValidator validator) : base(unitOfWork, validator)
        {
            _validator = validator;
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

        public async Task<BgpPeer> AddAsync(BgpPeer bgpPeer)
        {
            await _validator.ValidateNewAsync(bgpPeer);
            if (!_validator.IsValid) throw new ServiceValidationException();
            this.UnitOfWork.BgpPeerRepository.Insert(bgpPeer);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(bgpPeer.BgpPeerID, deep: true, asTrackable: false);
        }
 
        public async Task<BgpPeer> UpdateAsync(BgpPeer bgpPeer)
        {
            await _validator.ValidateChangesAsync(bgpPeer);
            if (!_validator.IsValid) throw new ServiceValidationException();
            this.UnitOfWork.BgpPeerRepository.Update(bgpPeer);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(bgpPeer.BgpPeerID, deep: true, asTrackable: false);
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

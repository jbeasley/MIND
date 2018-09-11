using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models.RequestModels;
using SCM.Data;
using SCM.Models;
using System.Net;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for BGP Peers
    /// </summary>
    public class BgpPeerValidator : BaseValidator, IBgpPeerValidator
    {
        public BgpPeerValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Validate deletion of a BGP Peer. A BGP Peer cannot be deleted if it used in any inbound or outbound
        /// routing policy.
        /// </summary>
        /// <param name="bgpPeerId"></param>
        /// <returns></returns>
        public async Task ValidateDeleteAsync(int bgpPeerId)
        {
            var bgpPeer = (from result in await _unitOfWork.BgpPeerRepository.GetAsync(
              q =>
                q.BgpPeerID == bgpPeerId,
                includeProperties: "VpnTenantCommunitiesIn.AttachmentSet," +
                "VpnTenantCommunitiesOut.AttachmentSet," +
                "VpnTenantIpNetworksIn.AttachmentSet," +
                "VpnTenantIpNetworksOut.AttachmentSet," +
                "VpnTenantCommunitiesIn.TenantCommunity," +
                "VpnTenantCommunitiesOut.TenantCommunity," +
                "VpnTenantIpNetworksIn.TenantIpNetwork," +
                "VpnTenantIpNetworksOut.TenantIpNetwork",
                AsTrackable: false)
                           select result)
                      .Single();

            bgpPeer.VpnTenantCommunitiesIn
                .ToList()
                .ForEach(x =>
                    ValidationDictionary.AddError(string.Empty, "The BGP Peer cannot be deleted because community "
                    + $"'{x.TenantCommunity.Name}' is applied to the inbound policy of attachment set '{x.AttachmentSet.Name}'.")
            );

            bgpPeer.VpnTenantCommunitiesOut
                .ToList()
                .ForEach(x =>
                    ValidationDictionary.AddError(string.Empty, "The BGP Peer cannot be deleted because community "
                    + $"'{x.TenantCommunity.Name}' is applied to the outbound policy of attachment set '{x.AttachmentSet.Name}'.")
                );

            bgpPeer.VpnTenantIpNetworksIn
                .ToList()
                .ForEach(x =>
                    ValidationDictionary.AddError(string.Empty, "The BGP Peer cannot be deleted because IP network "
                    + $"'{x.TenantIpNetwork.CidrName}' is applied to the inbound policy of attachment set '{x.AttachmentSet.Name}'.")
                );

            bgpPeer.VpnTenantIpNetworksOut
                .ToList()
                .ForEach(x =>
                    ValidationDictionary.AddError(string.Empty, "The BGP Peer cannot be deleted because IP network "
                    + $"'{x.TenantIpNetwork.CidrName}' is applied to the outbound policy of attachment set '{x.AttachmentSet.Name}'.")
                );
        }
    }
}

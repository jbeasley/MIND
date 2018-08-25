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
        /// Validate a new BGP Peer.
        /// </summary>
        /// <param name="bgpPeer"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(BgpPeer bgpPeer)
        {
            await ValidateBgpPeerIpAddress(bgpPeer);
        }

        /// <summary>
        /// Validate changes to a BGP Peer.
        /// </summary>
        /// <param name="bgpPeer"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(BgpPeer bgpPeer)
        {
            var currentBgpPeer = (from result in await _unitOfWork.BgpPeerRepository.GetAsync(
                          q =>
                            q.BgpPeerID == bgpPeer.BgpPeerID,
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

            if (currentBgpPeer.Ipv4PeerAddress != bgpPeer.Ipv4PeerAddress)
            {
                currentBgpPeer.VpnTenantCommunitiesIn
                    .ToList()
                    .ForEach(x =>
                        ValidationDictionary.AddError(string.Empty, "The IP address of the BGP Peer cannot be changed because community "
                        + $"'{x.TenantCommunity.Name}' is applied to the inbound policy of attachment set '{x.AttachmentSet.Name}'.")
                    );

                currentBgpPeer.VpnTenantCommunitiesOut
                    .ToList()
                    .ForEach(x =>
                        ValidationDictionary.AddError(string.Empty, "The IP address of the BGP Peer cannot be changed because community "
                        + $"'{x.TenantCommunity.Name}' is applied to the outbound policy of attachment set '{x.AttachmentSet.Name}'.")
                    );

                currentBgpPeer.VpnTenantIpNetworksIn
                    .ToList()
                    .ForEach(x =>
                        ValidationDictionary.AddError(string.Empty, "The IP address of the BGP Peer cannot be changed because IP network "
                        + $"'{x.TenantIpNetwork.CidrName}' is applied to the inbound policy of attachment set '{x.AttachmentSet.Name}'.")
                    );

                currentBgpPeer.VpnTenantIpNetworksOut
                    .ToList()
                    .ForEach(x =>
                        ValidationDictionary.AddError(string.Empty, "The IP address of the BGP Peer cannot be changed because IP network "
                        + $"'{x.TenantIpNetwork.CidrName}' is applied to the outbound policy of attachment set '{x.AttachmentSet.Name}'.")
                    );

                if (this.ValidationDictionary.IsValid) await ValidateBgpPeerIpAddress(bgpPeer);
            }
        }

        /// <summary>
        /// Validate deletion of a BGP Peer. A BGP Peer cannot be deleted if it used in any inbound or outbound
        /// routing policy.
        /// </summary>
        /// <param name="bgpPeer"></param>
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

        /// <summary>
        /// Helper to validate that the IP address of the BGP peer is valid in accordance with the
        /// reachability of the address from the associated routing instance.
        /// </summary>
        /// <param name="bgpPeer"></param>
        /// <returns></returns>
        private async Task ValidateBgpPeerIpAddress(BgpPeer bgpPeer)
        {
            // Skip the check if the peer IP address is several hops away
            // In this case the peer IP address will not be within the
            // same IP network as that assigned to the logical attachment.

            if (bgpPeer.IsMultiHop) return;

            var routingInstance = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                q => q.RoutingInstanceID == bgpPeer.RoutingInstanceID,
                includeProperties: "Vifs.Vlans.Vif.Attachment.Interfaces.Ports,Attachments.Interfaces.Attachment.Interfaces.Ports")
                                   select result)
                                       .Single();

            var bgpPeerIpAddress = IPAddress.Parse(bgpPeer.Ipv4PeerAddress);

            routingInstance.Vifs.SelectMany(x => x.Vlans)
                .ToList()
                .ForEach(x =>
                {
                    var network = IPNetwork.Parse(x.IpAddress, x.SubnetMask);
                    if (!network.Contains(bgpPeerIpAddress))
                    {
                        ValidationDictionary.AddError(string.Empty, $"IP address '{bgpPeer.Ipv4PeerAddress}' is not contained by the network " +
                        $"assigned to vif '{x.Vif.Name}' ({network.Network.ToString()}/{network.Cidr.ToString()}).");
                    }
                });


            routingInstance.Attachments.SelectMany(x => x.Interfaces)
                .ToList()
                .ForEach(x =>
                {
                    var network = IPNetwork.Parse(x.IpAddress, x.SubnetMask);
                    if (!network.Contains(bgpPeerIpAddress))
                    {
                        ValidationDictionary.AddError(string.Empty, $"IP address '{bgpPeer.Ipv4PeerAddress}' is not contained by the network " +
                            $"assigned to attachment '{x.Attachment.Name}' ({network.Network.ToString()}/{network.Cidr.ToString()}).");
                    }
                });
        }
    }
}

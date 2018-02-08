using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models.RequestModels;
using SCM.Services;
using SCM.Models;
using System.Net;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for BGP Peers
    /// </summary>
    public class BgpPeerValidator : BaseValidator, IBgpPeerValidator
    {
        public BgpPeerValidator(IBgpPeerService bgpPeerService, IRoutingInstanceService routingInstanceService, 
            IAttachmentService attachmentService, IVifService vifService)
        {
            BgpPeerService = bgpPeerService;
            RoutingInstanceService = routingInstanceService;
            AttachmentService = attachmentService;
            VifService = vifService;
        }

        private IBgpPeerService BgpPeerService { get; }
        private IRoutingInstanceService RoutingInstanceService { get; }
        private IAttachmentService AttachmentService { get; }
        private IVifService VifService { get; }

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
            var currentBgpPeer = await BgpPeerService.GetByIDAsync(bgpPeer.BgpPeerID);
            if (currentBgpPeer.IpAddress != bgpPeer.IpAddress)
            {
                if (currentBgpPeer.VpnTenantCommunitiesIn.Any())
                {
                    ValidationDictionary.AddError(string.Empty, "The IP address of the BGP Peer cannot be changed because inbound community-based "
                       + $"policy is applied to the peer for the following Attachment Sets:");

                    foreach (var vpnTenantCommunityIn in currentBgpPeer.VpnTenantCommunitiesIn)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Attachment Set '{vpnTenantCommunityIn.AttachmentSet.Name}'.");
                    }
                }

                if (currentBgpPeer.VpnTenantCommunitiesOut.Any())
                {
                    ValidationDictionary.AddError(string.Empty, "The IP address of the BGP Peer cannot be changed because outbound community-based "
                      + $"policy is applied to the peer for the following Attachment Sets:");

                    foreach (var vpnTenantCommunityOut in currentBgpPeer.VpnTenantCommunitiesOut)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Attachment Set '{vpnTenantCommunityOut.AttachmentSet.Name}'.");
                    }
                }

                if (currentBgpPeer.VpnTenantNetworksIn.Any())
                {
                    ValidationDictionary.AddError(string.Empty, "The IP address of the BGP Peer cannot be changed because inbound network-based "
                        + $"policy is applied to the peer for the following Attachment Sets:");

                    foreach (var vpnTenantNetworkIn in currentBgpPeer.VpnTenantNetworksIn)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Attachment Set '{vpnTenantNetworkIn.AttachmentSet.Name}'.");
                    }
                }

                if (currentBgpPeer.VpnTenantNetworksOut.Any())
                {
                    ValidationDictionary.AddError(string.Empty, "The IP address of the BGP Peer cannot be changed because outbound network-based "
                        + $"policy is applied to the peer for the following Attachment Sets and VPNs:");

                    foreach (var vpnTenantNetworkOut in currentBgpPeer.VpnTenantNetworksOut)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Attachment Set '{vpnTenantNetworkOut.AttachmentSet.Name}'.");
                    }
                }
            }

            // Proceed to check the IP address is valid only if the current validation state is valid.

            if (this.ValidationDictionary.IsValid)
            {
                await ValidateBgpPeerIpAddress(bgpPeer);
            }
        }

        /// <summary>
        /// Validate deletion of a BGP Peer. A BGP Peer cannot be deleted if it used in any inbound or outbound
        /// routing policy.
        /// </summary>
        /// <param name="bgpPeer"></param>
        /// <returns></returns>
        public void ValidateDelete(BgpPeer bgpPeer)
        {
            if (bgpPeer.VpnTenantCommunitiesIn.Any())
            {
                foreach (var vpnTenantCommunity in bgpPeer.VpnTenantCommunitiesIn)
                {
                    ValidationDictionary.AddError(string.Empty, "The BGP Peer cannot be deleted because it is associated with an inbound routing policy "
                        + $"for Tenant Community '{vpnTenantCommunity.TenantCommunity.Name}' in Attachment Set {vpnTenantCommunity.AttachmentSet.Name}.");
                }
            }

            if (bgpPeer.VpnTenantNetworksIn.Any())
            {
                foreach (var vpnTenantNetwork in bgpPeer.VpnTenantNetworksIn)
                {
                    ValidationDictionary.AddError(string.Empty, "The BGP Peer cannot be deleted because it is associated with an inbound routing policy "
                        + $"for Tenant Network '{vpnTenantNetwork.TenantNetwork.CidrName}' in Attachment Set {vpnTenantNetwork.AttachmentSet.Name}.");
                }
            }

            if (bgpPeer.VpnTenantCommunitiesOut.Any())
            {
                foreach (var vpnTenantCommunity in bgpPeer.VpnTenantCommunitiesOut)
                {
                    ValidationDictionary.AddError(string.Empty, "The BGP Peer cannot be deleted because it is associated with an outbound routing policy "
                        + $"for Tenant Community '{vpnTenantCommunity.TenantCommunity.Name}' in Attachment Set {vpnTenantCommunity.AttachmentSet.Name}.");
                }
            }

            if (bgpPeer.VpnTenantNetworksOut.Any())
            {
                foreach (var vpnTenantNetwork in bgpPeer.VpnTenantNetworksOut)
                {
                    ValidationDictionary.AddError(string.Empty, "The BGP Peer cannot be deleted because it is associated with an outbound routing policy "
                        + $"for Tenant Network '{vpnTenantNetwork.TenantNetwork.CidrName}' in Attachment Set {vpnTenantNetwork.AttachmentSet.Name}.");
                }
            }
        }

        /// <summary>
        /// Helper to validate that the IP address of the BGP peer is valid in accordance with the
        /// reachability of the IP address from the associated Attachment or VIF.
        /// </summary>
        /// <param name="bgpPeer"></param>
        /// <returns></returns>
        private async Task ValidateBgpPeerIpAddress(BgpPeer bgpPeer)
        {
            // Skip the check if the peer IP address is several hops away
            // from the logical attachment. In this case the peer IP address will not be within the
            // same IP network as that assigned to the logical attachment.

            if (bgpPeer.IsMultiHop)
            {
                return;
            }

            var routingInstance = await RoutingInstanceService.GetByIDAsync(bgpPeer.RoutingInstanceID);
            var bgpPeerIpAddress = IPAddress.Parse(bgpPeer.IpAddress);
            var match = false;
            var messages = new List<string>();

            var vlans = routingInstance.Vifs.Where(x => x.IsLayer3).SelectMany(x => x.Vlans);
            if (vlans.Any())
            {
                // The IP network assigned to at least one vlan must contain the IP address of the BGP peer

                foreach (var vlan in vlans)
                {
                    var network = IPNetwork.Parse(vlan.IpAddress, vlan.SubnetMask);
                    if (IPNetwork.Contains(network, bgpPeerIpAddress))
                    {
                        match = true;
                        break;
                    }
                    else
                    {
                        var vif = await VifService.GetByIDAsync(vlan.VifID.Value);
                        messages.Add($"IP Address '{bgpPeer.IpAddress}' is not contained by the network " +
                            $"assigned to Vif '{vif.Name}' ({network.Network.ToString()}/{network.Cidr.ToString()}).");
                    }
                }
            }

            // If we've found a match then no need to continue

            if (match)
            {
                return;
            }
            
            var interfaces = routingInstance.Attachments.Where(x => x.IsLayer3).SelectMany(x => x.Interfaces);
            if (interfaces.Any())
            {
                // The IP network assigned to at least one interface must contain the IP address of the BGP peer

                foreach (var iface in interfaces)
                {
                    var network = IPNetwork.Parse(iface.IpAddress, iface.SubnetMask);
                    if (IPNetwork.Contains(network, bgpPeerIpAddress))
                    {
                        match = true;
                        break;
                    }
                    else
                    {
                        var attachment = await AttachmentService.GetByIDAsync(iface.AttachmentID);
                        messages.Add($"IP Address '{bgpPeer.IpAddress}' is not contained by the network " +
                            $"assigned to Attachment '{attachment.Name}' ({network.Network.ToString()}/{network.Cidr.ToString()}).");
                    }
                }
            }

            // Populate validation dictionary with the generated messages to the return to the UI

            if (!match)
            {
                messages.ForEach(x => ValidationDictionary.AddError(string.Empty, x));
            }
        }
    }
}

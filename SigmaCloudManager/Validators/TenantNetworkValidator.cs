using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Tenant Networks
    /// </summary>
    public class TenantNetworkValidator : BaseValidator, ITenantNetworkValidator
    {
        public TenantNetworkValidator(IVpnTenantNetworkInService vpnTenantNetworkInService,
            IVpnTenantNetworkOutService vpnTenantNetworkOutService,
            IVpnTenantNetworkRoutingInstanceService vpnTenantNetworkRoutingInstanceService,
            IVpnTenantNetworkStaticRouteRoutingInstanceService vpnTenantNetworkStaticRouteRoutingInstanceService,
            ITenantNetworkService tenantNetworkService)
        {
            VpnTenantNetworkInService = vpnTenantNetworkInService;
            VpnTenantNetworkOutService = vpnTenantNetworkOutService;
            VpnTenantNetworkRoutingInstanceService = vpnTenantNetworkRoutingInstanceService;
            VpnTenantNetworkStaticRouteRoutingInstanceService = vpnTenantNetworkStaticRouteRoutingInstanceService;
            TenantNetworkService = tenantNetworkService;
        }

        private IVpnTenantNetworkInService VpnTenantNetworkInService { get; }
        private ITenantNetworkService TenantNetworkService { get; }
        private IVpnTenantNetworkOutService VpnTenantNetworkOutService { get; }
        private IVpnTenantNetworkRoutingInstanceService VpnTenantNetworkRoutingInstanceService { get; }
        private IVpnTenantNetworkStaticRouteRoutingInstanceService VpnTenantNetworkStaticRouteRoutingInstanceService { get; }

        /// <summary>
        /// Validate a new Tenant Network.
        /// </summary>
        /// <param name="tenantNetwork"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(TenantNetwork tenantNetwork)
        {
            await CheckOverlappingNetworks(tenantNetwork);
        }

        /// <summary>
        /// Validate deletion of a Tenant Network. The Tenant Network cannot be deleted if 
        /// the network is bound to a VPN.
        /// </summary>
        /// <param name="tenantNetwork"></param>
        public async Task ValidateDeleteAsync(TenantNetwork tenantNetwork)
        {
            var vpnTenantNetworksIn = await VpnTenantNetworkInService.GetAllByTenantNetworkIDAsync(tenantNetwork.TenantNetworkID);
            foreach (var vpnTenantNetworkIn in vpnTenantNetworksIn)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Network '{tenantNetwork.CidrName}' "
                    + $"cannot be deleted because it is bound to Attachment Set '{vpnTenantNetworkIn.AttachmentSet.Name}'.");
            }

            var vpnTenantNetworksOut = await VpnTenantNetworkOutService.GetAllByTenantNetworkIDAsync(tenantNetwork.TenantNetworkID);
            foreach (var vpnTenantNetworkOut in vpnTenantNetworksOut)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Network '{tenantNetwork.CidrName}' "
                    + $"cannot be deleted because it is used for Outbound Policy in Attachment Set '{vpnTenantNetworkOut.AttachmentSet.Name}'.");
            }

            var vpnTenantNetworkRoutingInstances = await VpnTenantNetworkRoutingInstanceService.GetAllByTenantNetworkIDAsync(tenantNetwork.TenantNetworkID);
            foreach (var vpnTenantNetworkRoutingInstance in vpnTenantNetworkRoutingInstances)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Network '{tenantNetwork.CidrName}' "
                    + $"cannot be deleted because it is used for VRF Policy in Attachment Set '{vpnTenantNetworkRoutingInstance.AttachmentSet.Name}'.");
            }

            var vpnTenantNetworkStaticRouteRoutingInstances = await VpnTenantNetworkStaticRouteRoutingInstanceService.GetAllByTenantNetworkIDAsync(tenantNetwork.TenantNetworkID);
            foreach (var vpnTenantNetworkStaticRouteRoutingInstance in vpnTenantNetworkStaticRouteRoutingInstances)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Network '{tenantNetwork.CidrName}' "
                    + $"cannot be deleted because it is used for VRF static routes in Attachment Set '{vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSet.Name}'.");
            }
        }

        /// <summary>
        /// Validate changes to a Tenant Network.
        /// </summary>
        /// <param name="tenantNetwork"></param>
        public async Task ValidateChangesAsync(TenantNetwork tenantNetwork)
        {
            if (!tenantNetwork.AllowExtranet)
            {
                var query = from vpnTenantNetworksIn in await VpnTenantNetworkInService.GetAllByTenantNetworkIDAsync(tenantNetwork.TenantNetworkID)
                            from vpnAttachmentSets in vpnTenantNetworksIn.AttachmentSet.VpnAttachmentSets
                            from e in vpnAttachmentSets.Vpn.ExtranetVpns
                            select e;

                var extranetVpnMembers = query.ToList();

                foreach (var extranetVpnMember in extranetVpnMembers)
                {
                    ValidationDictionary.AddError(string.Empty, "The 'Allow Extranet' attribute must be enabled for Tenant Network "
                        + $"'{tenantNetwork.CidrName}' because the network is bound to VPN '{extranetVpnMember.MemberVpn.Name}' "
                        + $"which belongs to Extranet VPN '{extranetVpnMember.ExtranetVpn.Name}'.");
                }
            }

            await CheckOverlappingNetworks(tenantNetwork);
        }

        private async Task CheckOverlappingNetworks(TenantNetwork tenantNetwork)
        {
            // Check if the Tenant Network overlaps or is overlapped 
            // by a Tenant Network owned by any other Tenant

            var tenantNetworks = await TenantNetworkService.GetAllAsync();
            foreach (var t in tenantNetworks)
            {
                // Same Tenant can have overlapping networks, different Tenants cannot

                if (t.TenantID != tenantNetwork.TenantID)
                {
                    if (IPNetwork.Overlap(IPNetwork.Parse(t.CidrName), IPNetwork.Parse(tenantNetwork.CidrName)))
                    {
                        ValidationDictionary.AddError(string.Empty, $"'{t.CidrName}' is owned by Tenant " +
                            $"'{t.Tenant.Name}' and overlaps '{tenantNetwork.CidrName}'.");
                    }
                }
            }
        }
    }
}

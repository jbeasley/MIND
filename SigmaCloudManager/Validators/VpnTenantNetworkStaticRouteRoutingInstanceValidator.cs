using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;
using Mind.Services;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for VPN Tenant Networks (Tenant Networks which are associated with a VPN)
    /// </summary>
    public class VpnTenantNetworkStaticRouteRoutingInstanceValidator : BaseValidator, IVpnTenantNetworkStaticRouteRoutingInstanceValidator
    {
        public VpnTenantNetworkStaticRouteRoutingInstanceValidator(IVpnAttachmentSetService vpnAttachmentSetService,
            ITenantIpNetworkService tenantNetworkService,
            IVpnTenantNetworkStaticRouteRoutingInstanceService vpnTenantNetworkStaticRouteRoutingInstanceService)
        {
            VpnAttachmentSetService = vpnAttachmentSetService;
            TenantNetworkService = tenantNetworkService;
            VpnTenantNetworkStaticRouteRoutingInstanceService = vpnTenantNetworkStaticRouteRoutingInstanceService;
        }

        private IVpnAttachmentSetService VpnAttachmentSetService { get; set; }
        private ITenantIpNetworkService TenantNetworkService { get; set; }
        private IVpnTenantNetworkStaticRouteRoutingInstanceService VpnTenantNetworkStaticRouteRoutingInstanceService { get; set; }

        /// <summary>
        /// Validates a Tenant Network for binding to an Attachment Set.
        /// </summary>
        /// <param name="vpnTenantNetworkStaticRouteRoutingInstance"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VpnTenantNetworkStaticRouteRoutingInstance vpnTenantNetworkStaticRouteRoutingInstance)
        {
            var vpnAttachmentSets = await VpnAttachmentSetService.GetAllByAttachmentSetIDAsync(vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID);
            var vpns = vpnAttachmentSets.Select(x => x.Vpn);

            foreach (var vpn in vpns)
            if (vpn.IsExtranet)
            {
                var tenantNetwork = await TenantNetworkService.GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstance.TenantNetworkID);
                if (!tenantNetwork.AllowExtranet)
                {
                    ValidationDictionary.AddError(string.Empty, $"Tenant Network '{tenantNetwork.CidrName}' "
                        + "is not enabled for Extranet.");
                }
            }
        }
    }
}

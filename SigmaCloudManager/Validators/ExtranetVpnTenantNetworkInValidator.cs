using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models.RequestModels;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Extranet VPN Tenant Networks
    /// </summary>
    public class ExtranetVpnTenantNetworkInValidator : BaseValidator, IExtranetVpnTenantNetworkInValidator
    {
        public ExtranetVpnTenantNetworkInValidator(IExtranetVpnMemberService extranetVpnMemberService,
            IVpnTenantNetworkInService vpnTenantNetworkInService)
        {
            VpnTenantNetworkInService = vpnTenantNetworkInService;
            ExtranetVpnMemberService = extranetVpnMemberService;
        }

        private IExtranetVpnMemberService ExtranetVpnMemberService { get; }
        private IVpnTenantNetworkInService VpnTenantNetworkInService { get; }

        /// <summary>
        /// Validate a new Extranet VPN Tenant Network
        /// </summary>
        /// <param name="extranetVpnTenantNetworkIn"></param>
        public async Task ValidateNewAsync(ExtranetVpnTenantNetworkIn extranetVpnTenantNetworkIn)
        {
            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(extranetVpnTenantNetworkIn.ExtranetVpnMemberID);

            if (!extranetVpnMember.ExtranetVpn.IsExtranet)
            {
                ValidationDictionary.AddError(string.Empty, $"VPN '{extranetVpnMember.ExtranetVpn.Name}' is not enabled for Extranet.");
            }

            var vpnTenantNetwork = await VpnTenantNetworkInService.GetByIDAsync(extranetVpnTenantNetworkIn.VpnTenantNetworkInID);
            if (!vpnTenantNetwork.TenantNetwork.AllowExtranet)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Network '{vpnTenantNetwork.TenantNetwork.CidrName}' is not enabled for Extranet.");
            }
        }
    }
}

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
    /// Validator for Extranet VPN Tenant Communities
    /// </summary>
    public class ExtranetVpnTenantCommunityInValidator : BaseValidator, IExtranetVpnTenantCommunityInValidator
    {
        public ExtranetVpnTenantCommunityInValidator(IExtranetVpnMemberService extranetVpnMemberService,
            IVpnTenantCommunityInService vpnTenantCommunityInService)
        {
            VpnTenantCommunityInService = vpnTenantCommunityInService;
            ExtranetVpnMemberService = extranetVpnMemberService;
        }

        private IExtranetVpnMemberService ExtranetVpnMemberService { get; }
        private IVpnTenantCommunityInService VpnTenantCommunityInService { get; }

        /// <summary>
        /// Validate a new Extranet VPN Tenant Community
        /// </summary>
        /// <param name="extranetVpnTenantCommunityIn"></param>
        public async Task ValidateNewAsync(ExtranetVpnTenantCommunityIn extranetVpnTenantCommunityIn)
        {
            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(extranetVpnTenantCommunityIn.ExtranetVpnMemberID);

            if (!extranetVpnMember.ExtranetVpn.IsExtranet)
            {
                ValidationDictionary.AddError(string.Empty, $"VPN '{extranetVpnMember.ExtranetVpn.Name}' is not enabled for Extranet.");
            }

            var vpnTenantCommunity = await VpnTenantCommunityInService.GetByIDAsync(extranetVpnTenantCommunityIn.VpnTenantCommunityInID);
            if (!vpnTenantCommunity.TenantCommunity.AllowExtranet)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Community '{vpnTenantCommunity.TenantCommunity.Name}' is not enabled for Extranet.");
            }
        }
    }
}

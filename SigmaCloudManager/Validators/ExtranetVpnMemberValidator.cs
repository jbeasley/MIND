using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models.RequestModels;
using SCM.Services;
using SCM.Models;
using Mind.Services;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Extranet VPN Members
    /// </summary>
    public class ExtranetVpnMemberValidator : BaseValidator, IExtranetVpnMemberValidator
    {
        public ExtranetVpnMemberValidator(IExtranetVpnMemberService extranetVpnMemberService, IVpnService vpnService)
        {
            VpnService = vpnService;
            ExtranetVpnMemberService = extranetVpnMemberService;
        }

        private IExtranetVpnMemberService ExtranetVpnMemberService { get; }
        private IVpnService VpnService { get; }

        /// <summary>
        /// Validate a new Extranet VPN Member
        /// </summary>
        /// <param name="extranetVpnMember"></param>
        public async Task ValidateNewAsync(ExtranetVpnMember extranetVpnMember)
        {
            var extranetVpn = await VpnService.GetByIDAsync(extranetVpnMember.ExtranetVpnID.Value);
            var memberVpn = await VpnService.GetByIDAsync(extranetVpnMember.MemberVpnID);

            if (!extranetVpn.IsExtranet)
            {
                ValidationDictionary.AddError(string.Empty, $"VPN '{extranetVpn.Name}' is not enabled for Extranet.");
            }

            /* The following code can be removed ONLY WHEN the network platform support Multicast Extranet VPN */
            /* Start of logic which prevents Extranet Multicast VPN */

            if (extranetVpn.IsMulticastVpn)
            {
                ValidationDictionary.AddError(string.Empty, "Multicast VPN does not currently support Extranet. This is because the Sigma "
                   + " network platform does not currently support Extrant Multicast VPN.");
            }

            if (memberVpn.IsMulticastVpn)
            {
                ValidationDictionary.AddError(string.Empty, "A Multicast VPN cannot currently be a member of an Extranet VPN. "
                   + "This is because the Sigma network platform does not currently support Extrant Multicast VPN.");
            }

            /* End of logic which prevents Extranet Multicast VPN */

            /* The following logic should be included when Extranet MVPN is supported

           if (extranetVpn.IsMulticastVpn != memberVpn.IsMulticastVpn)
           {
               if (extranetVpn.IsMulticastVpn)
               {
                   ValidationDictionary.AddError(string.Empty, $"VPN '{memberVpn.Name}' must be a Multicast VPN in order to become a member "
                       + $"of Extranet Multicast VPN '{extranetVpn.Name}'");
               }
               else
               {
                   ValidationDictionary.AddError(string.Empty, $"Multicast VPN '{memberVpn.Name}' cannot be added to unicast Extranet "
                       + $"VPN '{extranetVpn.Name}'.");
               }
           }

           if (extranetVpn.IsMulticastVpn)
           {
               if (extranetVpn.MulticastVpnServiceType.MvpnServiceType != memberVpn.MulticastVpnServiceType.MvpnServiceType)
               {
                   ValidationDictionary.AddError(string.Empty, $"The Multicast Service Type of VPN '{memberVpn.Name}' must be the same "
                       + $"as the Extranet VPN '{extranetVpn.Name}'. The Multicast Service Type of VPN '{extranetVpn.Name}' is " 
                       + $"'{extranetVpn.MulticastVpnServiceType.Name}'. The Multicast Service Type of VPN '{memberVpn.Name}' is "
                       + $"'{memberVpn.MulticastVpnServiceType.Name}'.");
               }

               if (extranetVpn.VpnTopologyType.TopologyType == TopologyType.HubandSpoke)
               {
                   if (memberVpn.VpnTopologyType.TopologyType == TopologyType.HubandSpoke)
                   {
                       if (extranetVpn.MulticastVpnDirectionType.Name != memberVpn.MulticastVpnDirectionType.Name)
                       {
                           ValidationDictionary.AddError(string.Empty, $"The Multicast VPN Direction Type of VPN '{memberVpn.Name}' "
                               + $"must be '{extranetVpn.MulticastVpnDirectionType.Name}' because the Multicast VPN Direction Type of "
                               + $"of Extranet VPN '{extranetVpn.Name}' is '{extranetVpn.MulticastVpnDirectionType.Name}'.");

                       }
                   }
               }
           }

           */

            if (memberVpn.IsExtranet)
            {
                ValidationDictionary.AddError(string.Empty, $"An Extranet VPN cannot be a member of another Extranet VPN.");
            }

            if (extranetVpn.PlaneID != null)
            {
                if (memberVpn.PlaneID != extranetVpn.PlaneID)
                {
                    ValidationDictionary.AddError(string.Empty, $"VPN '{memberVpn.Name}' cannot be a member of Extranet VPN '{extranetVpn.Name}' "
                        + $"because the Extranet VPN is planar-scoped to Plane '{extranetVpn.Plane.Name}' but the VPN '{memberVpn.Name}' is not "
                        + "scoped to the same plane.");
                }
            }

            if (extranetVpn.RegionID != null)
            {
                if (memberVpn.RegionID != extranetVpn.RegionID)
                {
                    ValidationDictionary.AddError(string.Empty, $"VPN '{memberVpn.Name}' cannot be a member of Extranet VPN '{extranetVpn.Name}' "
                        + $"because the Extranet VPN is regionally-scoped to Region '{extranetVpn.Region.Name}' but the VPN '{memberVpn.Name}' is not "
                        + "scoped to the same region.");
                }
            }

            if (memberVpn.PlaneID != null)
            {
                if (memberVpn.PlaneID != extranetVpn.PlaneID)
                {
                    ValidationDictionary.AddError(string.Empty, $"VPN '{memberVpn.Name}' cannot be a member of Extranet VPN '{extranetVpn.Name}' "
                        + $"because VPN '{memberVpn.Name}' is planar-scoped to Plane '{memberVpn.Plane.Name}' but the Extranet VPN is not "
                        + "scoped to the same plane.");
                }
            }

            if (memberVpn.RegionID != null)
            {
                if (memberVpn.RegionID != extranetVpn.RegionID)
                {
                    ValidationDictionary.AddError(string.Empty, $"VPN '{memberVpn.Name}' cannot be a member of Extranet VPN '{extranetVpn.Name}' "
                        + $"because VPN '{memberVpn.Name}' is regionally-scoped to Region '{memberVpn.Region.Name}' but the Extranet VPN is not "
                        + "scoped to the same region.");
                }
            }
        }
    }
}

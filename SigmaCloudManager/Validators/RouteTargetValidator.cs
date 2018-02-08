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
    /// Validator for Route Targets
    /// </summary>
    public class RouteTargetValidator : BaseValidator, IRouteTargetValidator
    {
        public RouteTargetValidator(IVpnService vpnService,
            IVpnAttachmentSetService vpnAttachmentSetService,
            IRouteTargetRangeService routeTargetRangeService)
        {
            VpnService = vpnService;
            RouteTargetRangeService = routeTargetRangeService;
            VpnAttachmentSetService = vpnAttachmentSetService;
        }

        private IVpnService VpnService { get; }
        private IRouteTargetRangeService RouteTargetRangeService { get; }
        private IVpnAttachmentSetService VpnAttachmentSetService { get; }

        public async Task ValidateNewAsync(RouteTargetRequest request)
        {
            var rtRange = await RouteTargetRangeService.GetByIDAsync(request.RouteTargetRangeID);
            if (rtRange == null)
            {
                ValidationDictionary.AddError(string.Empty, "The requested Route Target Range was not found.");
                return;
            }

            var vpn = await VpnService.GetByIDAsync(request.VpnID.Value);
            if (vpn.IsNovaVpn)
            {
                if (rtRange.Name != "Default")
                {
                    ValidationDictionary.AddError(string.Empty, "The Default Route Target Range must be selected for standard Nova VPNs.");
                }
            }
            else
            {
                if (rtRange.Name == "Default")
                {
                    ValidationDictionary.AddError(string.Empty, "The Default Route Target Range cannot be selected for non-standard VPNs.");
                }
            }

            if (!request.AutoAllocateAssignedNumberSubField)
            {
                if (request.RequestedAssignedNumberSubField == null)
                {
                    ValidationDictionary.AddError(string.Empty, "The assigned-number sub-field value must be specified, or select the auto-allocate option.");
                }
                else
                {
                    if (!Enumerable.Range(rtRange.AssignedNumberSubFieldStart, rtRange.AssignedNumberSubFieldCount)
                        .Contains(request.RequestedAssignedNumberSubField.Value))
                    {
                        ValidationDictionary.AddError(string.Empty, "The requested assigned number sub-field value is invalid for the specified RT range. "
                            + $"A value between {rtRange.AssignedNumberSubFieldStart} and {rtRange.AssignedNumberSubFieldStart + rtRange.AssignedNumberSubFieldCount -1 } "
                            + "must be specfied.");
                    }
                }
            }
        }

        /// <summary>
        /// Validate existing Route Targets for a VPN
        /// </summary>
        /// <param name="vpn"></param>
        /// <returns></returns>
        public void ValidateExisting(Vpn vpn)
        {
            var protocolType = vpn.VpnTopologyType.VpnProtocolType.ProtocolType;
            var topologyType = vpn.VpnTopologyType.Name;
            var countOfRouteTargets = vpn.RouteTargets.Count();
            var countOfExportRouteTarget = vpn.RouteTargets.Where(r => r.IsHubExport == true).Count();

            if (protocolType == ProtocolType.Ethernet)
            {
                if (countOfExportRouteTarget > 0)
                {
                    ValidationDictionary.AddError(string.Empty,$"There is a problem with '{vpn.Name}'. "
                        + "A hub export route target cannot be defined for Ethernet VPN types.");
                }
            }
            else
            {
                if (topologyType == "Any-to-Any")
                {
                    if (countOfRouteTargets != 1)
                    {
                        ValidationDictionary.AddError(string.Empty, $"There is a problem with '{vpn.Name}'. " 
                            + "Any-to-Any IP VPNs require one route target.");
                    }

                    if (countOfExportRouteTarget > 0)
                    {
                        ValidationDictionary.AddError(string.Empty, $"There is a problem with '{vpn.Name}'. "
                            + "Hub Export cannot be set for Any-to-Any IP VPN types.");
                    }
                }
                else if (topologyType == "Hub-and-Spoke")
                {
                    if (countOfRouteTargets != 2)
                    {
                        ValidationDictionary.AddError(string.Empty, $"There is a problem with '{vpn.Name}'. " 
                            + "Hub-and-Spoke IP VPNs require two route targets, one of which must be an export route target.");
                    }

                    if (countOfExportRouteTarget != 1)
                    {
                        ValidationDictionary.AddError(string.Empty, $"There is a problem with '{vpn.Name}'. " 
                            + "Hub-and-Spoke IP VPNs require one export route target.");
                    }
                }
            }
        }

        /// <summary>
        /// Validates that Route Targets for a VPN can be changed. This is NOT possible
        /// if the VPN has any Attachment Sets bound to it.
        /// </summary>
        /// <param name="vpn"></param>
        /// <returns></returns>
        public async Task ValidateRouteTargetsChangeableAsync(Vpn vpn)
        {
            var vpnAttachmentSets = await VpnAttachmentSetService.GetAllByVpnIDAsync(vpn.VpnID);

            if (vpnAttachmentSets.Count() > 0)
            {
                vpnAttachmentSets.ToList().ForEach(a => ValidationDictionary.AddError(string.Empty, "Attachment Set "
                    + $"'{a.AttachmentSet.Name}' is bound to VPN '{vpn.Name}'."));
            }
        }
    }
}

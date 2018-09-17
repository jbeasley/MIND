using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;
using SCM.Data;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for tenant IP network associations with attachment sets
    /// </summary>
    public class VpnTenantIpNetworkStaticRouteRoutingInstanceValidator : BaseValidator, IVpnTenantIpNetworkStaticRouteRoutingInstanceValidator
    {
        public VpnTenantIpNetworkStaticRouteRoutingInstanceValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Validates a new tenant IP network association with an attachment set as a static route.
        /// </summary>
        /// <param name="vpnTenantIpNetworkStaticRouteRoutingInstance"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VpnTenantIpNetworkRoutingInstanceStaticRoute vpnTenantIpNetworkStaticRouteRoutingInstance)
        {
            var tenantIpNetwork = await _unitOfWork.TenantIpNetworkRepository.GetByIDAsync(vpnTenantIpNetworkStaticRouteRoutingInstance.TenantIpNetworkID);
            (from result in await _unitOfWork.VpnAttachmentSetRepository.GetAsync(q =>
                                q.AttachmentSetID == vpnTenantIpNetworkStaticRouteRoutingInstance.AttachmentSetID && q.Vpn.IsExtranet)
                                select result.Vpn)
                                .ToList()
                                .ForEach(
                                   x => ValidationDictionary.AddError(string.Empty, $"Tenant IP network '{tenantIpNetwork.CidrName}' " +
                                    "cannot be added to the attachment set because the attachment set is associated with VPN {x.Name} which " +
                                    "is an extranet VPN, and the tenant IP network is not enabled for extranet. Update the tenant IP network " +
                                    "resource to enable it for extranet services."));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators

{
    /// <summary>
    /// Validator for Tenant Community Sets
    /// </summary>
    public class TenantCommunitySetValidator : BaseValidator, ITenantCommunitySetValidator
    {
        public TenantCommunitySetValidator(ITenantCommunitySetService tenantCommunitySetService, 
            IVpnTenantCommunityRoutingInstanceService vpnTenantCommunityRoutingInstanceService)
        {
            TenantCommunitySetService = tenantCommunitySetService;
            VpnTenantCommunityRoutingInstanceService = vpnTenantCommunityRoutingInstanceService;
        }

        private ITenantCommunitySetService TenantCommunitySetService { get; }
        private IVpnTenantCommunityRoutingInstanceService VpnTenantCommunityRoutingInstanceService { get; }

        public async Task ValidateNewAsync(TenantCommunitySet tenantCommunitySet)
        {
            var existsTenantCommunitySet = await TenantCommunitySetService.GetByNameAsync(tenantCommunitySet.Name);
            if (existsTenantCommunitySet != null)
            {
                ValidationDictionary.AddError(string.Empty, $"Community Set {tenantCommunitySet.Name} "
                    + $"already exists for Tenant {existsTenantCommunitySet.Tenant.Name}.");
            }
        }

        public async Task ValidateChangesAsync(TenantCommunitySet tenantCommunitySet)
        {
            var existsTenantCommunitySet = await TenantCommunitySetService.GetByNameAsync(tenantCommunitySet.Name);
            if (existsTenantCommunitySet != null)
            {
                if (existsTenantCommunitySet.TenantCommunitySetID != tenantCommunitySet.TenantCommunitySetID)
                {
                    ValidationDictionary.AddError(string.Empty, $"Community Set {tenantCommunitySet.Name} "
                        + $"already exists for Tenant {existsTenantCommunitySet.Tenant.Name}.");
                }
            }
        }

        /// <summary>
        /// Validate deletion of a Tenant Community Set. The Tenant Community Set cannot be deleted if 
        /// the community set is used within a VRF policy.
        /// </summary>
        /// <param name="tenantCommunitySet"></param>
        public async Task ValidateDeleteAsync(TenantCommunitySet tenantCommunitySet)
        {
            var vpnTenantCommunitiesRoutingInstance = await VpnTenantCommunityRoutingInstanceService.GetAllByTenantCommunitySetIDAsync(tenantCommunitySet.TenantCommunitySetID);
            foreach (var vpnTenantCommunityRoutingInstance in vpnTenantCommunitiesRoutingInstance)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Community Set '{tenantCommunitySet.Name}' "
                    + $"cannot be deleted because it is bound to Attachment Set '{vpnTenantCommunityRoutingInstance.AttachmentSet.Name}'.");
            }
        }
    }
}

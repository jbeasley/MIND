using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators

{
    /// <summary>
    /// Validator for Tenants
    /// </summary>
    public class TenantValidator : BaseValidator, ITenantValidator
    {
        public TenantValidator(ITenantService tenantService, ITenantAttachmentService tenantAttachmentService, IVpnService vpnService)
        {
            TenantService = tenantService;
            TenantAttachmentService = tenantAttachmentService;
            VpnService = vpnService;
        }

        private ITenantService TenantService { get; set; }
        private ITenantAttachmentService TenantAttachmentService { get; set; }
        private IVpnService VpnService { get; set; }

        /// <summary>
        /// Validate deletion of a Tenant. The Tenant cannot be deleted if either Attachment 
        /// or VPNs are allocated to the Tenant.
        /// </summary>
        /// <param name="tenant"></param>
        public async Task ValidateDeleteAsync(Tenant tenant)
        {
            var attachments = await TenantAttachmentService.GetAllByTenantIDAsync(tenant.TenantID);
            if (attachments.Count() > 0)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant '{tenant.Name}' cannot be deleted because Attachments are allocated.");
            }

            var vpns = await VpnService.GetAllByTenantIDAsync(tenant.TenantID);
            if (vpns.Count() > 0)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant '{tenant.Name}' cannot be deleted because VPNs are allocated.");
            }
        }
    }
}

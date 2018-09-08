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
    /// Validator for Tenants
    /// </summary>
    public class TenantValidator : BaseValidator, ITenantValidator
    {
        private readonly ITenantAttachmentService _tenantAttachmentService;
        private readonly IVpnService _vpnService;

        public TenantValidator(ITenantAttachmentService tenantAttachmentService, IVpnService vpnService)
        {
            _tenantAttachmentService = tenantAttachmentService;
            _vpnService = vpnService;
        }

        /// <summary>
        /// Validate deletion of a Tenant. The Tenant cannot be deleted if either Attachment 
        /// or VPNs are allocated to the Tenant.
        /// </summary>
        /// <param name="tenantId"></param>
        public async Task ValidateDeleteAsync(int tenantId)
        {
            var attachments = await _tenantAttachmentService.GetAllByTenantIDAsync(tenantId, includeProperties: false);
            if (attachments.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"The tenant cannot be deleted because Attachments are allocated.");
            }

            var vpns = await _vpnService.GetAllByTenantIDAsync(tenantId);
            if (vpns.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant cannot be deleted because VPNs are allocated.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Api.Models;
using AutoMapper;
using SCM.Services;
using SCM.Validators;

namespace SCM.Api.Validators
{
    public class VpnTenantNetworkInApiValidator : BaseApiValidator, IVpnTenantNetworkInApiValidator
    {
        public VpnTenantNetworkInApiValidator(ITenantNetworkService tenantNetworkService,
            IVpnService vpnService,
            IAttachmentSetService attachmentSetService,
            IMapper mapper)
        {
            TenantNetworkService = tenantNetworkService;
            VpnService = vpnService;
            AttachmentSetService = attachmentSetService;
            Mapper = mapper;
        }

        private ITenantNetworkService TenantNetworkService { get; set; }
        private ITenantCommunityService TenantCommunityService { get; set; }
        private IVpnService VpnService { get; set; }
        private IAttachmentSetService AttachmentSetService { get; set; }
        private IMapper Mapper { get; set; }

        /// <summary>
        /// Validate a new VpnTenantNetworkIn API request
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VpnTenantNetworkInRequestApiModel apiRequest)
        {

            var tenantNetwork = await TenantNetworkService.GetByIDAsync(apiRequest.TenantNetworkID.Value);
            if (tenantNetwork == null)
            {
                ValidationDictionary.AddError("TenantNetworkID", $"Tenant Network with ID "
                    + $"'{apiRequest.TenantNetworkID}' was not found.");
            }
        }
    }
}

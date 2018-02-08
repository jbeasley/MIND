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
    public class VpnAttachmentSetApiValidator : BaseApiValidator, IVpnAttachmentSetApiValidator
    {
        public VpnAttachmentSetApiValidator( 
            ITenantNetworkService tenantNetworkService,
            ITenantCommunityService tenantCommunityService,
            IVpnService vpnService,
            IAttachmentSetService attachmentSetService,
            IVpnAttachmentSetValidator vpnAttachmentSetValidator,
            IMapper mapper)
        {
            TenantNetworkService = tenantNetworkService;
            TenantCommunityService = tenantCommunityService;
            VpnService = vpnService;
            AttachmentSetService = attachmentSetService;
            VpnAttachmentSetValidator = vpnAttachmentSetValidator;
            Mapper = mapper;
        }

        private ITenantNetworkService TenantNetworkService { get; set; }
        private ITenantCommunityService TenantCommunityService { get; set; }
        private IVpnService VpnService { get; set; }
        private IAttachmentSetService AttachmentSetService { get; set; }
        private IVpnAttachmentSetValidator VpnAttachmentSetValidator { get; set; }
        private IMapper Mapper { get; set; }

        public new IValidationDictionary ValidationDictionary
        {
            get
            {
                return base.ValidationDictionary;
            }
            set
            {
                base.ValidationDictionary = value;
                VpnAttachmentSetValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validate a new VpnAttachmentSet API request
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VpnAttachmentSetRequestApiModel apiRequest)
        {
            var vpn = await VpnService.GetByIDAsync(apiRequest.VpnID);
            if (vpn == null)
            {
                ValidationDictionary.AddError("VpnID", "The VPN was not found.");
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(apiRequest.AttachmentSetID.Value);
            if (attachmentSet == null)
            {
                ValidationDictionary.AddError("AttachmentSetID", "The Attachment Set was not found.");
            }

            if (ValidationDictionary.IsValid)
            {
                // Hand-off to VPN Attachment Set Validator

                await VpnAttachmentSetValidator.ValidateNewAsync(Mapper.Map<VpnAttachmentSet>(apiRequest));
            }
        }
    }
}

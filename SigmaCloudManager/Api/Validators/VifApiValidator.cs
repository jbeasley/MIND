using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SCM.Api.Models;
using SCM.Services;
using SCM.Models.RequestModels;
using SCM.Validators;
using SCM.Models;

namespace SCM.Api.Validators
{
    /// <summary>
    /// Validator for API requests for Vifs.
    /// </summary>
    public class VifApiValidator : BaseApiValidator, IVifApiValidator
    {
        public VifApiValidator(
            ITenantService tenantService,
            IContractBandwidthPoolService contractBandwidthPoolService,
            IContractBandwidthService contractBandwidthService,
            IAttachmentService attachmentService,
            IVifValidator vifValidator,
            IMapper mapper)
        {
            TenantService = tenantService;
            ContractBandwidthService = contractBandwidthService;
            ContractBandwidthPoolService = contractBandwidthPoolService;
            AttachmentService = attachmentService;
            VifValidator = vifValidator;
            Mapper = mapper;
        }

        private IAttachmentService AttachmentService { get; set; }
        private ITenantService TenantService { get; set; }
        private IContractBandwidthService ContractBandwidthService { get; set; }
        private IContractBandwidthPoolService ContractBandwidthPoolService { get; set; }
        private IVifValidator VifValidator { get; set; }
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
                VifValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validate a new Vif request
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VifRequestApiModel apiRequest)
        {
            var tenant = await TenantService.GetByIDAsync(apiRequest.TenantID.Value);
            if (tenant == null)
            {
                ValidationDictionary.AddError("TenantID", "The Tenant was not found.");
            }

            var attachment = await AttachmentService.GetByIDAsync(apiRequest.AttachmentID.Value);
            if (attachment == null)
            {
                ValidationDictionary.AddError("AttachmentID", "The Attachment was not found.");
            }

            if (apiRequest.ContractBandwidthID != null)
            {
                var contractBandwidth = await ContractBandwidthService.GetByIDAsync(apiRequest.ContractBandwidthID.Value);
                if (contractBandwidth == null)
                {
                    ValidationDictionary.AddError("ContractBandwidthID", "The requested Contract Bandwidth is not valid.");
                }
            }

            if (apiRequest.ContractBandwidthPoolID != null)
            {
                var contractBandwidthPool = await ContractBandwidthPoolService.GetByIDAsync(apiRequest.ContractBandwidthPoolID.Value);
                if (contractBandwidthPool == null)
                {
                    ValidationDictionary.AddError("ContractBandwidthPoolID", "The requested Contract Bandwidth Pool is not valid.");
                }
            }

            if (ValidationDictionary.IsValid)
            {
                // Hand-off to Vif Validator

                await VifValidator.ValidateNewAsync(Mapper.Map<VifRequest>(apiRequest));
            }
        }

        /// <summary>
        /// Validates a Vif can be deleted.
        /// </summary>
        /// <param name="vif"></param>
        /// <returns></returns>
        public void ValidateDelete(Vif vif)
        {
            VifValidator.ValidateDelete(vif);
        }
    }
}

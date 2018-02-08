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
    public class AttachmentSetApiValidator : BaseApiValidator, IAttachmentSetApiValidator
    {
        public AttachmentSetApiValidator(
            IAttachmentSetService attachmentSetService,
            IRegionService regionService,
            ISubRegionService subRegionService,
            ITenantService tenantService,
            IAttachmentSetValidator attachmentSetValidator,
            IAttachmentSetRoutingInstanceValidator attachmentSetRoutingInstanceValidator,
            IAttachmentRedundancyService attachmentRedundancyService,
            IMapper mapper)
        {
            AttachmentSetService = attachmentSetService;
            RegionService = regionService;
            SubRegionService = subRegionService;
            TenantService = tenantService;
            AttachmentRedundancyService = attachmentRedundancyService;
            Mapper = mapper;

            AttachmentSetValidator = attachmentSetValidator;
            AttachmentSetRoutingInstanceValidator = attachmentSetRoutingInstanceValidator;
    
        }

        private IAttachmentSetService AttachmentSetService { get; set; }
        private IRegionService RegionService { get; set; }
        private ISubRegionService SubRegionService { get; set; }
        private ITenantService TenantService { get; set; }
        private IAttachmentRedundancyService AttachmentRedundancyService { get; set; }
        private IAttachmentSetValidator AttachmentSetValidator { get; set; }
        private IAttachmentSetRoutingInstanceValidator AttachmentSetRoutingInstanceValidator { get; set; }
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
                AttachmentSetValidator.ValidationDictionary = value;
                AttachmentSetRoutingInstanceValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validates an Attachment Set. This method is a wrapper around the ValidateRoutingInstancesConfiguredCorrectly
        /// method of the AttachmentSetRoutingInstance validator.
        /// </summary>
        /// <param name="attachmentSet"></param>
        /// <returns></returns>
        public async Task ValidateAsync(AttachmentSet attachmentSet)
        {
            await AttachmentSetRoutingInstanceValidator.ValidateRoutingInstancesConfiguredCorrectlyAsync(attachmentSet);
        }

        /// <summary>
        /// Validate an Attachment Set request
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(AttachmentSetRequestApiModel apiRequest)
        {
            var tenant = await TenantService.GetByIDAsync(apiRequest.TenantID.Value);
            if (tenant == null)
            {
                ValidationDictionary.AddError("TenantID", "The Tenant was not found.");
            }

            var region = await RegionService.GetByIDAsync(apiRequest.RegionID.Value);
            if (region == null)
            {
                ValidationDictionary.AddError("RegionID", "The Region was not found.");
            }

            if (apiRequest.SubRegionID != null)
            {
                var subRegion = await SubRegionService.GetByIDAsync(apiRequest.SubRegionID.Value);
                if (subRegion == null)
                {
                    ValidationDictionary.AddError("SubRegionID", "The Sub-Region was not found.");
                }
            }

            var attachmentRedundancy = await AttachmentRedundancyService.GetByIDAsync(apiRequest.AttachmentRedundancyID.Value);
            if (attachmentRedundancy == null)
            {
                ValidationDictionary.AddError("AttachmentRedundancyID", "The Attachment Redundancy was not found.");
            }

            if (ValidationDictionary.IsValid)
            {
                // Validate the Attachment Set

                await AttachmentSetValidator.ValidateNewAsync(Mapper.Map<AttachmentSet>(apiRequest));
            }
        }

        /// <summary>
        /// Validate changes to an Attachment Set
        /// </summary>
        /// <param name="apiModel"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(AttachmentSet attachmentSet)
        {
            // Validate updatable fields

            var attachmentRedundancy = await AttachmentRedundancyService.GetByIDAsync(attachmentSet.AttachmentRedundancyID);
            if (attachmentRedundancy == null)
            {
                ValidationDictionary.AddError("AttachmentRedundancyID", "The Attachment Redundancy was not found.");
            }

            if (attachmentSet.SubRegionID != null)
            {
                var subRegion = await SubRegionService.GetByIDAsync(attachmentSet.SubRegionID.Value);
                if (subRegion == null)
                {
                    ValidationDictionary.AddError("SubRegionID", "The Sub-Region was not found.");
                }
            }

            if (ValidationDictionary.IsValid)
            {
                // Hand-off to Attachment Set Validator

                await AttachmentSetValidator.ValidateChangesAsync(attachmentSet);
            }
        }

        /// <summary>
        /// Validate an Attachment Set can be deleted.
        /// </summary>
        /// <param name="attachmentSet"></param>
        /// <returns></returns>
        public async Task ValidateDeleteAsync(AttachmentSet attachmentSet)
        {
            await AttachmentSetValidator.ValidateDeleteAsync(attachmentSet);
        }
    }
}

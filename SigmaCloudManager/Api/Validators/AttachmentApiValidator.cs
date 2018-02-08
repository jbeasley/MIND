using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Api.Models;
using SCM.Validators;
using SCM.Services;
using AutoMapper;
using SCM.Models;
using SCM.Models.RequestModels;

namespace SCM.Api.Validators
{
    /// <summary>
    /// Validates API requests for Attachments.
    /// </summary>
    public class AttachmentApiValidator: BaseApiValidator, IAttachmentApiValidator
    {
        public AttachmentApiValidator(
            IAttachmentService attachmentService,
            ITenantService tenantService,
            ILocationService locationService,
            IAttachmentBandwidthService attachmentBandwidthService,
            IPlaneService planeService,
            IContractBandwidthService contractBandwidthService,
            IAttachmentValidator attachmentValidator,
            IMapper mapper)
        {
            AttachmentService = attachmentService;
            TenantService = tenantService;
            LocationService = locationService;
            AttachmentBandwidthService = attachmentBandwidthService;
            ContractBandwidthService = contractBandwidthService;
            PlaneService = planeService;
            AttachmentValidator = attachmentValidator;
            Mapper = mapper;
        }

        private IAttachmentService AttachmentService { get; set; }
        private IPlaneService PlaneService { get; set; }
        private ILocationService LocationService { get; set; }
        private ITenantService TenantService { get; set; }
        private IAttachmentBandwidthService AttachmentBandwidthService { get; set; }
        private IContractBandwidthService ContractBandwidthService { get; set; }
        private IAttachmentValidator AttachmentValidator { get; set; }
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
                AttachmentValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validate an request for a new Attachment
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(AttachmentRequestApiModel apiRequest)
        {
            var tenant = await TenantService.GetByIDAsync(apiRequest.TenantID);
            if (tenant == null)
            {
                ValidationDictionary.AddError("TenantID", "The Tenant was not found.");
            }

            var location = await LocationService.GetByIDAsync(apiRequest.LocationID);
            if (location == null)
            {
                ValidationDictionary.AddError("LocationID", "The Location was not found.");
            }

            var attachmentBandwidth = await AttachmentBandwidthService.GetByIDAsync(apiRequest.BandwidthID.Value);
            if (attachmentBandwidth == null)
            {
               ValidationDictionary.AddError("BandwidthID", "The Attachment Bandwidth was not found.");
            }

            if (apiRequest.ContractBandwidthID != null)
            {
                var contractBandwidth = await ContractBandwidthService.GetByIDAsync(apiRequest.ContractBandwidthID.Value);
                if (contractBandwidth == null)
                {
                    ValidationDictionary.AddError("ContractBandwidthID","The Contract Bandwidth was not found.");
                }
            }

            if (apiRequest.PlaneID != null)
            {
                var plane = await PlaneService.GetByIDAsync(apiRequest.PlaneID.Value);
                if (plane == null)
                {
                    ValidationDictionary.AddError("PlaneID","The requested Plane is not valid.");
                }
            }

            if (this.ValidationDictionary.IsValid)
            {
                // Hand-off to Attachment Validator

                await AttachmentValidator.ValidateNewAsync(Mapper.Map<AttachmentRequest>(apiRequest));
            }
        }

        /// <summary>
        /// Validates an Attachment can be deleted.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void ValidateDelete(Attachment attachment)
        {
            AttachmentValidator.ValidateDelete(attachment);
        }
    }
}

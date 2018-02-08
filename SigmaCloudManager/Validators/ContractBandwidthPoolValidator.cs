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
    /// Validator for Contract Bandwidth Pools
    /// </summary>
    public class ContractBandwidthPoolValidator : BaseValidator, IContractBandwidthPoolValidator
    {
        public ContractBandwidthPoolValidator(IContractBandwidthService contractBandwidthService,
            IVifService vifService,
            IContractBandwidthPoolService contractBandwidthPoolService,
            IAttachmentService attachmentService)
        {
            ContractBandwidthService = contractBandwidthService;
            ContractBandwidthPoolService = contractBandwidthPoolService;
            VifService = vifService;
            AttachmentService = attachmentService;
        }

        private IContractBandwidthService ContractBandwidthService { get; set; }
        private IVifService VifService { get; set; }
        private IAttachmentService AttachmentService { get; set; }
        private IContractBandwidthPoolService ContractBandwidthPoolService { get; set; }

        /// <summary>
        /// Validate a request for a new Contract Bandwidth Pool to be associated
        /// with an Attachment.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(AttachmentRequest request)
        {
            var contractBandwidth = await ContractBandwidthService.GetByIDAsync(request.ContractBandwidthID.Value);
            if (contractBandwidth.BandwidthMbps > request.Bandwidth.BandwidthGbps * 1000)
            {
                ValidationDictionary.AddError("ContractBandwidthID", "The requested Contract Bandwidth of "
                    + $"{contractBandwidth.BandwidthMbps} Mbps exceeds the "
                    + $"Attachment Bandwidth of {request.Bandwidth.BandwidthGbps} Gbps. "
                    + "Select a lower contract bandwidth or request a higher Attachment Bandwidth.");
            }
        }

        /// <summary>
        /// Validate a request for a Contract Bandwidth Pool for a new VIF
        /// </summary>
        /// <param name="vifRequest"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VifRequest vifRequest)
        {
            await ValidationHelperAsync(vifRequest.AttachmentID, vifRequest.TenantID, vifRequest.ContractBandwidthPoolID, vifRequest.ContractBandwidthID);
        }

        /// <summary>
        /// Validate updates to a Contract Bandwidth Pool associated with an existing Attachment.
        /// </summary>
        /// <param name="attachmentUpdate"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(AttachmentUpdate attachmentUpdate)
        {
            // Get arguments needed to check for sufficient bandwidth

            var attachment = await AttachmentService.GetByIDAsync(attachmentUpdate.AttachmentID);

            if (!attachment.IsTagged)
            {
                var attachmentBandwidth = attachment.AttachmentBandwidth;

                var contractBandwidth = await ContractBandwidthService.GetByIDAsync(attachmentUpdate.ContractBandwidthID.Value);
                if (contractBandwidth.BandwidthMbps > attachmentBandwidth.BandwidthGbps * 1000)
                {
                    ValidationDictionary.AddError("ContractBandwidthID", "The requested Contract Bandwidth of "
                        + $"{contractBandwidth.BandwidthMbps} Mbps exceeds the "
                        + $"Attachment Bandwidth of {attachmentBandwidth.BandwidthGbps} Gbps. "
                        + "Select a lower contract bandwidth.");
                }
            }
        }

        /// <summary>
        /// Validate updates to a Contract Bandwidth Pool associated with an existing VIF.
        /// </summary>
        /// <param name="vifUpdate"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(VifUpdate vifUpdate)
        {
            await ValidationHelperAsync(vifUpdate.AttachmentID, vifUpdate.TenantID, vifUpdate.ContractBandwidthPoolID, vifUpdate.ContractBandwidthID);
        }

        /// <summary>
        /// Helper to validate a Contract Bandwidth Pool to be associated with 
        /// a VIF.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task ValidationHelperAsync(int attachmentID, int? tenantID, int? contractBandwidthPoolID, int? contractBandwidthID)
        {
            var vifs = await VifService.GetAllByAttachmentIDAsync(attachmentID);

            if (contractBandwidthPoolID != null)
            {
                // Request is to share an existing Contract Bandwidth Pool

                if (vifs.Where(q => q.ContractBandwidthPoolID == contractBandwidthPoolID).Count() == 0)
                {
                    // The request is invalid - a request to share a Contract Bandwidth Pool must be for Vifs
                    // associated with a common Attachment

                    ValidationDictionary.AddError("ContractBandwidthPoolID", "The selected Contract Bandwidth Pool is invalid "
                        + "because it is not associated with any Vif of the current Attachment.");
                }

                var contractBandwidthPool = await ContractBandwidthPoolService.GetByIDAsync(contractBandwidthPoolID.Value);
                if (contractBandwidthPool.TenantID != tenantID)
                {
                    // The request is invalid - a request to share a Contract Bandwidth Pool must be for vifs
                    // associated with a common Tenant

                    ValidationDictionary.AddError("ContractBandwidthPoolID", "The selected Contract Bandwidth Pool is invalid "
                        + "because it does not belong to the Tenant specified in the request.");
                }
            }

            // Get arguments needed to check for sufficient bandwidth

            var attachment = await AttachmentService.GetByIDAsync(attachmentID);
            var attachmentBandwidth = attachment.AttachmentBandwidth;

            // Calculate aggregate bandwidth used from distinct Contract Bandwidth Pool assignments

            var aggregateContractBandwidthMbps = vifs.GroupBy(q => q.ContractBandwidthPoolID)
                .Select(group => group.First())
                .Sum(q => q.ContractBandwidthPool.ContractBandwidth.BandwidthMbps);


            // If a Contract Bandwidth is specified then the required bandwidth must be added to the current aggregate bandwidth.
            // If a Contract Bandwidth Pool is specified then the required bandwidth is already accounted for since the bandwidth
            // requested is to be shared with other vifs

            int requestedBandwidthMbps = 0;
            if (contractBandwidthID != null)
            {
                var contractBandwidth = await ContractBandwidthService.GetByIDAsync(contractBandwidthID.Value);
                requestedBandwidthMbps = contractBandwidth.BandwidthMbps;
            }

            // Check sufficient bandwidth is available

            if ((aggregateContractBandwidthMbps + requestedBandwidthMbps) > attachmentBandwidth.BandwidthGbps * 1000)
            {
                ValidationDictionary.AddError("ContractBandwidthID", "The requested Contract Bandwidth exceeds the "
                    + "remaining bandwidth of the attachment. "
                    + $"Remaining bandwidth : {(attachmentBandwidth.BandwidthGbps * 1000) - aggregateContractBandwidthMbps} Mbps."
                    + $"Requested bandwidth : {requestedBandwidthMbps} Mbps.");
            }
        }
    }
}

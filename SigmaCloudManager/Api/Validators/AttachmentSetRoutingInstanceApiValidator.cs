using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Api.Models;
using AutoMapper;
using SCM.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SCM.Data;
using SCM.Validators;

namespace SCM.Api.Validators
{
    public class AttachmentSetRoutingInstanceApiValidator : BaseApiValidator, IAttachmentSetRoutingInstanceApiValidator
    {
        public AttachmentSetRoutingInstanceApiValidator(IRoutingInstanceService vrfService,
            IAttachmentSetService attachmentSetService,
            IAttachmentSetRoutingInstanceValidator attachmentSetRoutingInstanceValidator,
            IMapper mapper)
        {
            RoutingInstanceService = vrfService;
            AttachmentSetService = attachmentSetService;
            AttachmentSetRoutingInstanceValidator = attachmentSetRoutingInstanceValidator;
            Mapper = mapper;
        }

        private IRoutingInstanceService RoutingInstanceService { get; set; }
        private IAttachmentSetService AttachmentSetService { get; set; }
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
                AttachmentSetRoutingInstanceValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Create a new AttachmentSetRoutingInstance from an API model
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(AttachmentSetRoutingInstanceRequestApiModel apiRequest)
        {
            var attachmentSet = await AttachmentSetService.GetByIDAsync(apiRequest.AttachmentSetID);
            if (attachmentSet == null)
            {
                ValidationDictionary.AddError("AttachmentSetID", "The Attachment Set was not found.");
            }

            var vrf = await RoutingInstanceService.GetByIDAsync(apiRequest.RoutingInstanceID.Value);
            if (vrf == null)
            {
                ValidationDictionary.AddError("RoutingInstanceID", "The VRF was not found.");
            }

            if (ValidationDictionary.IsValid)
            {
                // Hand-off to Attachment Set VRF Validator

                await AttachmentSetRoutingInstanceValidator.ValidateNewAsync(Mapper.Map<AttachmentSetRoutingInstance>(apiRequest));
            }
        }
    }
}

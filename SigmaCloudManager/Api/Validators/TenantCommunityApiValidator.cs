using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SCM.Models;
using SCM.Api.Models;
using SCM.Services;
using SCM.Validators;

namespace SCM.Api.Validators
{
    /// <summary>
    ///  API validator for Tenant Communities
    /// </summary>
    public class TenantCommunityApiValidator : BaseApiValidator, ITenantCommunityApiValidator
    {
        public TenantCommunityApiValidator(ITenantService tenantService,
            ITenantCommunityValidator tenantCommunityValidator,
            IMapper mapper)
        {
            TenantCommunityValidator = tenantCommunityValidator;
            TenantService = tenantService;
            Mapper = mapper;
        }

        private ITenantService TenantService { get; }
        private ITenantCommunityValidator TenantCommunityValidator { get; }
        private IMapper Mapper { get; }

        public new IValidationDictionary ValidationDictionary
        {
            get
            {
                return base.ValidationDictionary;
            }
            set
            {
                base.ValidationDictionary = value;
                TenantCommunityValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validate a new Tenant Community.
        /// </summary>
        /// <param name="tenantCommunity"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(TenantCommunityRequestApiModel apiRequest)
        {
            var tenant = await TenantService.GetByIDAsync(apiRequest.TenantID.Value);
            if (tenant == null)
            {
                ValidationDictionary.AddError(String.Empty, "The Tenant was not found.");
            }

            await TenantCommunityValidator.ValidateNewAsync(Mapper.Map<TenantCommunity>(apiRequest));
        }

        /// <summary>
        /// Validate a request to delete a Tenant Community
        /// </summary>
        /// <param name="tenantCommunity"></param>
        /// <returns></returns>
        public async Task ValidateDeleteAsync(TenantCommunity tenantCommunity)
        {
            // Hand-off to Tenant Community Validator to process request

            await TenantCommunityValidator.ValidateDeleteAsync(tenantCommunity);
        }
    }
}

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
    ///  API validator for Tenant Networks
    /// </summary>
    public class TenantNetworkApiValidator : BaseApiValidator, ITenantNetworkApiValidator
    {
        public TenantNetworkApiValidator(ITenantService tenantService,
            ITenantNetworkValidator tenantNetworkValidator, 
            IMapper mapper)
        {
            TenantNetworkValidator = tenantNetworkValidator;
            TenantService = tenantService;
            Mapper = mapper;
        }

        private ITenantService TenantService { get; }
        private ITenantNetworkValidator TenantNetworkValidator { get; }
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
                TenantNetworkValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validate a new Tenant Network.
        /// </summary>
        /// <param name="tenantNetwork"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(TenantNetworkRequestApiModel apiRequest)
        {
            var tenant = await TenantService.GetByIDAsync(apiRequest.TenantID.Value);
            if (tenant == null)
            {
                ValidationDictionary.AddError(String.Empty, "The Tenant was not found.");
            }

            // Handoff to the Tenant Network Validator for further processing.

            await TenantNetworkValidator.ValidateNewAsync(Mapper.Map<TenantNetwork>(apiRequest));
        }

        /// <summary>
        /// Validate a request to delete a Tenant Network
        /// </summary>
        /// <param name="tenantNetwork"></param>
        /// <returns></returns>
        public async Task ValidateDeleteAsync(TenantNetwork tenantNetwork)
        {
            // Hand-off to Tenant Network Validator to process request

            await TenantNetworkValidator.ValidateDeleteAsync(tenantNetwork);
        }
    }
}

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
    /// <summary>
    ///  API validator for Tenants
    /// </summary>
    public class TenantApiValidator : BaseApiValidator, ITenantApiValidator
    {
        public TenantApiValidator(
            ITenantValidator tenantValidator)
        {
            TenantValidator = tenantValidator;
        }

        private ITenantValidator TenantValidator { get; set; }

        public new IValidationDictionary ValidationDictionary
        {
            get
            {
                return base.ValidationDictionary;
            }
            set
            {
                base.ValidationDictionary = value;
                TenantValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validate a request to delete a Tenant Network
        /// </summary>
        /// <param name="tenantApiModel"></param>
        /// <returns></returns>
        public async Task ValidateDeleteAsync(Tenant tenant)
        {
            // Hand-off to Tenant Validator to process request

            await TenantValidator.ValidateDeleteAsync(tenant);
        }
    }
}

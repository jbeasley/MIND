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
    public class VpnTenantCommunityInApiValidator : BaseApiValidator, IVpnTenantCommunityInApiValidator
    {
        public VpnTenantCommunityInApiValidator(ITenantCommunityService tenantCommunityService,
            IMapper mapper)
        {
            TenantCommunityService = tenantCommunityService;
            Mapper = mapper;
        }

        private ITenantCommunityService TenantCommunityService { get; set; }
        private IMapper Mapper { get; set; }

        /// <summary>
        /// Validate a new VpnTenantCommunityIn API request
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VpnTenantCommunityInRequestApiModel apiRequest)
        {

            var tenantCommunity = await TenantCommunityService.GetByIDAsync(apiRequest.TenantCommunityID.Value);
            if (tenantCommunity == null)
            {
                ValidationDictionary.AddError("TenantCommunityID", $"Tenant Community with ID "
                    + $"'{apiRequest.TenantCommunityID}' was not found.");
            }
        }
    }
}

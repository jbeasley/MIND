using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using AutoMapper;
using Mind.Api.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using Mind.Api.Models;
using System.ComponentModel.DataAnnotations;
using Mind.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Mind.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Mind.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Tenant Domain Community Outbound Policy")]
    public class TenantDomainCommunityOutboundPolicyApiController : BaseApiController
    {
        private readonly ITenantDomainCommunityOutboundPolicyService _tenantDomainCommunityOutboundPolicyService;

        /// <summary>
        /// API for creating and managing the lifecycle of tenant domaincommunity outbound policy. 
        /// </summary>
        /// <param name="tenantDomainCommunityOutboundPolicyService"></param>
        /// <param name="mapper"></param>
        public TenantDomainCommunityOutboundPolicyApiController(ITenantDomainCommunityOutboundPolicyService tenantDomainCommunityOutboundPolicyService, IMapper mapper) : 
            base(tenantDomainCommunityOutboundPolicyService, mapper)
        {
            _tenantDomainCommunityOutboundPolicyService = tenantDomainCommunityOutboundPolicyService;
        }

        /// <summary>
        /// Add a tenant community to the outbound policy of a given tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="body">request object that generates a new outbound policy entry for a tenant community</param>
        /// <response code="201">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/outbound-policy/communities/")]
        [ValidateModelState]
        [ValidateTenantDomainDeviceExists]
        [SwaggerOperation("AddTenantDomainCommunityOutboundPolicy")]
        [SwaggerResponse(statusCode: 201, type: typeof(TenantDomainCommunityOutboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> AddTenantDomainCommunityOutboundPolicy([FromRoute][Required]int? deviceId, 
            [FromBody]TenantDomainCommunityOutboundPolicyRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.TenantDomainCommunityOutboundPolicyRequest>(body);
                var vpnTenantCommunityOut = await _tenantDomainCommunityOutboundPolicyService.AddAsync(deviceId.Value, request);
                var tenantDomainCommunityOutboundPolicyApiModel = Mapper.Map<Mind.Api.Models.TenantDomainCommunityOutboundPolicy>(vpnTenantCommunityOut);
                return CreatedAtRoute("GetTenantDomainCommunityOutboundPolicy", new
                {
                    deviceId,
                    vpnTenantCommunityOutId = vpnTenantCommunityOut.VpnTenantCommunityOutID
                }, tenantDomainCommunityOutboundPolicyApiModel);
            }

            catch (BuilderBadArgumentsException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (BuilderUnableToCompleteException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (IllegalStateException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Update a tenant community which is associated with the outbound policy of a tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="vpnTenantCommunityOutId">ID of the vpn tenant community to update</param>
        /// <param name="body">IP network request object that applies updates to an existingcommunity associated 
        /// with the outbound policy of the attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/outbound-policy/communities/{vpnTenantCommunityOutId}")]
        [ValidateModelState]
        [ValidateTenantDomainCommunityOutboundPolicyExists]
        [SwaggerOperation("UpdateTenantDomainCommunityOutboundPolicy")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateTenantDomainCommunityOutboundPolicy([FromRoute][Required]int? deviceId, [FromRoute][Required]int? vpnTenantCommunityOutId,
            [FromBody]TenantDomainCommunityOutboundPolicyUpdate body)
        {
            try
            {
                var item = await _tenantDomainCommunityOutboundPolicyService.GetByIDAsync(vpnTenantCommunityOutId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                var update = Mapper.Map<Mind.Models.RequestModels.TenantDomainCommunityOutboundPolicyUpdate>(body);
                var vpnTenantCommunityOut = await _tenantDomainCommunityOutboundPolicyService.UpdateAsync(item.VpnTenantCommunityOutID, update);
                vpnTenantCommunityOut.SetModifiedHttpHeaders(Response);

                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (BuilderBadArgumentsException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (BuilderUnableToCompleteException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (IllegalStateException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Delete a tenant community from the outbound policy of a given tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the device</param>
        /// <param name="vpnTenantCommunityOutId">ID of the inbound policyk</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/outbound-policy/communities/{vpnTenantCommunityOutId}")]
        [ValidateModelState]
        [ValidateTenantDomainCommunityOutboundPolicyExists]
        [SwaggerOperation("DeleteTenantDomainCommunitytOutboundPolicy")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteTenantDomainCommunityOutboundPolicy([FromRoute][Required]int? deviceId, [FromRoute][Required]int? vpnTenantCommunityOutId)
        {
            try
            {
                await _tenantDomainCommunityOutboundPolicyService.DeleteAsync(vpnTenantCommunityOutId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find a tenant community which is associated with the outbound policy of a tenant domain device
        /// </summary>
        /// <remarks>Returns a single tenant community outbound policy</remarks>
        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="vpnTenantCommunityOutId">ID of the outbound policy</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/outbound-policy/communities/{vpnTenantCommunityOutId}", Name = "GetTenantDomainCommunityOutboundPolicy")]
        [ValidateModelState]
        [ValidateTenantDomainCommunityOutboundPolicyExists]
        [SwaggerOperation("GetTenantDomainCommunityOutboundPolicy")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantDomainCommunityOutboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetTenantDomainCommunityOutboundPolicy([FromRoute][Required]int? deviceId,
            [FromRoute][Required]int? vpnTenantCommunityOutId, [FromQuery]bool? deep)
        {
            var vpnTenantCommunityOut = await _tenantDomainCommunityOutboundPolicyService.GetByIDAsync(vpnTenantCommunityOutId.Value, deep: deep);
            if (vpnTenantCommunityOut.HasBeenModified(Request))
            {
                vpnTenantCommunityOut.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<TenantDomainCommunityOutboundPolicy>(vpnTenantCommunityOut));
        }

        /// <summary>
        /// Find all tenant communitys which are associated with the outbound policy of a tenant domain device
        /// </summary>
        /// <remarks>Returns a list of tenant community outbound policy objects</remarks>
        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/outbound-policy/communities")]
        [ValidateModelState]
        [ValidateTenantDomainDeviceExists]
        [SwaggerOperation("GetAllTenantDomainCommunityOutboundPolicy")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantDomainCommunityOutboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAllTenantDomainCommunityOutboundPolicy([FromRoute][Required]int? deviceId, [FromQuery]bool? deep)
        {
            var vpnTenantCommunitysIn = await _tenantDomainCommunityOutboundPolicyService.GetAllByDeviceIDAsync(deviceId.Value, deep: deep);
            return Ok(Mapper.Map<List<TenantDomainCommunityOutboundPolicy>>(vpnTenantCommunitysIn));
        }
    }
}

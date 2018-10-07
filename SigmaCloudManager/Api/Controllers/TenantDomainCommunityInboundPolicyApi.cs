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
    [ApiExplorerSettings(GroupName = "Tenant Domain Community Inbound Policy")]
    public class TenantDomainCommunityInboundPolicyApiController : BaseApiController
    {
        private readonly ITenantDomainCommunityInboundPolicyService _tenantDomainCommunityInboundPolicyService;

        /// <summary>
        /// API for creating and managing the lifecycle of tenant domain community inbound policy. 
        /// </summary>
        /// <param name="tenantDomainCommunityInboundPolicyService"></param>
        /// <param name="mapper"></param>
        public TenantDomainCommunityInboundPolicyApiController(ITenantDomainCommunityInboundPolicyService tenantDomainCommunityInboundPolicyService, IMapper mapper) : 
            base(tenantDomainCommunityInboundPolicyService, mapper)
        {
            _tenantDomainCommunityInboundPolicyService = tenantDomainCommunityInboundPolicyService;
        }

        /// <summary>
        /// Add a tenant community to the inbound policy of a given tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="body">request object that generates a new inbound policy entry for a tenant community</param>
        /// <response code="201">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/inbound-policy/communities/")]
        [ValidateModelState]
        [ValidateTenantDomainDeviceExists]
        [SwaggerOperation("AddTenantDomainCommunityInboundPolicy")]
        [SwaggerResponse(statusCode: 201, type: typeof(TenantDomainCommunityInboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> AddTenantDomainCommunityInboundPolicy([FromRoute][Required]int? deviceId, 
            [FromBody]TenantDomainCommunityInboundPolicyRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.TenantDomainCommunityInboundPolicyRequest>(body);
                var vpnTenantCommunityIn = await _tenantDomainCommunityInboundPolicyService.AddAsync(deviceId.Value, request);
                var tenantDomainCommunityInboundPolicyApiModel = Mapper.Map<Mind.Api.Models.TenantDomainCommunityInboundPolicy>(vpnTenantCommunityIn);
                return CreatedAtRoute("GetTenantDomainCommunityInboundPolicy", new
                {
                    deviceId,
                    vpnTenantCommunityInId = vpnTenantCommunityIn.VpnTenantCommunityInID
                }, tenantDomainCommunityInboundPolicyApiModel);
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
        /// Update a tenant community which is associated with the inbound policy of a tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="vpnTenantCommunityInId">ID of the vpn tenant community to update</param>
        /// <param name="body">community request object that applies updates to an existing community associated 
        /// with the inbound policy of the attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/inbound-policy/communities/{vpnTenantCommunityInId}")]
        [ValidateModelState]
        [ValidateTenantDomainCommunityInboundPolicyExists]
        [SwaggerOperation("UpdateTenantDomainCommunityInboundPolicy")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateTenantDomainCommunityInboundPolicy([FromRoute][Required]int? deviceId, [FromRoute][Required]int? vpnTenantCommunityInId,
            [FromBody]TenantDomainCommunityInboundPolicyUpdate body)
        {
            try
            {
                var item = await _tenantDomainCommunityInboundPolicyService.GetByIDAsync(vpnTenantCommunityInId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                var update = Mapper.Map<Mind.Models.RequestModels.TenantDomainCommunityInboundPolicyUpdate>(body);
                var vpnTenantCommunityIn = await _tenantDomainCommunityInboundPolicyService.UpdateAsync(item.VpnTenantCommunityInID, update);
                vpnTenantCommunityIn.SetModifiedHttpHeaders(Response);

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
        /// Delete a tenant community from the inbound policy of a given tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the device</param>
        /// <param name="vpnTenantCommunityInId">ID of the inbound policyk</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/inbound-policy/communities/{vpnTenantCommunityInId}")]
        [ValidateModelState]
        [ValidateTenantDomainCommunityInboundPolicyExists]
        [SwaggerOperation("DeleteTenantDomainCommunitytInboundPolicy")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteTenantDomainCommunityInboundPolicy([FromRoute][Required]int? deviceId, [FromRoute][Required]int? vpnTenantCommunityInId)
        {
            try
            {
                await _tenantDomainCommunityInboundPolicyService.DeleteAsync(vpnTenantCommunityInId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find a tenant community which is associated with the inbound policy of a tenant domain device
        /// </summary>
        /// <remarks>Returns a single tenant community inbound policy</remarks>
        /// <param name="deviceId">ID of the tenant dommain device</param>
        /// <param name="vpnTenantCommunityInId">ID of the inbound policy</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/inbound-policy/communities/{vpnTenantCommunityInId}", Name = "GetTenantDomainCommunityInboundPolicy")]
        [ValidateModelState]
        [ValidateTenantDomainCommunityInboundPolicyExists]
        [SwaggerOperation("GetTenantDomainCommunityInboundPolicy")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantDomainCommunityInboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetTenantDomainCommunityInboundPolicy([FromRoute][Required]int? deviceId,
            [FromRoute][Required]int? vpnTenantCommunityInId, [FromQuery]bool? deep)
        {
            var vpnTenantCommunityIn = await _tenantDomainCommunityInboundPolicyService.GetByIDAsync(vpnTenantCommunityInId.Value, deep: deep);
            if (vpnTenantCommunityIn.HasBeenModified(Request))
            {
                vpnTenantCommunityIn.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<TenantDomainCommunityInboundPolicy>(vpnTenantCommunityIn));
        }

        /// <summary>
        /// Find all tenant communities which are associated with the inbound policy of a tenant domain device
        /// </summary>
        /// <remarks>Returns a list of tenant community inbound policy objects</remarks>
        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/inbound-policy/communities")]
        [ValidateModelState]
        [ValidateTenantDomainDeviceExists]
        [SwaggerOperation("GetAllTenantDomainCommunityInboundPolicy")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantDomainCommunityInboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAllTenantDomainCommunityInboundPolicy([FromRoute][Required]int? deviceId, [FromQuery]bool? deep)
        {
            var vpnTenantCommunitysIn = await _tenantDomainCommunityInboundPolicyService.GetAllByDeviceIDAsync(deviceId.Value, deep: deep);
            return Ok(Mapper.Map<List<TenantDomainCommunityInboundPolicy>>(vpnTenantCommunitysIn));
        }
    }
}

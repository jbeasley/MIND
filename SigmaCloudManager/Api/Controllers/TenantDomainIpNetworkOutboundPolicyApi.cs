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
    public class TenantDomainIpNetworkOutboundPolicyApiController : BaseApiController
    {
        private readonly ITenantDomainIpNetworkOutboundPolicyService _tenantDomainIpNetworkOutboundPolicyService;

        /// <summary>
        /// API for creating and managing the lifecycle of tenant domain IP network outbound policy. 
        /// </summary>
        /// <param name="tenantDomainIpNetworkOutboundPolicyService"></param>
        /// <param name="mapper"></param>
        public TenantDomainIpNetworkOutboundPolicyApiController(ITenantDomainIpNetworkOutboundPolicyService tenantDomainIpNetworkOutboundPolicyService, IMapper mapper) : 
            base(tenantDomainIpNetworkOutboundPolicyService, mapper)
        {
            _tenantDomainIpNetworkOutboundPolicyService = tenantDomainIpNetworkOutboundPolicyService;
        }

        /// <summary>
        /// Add a tenant IP network to the outbound policy of a given tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="body">request object that generates a new outbound policy entry for a tenant IP network</param>
        /// <response code="201">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/outbound-policy/ip-networks/")]
        [ValidateModelState]
        [ValidateTenantDomainDeviceExists]
        [SwaggerOperation("AddTenantDomainIpNetworkOutboundPolicy")]
        [SwaggerResponse(statusCode: 201, type: typeof(TenantDomainIpNetworkOutboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> AddTenantDomainIpNetworkOutboundPolicy([FromRoute][Required]int? deviceId, 
            [FromBody]TenantDomainIpNetworkOutboundPolicyRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.TenantDomainIpNetworkOutboundPolicyRequest>(body);
                var vpnTenantIpNetworkOut = await _tenantDomainIpNetworkOutboundPolicyService.AddAsync(deviceId.Value, request);
                var tenantDomainIpNetworkOutboundPolicyApiModel = Mapper.Map<Mind.Api.Models.TenantDomainIpNetworkOutboundPolicy>(vpnTenantIpNetworkOut);
                return CreatedAtRoute("GetTenantDomainIpNetworkOutboundPolicy", new
                {
                    deviceId,
                    vpnTenantIpNetworkOutId = vpnTenantIpNetworkOut.VpnTenantIpNetworkOutID
                }, tenantDomainIpNetworkOutboundPolicyApiModel);
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
        /// Update a tenant IP network which is associated with the outbound policy of a tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="vpnTenantIpNetworkOutId">ID of the vpn tenant IP network to update</param>
        /// <param name="body">IP network request object that applies updates to an existing IP network associated 
        /// with the outbound policy of the attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/outbound-policy/ip-networks/{vpnTenantIpNetworkOutId}")]
        [ValidateModelState]
        [ValidateTenantDomainIpNetworkOutboundPolicyExists]
        [SwaggerOperation("UpdateTenantDomainIpNetworkOutboundPolicy")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateTenantDomainIpNetworkOutboundPolicy([FromRoute][Required]int? deviceId, [FromRoute][Required]int? vpnTenantIpNetworkOutId,
            [FromBody]TenantDomainIpNetworkOutboundPolicyUpdate body)
        {
            try
            {
                var item = await _tenantDomainIpNetworkOutboundPolicyService.GetByIDAsync(vpnTenantIpNetworkOutId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                var update = Mapper.Map<Mind.Models.RequestModels.TenantDomainIpNetworkOutboundPolicyUpdate>(body);
                var vpnTenantIpNetworkOut = await _tenantDomainIpNetworkOutboundPolicyService.UpdateAsync(item.VpnTenantIpNetworkOutID, update);
                vpnTenantIpNetworkOut.SetModifiedHttpHeaders(Response);

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
        /// Delete a tenant IP network from the outbound policy of a given tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the device</param>
        /// <param name="vpnTenantIpNetworkOutId">ID of the inbound policyk</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/outbound-policy/ip-networks/{vpnTenantIpNetworkOutId}")]
        [ValidateModelState]
        [ValidateTenantDomainIpNetworkOutboundPolicyExists]
        [SwaggerOperation("DeleteTenantDomainIpNetworktOutboundPolicy")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteTenantDomainIpNetworkOutboundPolicy([FromRoute][Required]int? deviceId, [FromRoute][Required]int? vpnTenantIpNetworkOutId)
        {
            try
            {
                await _tenantDomainIpNetworkOutboundPolicyService.DeleteAsync(vpnTenantIpNetworkOutId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find a tenant IP network which is associated with the outbound policy of a tenant domain device
        /// </summary>
        /// <remarks>Returns a single tenant IP network outbound policy</remarks>
        /// <param name="deviceId">ID of the tenant dommain device</param>
        /// <param name="vpnTenantIpNetworkOutId">ID of the outbound policy</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/outbound-policy/ip-networks/{vpnTenantIpNetworkOutId}", Name = "GetTenantDomainIpNetworkOutboundPolicy")]
        [ValidateModelState]
        [ValidateTenantDomainIpNetworkOutboundPolicyExists]
        [SwaggerOperation("GetTenantDomainIpNetworkOutboundPolicy")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantDomainIpNetworkOutboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetTenantDomainIpNetworkOutboundPolicy([FromRoute][Required]int? deviceId,
            [FromRoute][Required]int? vpnTenantIpNetworkOutId, [FromQuery]bool? deep)
        {
            var vpnTenantIpNetworkOut = await _tenantDomainIpNetworkOutboundPolicyService.GetByIDAsync(vpnTenantIpNetworkOutId.Value, deep: deep);
            if (vpnTenantIpNetworkOut.HasBeenModified(Request))
            {
                vpnTenantIpNetworkOut.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<TenantDomainIpNetworkOutboundPolicy>(vpnTenantIpNetworkOut));
        }

        /// <summary>
        /// Find all tenant IP networks which are associated with the outbound policy of a tenant domain device
        /// </summary>
        /// <remarks>Returns a list of tenant IP network outbound policy objects</remarks>
        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/outbound-policy/ip-networks")]
        [ValidateModelState]
        [ValidateTenantDomainDeviceExists]
        [SwaggerOperation("GetAllTenantDomainIpNetworkOutboundPolicy")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantDomainIpNetworkOutboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAllTenantDomainIpNetworkOutboundPolicy([FromRoute][Required]int? deviceId, [FromQuery]bool? deep)
        {
            var vpnTenantIpNetworksIn = await _tenantDomainIpNetworkOutboundPolicyService.GetAllByDeviceIDAsync(deviceId.Value, deep: deep);
            return Ok(Mapper.Map<List<TenantDomainIpNetworkOutboundPolicy>>(vpnTenantIpNetworksIn));
        }
    }
}

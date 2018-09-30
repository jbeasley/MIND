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
    public class TenantDomainIpNetworkInboundPolicyApiController : BaseApiController
    {
        private readonly ITenantDomainIpNetworkInboundPolicyService _tenantDomainIpNetworkInboundPolicyService;

        /// <summary>
        /// API for creating and managing the lifecycle of tenant domain IP network inbound policy. 
        /// </summary>
        /// <param name="tenantDomainIpNetworkInboundPolicyService"></param>
        /// <param name="mapper"></param>
        public TenantDomainIpNetworkInboundPolicyApiController(ITenantDomainIpNetworkInboundPolicyService tenantDomainIpNetworkInboundPolicyService, IMapper mapper) : 
            base(tenantDomainIpNetworkInboundPolicyService, mapper)
        {
            _tenantDomainIpNetworkInboundPolicyService = tenantDomainIpNetworkInboundPolicyService;
        }

        /// <summary>
        /// Add a tenant IP network to the inbound policy of a given tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="body">request object that generates a new inbound policy entry for a tenant IP network</param>
        /// <response code="201">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/inbound-policy/ip-networks/")]
        [ValidateModelState]
        [ValidateTenantDomainDeviceExists]
        [SwaggerOperation("AddTenantDomainIpNetworkInboundPolicy")]
        [SwaggerResponse(statusCode: 201, type: typeof(TenantDomainIpNetworkInboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> AddTenantDomainIpNetworkInboundPolicy([FromRoute][Required]int? deviceId, 
            [FromBody]TenantDomainIpNetworkInboundPolicyRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.TenantDomainIpNetworkInboundPolicyRequest>(body);
                var vpnTenantIpNetworkIn = await _tenantDomainIpNetworkInboundPolicyService.AddAsync(deviceId.Value, request);
                var tenantDomainIpNetworkInboundPolicyApiModel = Mapper.Map<Mind.Api.Models.TenantDomainIpNetworkInboundPolicy>(vpnTenantIpNetworkIn);
                return CreatedAtRoute("GetTenantDomainIpNetworkInboundPolicy", new
                {
                    deviceId,
                    vpnTenantIpNetworkInId = vpnTenantIpNetworkIn.VpnTenantIpNetworkInID
                }, tenantDomainIpNetworkInboundPolicyApiModel);
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
        /// Update a tenant IP network which is associated with the inbound policy of a tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="vpnTenantIpNetworkInId">ID of the vpn tenant IP network to update</param>
        /// <param name="body">IP network request object that applies updates to an existing IP network associated 
        /// with the inbound policy of the attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/inbound-policy/ip-networks/{vpnTenantIpNetworkInId}")]
        [ValidateModelState]
        [ValidateTenantDomainIpNetworkInboundPolicyExists]
        [SwaggerOperation("UpdateTenantDomainIpNetworkInboundPolicy")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateTenantDomainIpNetworkInboundPolicy([FromRoute][Required]int? deviceId, [FromRoute][Required]int? vpnTenantIpNetworkInId,
            [FromBody]TenantDomainIpNetworkInboundPolicyUpdate body)
        {
            try
            {
                var item = await _tenantDomainIpNetworkInboundPolicyService.GetByIDAsync(vpnTenantIpNetworkInId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                var update = Mapper.Map<Mind.Models.RequestModels.TenantDomainIpNetworkInboundPolicyUpdate>(body);
                var vpnTenantIpNetworkIn = await _tenantDomainIpNetworkInboundPolicyService.UpdateAsync(item.VpnTenantIpNetworkInID, update);
                vpnTenantIpNetworkIn.SetModifiedHttpHeaders(Response);

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
        /// Delete a tenant IP network from the inbound policy of a given tenant domain device
        /// </summary>

        /// <param name="deviceId">ID of the device</param>
        /// <param name="vpnTenantIpNetworkInId">ID of the inbound policyk</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/inbound-policy/ip-networks/{vpnTenantIpNetworkInId}")]
        [ValidateModelState]
        [ValidateTenantDomainIpNetworkInboundPolicyExists]
        [SwaggerOperation("DeleteTenantDomainIpNetworktInboundPolicy")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteTenantDomainIpNetworkInboundPolicy([FromRoute][Required]int? deviceId, [FromRoute][Required]int? vpnTenantIpNetworkInId)
        {
            try
            {
                await _tenantDomainIpNetworkInboundPolicyService.DeleteAsync(vpnTenantIpNetworkInId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find a tenant IP network which is associated with the inbound policy of a tenant domain device
        /// </summary>
        /// <remarks>Returns a single tenant IP network inbound policy</remarks>
        /// <param name="deviceId">ID of the tenant dommain device</param>
        /// <param name="vpnTenantIpNetworkInId">ID of the inbound policy</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/inbound-policy/ip-networks/{vpnTenantIpNetworkInId}", Name = "GetTenantDomainIpNetworkInboundPolicy")]
        [ValidateModelState]
        [ValidateTenantDomainIpNetworkInboundPolicyExists]
        [SwaggerOperation("GetTenantDomainIpNetworkInboundPolicy")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantDomainIpNetworkInboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetTenantDomainIpNetworkInboundPolicy([FromRoute][Required]int? deviceId,
            [FromRoute][Required]int? vpnTenantIpNetworkInId, [FromQuery]bool? deep)
        {
            var vpnTenantIpNetworkIn = await _tenantDomainIpNetworkInboundPolicyService.GetByIDAsync(vpnTenantIpNetworkInId.Value, deep: deep);
            if (vpnTenantIpNetworkIn.HasBeenModified(Request))
            {
                vpnTenantIpNetworkIn.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<TenantDomainIpNetworkInboundPolicy>(vpnTenantIpNetworkIn));
        }

        /// <summary>
        /// Find all tenant IP networks which are associated with the inbound policy of a tenant domain device
        /// </summary>
        /// <remarks>Returns a list of tenant IP network inbound policy objects</remarks>
        /// <param name="deviceId">ID of the tenant domain device</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/inbound-policy/ip-networks")]
        [ValidateModelState]
        [ValidateTenantDomainDeviceExists]
        [SwaggerOperation("GetAllTenantDomainIpNetworkInboundPolicy")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantDomainIpNetworkInboundPolicy), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAllTenantDomainIpNetworkInboundPolicy([FromRoute][Required]int? deviceId, [FromQuery]bool? deep)
        {
            var vpnTenantIpNetworksIn = await _tenantDomainIpNetworkInboundPolicyService.GetAllByDeviceIDAsync(deviceId.Value, deep: deep);
            return Ok(Mapper.Map<List<TenantDomainIpNetworkInboundPolicy>>(vpnTenantIpNetworksIn));
        }
    }
}

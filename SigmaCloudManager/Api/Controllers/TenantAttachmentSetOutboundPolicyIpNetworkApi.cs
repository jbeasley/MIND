using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using AutoMapper;
using Mind.Api.Attributes;
using Mind.Api.Models;
using System.ComponentModel.DataAnnotations;
using Mind.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Mind.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Mind.Api.Controllers
{
    /// <summary>
    /// Create and manage the lifecycle of attachment set outbound policy for IP networks
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Provider Domain Tenant IP Network Outbound Policy")]
    public class TenantAttachmentSetOutboundPolicyIpNetworkApiController : BaseApiController
    {
        private readonly IProviderDomainIpNetworkOutboundPolicyService _providerDomainIpNetworkOutboundPolicyService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerDomainIpNetworkOutboundPolicyService"></param>
        /// <param name="mapper"></param>
        public TenantAttachmentSetOutboundPolicyIpNetworkApiController(IProviderDomainIpNetworkOutboundPolicyService providerDomainIpNetworkOutboundPolicyService, IMapper mapper) : 
            base(providerDomainIpNetworkOutboundPolicyService, mapper)
        {
            _providerDomainIpNetworkOutboundPolicyService = providerDomainIpNetworkOutboundPolicyService;
        }

        /// <summary>
        /// Add a tenant IP network to the outbound policy of a given attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="body">request object that generates a new outbound policy entry for a tenant IP network</param>
        /// <response code="201">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/outbound-policy/ip-networks")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("AddAttachmentSetOutboundPolicyTenantIpNetwork")]
        [SwaggerResponse(statusCode: 201, type: typeof(VpnTenantIpNetworkOut), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> AddAttachmentSetOutboundPolicyTenantIpNetwork([FromRoute][Required]int? attachmentSetId,
            [FromBody]VpnTenantIpNetworkOutRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.VpnTenantIpNetworkOutRequest>(body);
                var vpnTenantIpNetworkOut = await _providerDomainIpNetworkOutboundPolicyService.AddAsync(attachmentSetId.Value, request);
                var vpnTenantIpNetworkOutApiModel = Mapper.Map<Mind.Api.Models.VpnTenantIpNetworkOut>(vpnTenantIpNetworkOut);
                return CreatedAtRoute("GetVpnTenantIpNetworkOut", new
                {
                    attachmentSetId = vpnTenantIpNetworkOut.AttachmentSetID,
                    vpnTenantIpNetworkOutId = vpnTenantIpNetworkOut.VpnTenantIpNetworkOutID
                }, vpnTenantIpNetworkOutApiModel);
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
        /// Update a tenant IP network which is associated with the outbound policy of an attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantIpNetworkOutId">ID of the vpn tenant IP network to update</param>
        /// <param name="body">IP network request object that applies updates to an existing IP network associated 
        /// with the outbound policy of the attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/outbound-policy/ip-networks/{vpnTenantIpNetworkOutId}")]
        [ValidateModelState]
        [ValidateVpnTenantIpNetworkOutExists]
        [SwaggerOperation("UpdateAttachmentSetOutboundPolicyTenantIpNetwork")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateAttachmentSetOutboundPolicyTenantIpNetwork([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? vpnTenantIpNetworkOutId,
            [FromBody]VpnTenantIpNetworkOutUpdate body)
        {
            try
            {
                var item = await _providerDomainIpNetworkOutboundPolicyService.GetByIDAsync(vpnTenantIpNetworkOutId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                var update = Mapper.Map<Mind.Models.RequestModels.VpnTenantIpNetworkOutUpdate>(body);
                var vpnTenantIpNetworkOut = await _providerDomainIpNetworkOutboundPolicyService.UpdateAsync(item.VpnTenantIpNetworkOutID, update);
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
        /// Delete a tenant IP network from the outbound policy of a given attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantIpNetworkOutId">ID of the vpn tenant IP network</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/outbound-policy/ip-networks/{vpnTenantIpNetworkOutId}")]
        [ValidateModelState]
        [ValidateVpnTenantIpNetworkOutExists]
        [SwaggerOperation("DeleteAttachmentSetOutboundPolicyTenantIpNetwork")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteAttachmentSetOutboundPolicyTenantIpNetwork([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? vpnTenantIpNetworkOutId)
        {
            try
            {
                await _providerDomainIpNetworkOutboundPolicyService.DeleteAsync(vpnTenantIpNetworkOutId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find a tenant IP network which is associated with the outbound policy of an attachment set by ID
        /// </summary>
        /// <remarks>Returns a single vpn tenant IP network</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantIpNetworkOutId">ID of the vpn tenant IP network</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/outbound-policy/ip-networks/{vpnTenantIpNetworkOutId}", Name = "GetVpnTenantIpNetworkOut")]
        [ValidateModelState]
        [ValidateVpnTenantIpNetworkOutExists]
        [SwaggerOperation("GetAttachmentSetOutboundPolicyTenantIpNetwork")]
        [SwaggerResponse(statusCode: 200, type: typeof(VpnTenantIpNetworkOut), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAttachmentSetOutboundPolicyTenantIpNetwork([FromRoute][Required]int? attachmentSetId, 
            [FromRoute][Required]int? vpnTenantIpNetworkOutId, [FromQuery]bool? deep)
        {
            var vpnTenantIpNetworkOut = await _providerDomainIpNetworkOutboundPolicyService.GetByIDAsync(vpnTenantIpNetworkOutId.Value, deep: deep);
            if (vpnTenantIpNetworkOut.HasBeenModified(Request))
            {
                vpnTenantIpNetworkOut.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<VpnTenantIpNetworkOut>(vpnTenantIpNetworkOut));
        }

        /// <summary>
        /// Find all tenant IP network which are associated with the outbound policy of an attachment set
        /// </summary>
        /// <remarks>Returns a list of vpn tenant IP network objects</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/outbound-policy/ip-networks")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("GetAllAttachmentSetOutboundPolicyTenantIpNetworks")]
        [SwaggerResponse(statusCode: 200, type: typeof(VpnTenantIpNetworkOut), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAllAttachmentSetOutboundPolicyTenantIpNetwork([FromRoute][Required]int? attachmentSetId,[FromQuery]bool? deep)
        {
            var vpnTenantIpNetworksOut = await _providerDomainIpNetworkOutboundPolicyService.GetAllByAttachmentSetIDAsync(attachmentSetId.Value, deep: deep);
            return Ok(Mapper.Map<List<VpnTenantIpNetworkOut>>(vpnTenantIpNetworksOut));
        }
    }
}

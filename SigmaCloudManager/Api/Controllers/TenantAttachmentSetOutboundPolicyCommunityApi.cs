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
    /// Create and manage the lifecycle of attachment set outbound policy for Communitys
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Provider Domain Tenant Community Outbound Policy")]
    public class TenantAttachmentSetOutboundPolicyCommunityApiController : BaseApiController
    {
        private readonly IProviderDomainCommunityOutboundPolicyService _providerDomainCommunityOutboundPolicyService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerDomainCommunityOutboundPolicyService"></param>
        /// <param name="mapper"></param>
        public TenantAttachmentSetOutboundPolicyCommunityApiController(IProviderDomainCommunityOutboundPolicyService providerDomainCommunityOutboundPolicyService, IMapper mapper) : 
            base(providerDomainCommunityOutboundPolicyService, mapper)
        {
            _providerDomainCommunityOutboundPolicyService = providerDomainCommunityOutboundPolicyService;
        }

        /// <summary>
        /// Add a tenant community to the outbound policy of a given attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="body">request object that generates a new outbound policy entry for a tenant community</param>
        /// <response code="201">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/outbound-policy/communities")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("AddAttachmentSetOutboundPolicyTenantCommunity")]
        [SwaggerResponse(statusCode: 201, type: typeof(VpnTenantCommunityOut), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> AddAttachmentSetOutboundPolicyTenantCommunity([FromRoute][Required]int? attachmentSetId,
            [FromBody]VpnTenantCommunityOutRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.VpnTenantCommunityOutRequest>(body);
                var vpnTenantCommunityOut = await _providerDomainCommunityOutboundPolicyService.AddAsync(attachmentSetId.Value, request);
                var vpnTenantCommunityOutApiModel = Mapper.Map<Mind.Api.Models.VpnTenantCommunityOut>(vpnTenantCommunityOut);
                return CreatedAtRoute("GetVpnTenantCommunityOut", new
                {
                    attachmentSetId = vpnTenantCommunityOut.AttachmentSetID,
                    vpnTenantCommunityOutId = vpnTenantCommunityOut.VpnTenantCommunityOutID
                }, vpnTenantCommunityOutApiModel);
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
        /// Update a tenant community which is associated with the outbound policy of an attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantCommunityOutId">ID of the vpn tenant community to update</param>
        /// <param name="body">Community request object that applies updates to an existing community associated 
        /// with the outbound policy of the attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/outbound-policy/communities/{vpnTenantCommunityOutId}")]
        [ValidateModelState]
        [ValidateVpnTenantCommunityOutExists]
        [SwaggerOperation("UpdateAttachmentSetOutboundPolicyTenantCommunity")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateAttachmentSetOutboundPolicyTenantCommunity([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? vpnTenantCommunityOutId,
            [FromBody]VpnTenantCommunityOutUpdate body)
        {
            try
            {
                var item = await _providerDomainCommunityOutboundPolicyService.GetByIDAsync(vpnTenantCommunityOutId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                var update = Mapper.Map<Mind.Models.RequestModels.VpnTenantCommunityOutUpdate>(body);
                var vpnTenantCommunityOut = await _providerDomainCommunityOutboundPolicyService.UpdateAsync(item.VpnTenantCommunityOutID, update);
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
        /// Delete a tenant community from the outbound policy of a given attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantCommunityOutId">ID of the vpn tenant community</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/outbound-policy/communities/{vpnTenantCommunityOutId}")]
        [ValidateModelState]
        [ValidateVpnTenantCommunityOutExists]
        [SwaggerOperation("DeleteAttachmentSetOutboundPolicyTenantCommunity")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteAttachmentSetOutboundPolicyTenantCommunity([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? vpnTenantCommunityOutId)
        {
            try
            {
                await _providerDomainCommunityOutboundPolicyService.DeleteAsync(vpnTenantCommunityOutId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find a tenant community which is associated with the outbound policy of an attachment set by ID
        /// </summary>
        /// <remarks>Returns a single vpn tenant community</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantCommunityOutId">ID of the vpn tenant community</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/outbound-policy/communities/{vpnTenantCommunityOutId}", Name = "GetVpnTenantCommunityOut")]
        [ValidateModelState]
        [ValidateVpnTenantCommunityOutExists]
        [SwaggerOperation("GetAttachmentSetOutboundPolicyTenantCommunity")]
        [SwaggerResponse(statusCode: 200, type: typeof(VpnTenantCommunityOut), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAttachmentSetOutboundPolicyTenantCommunity([FromRoute][Required]int? attachmentSetId, 
            [FromRoute][Required]int? vpnTenantCommunityOutId, [FromQuery]bool? deep)
        {
            var vpnTenantCommunityOut = await _providerDomainCommunityOutboundPolicyService.GetByIDAsync(vpnTenantCommunityOutId.Value, deep: deep);
            if (vpnTenantCommunityOut.HasBeenModified(Request))
            {
                vpnTenantCommunityOut.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<VpnTenantCommunityOut>(vpnTenantCommunityOut));
        }

        /// <summary>
        /// Find all tenant community which are associated with the outbound policy of an attachment set
        /// </summary>
        /// <remarks>Returns a list of vpn tenant community objects</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/outbound-policy/communities")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("GetAllAttachmentSetOutboundPolicyTenantCommunitys")]
        [SwaggerResponse(statusCode: 200, type: typeof(VpnTenantCommunityOut), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAllAttachmentSetOutboundPolicyTenantCommunity([FromRoute][Required]int? attachmentSetId,[FromQuery]bool? deep)
        {
            var vpnTenantCommunitysOut = await _providerDomainCommunityOutboundPolicyService.GetAllByAttachmentSetIDAsync(attachmentSetId.Value, deep: deep);
            return Ok(Mapper.Map<List<VpnTenantCommunityOut>>(vpnTenantCommunitysOut));
        }
    }
}

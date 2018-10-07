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
    /// <summary>
    /// Create and manage the lifecycle of attachment set inbound policy for BGP communities
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Provider Domain Tenant Community Inbound Policy")]
    public class TenantAttachmentSetInboundPolicyCommunityApiController : BaseApiController
    {
        private readonly IProviderDomainCommunityInboundPolicyService _providerDomainCommunityInboundPolicyService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerDomainCommunityInboundPolicyService"></param>
        /// <param name="mapper"></param>
        public TenantAttachmentSetInboundPolicyCommunityApiController(IProviderDomainCommunityInboundPolicyService providerDomainCommunityInboundPolicyService, IMapper mapper) : 
            base(providerDomainCommunityInboundPolicyService, mapper)
        {
            _providerDomainCommunityInboundPolicyService = providerDomainCommunityInboundPolicyService;
        }

        /// <summary>
        /// Add a tenant community to the inbound policy of a given attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="body">request object that generates a new inbound policy entry for a tenant community</param>
        /// <response code="201">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/inbound-policy/communities/")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("AddAttachmentSetInboundPolicyTenantCommunity")]
        [SwaggerResponse(statusCode: 201, type: typeof(VpnTenantCommunityIn), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> AddAttachmentSetInboundPolicyTenantCommunity([FromRoute][Required]int? attachmentSetId, 
            [FromBody]VpnTenantCommunityInRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.VpnTenantCommunityInRequest>(body);
                var vpnTenantCommunityIn = await _providerDomainCommunityInboundPolicyService.AddAsync(attachmentSetId.Value, request);
                var vpnTenantCommunityInApiModel = Mapper.Map<Mind.Api.Models.VpnTenantCommunityIn>(vpnTenantCommunityIn);
                return CreatedAtRoute("GetVpnTenantCommunityIn", new
                {
                    attachmentSetId = vpnTenantCommunityIn.AttachmentSetID,
                    vpnTenantCommunityInId = vpnTenantCommunityIn.VpnTenantCommunityInID
                }, vpnTenantCommunityInApiModel);
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
        /// Update a tenant community which is associated with the inbound policy of an attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantCommunityInId">ID of the vpn tenant community to update</param>
        /// <param name="body">IP network request object that applies updates to an existing IP network associated 
        /// with the inbound policy of the attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/inbound-policy/communities/{vpnTenantCommunityInId}")]
        [ValidateModelState]
        [ValidateVpnTenantCommunityInExists]
        [SwaggerOperation("UpdateAttachmentSetInboundPolicyTenantCommunity")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateAttachmentSetInboundPolicyTenantCommunity([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? vpnTenantCommunityInId,
            [FromBody]VpnTenantCommunityInUpdate body)
        {
            try
            {
                var item = await _providerDomainCommunityInboundPolicyService.GetByIDAsync(vpnTenantCommunityInId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                var update = Mapper.Map<Mind.Models.RequestModels.VpnTenantCommunityInUpdate>(body);
                var vpnTenantCommunityIn = await _providerDomainCommunityInboundPolicyService.UpdateAsync(item.VpnTenantCommunityInID, update);
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
        /// Delete a tenant community from the inbound policy of a given attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantCommunityInId">ID of the vpn tenant community</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/inbound-policy/communities/{vpnTenantCommunityInId}")]
        [ValidateModelState]
        [ValidateVpnTenantCommunityInExists]
        [SwaggerOperation("DeleteAttachmentSetInboundPolicyTenantCommunity")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteAttachmentSetInboundPolicyTenantCommunity([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? vpnTenantCommunityInId)
        {
            try
            {
                await _providerDomainCommunityInboundPolicyService.DeleteAsync(vpnTenantCommunityInId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find a tenant community which is associated with the inbound policy of an attachment set by ID
        /// </summary>
        /// <remarks>Returns a single vpn tenant community</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantCommunityInId">ID of the vpn tenant community</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/inbound-policy/communities/{vpnTenantCommunityInId}", Name = "GetVpnTenantCommunityIn")]
        [ValidateModelState]
        [ValidateVpnTenantCommunityInExists]
        [SwaggerOperation("GetAttachmentSetInboundPolicyTenantCommunity")]
        [SwaggerResponse(statusCode: 200, type: typeof(VpnTenantCommunityIn), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAttachmentSetInboundPolicyTenantCommunity([FromRoute][Required]int? attachmentSetId,
            [FromRoute][Required]int? vpnTenantCommunityInId, [FromQuery]bool? deep)
        {
            var vpnTenantCommunityIn = await _providerDomainCommunityInboundPolicyService.GetByIDAsync(vpnTenantCommunityInId.Value, deep: deep);
            if (vpnTenantCommunityIn.HasBeenModified(Request))
            {
                vpnTenantCommunityIn.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<VpnTenantCommunityIn>(vpnTenantCommunityIn));
        }

        /// <summary>
        /// Find all tenant community which are associated with the inbound policy of an attachment set
        /// </summary>
        /// <remarks>Returns a list of vpn tenant community objects</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/inbound-policy/communities")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("GetAllAttachmentSetInboundPolicyTenantCommunitys")]
        [SwaggerResponse(statusCode: 200, type: typeof(VpnTenantCommunityIn), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAllAttachmentSetInboundPolicyTenantCommunitys([FromRoute][Required]int? attachmentSetId, [FromQuery]bool? deep)
        {
            var vpnTenantCommunitysIn = await _providerDomainCommunityInboundPolicyService.GetAllByAttachmentSetIDAsync(attachmentSetId.Value, deep: deep);
            return Ok(Mapper.Map<List<VpnTenantCommunityIn>>(vpnTenantCommunitysIn));
        }
    }
}

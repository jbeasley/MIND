﻿using Microsoft.AspNetCore.Mvc;
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

namespace Mind.Api.Controllers
{
    [ApiVersion("1.0")]
    public class TenantAttachmentSetInboundPolicyIpNetworkController : BaseApiController
    {
        private readonly IVpnTenantIpNetworkInService _vpnTenantIpNetworkInService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vpnTenantIpNetworkService"></param>
        /// <param name="mapper"></param>
        public TenantAttachmentSetInboundPolicyIpNetworkController(IVpnTenantIpNetworkInService vpnTenantIpNetworkInService, IMapper mapper) : 
            base(vpnTenantIpNetworkInService, mapper)
        {
            _vpnTenantIpNetworkInService = vpnTenantIpNetworkInService;
        }

        /// <summary>
        /// Add a tenant IP network to the inbound policy of a given attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="tenantIpNetworkId">ID of the tenant IP network</param>
        /// <param name="body">request object that generates a new inbound policy entry for a tenant IP network</param>
        /// <response code="201">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/inbound-policy/tenant-ip-networks/{tenantIpNetworkId}")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [ValidateTenantIpNetworkExists]
        [SwaggerOperation("AddAttachmentSetInboundPolicyTenantIpNetwork")]
        [SwaggerResponse(statusCode: 201, type: typeof(VpnTenantIpNetworkIn), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> AddAttachmentSetInboundPolicyTenantIpNetwork([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? tenantIpNetworkId, 
            [FromBody]VpnTenantIpNetworkInRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.VpnTenantIpNetworkInRequest>(body);
                var vpnTenantIpNetworkIn = await _vpnTenantIpNetworkInService.AddAsync(attachmentSetId.Value, tenantIpNetworkId.Value, request);
                var vpnTenantIpNetworkInApiModel = Mapper.Map<Mind.Api.Models.VpnTenantIpNetworkIn>(vpnTenantIpNetworkIn);
                return CreatedAtRoute("GetVpnTenantIpNetworkIn", new
                {
                    attachmentSetId = vpnTenantIpNetworkIn.AttachmentSetID,
                    tenantIpNetworkId = vpnTenantIpNetworkIn.TenantIpNetworkID
                }, vpnTenantIpNetworkInApiModel);
            }

            catch (BuilderBadArgumentsException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Update a tenant IP network which is associated with the inbound policy of an attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="tenantIpNetworkId">ID of the tenant IP network to update</param>
        /// <param name="body">IPv4 network request object that applies updates to an existing IP network associated 
        /// with the inbound policy of the attachment set</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPut]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/inbound-policy/tenant-ip-networks/{tenantIpNetworkId}")]
        [ValidateModelState]
        [ValidateVpnTenantIpNetworkInExists]
        [SwaggerOperation("UpdateAttachmentSetInboundPolicyTenantIpNetwork")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantIpNetwork), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateAttachmentSetInboundPolicyTenantIpNetwork([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? tenantIpNetworkId,
            [FromBody]VpnTenantIpNetworkInRequest body)
        {
            try
            {
                var item = await _vpnTenantIpNetworkInService.GetByAttachmentSetIDAndTenantIpNetworkIDAsync(attachmentSetId.Value, tenantIpNetworkId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                var request = Mapper.Map<Mind.Models.RequestModels.VpnTenantIpNetworkInRequest>(body);
                var vpnTenantIpNetworkIn = await _vpnTenantIpNetworkInService.UpdateAsync(item.VpnTenantIpNetworkInID, request);
                vpnTenantIpNetworkIn.SetModifiedHttpHeaders(Response);
                var vpnTenantIpNetworkInApiModel = Mapper.Map<Mind.Api.Models.VpnTenantIpNetworkIn>(vpnTenantIpNetworkIn);

                return Ok(vpnTenantIpNetworkInApiModel);
            }

            catch (BuilderBadArgumentsException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Delete a tenant IP network from the inbound policy of a given attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="tenantIpNetworkId">ID of the tenant IP network</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/inbound-policy/tenant-ip-networks/{tenantIpNetworkId}")]
        [ValidateModelState]
        [ValidateVpnTenantIpNetworkInExists]
        [SwaggerOperation("DeleteAttachmentSetInboundPolicyTenantIpNetwork")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteAttachmentSetInboundPolicyTenantIpNetwork([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? tenantIpNetworkId)
        {
            try
            {
                await _vpnTenantIpNetworkInService.DeleteAsync(attachmentSetId.Value, tenantIpNetworkId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find a tenant IP network which is associated with the inbound policy of an attachment set by ID
        /// </summary>
        /// <remarks>Returns a single vpn tenant IP network</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="tenantIpNetworkId">ID of the tenant IP network</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/inbound-policy/tenant-ip-networks/{tenantIpNetworkId}", Name = "GetVpnTenantIpNetworkIn")]
        [ValidateModelState]
        [ValidateVpnTenantIpNetworkInExists]
        [SwaggerOperation("GetAttachmentSetInboundPolicyTenantIpNetwork")]
        [SwaggerResponse(statusCode: 200, type: typeof(VpnTenantIpNetworkIn), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAttachmentSetInboundPolicyTenantIpNetwork([FromRoute][Required]int? attachmentSetId, 
            [FromRoute][Required]int? tenantIpNetworkId, [FromQuery]bool? deep)
        {
            var vpnTenantIpNetworkIn = await _vpnTenantIpNetworkInService.GetByAttachmentSetIDAndTenantIpNetworkIDAsync(attachmentSetId.Value, tenantIpNetworkId.Value, deep: deep);
            if (vpnTenantIpNetworkIn.HasBeenModified(Request))
            {
                vpnTenantIpNetworkIn.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<VpnTenantIpNetworkIn>(vpnTenantIpNetworkIn));
        }
    }
}

/*
 * MIND API
 *
 * This is the Master Inventory Network Database (MIND) API. MIND provides automated allocation of technical attributes needed to create IP and Ethernet VPNs on the global Sigma network. MIND supports the 'Nova' services specfication which defines the collection of connectivity services supported by ENT. Go to https://thehub.thomsonreuters.com/docs/DOC-2193014 to learn more.
 *
 * OpenAPI spec version: 1.0.0
 * Contact: jonathan.beasley@thomsonreuters.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Mind.Api.Attributes;
using Mind.Api.Models;
using AutoMapper;
using Mind.Services;
using Mind.Builders;
using Microsoft.EntityFrameworkCore;
using Mind.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Mind.Api.Controllers
{ 
    /// <summary>
    /// API for creating and managing the lifecycle of vpn attachment sets, i.e. attachment set associations with a vpn
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Provider Domain Attachment Set To VPN Bindings")]
    public class VpnAttachmentSetApiController : BaseApiController
    {
        private readonly IVpnAttachmentSetService _vpnAttachmentSetService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vpnAttachmentSetService"></param>
        /// <param name="mapper"></param>
        public VpnAttachmentSetApiController(IVpnAttachmentSetService vpnAttachmentSetService, IMapper mapper) : base(vpnAttachmentSetService, mapper)
        {
            _vpnAttachmentSetService = vpnAttachmentSetService;
        }

        /// <summary>
        /// Create a new association between a vpn and an attachment set
        /// </summary>

        /// <param name="vpnId">ID of the vpn</param>
        /// <param name="body">vpn attachment set request object that generates a new vpn attachment set</param>
        /// <response code="201">successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/vpns/{vpnId}/attachment-sets")]
        [ValidateModelState]
        [ValidateVpnExists]
        [SwaggerOperation("CreateVpnAttachmentSet")]
        [SwaggerResponse(statusCode: 201, type: typeof(VpnAttachmentSet), description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public async virtual Task<IActionResult> CreateAttachmentSet([FromRoute][Required]int? vpnId, [FromBody]VpnAttachmentSetRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.VpnAttachmentSetRequest>(body);
                var vpnAttachmentSet = await _vpnAttachmentSetService.AddAsync(vpnId.Value, request);
                var vpnAttachmentSetApiModel = Mapper.Map<Mind.Api.Models.VpnAttachmentSet>(vpnAttachmentSet);
                return CreatedAtRoute("GetVpnAttachmentSet", new { vpnId = vpnAttachmentSet.VpnID, attachmentSetId = vpnAttachmentSet.AttachmentSetID },
                    vpnAttachmentSetApiModel);
            }

            catch (BuilderBadArgumentsException ex)
            {
                return new BadArgumentsResult(ex.Message);
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
        /// Update an existing vpn attachment set
        /// </summary>

        ///<param name="vpnId">ID of the vpn</param>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="body">attachment set update object that updates an existing attachment set</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/vpns/{vpnId}/attachment-sets/{attachmentSetId}")]
        [ValidateModelState]
        [ValidateVpnAttachmentSetExists]
        [SwaggerOperation("UpdateVpnAttachmentSet")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateVpnAttachmentSet([FromRoute][Required]int? vpnId,
            [FromRoute][Required]int? attachmentSetId, [FromBody]Mind.Api.Models.VpnAttachmentSetUpdate body)
        {
            try
            {
                var item = await _vpnAttachmentSetService.GetByVpnIDAndAttachmentSetIDAsync(vpnId.Value, attachmentSetId.Value);
                if (item.HasPreconditionFailed(Request))  return new PreconditionFailedResult();
                
                var update = Mapper.Map<Mind.Models.RequestModels.VpnAttachmentSetUpdate>(body);
                var vpnAttachmentSet = await _vpnAttachmentSetService.UpdateAsync(vpnId.Value, attachmentSetId.Value, update);
                vpnAttachmentSet.SetModifiedHttpHeaders(Response);

                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (BuilderBadArgumentsException ex)
            {
                return new BadArgumentsResult(ex.Message);
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
        /// Deletes a vpn attachment set
        /// </summary>

        /// <param name="vpnId">ID of the vpn</param>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation failed</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/vpns/{vpnId}/attachment-sets/{attachmentSetId}")]
        [ValidateModelState]
        [ValidateVpnAttachmentSetExists]
        [SwaggerOperation("DeleteVpnAttachmentSet")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation failed")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteAttachmentSet([FromRoute][Required]int? attachmentSetId,[FromRoute][Required]int? vpnId)
        {
            try
            {
                await _vpnAttachmentSetService.DeleteAsync(vpnId.Value, attachmentSetId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (ServiceValidationException)
            {
                return new ValidationFailedResult(this.ModelState);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find a vpn attachment set
        /// </summary>
        /// <remarks>Returns a single vpn attachment set</remarks>
        /// <param name="vpnId">ID of the vpn</param>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/vpns/{vpnId}/attachment-sets/{attachmentSetId}", Name="GetVpnAttachmentSet")]
        [ValidateModelState]
        [ValidateVpnAttachmentSetExists]
        [SwaggerOperation("GetVpnAttachmentSet")]
        [SwaggerResponse(statusCode: 200, type: typeof(VpnAttachmentSet), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetVpnAttachmentSet([FromRoute][Required]int? vpnId, [FromRoute][Required]int? attachmentSetId,
            [FromQuery]bool? deep)
        {
            var vpnAttachmentSet = await _vpnAttachmentSetService.GetByVpnIDAndAttachmentSetIDAsync(vpnId.Value, attachmentSetId.Value, deep);
            if (vpnAttachmentSet.HasBeenModified(Request))
            {
                vpnAttachmentSet.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<VpnAttachmentSet>(vpnAttachmentSet));
        }

        /// <summary>
        /// Find all vpn attachment sets for a given vpn
        /// </summary>
        /// <remarks>Returns all vpn attachment sets for a given vpn</remarks>
        /// <param name="vpnId">ID of the vpn</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/vpns/{vpnId}/attachment-sets")]
        [ValidateModelState]
        [ValidateVpnExists]
        [SwaggerOperation("GetVpnAttachmentSetsByVpnId")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<VpnAttachmentSet>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetVpnAttachmentSetsByVpnId([FromRoute][Required]int? vpnId, [FromQuery]bool? deep)
        {
            var vpnAttachmentSets = await _vpnAttachmentSetService.GetAllByVpnIDAsync(vpnId.Value, deep);
            return Ok(Mapper.Map<List<VpnAttachmentSet>>(vpnAttachmentSets));
        }
    }
}

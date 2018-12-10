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
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using Mind.Services;
using Mind.Builders;
using Microsoft.EntityFrameworkCore;
using Mind.Models;
using IO.NovaAttSwagger.Client;

namespace Mind.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Provider Domain Tenant Virtual Interfaces")]
    public class ProviderDomainVifApiController : BaseApiController
    {
        private readonly IProviderDomainVifService _vifService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vifService"></param>
        /// <param name="mapper"></param>
        public ProviderDomainVifApiController(IProviderDomainVifService vifService, IMapper mapper) : base(vifService, mapper)
        {
            _vifService = vifService;
        }

        /// <summary>
        /// Create a new vif
        /// </summary>

        /// <param name="attachmentId">ID of the attachment under which the new vif will be created</param>
        /// <param name="body">vif request object that generates a new vif</param>
        /// <param name="syncToNetwork">Sync changes with the network</param>
        /// <response code="201">Successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/provider-attachments/{attachmentId}/vifs")]
        [ValidateModelState]
        [ValidateProviderDomainAttachmentExists]
        [SwaggerResponse(statusCode: 201, type: typeof(ProviderDomainVif), description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> CreateTenantProviderDomainVif([FromRoute][Required]int? attachmentId, [FromBody]ProviderDomainVifRequest body,
        [FromQuery] bool? syncToNetwork)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.ProviderDomainVifRequest>(body);
                var vif = await _vifService.AddAsync(attachmentId.Value, request, syncToNetwork.GetValueOrDefault());
                var vifApiModel = Mapper.Map<Mind.Api.Models.ProviderDomainVif>(vif);
                return CreatedAtRoute("GetProviderDomainVif", new { vifId = vif.VifID }, vifApiModel);
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

            catch (ServiceBadArgumentsException ex)
            {
                return new BadArgumentsResult(ex.Message);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }

            catch (ApiException) 
            {
                return new NetworkUpdateFailedResult();
            }
        }

        /// <summary>
        /// Deletes a vif
        /// </summary>

        /// <param name="attachmentId">ID of the attachment</param>
        /// <param name="vifId">ID of the vif</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation failed</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/provider-attachments/{attachmentId}/vifs/{vifId}")]
        [ValidateModelState]
        [ValidateProviderDomainVifExists]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation failed")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteProviderDomainVif([FromRoute][Required]int attachmentId, [FromRoute][Required]int? vifId)
        {
            try
            {
                await _vifService.DeleteAsync(vifId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (IllegalDeleteAttemptException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }

            catch (ApiException)
            {
                return new NetworkUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find all provider domain vifs for a given attachment
        /// </summary>
        /// <remarks>Returns all vifs for a given attachment</remarks>
        /// <param name="attachmentId">ID of the attachment</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/provider-attachments/{attachmentId}/vifs")]
        [ValidateModelState]
        [ValidateProviderDomainAttachmentExists]
        [SwaggerOperation("GetProviderDomainVifsByAttachmentId")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ProviderDomainVif>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetProviderDomainVifsByAttachmentId([FromRoute][Required]int? attachmentId, [FromQuery]bool? deep)
        {
            var vifs = await _vifService.GetAllByAttachmentIDAsync(attachmentId.Value, deep);
            return Ok(Mapper.Map<List<ProviderDomainVif>>(vifs));
        }

        /// <summary>
        /// Find vif by ID
        /// </summary>
        /// <remarks>Returns a single vif</remarks>
        /// <param name="attachmentId"></param>
        /// <param name="vifId">ID of the vif</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/provider-attachments/{attachmentId}/vifs/{vifId}", Name ="GetProviderDomainVif")]
        [ValidateModelState]
        [ValidateProviderDomainVifExists]
        [SwaggerOperation("GetProviderDomainVifById")]
        [SwaggerResponse(statusCode: 200, type: typeof(ProviderDomainVif), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public async virtual Task<IActionResult> GetProvideDomainVifById([FromRoute][Required]int? attachmentId, [FromRoute][Required]int? vifId,[FromQuery]bool? deep)
        {
            var vif = await _vifService.GetByIDAsync(vifId.Value, deep);
            if (vif.HasBeenModified(Request))
            {
                vif.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<ProviderDomainVif>(vif));
        }

        /// <summary>
        /// Update an existing vif
        /// </summary>

        /// <param name="attachmentId">ID of the attachment</param>
        /// <param name="vifId">ID of the vif</param>
        /// <param name="body">vif update object that updates an existing vif</param>
        /// <param name="syncToNetwork">Sync changes with the network</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/provider-attachments/{attachmentId}/vifs/{vifId}")]
        [ValidateModelState]
        [ValidateProviderDomainVifExists]
        [SwaggerOperation("UpdateProviderDomainVif")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateProviderDomainVif([FromRoute][Required]int? attachmentId,
            [FromRoute][Required]int? vifId, [FromBody]Mind.Api.Models.ProviderDomainVifUpdate body, [FromQuery] bool? syncToNetwork)
        { 
            try
            {
                var item = await _vifService.GetByIDAsync(vifId.Value, asTrackable: false);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                var update = Mapper.Map<Mind.Models.RequestModels.ProviderDomainVifUpdate>(body);
                var vif = await _vifService.UpdateAsync(vifId.Value, update, syncToNetwork.GetValueOrDefault());
                vif.SetModifiedHttpHeaders(Response);

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

            catch (ServiceBadArgumentsException ex)
            {
                return new BadArgumentsResult(ex.Message);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }

            catch (ApiException)
            {
                return new NetworkUpdateFailedResult();
            }
        }

        /// <summary>
        /// Sync a vif to the network.
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="vifId">The ID of the vif</param>
        /// <response code="204">Successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database or the network</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/provider-attachments/{attachmentId}/vifs/{vifId}/sync")]
        [ValidateModelState]
        [ValidateProviderDomainVifExists]
        [SwaggerOperation("SyncProviderDomainVif")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database or the network")]
        public async Task<IActionResult> SyncToNetwork([FromRoute][Required]int? vifId)
        {
            try
            {
                await _vifService.SyncToNetworkPutAsync(vifId.Value);
            }

            catch (IllegalNetworkSyncAttemptException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (BuilderBadArgumentsException ex)
            {
                return new BadArgumentsResult(ex.Message);
            }

            catch (BuilderUnableToCompleteException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (ServiceBadArgumentsException ex)
            {
                return new BadArgumentsResult(ex.Message);
            }

            catch (ApiException)
            {
                return new NetworkUpdateFailedResult();
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}

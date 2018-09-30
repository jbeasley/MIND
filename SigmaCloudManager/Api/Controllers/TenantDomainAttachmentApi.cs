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
using Mind.Services;
using AutoMapper;
using SCM.Data;
using Microsoft.EntityFrameworkCore;
using Mind.Builders;
using Mind.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Mind.Api.Controllers
{ 
    /// <summary>
    /// Create and manage the lifecycle of attachments for devices within the tenant domain
    /// </summary>
    [ApiVersion("1.0")]
    public class TenantDomainAttachmentApiController : BaseApiController
    { 
        private readonly ITenantDomainAttachmentService _attachmentService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attachmentService"></param>
        /// <param name="mapper"></param>
        public TenantDomainAttachmentApiController(ITenantDomainAttachmentService attachmentService, IMapper mapper) : base(attachmentService, mapper)
        {
            _attachmentService = attachmentService;
        }

        /// <summary>
        /// Create a new attachment
        /// </summary>

        /// <param name="deviceId">ID of the device in the tenant domain</param>
        /// <param name="body">attachment request object that generates a new attachment</param>
        /// <response code="201">Successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/attachments")]
        [ValidateModelState]
        [ValidateTenantDomainDeviceExists]
        [SwaggerOperation("CreateTenantDomainAttachment")]
        [SwaggerResponse(statusCode: 201, type: typeof(TenantDomainAttachment), description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> CreateTenantDomainAttachment([FromRoute][Required]int? deviceId, [FromBody]Mind.Api.Models.TenantDomainAttachmentRequest body)
        {
            try
            {
                var request = Mapper.Map<SCM.Models.RequestModels.TenantDomainAttachmentRequest>(body);
                var attachment = await _attachmentService.AddAsync(deviceId.Value, request);
                var attachmentApiModel = Mapper.Map<Mind.Api.Models.TenantDomainAttachment>(attachment);
                return CreatedAtRoute("GetTenantDomainAttachment", new { attachmentId = attachment.AttachmentID }, attachmentApiModel);
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
        /// Deletes an attachment
        /// </summary>

        /// <param name="deviceId">ID of the device in the tenant domain</param>
        /// <param name="attachmentId">ID of the attachment</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation failed</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/attachments/{attachmentId}")]
        [ValidateModelState]
        [ValidateTenantDomainAttachmentExists]
        [SwaggerOperation("DeleteTenantDomainAttachment")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation failed")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteTenantDomainAttachment([FromRoute][Required]int? deviceId, [FromRoute][Required]int? attachmentId)
        {
            try
            {
                await _attachmentService.DeleteAsync(attachmentId.Value);
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
        }

        /// <summary>
        /// Find a tenant domain attachment by ID
        /// </summary>
        /// <remarks>Returns a single tenant domain attachment</remarks>
        /// <param name="deviceId">The ID of the device in the tenant domain</param>
        /// <param name="attachmentId">ID of the attachment</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/attachments/{attachmentId}", Name = "GetTenantDomainAttachment")]
        [ValidateModelState]
        [ValidateTenantDomainAttachmentExists]
        [SwaggerOperation("GetTenantDomainAttachmentById")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantDomainAttachment), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetTenantDomainAttachmentById([FromRoute][Required]int? deviceId, 
            [FromRoute][Required]int? attachmentId, [FromQuery]bool? deep)
        {
            var attachment = await _attachmentService.GetByIDAsync(attachmentId.Value, deep);
            if (attachment.HasBeenModified(Request))
            {
                attachment.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<TenantDomainAttachment>(attachment));
        }

        /// <summary>
        /// Find all tenant domain attachments for a given device
        /// </summary>
        /// <remarks>Returns all tenant domain attachments for a given device</remarks>
        /// <param name="deviceId">ID of the device in the tenant domain</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/attachments")]
        [ValidateModelState]
        [ValidateTenantDomainDeviceExists]
        [SwaggerOperation("GetTenantDomainAttachmentsByTenantId")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<TenantDomainAttachment>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public async virtual Task<IActionResult> GetTenantDomainAttachmentsByTenantId([FromRoute][Required]int? deviceId,[FromQuery]bool? deep)
        {
            var attachments = await _attachmentService.GetAllByDeviceIDAsync(deviceId.Value, deep);
            return Ok(Mapper.Map<List<TenantDomainAttachment>>(attachments));
        }

        /// <summary>
        /// Update an existing attachment
        /// </summary>

        /// <param name="deviceId">The ID of the device in the tenant domain</param>
        /// <param name="attachmentId">ID of the attachment</param>
        /// <param name="body">attachment update object that updates an existing attachment</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/tenant-domain-devices/{deviceId}/attachments/{attachmentId}")]
        [ValidateModelState]
        [ValidateTenantDomainAttachmentExists]
        [SwaggerOperation("UpdateTenantDomainAttachment")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateTenantDomainAttachment([FromRoute][Required]int? deviceId, 
            [FromRoute][Required]int? attachmentId, [FromBody]Mind.Api.Models.TenantDomainAttachmentUpdate body)
        {
            try
            {
                var item = await _attachmentService.GetByIDAsync(attachmentId.Value);
                if (item.HasPreconditionFailed(Request))
                {
                    return new PreconditionFailedResult();
                }

                var update = Mapper.Map<SCM.Models.RequestModels.TenantDomainAttachmentUpdate>(body);
                var attachment = await _attachmentService.UpdateAsync(attachmentId.Value, update);
                attachment.SetModifiedHttpHeaders(Response);

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
    }
}

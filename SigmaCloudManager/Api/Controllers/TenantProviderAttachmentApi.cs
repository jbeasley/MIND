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
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    public class ProviderDomainAttachmentApiController : BaseApiController
    { 
        private readonly IProviderDomainAttachmentService _attachmentService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attachmentService"></param>
        /// <param name="mapper"></param>
        public ProviderDomainAttachmentApiController(IProviderDomainAttachmentService attachmentService, IMapper mapper) : base(attachmentService, mapper)
        {
            _attachmentService = attachmentService;
        }

        /// <summary>
        /// Create a new attachment
        /// </summary>

        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="body">attachment request object that generates a new attachment</param>
        /// <response code="201">Successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/provider-attachments")]
        [ValidateModelState]
        [ValidateTenantExists]
        [SwaggerOperation("CreateProviderDomainAttachment")]
        [SwaggerResponse(statusCode: 201, type: typeof(Attachment), description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> CreateProviderDomainAttachment([FromRoute][Required]int? tenantId, [FromBody]Mind.Api.Models.ProviderDomainAttachmentRequest body)
        {
            try
            {
                var request = Mapper.Map<SCM.Models.RequestModels.ProviderDomainAttachmentRequest>(body);
                var attachment = await _attachmentService.AddAsync(tenantId.Value, request);
                var attachmentApiModel = Mapper.Map<Mind.Api.Models.Attachment>(attachment);
                return CreatedAtRoute("GetProviderDomainAttachment", new { attachmentId = attachment.AttachmentID }, attachmentApiModel);
            }

            catch (BuilderBadArgumentsException ex) 
            {
                return new BadArgumentsResult(ex.Message);
            }

            catch (BuilderUnableToCompleteException ex)
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

        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="attachmentId">ID of the attachment</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation failed</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/provider-attachments/{attachmentId}")]
        [ValidateProviderDomainAttachmentExists]
        [ValidateModelState]
        [SwaggerOperation("DeleteProviderDomainAttachment")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation failed")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteProviderDomainAttachment([FromRoute][Required]int? tenantId, [FromRoute][Required]int? attachmentId)
        {
            try
            {
                await _attachmentService.DeleteAsync(attachmentId.Value);
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
        /// Find a provider domain attachment by ID
        /// </summary>
        /// <remarks>Returns a single provider domain attachment</remarks>
        /// <param name="attachmentId">ID of the attachment</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/provider-attachments/{attachmentId}", Name = "GetProviderDomainAttachment")]
        [ValidateModelState]
        [ValidateProviderDomainAttachmentExists]
        [SwaggerOperation("GetProviderDomainAttachmentById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Attachment), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetProviderDomainAttachmentById([FromRoute][Required]int? tenantId, 
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

            return Ok(Mapper.Map<Attachment>(attachment));
        }

        /// <summary>
        /// Find all provider domain attachments for a given tenant
        /// </summary>
        /// <remarks>Returns all provider domain attachments for a given tenant</remarks>
        /// <param name="tenantId">ID of the tenant</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/provider-attachments")]
        [ValidateModelState]
        [ValidateTenantExists]
        [SwaggerOperation("GetProviderDomainAttachmentsByTenantId")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Attachment>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public async virtual Task<IActionResult> GetProviderDomainAttachmentsByTenantId([FromRoute][Required]int? tenantId,[FromQuery]bool? deep)
        {
            var attachments = await _attachmentService.GetAllByTenantIDAsync(tenantId.Value, deep);
            return Ok(Mapper.Map<List<Attachment>>(attachments));
        }

        /// <summary>
        /// Update an existing attachment
        /// </summary>

        /// <param name="attachmentId">ID of the attachment</param>
        /// <param name="body">attachment update object that updates an existing attachment</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPut]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/provider-attachments/{attachmentId}")]
        [ValidateModelState]
        [ValidateProviderDomainAttachmentExists]
        [SwaggerOperation("UpdateProviderDomainAttachment")]
        [SwaggerResponse(statusCode: 200, type: typeof(Attachment), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateProviderDomainAttachment([FromRoute][Required]int? tenantId, 
            [FromRoute][Required]int? attachmentId, [FromBody]Mind.Api.Models.ProviderDomainAttachmentUpdate body)
        {
            try
            {
                var item = await _attachmentService.GetByIDAsync(attachmentId.Value);
                if (item.HasPreconditionFailed(Request))
                {
                    return new PreconditionFailedResult();
                }

                var update = Mapper.Map<SCM.Models.RequestModels.ProviderDomainAttachmentUpdate>(body);
                var attachment = await _attachmentService.UpdateAsync(attachmentId.Value, update);
                attachment.SetModifiedHttpHeaders(Response);
                var attachmentApiModel = Mapper.Map<Mind.Api.Models.Attachment>(attachment);
                return Ok(attachmentApiModel);
            }

            catch (BuilderBadArgumentsException ex)
            {
                return new BadArgumentsResult(ex.Message);
            }

            catch (BuilderUnableToCompleteException ex)
            {
                return new ValidationFailedResult(ex.Message);
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
    }
}

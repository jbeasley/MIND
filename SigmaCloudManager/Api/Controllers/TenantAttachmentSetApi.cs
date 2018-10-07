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
    /// API for creating and managing the lifecycle of attachment sets
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Provider Domain Tenant Attachment Sets")]
    public class TenantAttachmentSetApiController : BaseApiController
    {
        private readonly IAttachmentSetService _attachmentSetService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attachmentSetService"></param>
        /// <param name="mapper"></param>
        public TenantAttachmentSetApiController(IAttachmentSetService attachmentSetService, IMapper mapper) : base(attachmentSetService, mapper)
        {
            _attachmentSetService = attachmentSetService;
        }

        /// <summary>
        /// Create a new attachment set
        /// </summary>

        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="body">attachment set request object that generates a new attachment set</param>
        /// <response code="201">successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/attachment-sets")]
        [ValidateModelState]
        [ValidateTenantExists]
        [SwaggerOperation("CreateAttachmentSet")]
        [SwaggerResponse(statusCode: 201, type: typeof(AttachmentSet), description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public async virtual Task<IActionResult> CreateAttachmentSet([FromRoute][Required]int? tenantId, [FromBody]AttachmentSetRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.AttachmentSetRequest>(body);
                var attachmentSet = await _attachmentSetService.AddAsync(tenantId.Value, request);
                var attachmentSetApiModel = Mapper.Map<Mind.Api.Models.AttachmentSet>(attachmentSet);
                return CreatedAtRoute("GetAttachmentSet", new { attachmentSetId = attachmentSet.AttachmentSetID }, attachmentSetApiModel);
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
        /// Update an existing attachment set
        /// </summary>

        ///<param name="tenantId"></param>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="body">attachment set update object that updates an existing attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/attachment-sets/{attachmentSetId}")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("UpdateAttachmentSet")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateAttachmentSet([FromRoute][Required]int? tenantId,
            [FromRoute][Required]int? attachmentSetId, [FromBody]Mind.Api.Models.AttachmentSetUpdate body)
        {
            try
            {
                var item = await _attachmentSetService.GetByIDAsync(attachmentSetId.Value);
                if (item.HasPreconditionFailed(Request))  return new PreconditionFailedResult();
                
                var update = Mapper.Map<Mind.Models.RequestModels.AttachmentSetUpdate>(body);
                var attachmentSet = await _attachmentSetService.UpdateAsync(attachmentSetId.Value, update);
                attachmentSet.SetModifiedHttpHeaders(Response);

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
        /// Deletes an attachment set
        /// </summary>

        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation failed</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/attachment-sets/{attachmentSetId}")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("DeleteAttachmentSet")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation failed")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteAttachmentSet([FromRoute][Required]int? attachmentSetId,[FromRoute][Required]int? tenantId)
        {
            try
            {
                await _attachmentSetService.DeleteAsync(attachmentSetId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (ServiceValidationException)
            {
                return new ValidationFailedResult(this.ModelState);
            }

            catch (DbUpdateException ex)
            {
                return new DatabaseUpdateFailedResult();
            }
        } 

        /// <summary>
        /// Find an attachment set by ID
        /// </summary>
        /// <remarks>Returns a single attachment set</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/attachment-sets/{attachmentSetId}", Name="GetAttachmentSet")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("GetAttachmentSetById")]
        [SwaggerResponse(statusCode: 200, type: typeof(AttachmentSet), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAttachmentSetById([FromRoute][Required]int? attachmentSetId, [FromQuery]bool? deep)
        {
            var attachmentSet = await _attachmentSetService.GetByIDAsync(attachmentSetId.Value, deep);
            if (attachmentSet.HasBeenModified(Request))
            {
                attachmentSet.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<AttachmentSet>(attachmentSet));
        }

        /// <summary>
        /// Validate the redundancy configuration of an attachment set
        /// </summary>
        /// <remarks>Returns an ApiResponse object with a message indicating the current state of the redundancy configuration
        /// of the attachment set </remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/attachment-sets/{attachmentSetId}/validate-redundancy")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("ValidateAttachmenSet")]
        [SwaggerResponse(statusCode: 200, type: typeof(ApiResponse), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> ValidateAttachmentSetRedundancy([FromRoute][Required]int? attachmentSetId)
        {
            var attachmentSet = await _attachmentSetService.GetByIDAsync(attachmentSetId.Value, deep: true);

            try
            {
                attachmentSet.ValidateAttachmentRedundancy();
                return Ok(new ApiResponse {
                    Code = "ValidationSuccess",
                    Message = "The attachment set redundancy is configured correctly."
                });
            }

            catch (IllegalStateException ex)
            {
                return Ok(new ApiResponse
                {
                    Code = "ValidationFailure",
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// Find all attachment sets for a given tenant
        /// </summary>
        /// <remarks>Returns all attachment sets for a given tenant</remarks>
        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/attachment-sets")]
        [ValidateModelState]
        [ValidateTenantExists]
        [SwaggerOperation("GetAttachmentSetsByTenantId")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<AttachmentSet>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAttachmentSetsByTenantId([FromRoute][Required]int? tenantId, [FromQuery]bool? deep)
        {
            var attachmentSets = await _attachmentSetService.GetAllByTenantIDAsync(tenantId.Value, deep);
            return Ok(Mapper.Map<List<AttachmentSet>>(attachmentSets));
        }
    }
}

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
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    public class TenantAttachmentSetRoutingInstanceApiController : BaseApiController
    {
        private readonly IAttachmentSetRoutingInstanceService _attachmentSetRoutingInstanceService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attachmentSetRoutingInstanceService"></param>
        /// <param name="mapper"></param>
        public TenantAttachmentSetRoutingInstanceApiController(IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService, IMapper mapper) : 
            base(attachmentSetRoutingInstanceService, mapper)
        {
            _attachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
        }

        /// <summary>
        /// Add a routing instance to a given attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="body">request object that generates a new routing instance entry for an attachment set</param>
        /// <response code="201">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/routing-instances")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("AddAttachmentSetRoutingInstance")]
        [SwaggerResponse(statusCode: 201, type: typeof(AttachmentSetRoutingInstance), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> AddAttachmentSetRoutingInstance([FromRoute][Required]int? attachmentSetId, 
            [FromBody][Required]RoutingInstanceForAttachmentSetRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.RoutingInstanceForAttachmentSetRequest>(body);
                var attachmentSetRoutingInstance = await _attachmentSetRoutingInstanceService.AddAsync(attachmentSetId.Value, request);
                var attachmentSetRoutingInstanceApiModel = Mapper.Map<Mind.Api.Models.AttachmentSetRoutingInstance>(attachmentSetRoutingInstance);
                return CreatedAtRoute("GetAttachmentSetRoutingInstance", 
                    new
                    {
                        attachmentSetId = attachmentSetRoutingInstance.AttachmentSetID,
                        routingInstanceId = attachmentSetRoutingInstance.RoutingInstanceID
                    }, 
                    attachmentSetRoutingInstanceApiModel);
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
        /// Update an existing routing instance association with an attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="routingInstanceId">ID of the routing instance to update</param>
        /// <param name="body">Attachmment set routing instance request object that applies updates to an existing attachment set routing instance</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/routing-instances/{routingInstanceId}")]
        [ValidateModelState]
        [ValidateAttachmentSetRoutingInstanceExists]
        [SwaggerOperation("UpdateAttachmentSetRoutingInstance")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateAttachmentSetRoutingInstance([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? routingInstanceId,
            [FromBody]RoutingInstanceForAttachmentSetUpdate body)
        {
            try
            {
                var item = await _attachmentSetRoutingInstanceService.GetByAttachmentSetIDAndRoutingInstanceIDAsync(attachmentSetId.Value, routingInstanceId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                Mapper.Map(body, item);
                item.AttachmentSetID = attachmentSetId.Value;
                item.RoutingInstanceID = routingInstanceId.Value;
                var attachmentSetRoutingInstance = await _attachmentSetRoutingInstanceService.UpdateAsync(item);
                attachmentSetRoutingInstance.SetModifiedHttpHeaders(Response);

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
        /// Delete a routing instance from an attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="routingInstanceId">ID of the routing instance</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation failed</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/routing-instances/{routingInstanceId}")]
        [ValidateModelState]
        [ValidateAttachmentSetRoutingInstanceExists]
        [SwaggerOperation("DeleteAttachmentSetRoutingInstance")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation failed")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteAttachmentSetRoutingInstance([FromRoute][Required]int? attachmentSetId, 
            [FromRoute][Required]int? routingInstanceId)
        {
            try
            {
                await _attachmentSetRoutingInstanceService.DeleteAsync(attachmentSetId.Value, routingInstanceId.Value);
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
        /// Find an attachment set routing instance by ID
        /// </summary>
        /// <remarks>Returns a single attachment set routing instance</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="routingInstanceId">ID of the routing instance</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/routing-instances/{routingInstanceId}", Name = "GetAttachmentSetRoutingInstance")]
        [ValidateModelState]
        [ValidateAttachmentSetRoutingInstanceExists]
        [SwaggerOperation("GetAttachmentSetRoutingInstanceById")]
        [SwaggerResponse(statusCode: 200, type: typeof(AttachmentSet), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAttachmentSetRoutingInstanceById([FromRoute][Required]int? attachmentSetId,
            [FromRoute][Required]int? routingInstanceId, [FromQuery]bool? deep)
        {
            var attachmentSetRoutingInstance = await _attachmentSetRoutingInstanceService.GetByAttachmentSetIDAndRoutingInstanceIDAsync(attachmentSetId.Value, 
                routingInstanceId.Value, deep);

            if (attachmentSetRoutingInstance.HasBeenModified(Request))
            {
                attachmentSetRoutingInstance.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<AttachmentSetRoutingInstance>(attachmentSetRoutingInstance));
        }
    }
}

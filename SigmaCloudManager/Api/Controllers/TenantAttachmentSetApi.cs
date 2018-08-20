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

namespace Mind.Api.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    public class TenantAttachmentSetApiController : BaseApiController
    {
        private readonly IAttachmentSetService _attachmentSetService;
        private readonly IAttachmentSetRoutingInstanceService _attachmentSetRoutingInstanceService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attachmentSetService"></param>
        /// <param name="mapper"></param>
        public TenantAttachmentSetApiController(IAttachmentSetService attachmentSetService, 
            IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService, IMapper mapper) : base(attachmentSetService, mapper)
        {
            _attachmentSetService = attachmentSetService;
            _attachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
        }

        /// <summary>
        /// Create a new attachment set
        /// </summary>

        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="body">attachment set request object that generates a new attachment set</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/attachment-sets")]
        [ValidateModelState]
        [ValidateTenantExists]
        [SwaggerOperation("CreateAttachmentSet")]
        [SwaggerResponse(statusCode: 201, type: typeof(Attachment), description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 400, type: typeof(ApiResponse), description: "Bad request")]
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

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Add a tenant IPv4 network to the inbound policy of a given attachment set
        /// </summary>
        
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="tenantIpv4NetworkId">ID of the tenant IPv4 network</param>
        /// <param name="body">request object that generates a new inbound policy entry for a tenant IPv4 network</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/inbound-policy/ipv4-network/{tenantIpv4NetworkId}")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("AddAttachmentSetInboundPolicyTenantIpv4Network")]
        [SwaggerResponse(statusCode: 204, type: typeof(ApiResponse), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult AddAttachmentSetInboundPolicyTenantIpv4Network([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? tenantIpv4NetworkId, [FromBody]VpnTenantIpv4NetworkIn body)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<null>\n  <code>123</code>\n  <type>aeiou</type>\n  <message>aeiou</message>\n</null>";
            exampleJson = "{\n  \"code\" : 0,\n  \"type\" : \"type\",\n  \"message\" : \"message\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<ApiResponse>(exampleJson)
            : default(ApiResponse);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Add a tenant IPv4 network to the outbound policy of a given attachment set
        /// </summary>
        
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="tenantIpv4NetworkId">ID of the tenant IPv4 network</param>
        /// <param name="body">request object that generates a new outbound policy entry for an tenant IPv4 network</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/attachment-set/{attachmentSetId}/outbound-policy/ipv4-network/{tenantIpv4NetworkId}")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("AddAttachmentSetOutboundPolicyTenantIpv4Network")]
        [SwaggerResponse(statusCode: 204, type: typeof(ApiResponse), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult AddAttachmentSetOutboundPolicyTenantIpv4Network([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? tenantIpv4NetworkId, [FromBody]VpnTenantIpv4NetworkOut body)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<null>\n  <code>123</code>\n  <type>aeiou</type>\n  <message>aeiou</message>\n</null>";
            exampleJson = "{\n  \"code\" : 0,\n  \"type\" : \"type\",\n  \"message\" : \"message\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<ApiResponse>(exampleJson)
            : default(ApiResponse);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Update an existing attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="body">attachment set update object that updates an existing attachment set</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPut]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/attachment-sets/{attachmentSetId}")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("UpdateAttachmentSet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Attachment), description: "Successful operation")]
        [SwaggerResponse(statusCode: 400, type: typeof(ApiResponse), description: "Bad arguments")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateAttachmentSet([FromRoute][Required]int? tenantId,
            [FromRoute][Required]int? attachmentSetId, [FromBody]Mind.Api.Models.AttachmentSetUpdate body)
        {
            try
            {
                var item = await _attachmentSetService.GetByIDAsync(attachmentSetId.Value);
                if (item.HasPreconditionFailed(Request))
                {
                    return new PreconditionFailedResult();
                }

                var update = Mapper.Map<Mind.Models.RequestModels.AttachmentSetUpdate>(body);
                var attachmentSet = await _attachmentSetService.UpdateAsync(attachmentSetId.Value, update);
                var attachmentSetApiModel = Mapper.Map<Mind.Api.Models.AttachmentSet>(attachmentSet);
                return Ok(attachmentSetApiModel);
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
        public virtual async Task<IActionResult> DeleteAttachmentSet([FromRoute][Required]int? attachmentSetId)
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

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Delete a tenant IPv4 network from the inbound policy of a given attachment set
        /// </summary>
        
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="tenantIpv4NetworkId">ID of the tenant IPv4 network</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/inbound-policy/ipv4-networks/{tenantIpv4NetworkId}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteAttachmentSetInboundPolicyTenantIpv4Network")]
        [SwaggerResponse(statusCode: 204, type: typeof(ApiResponse), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult DeleteAttachmentSetInboundPolicyTenantIpv4Network([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? tenantIpv4NetworkId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<null>\n  <code>123</code>\n  <type>aeiou</type>\n  <message>aeiou</message>\n</null>";
            exampleJson = "{\n  \"code\" : 0,\n  \"type\" : \"type\",\n  \"message\" : \"message\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<ApiResponse>(exampleJson)
            : default(ApiResponse);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Delete a tenant IPv4 network from the outbound policy of a given attachment set
        /// </summary>
        
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="tenantIpv4NetworkId">ID of the tenant IPv4 network</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/outbound-policy/ipv4-networks/{tenantIpv4NetworkId}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteAttachmentSetOutboundPolicyTenantIpv4Network")]
        [SwaggerResponse(statusCode: 204, type: typeof(ApiResponse), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult DeleteAttachmentSetOutboundPolicyTenantIpv4Network([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? tenantIpv4NetworkId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<null>\n  <code>123</code>\n  <type>aeiou</type>\n  <message>aeiou</message>\n</null>";
            exampleJson = "{\n  \"code\" : 0,\n  \"type\" : \"type\",\n  \"message\" : \"message\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<ApiResponse>(exampleJson)
            : default(ApiResponse);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Find an attachment set by ID
        /// </summary>
        /// <remarks>Returns a single attachment set</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
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
        /// Find all attachment sets for a given tenant
        /// </summary>
        /// <remarks>Returns all attachment sets for a given tenant</remarks>
        /// <param name="tenantId">ID of the tenant</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/attachment-sets")]
        [ValidateModelState]
        [ValidateTenantExists]
        [SwaggerOperation("GetAttachmentSetsByTenantId")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Attachment>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAttachmentSetsByTenantId([FromRoute][Required]int? tenantId, [FromQuery]bool? deep)
        {
            var attachmentSets = await _attachmentSetService.GetAllByTenantIDAsync(tenantId.Value, deep);
            return Ok(Mapper.Map<List<AttachmentSet>>(attachmentSets));
        }
    }
}

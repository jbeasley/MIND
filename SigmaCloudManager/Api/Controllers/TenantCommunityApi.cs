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
using Microsoft.EntityFrameworkCore;
using Mind.Services;
using AutoMapper;

using Mind.Models;
using Swashbuckle.AspNetCore.Annotations;
using Mind.Builders;

namespace Mind.Api.Controllers
{ 
    /// <summary>
    /// API for creating and managing the lifecycle of tenant communities.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Tenant Communities")]
    public class TenantCommunityApiController : BaseApiController
    {
        private readonly ITenantCommunityService _tenantCommunityService;

        public TenantCommunityApiController(ITenantCommunityService tenantCommunityService, IMapper mapper): base(tenantCommunityService, mapper)
        {
            _tenantCommunityService = tenantCommunityService;
        }

        /// <summary>
        /// Create a new tenant community
        /// </summary>

        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="body">Community request object that generates a new tenant community</param>
        /// <response code="201">Successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/communities")]
        [ValidateModelState]
        [ValidateTenantExists]
        [SwaggerOperation("CreateTenantCommunity")]
        [SwaggerResponse(statusCode: 201, type: typeof(TenantCommunity), description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "Resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> CreateTenantCommunity([FromRoute][Required]int? tenantId, [FromBody]TenantCommunityRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.TenantCommunityRequest>(body);
                var tenantCommunity = await _tenantCommunityService.AddAsync(tenantId.Value, request);
                var tenantCommunityApiModel = Mapper.Map<Mind.Api.Models.TenantCommunity>(tenantCommunity);
                return CreatedAtRoute("GetTenantCommunity", new { tenantCommunityId = tenantCommunity.TenantCommunityID }, tenantCommunityApiModel);
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

            catch (DbUpdateException ex)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Update a tenant community
        /// </summary>

        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="tenantCommunityId">ID of the tenant community to update</param>
        /// <param name="body">Community request object that applies updates to an existing community</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/communities/{tenantCommunityId}")]
        [ValidateModelState]
        [ValidateTenantCommunityExists]
        [SwaggerOperation("UpdateTenantCommunity")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateTenantCommunity([FromRoute][Required]int? tenantId,[FromRoute][Required]int? tenantCommunityId, 
            [FromBody]TenantCommunityRequest body)
        {
            try
            {
                var item = await _tenantCommunityService.GetByIDAsync(tenantCommunityId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();
                
                var update = Mapper.Map<Mind.Models.RequestModels.TenantCommunityRequest>(body);
                var tenantCommunity = await _tenantCommunityService.UpdateAsync(tenantCommunityId.Value, update);
                tenantCommunity.SetModifiedHttpHeaders(Response);

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
        /// Deletes a tenant community
        /// </summary>

        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="tenantCommunityId">ID of the tenant community</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/communities/{tenantCommunityId}")]
        [ValidateModelState]
        [ValidateTenantCommunityExists]
        [SwaggerOperation("DeleteTenantCommunity")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation failed")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteTenantIpv4Network([FromRoute][Required]int? tenantId, [FromRoute][Required]int? tenantCommunityId)
        {
            try
            {
                await _tenantCommunityService.DeleteAsync(tenantCommunityId.Value);
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
        /// Find all communities for a given tenant
        /// </summary>
        /// <remarks>Returns all communities for a given tenant</remarks>
        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/communities")]
        [ValidateModelState]
        [ValidateTenantExists]
        [SwaggerOperation("GetCommunitysByTenantId")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<TenantCommunity>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetCommunitysByTenantId([FromRoute][Required]int? tenantId,[FromQuery]bool? deep)
        {
            var tenantCommunitys = await _tenantCommunityService.GetAllByTenantIDAsync(tenantId.Value, deep: deep);
            return Ok(Mapper.Map<List<TenantCommunity>>(tenantCommunitys));
        }

        /// <summary>
        /// Find a tenant community by ID
        /// </summary>
        /// <remarks>Returns a single tenant community</remarks>
        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="tenantCommunityId">ID of the tenant community</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/tenants/{tenantId}/communities/{tenantCommunityId}", Name ="GetTenantCommunity")]
        [ValidateModelState]
        [ValidateTenantCommunityExists]
        [SwaggerOperation("GetTenantCommunityById")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantCommunity), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetTenantCommunityById([FromRoute][Required]int? tenantId, 
            [FromRoute][Required]int? tenantCommunityId,[FromQuery] bool? deep)
        {
            var tenantCommunity = await _tenantCommunityService.GetByIDAsync(tenantCommunityId.Value, deep: deep);
            if (tenantCommunity.HasBeenModified(Request)) {
                tenantCommunity.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<TenantCommunity>(tenantCommunity));
        }
    }
}

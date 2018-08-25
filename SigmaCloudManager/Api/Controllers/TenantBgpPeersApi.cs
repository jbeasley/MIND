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
using Swashbuckle.AspNetCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Mind.Api.Attributes;
using Mind.Api.Models;
using AutoMapper;
using SCM.Services;
using Mind.Services;
using Microsoft.EntityFrameworkCore;
using Mind.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Mind.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    public class TenantBgpPeersApiController : BaseApiController
    {
        private readonly IBgpPeerService _bgpPeerService;

        public TenantBgpPeersApiController(IMapper mapper, IBgpPeerService bgpPeerService) : base(bgpPeerService, mapper)
        {
            _bgpPeerService = bgpPeerService;
        }

        /// <summary>
        /// Add a bgp peer to a given routing instance
        /// </summary>
        
        /// <param name="routingInstanceId">ID of the routing instance</param>
        /// <param name="body">request object that creates a new BGP peer entry</param>
        /// <response code="201">Successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/routing-instances/{routingInstanceId}/bgp-peers")]
        [ValidateModelState]
        [SwaggerOperation("CreateRoutingInstanceBgpPeer")]
        [SwaggerResponse(statusCode: 201, type: typeof(BgpPeer), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> CreateRoutingInstanceBgpPeer([FromRoute][Required]int? routingInstanceId, [FromBody]BgpPeerRequest body)
        {
            try
            {
                var request = Mapper.Map<SCM.Models.BgpPeer>(body);
                request.RoutingInstanceID = routingInstanceId.Value;
                var bgpPeer = await _bgpPeerService.AddAsync(request);
                var bgpPeerApiModel = Mapper.Map<Mind.Api.Models.BgpPeer>(bgpPeer);
                return CreatedAtRoute("GetRoutingInstanceBgpPeer", new { bgpPeerId = bgpPeer.BgpPeerID }, bgpPeerApiModel);
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
        /// Update a bgp peer
        /// </summary>

        /// <param name="routingInstanceId">ID of the tenant</param>
        /// <param name="bgpPeerId">ID of the bgp peer to update</param>
        /// <param name="body">BGP peer request object that applies updates to an existing bgp peer</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPut]
        [Route("/v{version:apiVersion}/routing-instances/{routingInstanceId}/bgp-peers/{bgpPeerId}")]
        [ValidateModelState]
        [ValidateBgpPeerExists]
        [SwaggerOperation("UpdateBgpPeer")]
        [SwaggerResponse(statusCode: 200, type: typeof(BgpPeer), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateBgpPeer([FromRoute][Required]int? routingInstanceId, [FromRoute][Required]int? bgpPeerId,
            [FromBody]BgpPeerRequest body)
        {
            try
            {
                var item = await _bgpPeerService.GetByIDAsync(bgpPeerId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                Mapper.Map(body, item);
                var bgpPeer = await _bgpPeerService.UpdateAsync(item);
                bgpPeer.SetModifiedHttpHeaders(Response);
                var bgpPeerApiModel = Mapper.Map<Mind.Api.Models.BgpPeer>(bgpPeer);

                return Ok(bgpPeerApiModel);
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
        /// Delete a BGP peer from a given routing instance
        /// </summary>

        /// <param name="routingInstanceId">ID of the routing instance</param>
        /// <param name="bgpPeerId">ID of the bgp peer</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/routing-instances/{routingInstanceId}/bgp-peers/{bgpPeerId}")]
        [ValidateModelState]
        [ValidateBgpPeerExists]
        [SwaggerOperation("DeleteRoutingInstanceBgpPeer")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation failed")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteRoutingInstanceBgpPeer([FromRoute][Required]int? routingInstanceId, [FromRoute][Required]int? bgpPeerId)
        {
            try
            {
                await _bgpPeerService.DeleteAsync(bgpPeerId.Value);
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
        /// Find all BGP peers for a given routing instance
        /// </summary>
        /// <remarks>Returns all BGP peers for a given routing instance</remarks>
        /// <param name="routingInstanceId">ID of the routing instance</param>
        /// <response code="200">successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/routing-instances/{routingInstanceId}/bgp-peers")]
        [ValidateModelState]
        [SwaggerOperation("GetAllBgpPeersByRoutingInstance")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<BgpPeer>), description: "successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAllBgpPeersByRoutingInstanceId([FromRoute][Required]int? routingInstanceId,[FromQuery]bool? deep)
        {
            var bgpPeers = await _bgpPeerService.GetAllByRoutingInstanceIDAsync(routingInstanceId.Value, deep: deep);
            return Ok(Mapper.Map<List<BgpPeer>>(bgpPeers));
        }

        /// <summary>
        /// Find a single BGP peer for a given routing instance
        /// </summary>
        /// <remarks>Returns a single BGP peer for a given routing instance and BGP peer ID</remarks>
        /// <param name="routingInstanceId">ID of the routing instance</param>
        /// <param name="bgpPeerId">ID of the BGP peer</param>
        /// <response code="200">successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [ValidateBgpPeerExists]
        [Route("/v{version:apiVersion}/routing-instances/{routingInstanceId}/bgp-peers/{bgpPeerId}", Name="GetRoutingInstanceBgpPeer")]
        [ValidateModelState]
        [SwaggerOperation("GetRoutingInstanceBgpPeerById")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<BgpPeer>), description: "successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetRoutingInstanceBgpPeerById([FromRoute][Required]int? routingInstanceId, [FromRoute][Required]int? bgpPeerId, [FromQuery]bool? deep)
        {
            var bgpPeer = await _bgpPeerService.GetByIDAsync(bgpPeerId.Value, deep: deep);
            if (bgpPeer.HasBeenModified(Request))
            {
                bgpPeer.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<BgpPeer>(bgpPeer));
        }
    }
}

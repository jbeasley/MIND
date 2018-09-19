using Microsoft.AspNetCore.Mvc;
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
using Swashbuckle.AspNetCore.Annotations;

namespace Mind.Api.Controllers
{
    [ApiVersion("1.0")]
    public class TenantAttachmentSetIpNetworkStaticRouteApiController : BaseApiController
    {
        private readonly IVpnTenantIpNetworkRoutingInstanceStaticRouteService _vpnTenantIpNetworkRoutingInstanceStaticRouteService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vpnTenantIpNetworkRoutingInstanceStaticRouteService"></param>
        /// <param name="mapper"></param>
        public TenantAttachmentSetIpNetworkStaticRouteApiController(IVpnTenantIpNetworkRoutingInstanceStaticRouteService vpnTenantIpNetworkRoutingInstanceStaticRouteService, IMapper mapper) : 
            base(vpnTenantIpNetworkRoutingInstanceStaticRouteService, mapper)
        {
            _vpnTenantIpNetworkRoutingInstanceStaticRouteService = vpnTenantIpNetworkRoutingInstanceStaticRouteService;
        }

        /// <summary>
        /// Add a static route for a tenant IP network to one or more routing instances of a given attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="body">request object that generates a new static route entry for a tenant IP network</param>
        /// <response code="201">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/static-routes/")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("AddAttachmentSetRoutingInstanceStaticRoute")]
        [SwaggerResponse(statusCode: 201, type: typeof(VpnTenantIpNetworkRoutingInstanceStaticRoute), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> AddAttachmentSetRoutingInstanceStaticRoute([FromRoute][Required]int? attachmentSetId, 
            [FromBody]VpnTenantIpNetworkRoutingInstanceStaticRouteRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.VpnTenantIpNetworkRoutingInstanceStaticRouteRequest>(body);
                var vpnTenantIpNetworkRoutingInstanceStaticRoute = await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.AddAsync(attachmentSetId.Value, request);
                var vpnTenantIpNetworkRoutingInstanceStaticRouteApiModel = Mapper.Map<Mind.Api.Models.VpnTenantIpNetworkRoutingInstanceStaticRoute>(vpnTenantIpNetworkRoutingInstanceStaticRoute);
                return CreatedAtRoute("GetAttachmentSetRoutingInstanceStaticRoute", new
                {
                    attachmentSetId = vpnTenantIpNetworkRoutingInstanceStaticRoute.AttachmentSetID,
                    vpnTenantIpNetworkRoutingInstanceStaticRouteId = vpnTenantIpNetworkRoutingInstanceStaticRoute.VpnTenantIpNetworkRoutingInstanceStaticRouteID
                }, vpnTenantIpNetworkRoutingInstanceStaticRouteApiModel);
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
        /// Update a static route for a tenant IP network which is associated with a routing instance of an attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantIpNetworkRoutingInstanceStaticRouteId">ID of the tenant IP network static route to update</param>
        /// <param name="body">Routing instance static route update object that applies updates to an existing static route</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/static-routes/{vpnTenantIpNetworkRoutingInstanceStaticRouteId}")]
        [ValidateModelState]
        [ValidateVpnTenantIpNetworkRoutingInstanceStaticRouteExists]
        [SwaggerOperation("UpdateAttachmentSetRoutingInstanceStaticRoute")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateAttachmentSetRoutingInstanceStaticRoute([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? vpnTenantIpNetworkRoutingInstanceStaticRouteId,
            [FromBody]VpnTenantIpNetworkRoutingInstanceStaticRouteUpdate body)
        {
            try
            {
                var item = await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.GetByIDAsync(vpnTenantIpNetworkRoutingInstanceStaticRouteId.Value);
                if (item.HasPreconditionFailed(Request)) return new PreconditionFailedResult();

                var update = Mapper.Map<Mind.Models.RequestModels.VpnTenantIpNetworkRoutingInstanceStaticRouteUpdate>(body);
                var vpnTenantIpNetworkRoutingInstanceStaticRoute = await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.UpdateAsync(item.VpnTenantIpNetworkRoutingInstanceStaticRouteID, update);
                vpnTenantIpNetworkRoutingInstanceStaticRoute.SetModifiedHttpHeaders(Response);

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
        /// Delete a static route for a tenant IP network from a given attachment set
        /// </summary>

        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantIpNetworkRoutingInstanceStaticRouteId">ID of the tenant IP network static route to delete</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/static-routes/{vpnTenantIpNetworkRoutingInstanceStaticRouteId}")]
        [ValidateModelState]
        [ValidateVpnTenantIpNetworkRoutingInstanceStaticRouteExists]
        [SwaggerOperation("DeleteAttachmentSetRoutingInstanceStaticRoute")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteAttachmentSetRoutingInstanceStaticRoute([FromRoute][Required]int? attachmentSetId, [FromRoute][Required]int? vpnTenantIpNetworkRoutingInstanceStaticRouteId)
        {
            try
            {
                await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.DeleteAsync(vpnTenantIpNetworkRoutingInstanceStaticRouteId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find a static route for a tenant IP network which is associated with an attachment set by ID
        /// </summary>
        /// <remarks>Returns a single static route entry</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="vpnTenantIpNetworkRoutingInstanceStaticRouteId">ID of the static route record</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/static-routes/{vpnTenantIpNetworkRoutingInstanceStaticRouteId}", Name = "GetAttachmentSetRoutingInstanceStaticRoute")]
        [ValidateModelState]
        [ValidateVpnTenantIpNetworkRoutingInstanceStaticRouteExists]
        [SwaggerOperation("GetAttachmentSetRoutingInstanceStaticRoute")]
        [SwaggerResponse(statusCode: 200, type: typeof(VpnTenantIpNetworkRoutingInstanceStaticRoute), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAttachmentSetRoutingInstanceStaticRoute([FromRoute][Required]int? attachmentSetId,
            [FromRoute][Required]int? vpnTenantIpNetworkRoutingInstanceStaticRouteId, [FromQuery]bool? deep)
        {
            var vpnTenantIpNetworkIn = await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.GetByIDAsync(vpnTenantIpNetworkRoutingInstanceStaticRouteId.Value, deep: deep);
            if (vpnTenantIpNetworkIn.HasBeenModified(Request))
            {
                vpnTenantIpNetworkIn.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<VpnTenantIpNetworkRoutingInstanceStaticRoute>(vpnTenantIpNetworkIn));
        }

        /// <summary>
        /// Find all static routes which are associated with an attachment set
        /// </summary>
        /// <remarks>Returns a list of vpn tenant IP network routing instance static route objects</remarks>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/attachment-sets/{attachmentSetId}/static-routes")]
        [ValidateModelState]
        [ValidateAttachmentSetExists]
        [SwaggerOperation("GetAllAttachmentSetRoutingInstanceStaticRoutes")]
        [SwaggerResponse(statusCode: 200, type: typeof(VpnTenantIpNetworkRoutingInstanceStaticRoute), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetAllAttachmentSetRoutingInstanceStaticRoutes([FromRoute][Required]int? attachmentSetId, [FromQuery]bool? deep)
        {
            var vpnTenantIpNetworkRoutingInstanceStaticRoutes = await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.GetAllByAttachmentSetIDAsync(attachmentSetId.Value, deep: deep);
            return Ok(Mapper.Map<List<VpnTenantIpNetworkRoutingInstanceStaticRoute>>(vpnTenantIpNetworkRoutingInstanceStaticRoutes));
        }
    }
}

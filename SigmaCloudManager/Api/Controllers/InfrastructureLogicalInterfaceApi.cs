/*
 * MIND API
 *
 * This is the Master Inventory Network Database (MIND) API. MIND provides automated allocation of technical attributes needed to create IP and Ethernet VPNs on the global Sigma network. MIND suplogical interfaces the 'Nova' services specfication which defines the collection of connectivity services suplogical interfaceed by ENT. Go to https://thehub.thomsonreuters.com/docs/DOC-2193014 to learn more.
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
using SCM.Models;

namespace Mind.Api.Controllers
{ 
    /// <summary>
    /// Create and manage the lifecycle of infrastructure device logical interfaces
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Infrastructure Logical Interfaces")]
    public class InfrastructureLogicalInterfaceApiController : BaseApiController
    { 
        private readonly IInfrastructureLogicalInterfaceService _infrastructureLogicalInterfaceService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infrastructureLogicalInterfaceService"></param>
        /// <param name="mapper"></param>
        public InfrastructureLogicalInterfaceApiController(IInfrastructureLogicalInterfaceService infrastructureLogicalInterfaceService, IMapper mapper) : base(infrastructureLogicalInterfaceService, mapper)
        {
            _infrastructureLogicalInterfaceService = infrastructureLogicalInterfaceService;
        }

        /// <summary>
        /// Create a new infrastructure logical interface
        /// </summary>

        /// <param name="deviceId">The ID of the infrastructure device to which the 
        /// new logical interface will be assigned</param>
        /// <param name="body">logical interface request object that generates a new infrastructure logical interface</param>
        /// <response code="201">Successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}/logical-interfaces")]
        [ValidateModelState]
        [ValidateInfrastructureDeviceExists]
        [SwaggerOperation("CreateInfrastructureLogicalInterface")]
        [SwaggerResponse(statusCode: 201, type: typeof(Mind.Api.Models.LogicalInterface), description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> CreateInfrastructureLogicalInterface([FromRoute][Required]int? deviceId, 
            [FromBody]Mind.Api.Models.InfrastructureLogicalInterfaceRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.InfrastructureLogicalInterfaceRequest>(body);
                var logicalInterface = await _infrastructureLogicalInterfaceService.AddAsync(deviceId.Value, request);
                var logicalInterfaceApiModel = Mapper.Map<Mind.Api.Models.LogicalInterface>(logicalInterface);
                return CreatedAtRoute("GetInfrastructureLogicalInterface", new
                {
                    deviceId,
                    logicalInterfaceId = logicalInterface.LogicalInterfaceID
                }, logicalInterfaceApiModel);
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
        /// Deletes an infrastructure logical interface
        /// </summary>

        /// <param name="deviceId">ID of the infrastructure device and to which the 
        /// logical interface to be deleted belongs</param>
        /// <param name="logicalInterfaceId">ID of the infrastructure logical interface</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation failed</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}/logical-interfaces/{logicalInterfaceId}")]
        [ValidateModelState]
        [ValidateInfrastructureLogicalInterfaceExists]
        [SwaggerOperation("DeleteInfrastructureLogicalInterface")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation failed")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteInfrastructureLogicalInterface([FromRoute][Required]int? deviceId, [FromRoute][Required]int? logicalInterfaceId)
        {
            try
            {
                await _infrastructureLogicalInterfaceService.DeleteAsync(logicalInterfaceId.Value);
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
        /// Find an infrastructure logical interface by ID
        /// </summary>
        /// <remarks>Returns a single infrastructure logical interface</remarks>
        /// <param name="deviceId">ID of the infrastructure device and to which the logical interface
        /// to be returned belongs</param>
        /// <param name="logicalInterfaceId">ID of the infrastructure logical interface</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}/logical-interfaces/{logicalInterfaceId}", Name = "GetInfrastructureLogicalInterface")]
        [ValidateModelState]
        [ValidateInfrastructureLogicalInterfaceExists]
        [SwaggerOperation("GetInfrastructureLogicalInterfaceById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Mind.Api.Models.LogicalInterface), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetInfrastructureLogicalInterfaceById([FromRoute][Required]int? deviceId, 
            [FromRoute][Required]int? logicalInterfaceId, [FromQuery]bool? deep)
        {
            var logicalInterface = await _infrastructureLogicalInterfaceService.GetByIDAsync(logicalInterfaceId.Value, deep: deep);
            if (logicalInterface.HasBeenModified(Request))
            {
                logicalInterface.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<Mind.Api.Models.LogicalInterface>(logicalInterface));
        }

        /// <summary>
        /// Find all infrastructure logical interfaces for a given device
        /// </summary>
        /// <remarks>Returns all infrastructure logical interfaces which exist for a given routing instance</remarks>
        /// <param name="deviceId">The ID of the infrastructure device for which all logical intefaces
        /// will be returned</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}/logical-interfaces")]
        [ValidateModelState]
        [ValidateInfrastructureDeviceExists]
        [SwaggerOperation("GetInfrastructureLogicalInterfaces")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Mind.Api.Models.LogicalInterface>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public async virtual Task<IActionResult> GetInfrastructureLogicalInterfaces([FromRoute][Required]int? deviceId, [FromQuery]bool? deep)
        {
            var logicalInterfaces = await _infrastructureLogicalInterfaceService.GetAllByDeviceIDAsync(deviceId.Value, deep: deep);
            return Ok(Mapper.Map<List<Mind.Api.Models.LogicalInterface>>(logicalInterfaces));
        }

        /// <summary>
        /// Update an existing infrastructure logical interface
        /// </summary>

        /// <param name="deviceId">ID of the infrastructure device</param>
        /// <param name="logicalInterfaceId">ID of the infrastructure logical interface</param>
        /// <param name="body">Infrastructure logical interface update object that updates an existing logical interface</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}/logical-interfaces/{logicalInterfaceId}")]
        [ValidateModelState]
        [ValidateInfrastructureLogicalInterfaceExists]
        [SwaggerOperation("UpdateInfrastructureLogicalInterface")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateInfrastructureLogicalInterface([FromRoute][Required]int? deviceId, [FromRoute][Required]int? logicalInterfaceId, 
            [FromBody]Mind.Api.Models.LogicalInterfaceUpdate body)
        {
            try
            {
                var item = await _infrastructureLogicalInterfaceService.GetByIDAsync(logicalInterfaceId.Value);
                if (item.HasPreconditionFailed(Request))
                {
                    return new PreconditionFailedResult();
                }

                var update = Mapper.Map<Mind.Models.RequestModels.LogicalInterfaceUpdate>(body);
                var logicalInterface = await _infrastructureLogicalInterfaceService.UpdateAsync(logicalInterfaceId.Value, update);
                logicalInterface.SetModifiedHttpHeaders(Response);

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

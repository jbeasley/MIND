/*
 * MIND API
 *
 * This is the Master Inventory Network Database (MIND) API. MIND provides automated allocation of technical attributes needed to create IP and Ethernet VPNs on the global Sigma network. MIND supports the 'Nova' services specfication which defines the collection of connectivity services supported by ENT. Go to https://thehub.thomsonreuters.com/docs/DOC-2193014 to learn more.
 *
 * OpenAPI spec version: 1.0.0
 * Contact: jonathan.beasley@thomsonreuters.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Mind.Api.Attributes;
using Mind.Api.Models;
using Mind.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mind.Builders;
using Mind.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Mind.Api.Controllers
{
    /// <summary>
    /// Create and manage the lifecycle of infrastructure device ports
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Infrastructure Ports")]
    public class InfrastructurePortApiController : BaseApiController
    { 
        private readonly IInfrastructurePortService _infrastructurePortService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infrastructurePortService"></param>
        /// <param name="mapper"></param>
        public InfrastructurePortApiController(IInfrastructurePortService infrastructurePortService, IMapper mapper) : base(infrastructurePortService, mapper)
        {
            _infrastructurePortService = infrastructurePortService;
        }

        /// <summary>
        /// Create a new infrastructure port
        /// </summary>

        /// <param name="deviceId">The ID of the infrastructure device</param>
        /// <param name="body">port request object that generates a new infrastructure port</param>
        /// <response code="201">Successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}/ports")]
        [ValidateModelState]
        [ValidateInfrastructureDeviceExists]
        [SwaggerOperation("CreateInfrastructurePort")]
        [SwaggerResponse(statusCode: 201, type: typeof(Port), description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> CreateInfrastructurePort([FromRoute][Required]int? deviceId, [FromBody]Mind.Api.Models.PortRequestOrUpdate body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.PortRequestOrUpdate>(body);
                var port = await _infrastructurePortService.AddAsync(deviceId.Value, request);
                var portApiModel = Mapper.Map<Mind.Api.Models.Port>(port);
                return CreatedAtRoute("GetInfrastructurePort", new { deviceId, portId = port.ID }, portApiModel);
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
        /// Deletes an infrastructure port
        /// </summary>

        /// <param name="deviceId">ID of the infrastructure device</param>
        /// <param name="portId">ID of the infrastructure port</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation failed</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}/ports/{portId}")]
        [ValidateModelState]
        [ValidateInfrastructurePortExists]
        [SwaggerOperation("DeleteInfrastructurePort")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation failed")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteInfrastructurePort([FromRoute][Required]int? deviceId, [FromRoute][Required]int? portId)
        {
            try
            {
                await _infrastructurePortService.DeleteAsync(portId.Value);
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
        /// Find an infrastructure port by ID
        /// </summary>
        /// <remarks>Returns a single infrastructure port</remarks>
        /// <param name="deviceId">ID of the infrastructure device</param>
        /// <param name="portId">ID of the infrastructure port</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}/ports/{portId}", Name = "GetInfrastructurePort")]
        [ValidateModelState]
        [ValidateInfrastructurePortExists]
        [SwaggerOperation("GetInfrastructurePortById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Port), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetInfrastructurePortById([FromRoute][Required]int? deviceId, 
            [FromRoute][Required]int? portId, [FromQuery]bool? deep)
        {
            var port = await _infrastructurePortService.GetByIDAsync(portId.Value, deep: deep);
            if (port.HasBeenModified(Request))
            {
                port.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<Port>(port));
        }

        /// <summary>
        /// Find all infrastructure ports for a given device
        /// </summary>
        /// <remarks>Returns all infrastructure ports which exist for a given device</remarks>
        /// <param name="deviceId">The ID of the infrastructure device</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}/ports")]
        [ValidateModelState]
        [ValidateInfrastructureDeviceExists]
        [SwaggerOperation("GetInfrastructurePorts")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Port>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public async virtual Task<IActionResult> GetInfrastructurePorts([FromRoute][Required]int? deviceId, [FromQuery]bool? deep)
        {
            var ports = await _infrastructurePortService.GetAllByDeviceIDAsync(deviceId.Value, deep: deep);
            return Ok(Mapper.Map<List<Port>>(ports));
        }

        /// <summary>
        /// Update an existing infrastructure port
        /// </summary>

        /// <param name="deviceId">ID of the device</param>
        /// <param name="portId">ID of the infrastructure port</param>
        /// <param name="body">Port update object that updates an existing infrastructure port</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}/ports/{portId}")]
        [ValidateModelState]
        [ValidateInfrastructurePortExists]
        [SwaggerOperation("UpdateInfrastructurePort")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateInfrastructurePort([FromRoute][Required]int? deviceId, [FromRoute][Required]int? portId, 
            [FromBody]Mind.Api.Models.PortRequestOrUpdate body)
        {
            try
            {
                var item = await _infrastructurePortService.GetByIDAsync(portId.Value);
                if (item.HasPreconditionFailed(Request))
                {
                    return new PreconditionFailedResult();
                }

                var update = Mapper.Map<Mind.Models.RequestModels.PortRequestOrUpdate>(body);
                var port = await _infrastructurePortService.UpdateAsync(portId.Value, update);
                port.SetModifiedHttpHeaders(Response);

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
